using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esp_tools_gui
{
    class ToolTool : Tool
    {
        public String ChipType { get; set; } = "";
        public String Chip { get; set; } = "";
        public String Features { get; set; } = "";
        public String Crystal { get; set; } = "";
        public String MAC { get; set; } = "";
        public String Flash { get; set; } = "";

        public List<Partition> Partitions { get; set; } = new List<Partition>();

        public ToolTool() : base(Properties.Resources.esptool, "esptool.exe")
        {

        }

        public void Parse(string args)
        {
            var str = Execute(args);
            if (args.Contains("chip_id"))
            {
                ChipType = RegexSimple("Detecting chip type...", str);
                Chip = RegexSimple("Chip is", str);
                Features = RegexSimple("Features:", str);
                Crystal = RegexSimple("Crystal is", str);
                MAC = RegexSimple("MAC:", str);
            }

            if (args.Contains("flash_id"))
            {
                ChipType = RegexSimple("Detecting chip type...", str);
                Chip = RegexSimple("Chip is", str);
                Features = RegexSimple("Features:", str);
                Crystal = RegexSimple("Crystal is", str);
                MAC = RegexSimple("MAC:", str);
                Flash = RegexSimple("Detected flash size:", str);
            }

            if (args.Contains("read_flash") && args.Contains("temp.bin"))
            {
                using (FileStream fs2 = new FileStream("temp.bin", FileMode.Open))
                {
                    using (BinaryReader r = new BinaryReader(fs2))
                    {
                        Int16 start;
                        Partitions.Clear();

                        do
                        {
                            var p = new Partition();

                            start = r.ReadByte();
                            if (start != 0xaa) break;
                            p.Flags = r.ReadByte();
                            p.Type = r.ReadByte();
                            p.Subtype = r.ReadByte();
                            p.Offset = r.ReadUInt32();
                            p.Size = r.ReadUInt32();
                            p.Name = Encoding.UTF8.GetString(r.ReadBytes(20), 0, 20);
                            Partitions.Add(p);
                        } while (start == 0xaa);
                    }
                }
            }
        }
    }


    class Partition
    {
        public int Type { get; set; }
        public int Subtype { get; set; }
        public uint Offset { get; set; }
        public uint Size {get; set;}
        public int Flags { get; set; }
        public string Name { get; set; }

        public string GetTypeName()
        {
            if (Type == 0) return "APP";
            else if (Type == 1) return "DATA";
            else return "CUSTOM " + Type;
        }

        public string GetSubTypeName()
        {
            if (Type == 0)
            {
                if (Subtype == 0) return "FACTORY";
                else if(Subtype < 0x20) return "OTA_" + (Subtype - 0x10).ToString("X");
                else if (Subtype == 0x20) return "OTA_TEST";
                else return "OTA_? " + Subtype.ToString("X");
            }
            else if (Type == 1)
            {
                switch(Subtype)
                {
                    case 0: return "OTA";
                    case 1: return "PHY";
                    case 2: return "NVS";
                    case 4: return "NVS_KEYS";
                    default: return "UNKNOWN " + Subtype.ToString("X");
                }
            }
            else return Type.ToString();
        }

        public string GetSize()
        {
            double size = Size;
            if (size < 1000) return size.ToString("0") + " bytes";
            size = size / 1024;
            if (size < 1000) return size.ToString("0.000") + " kb";
            size = size / 1024;
            return size.ToString("0.000") + " Mb";
        }
    }
}
