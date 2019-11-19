using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace esp_tools_gui
{
    public class Tool
    {
        private byte[] _resource;
        private string _exe;

        public static string _com;
        public static string _baud;

        private StringBuilder _outStr = new StringBuilder();
        protected readonly static string ExePath = Path.Combine(Directory.GetCurrentDirectory(), "exes");
        private bool _useComArgs = true;

        public event EventHandler<CustomEventArgs> ConsoleEvent;

        public Tool(byte[] resource, string executable, bool AddComArgs)
        {
            _resource = resource;
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

            var t = Task.Run(() =>
            {
                string tempExeName = Path.Combine(ExePath, _exe);
                if (!File.Exists(tempExeName))
                {
                    var f = File.Create(tempExeName);
                    f.Write(_resource, 0, _resource.Length);
                    f.Close();
                }

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                using (var p = new Process())
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
                    p.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
                    p.ErrorDataReceived += new DataReceivedEventHandler(ErrorHandler);
                    p.Start();
                    p.BeginOutputReadLine();
                    p.BeginErrorReadLine();
                    p.WaitForExit();
                    p.Close();
                }

                stopwatch.Stop();
                long elapsed_time = stopwatch.ElapsedMilliseconds;
                ConsoleEvent.Invoke(this, new CustomEventArgs { input = " [ execution time ] " + (elapsed_time / 1000.0) + " s" });

                Connecting.Terminate();
            });

            conn.ShowDialog();

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
