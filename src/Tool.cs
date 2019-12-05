using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace esp_tools_gui
{
    public class Tool
    {
        private string _exe;

        public static string _com = "";
        public static string _baud = "";
        private static bool _dllLoaded = false;

        private StringBuilder _outStr = new StringBuilder();
        protected readonly static string ExePath = Path.Combine(Directory.GetCurrentDirectory(), "exes");
        private bool _useComArgs = true;

        public event EventHandler<CustomEventArgs> ConsoleEvent;

        public Tool(string executable, bool AddComArgs)
        {
            _exe = executable;
            _useComArgs = AddComArgs;
        }
        private String GetComParam()
        {
            if (_useComArgs)
                return "--port " + _com + " --baud " + _baud + " ";
            else
                return "";
        }

        public async Task<string> Execute(string args)
        {
            Connecting conn = new Connecting(_exe, GetComParam() + args);
            CancellationTokenSource cts = new CancellationTokenSource();

            var t = Task.Run(() =>
            {
                string tempExeName = Path.Combine(ExePath, _exe);

                if (!File.Exists(Path.Combine(ExePath, "python37.dll")))
                {
                    ConsoleEvent.Invoke(this, new CustomEventArgs { error = "Python DLL is not loaded." });
                }
                else if (!File.Exists(tempExeName))
                {
                    var errMsg = "[" + _exe + "] File '" + _exe + "' not found in directory " + ExePath + "! Unable to start request.";
                    ConsoleEvent.Invoke(this, new CustomEventArgs { error = errMsg });
                }
                else if (_com.Length < 3)
                {
                    var errMsg = "[" + _exe + "] COM port is invalid (" + _com + ") - please select the right port and try again.";
                    ConsoleEvent.Invoke(this, new CustomEventArgs { error = errMsg });
                }
                else
                {
                    int procId = 0;
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();
                    using (var p = new Process())
                    {
                        try
                        {
                            using (cts.Token.Register(()=> { p.Kill(); p.WaitForExit(500); p.Close(); p.Dispose(); }))
                            {
                                _outStr.Clear();
                                _outStr.Append(" >>> " + _exe + " " + GetComParam() + args + "\r\n");
                                ConsoleEvent.Invoke(this, new CustomEventArgs { input = _exe + " " + GetComParam() + args });
                                p.StartInfo = new ProcessStartInfo
                                {
                                    FileName = tempExeName,
                                    UseShellExecute = false,
                                    WorkingDirectory = ExePath,
                                    WindowStyle = ProcessWindowStyle.Hidden,
                                    CreateNoWindow = true,
                                    Arguments = GetComParam() + args,
                                    RedirectStandardOutput = true,
                                    RedirectStandardError = true
                                };

                                //p.StartInfo.RedirectStandardError = true;
                                //p.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
                                p.ErrorDataReceived += new DataReceivedEventHandler(ErrorHandler);
                                p.Start();
                                //p.BeginOutputReadLine();
                                p.BeginErrorReadLine();
                                procId = p.Id;

                                int dataReceived = 0;
                                var sTemp = "";
                                var outputReadTask = Task.Run(() =>
                                {
                                    int iCh = 0;
                                    var s2 = "";
                                    var s3 = "";
                                    try
                                    {
                                        do
                                        {
                                            iCh = p.StandardOutput.Read();
                                            if (iCh >= 0)
                                            {
                                                if (iCh == 8)
                                                {
                                                    s3 = "\r\n";
                                                    s2 = " ";
                                                }
                                                else
                                                {
                                                    s2 = s3 + char.ConvertFromUtf32(iCh);
                                                    if (s3.Length > 0) s3 = "";
                                                }
                                                sTemp += s2;
                                            }
                                        } while (iCh >= 0);
                                    }
                                    catch (Exception e)
                                    {
                                        int k = 0;
                                    }
                                });

                                do
                                {
                                    try
                                    {
                                        if (sTemp.Length > 0)
                                        {
                                            dataReceived++;
                                            var s = sTemp;
                                            sTemp = "";
                                            _outStr.Append(s);
                                            ConsoleEvent.Invoke(this, new CustomEventArgs { output = s });
                                        }
                                        else
                                        {
                                            p.WaitForExit(200);
                                        }
                                    }
                                    catch (Exception) { }

                                    if (dataReceived == 0 && stopwatch.ElapsedMilliseconds > 3000)
                                    {
                                        ConsoleEvent.Invoke(this, new CustomEventArgs { error = "Request timeout." });
                                        break;
                                    }
                                } while (!p.HasExited);
                                if (sTemp.Length > 0)
                                {
                                    _outStr.Append(sTemp);
                                    ConsoleEvent.Invoke(this, new CustomEventArgs { output = sTemp });
                                }
                            }
                        }
                        catch(Exception ex)
                        {
                            ConsoleEvent.Invoke(this, new CustomEventArgs { error = ex.Message });
                        }
                        try
                        {
                            p.Close();
                        }
                        catch (Exception) { }
                    }

                    stopwatch.Stop();
                    long elapsed_time = stopwatch.ElapsedMilliseconds;
                    ConsoleEvent.Invoke(this, new CustomEventArgs { input = " [ execution time ] " + (elapsed_time / 1000.0) + " s" });

                    try
                    {
                        if (Process.GetProcessesByName(_exe.Replace(".exe", "")).Length > 0)
                        {
                            Process.GetProcessesByName(_exe.Replace(".exe", ""))[0].Kill();
                        }
                    }
                    catch (Exception) { }
                }

                Connecting.Terminate();
            }, cts.Token);

            conn.ShowDialog();
            if(!t.IsCompleted) // was aborted
            {
                ConsoleEvent.Invoke(this, new CustomEventArgs { error = "Request aborted." });
                cts.Cancel();
                t.Wait(500);
            }

            return await Task.FromResult(_outStr.ToString());
        }

        public string GetExePath()
        {
            return ExePath;
        }

        public string RegexSimple(string beginsWith, string txt)
        {
            var found = Regex.Match(txt, "(" + beginsWith + " )(.*]?)");
            if (found.Groups.Count == 3)
            {
                return found.Groups[2].ToString().Trim();
            }
            else return "";
        }

        public List<string> RegexFuse(string line)
        {
            List<string> values = new List<string>();
            var found = Regex.Match(line, @"([a-zA-Z0-9_]*)[ ]*(.*)[\=](.*) ([R|W|\/]*) (.*)");
            if(found.Groups.Count > 4)
            {
                values.Add(found.Groups[1].Value.Trim());
                values.Add(found.Groups[2].Value.Trim());
                values.Add(found.Groups[3].Value.Trim());
                values.Add(found.Groups[4].Value.Trim());
            }
            return values;
            //XPD_SDIO_FORCE         Ignore MTDI pin (GPIO12) for VDD_SDIO on reset    = 0 R/W (0x0)
        }

        private void OutputHandler(object sender, DataReceivedEventArgs e)
        {
            // Prepend line numbers to each line of the output.
            if (e.Data != null)
            {
                ConsoleEvent.Invoke(this, new CustomEventArgs { output = e.Data });
                _outStr.Append("\r\n" + e.Data);
            }
        }

        private void ErrorHandler(object sender, DataReceivedEventArgs e)
        {
            // Prepend line numbers to each line of the output.
            if (e.Data != null)
            {
                ConsoleEvent.Invoke(this, new CustomEventArgs { error = e.Data });
                _outStr.Append("\r\n" + e.Data);
            }
        }
    }
}
