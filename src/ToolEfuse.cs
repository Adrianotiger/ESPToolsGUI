using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esp_tools_gui
{
    class ToolEfuse : Tool
    {
        public Dictionary<string, List<EFuse>> Fuses = new Dictionary<string, List<EFuse>>();

        public ToolEfuse() : base("espefuse.exe", true)
        {
            
        }

        public async void Parse(string args)
        {
            var str = await Execute(args);
            if(args.Contains("summary"))
            {
                //ChipType = RegexSimple("Detecting chip type...", str);
                var substr = str.Split(new string[] { ":\r" }, StringSplitOptions.RemoveEmptyEntries);
                var first = true;
                List<EFuse> eFuseList;
                Fuses.Clear();
                var nextTitle = "";
                foreach(var block in substr)
                {
                    var lines = block.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    if(first)
                    {
                        nextTitle = lines[lines.Length - 1];
                        first = false;
                    }
                    else
                    {
                        eFuseList = new List<EFuse>();
                        Fuses.Add(nextTitle, eFuseList);
                        nextTitle = lines.Last();
                        var memLine = "";
                        foreach (var line in lines)
                        {
                            var values = RegexFuse(memLine + line);
                            if(values.Count >= 3)
                            {
                                eFuseList.Add(new EFuse { Title = values[0], Description = values[1], Value = values[2], ReadWrite = values[3] });
                                memLine = "";
                            }
                            else
                            {
                                memLine = line.Trim();
                            }
                        }
                    }
                }
            }
        }
    }

    class EFuse
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Value { get; set; } = "";
        public string ReadWrite { get; set; } = "";
    }
}
