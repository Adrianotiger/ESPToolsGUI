using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace esp_tools_gui
{
    class Tool
    {
        private byte[] _resource;
        private string _exe;

        public static string _com;
        public static string _baud;

        private StringBuilder _outStr = new StringBuilder();
        private readonly static string ExePath = Directory.GetCurrentDirectory();

        public event EventHandler<CustomEventArgs> ConsoleEvent;

        public Tool(byte[] resource, string executable)
        {
            _resource = resource;
            _exe = executable;
        }
        private String GetComParam()
        {
            return "--port " + _com + " --baud " + _baud + " ";
        }

        public string Execute(string args)
        {
            string tempExeName = Path.Combine(ExePath, _exe);
            if (!File.Exists(tempExeName))
            {
                var f = File.Create(tempExeName);
                f.Write(_resource, 0, _resource.Length);
                f.Close();
            }

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
            return _outStr.ToString();
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
