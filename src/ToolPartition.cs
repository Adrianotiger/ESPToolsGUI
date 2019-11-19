using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esp_tools_gui
{
    public class ToolPartition : Tool
    {
        public ToolPartition() : base(Properties.Resources.gen_esp32part, "gen_esp32part.exe", false)
        {

        }

        public async Task<string> CreatePartition(int nvs, int ota0, int ota1, int eeprom, int spiffs)
        {
            int addr = 0x9000;
            String text = "# Name,   Type, SubType, Offset,  Size, Flags\n";
            if(nvs > 0)
            {
                text += "nvs, data, nvs, 0x" + addr.ToString("x") + ", 0x" + nvs.ToString("x") + ",\n";
                addr += nvs;
            }
            text += "otadata, data, ota, 0x" + addr.ToString("x") + ", 0x" + (PartTool.Otad).ToString("x") + ",\n";
            addr += PartTool.Otad;
            text += "app0, app, ota_0, 0x" + addr.ToString("x") + ", 0x" + ota0.ToString("x") + ",\n";
            addr += ota0;
            if (ota1 > 0)
            {
                text += "app1, app, ota_1, 0x" + addr.ToString("x") + ", 0x" + ota1.ToString("x") + ",\n";
                addr += ota1;
            }
            if (eeprom > 0)
            {
                text += "eeprom, data, 0x99, 0x" + addr.ToString("x") + ", 0x" + eeprom.ToString("x") + ",\n";
                addr += eeprom;
            }
            if (spiffs > 0)
            {
                text += "spiffs, data, spiffs, 0x" + addr.ToString("x") + ", 0x" + spiffs.ToString("x") + ",\n";
                addr += spiffs;
            }

            String csvFile = GetPartitionPath(false);
            String binFile = GetPartitionPath(true);

            if (File.Exists(csvFile)) File.Delete(csvFile);
            if (File.Exists(binFile)) File.Delete(binFile);
            File.WriteAllText(csvFile, text);

            return await Execute("part.csv part.bin");

            /*
                nvs,      data, nvs,     0x9000,  0x5000,
                otadata,  data, ota,     0xe000,  0x2000,
                app0,     app,  ota_0,   0x10000, 0x140000,
                app1,     app,  ota_1,   0x150000,0x140000,
                spiffs,   data, spiffs,  0x290000,0x170000,
           */
        }

        public string GetPartitionPath(bool bin)
        {
            if(bin)
            {
                return ExePath + "\\part.bin";
            }
            else
            {
                return ExePath + "\\part.csv";
            }
        }

        public async Task<bool> ImportTable(string filename)
        {
            String csvFile = GetPartitionPath(false);
            String binFile = GetPartitionPath(true);

            if (File.Exists(csvFile)) File.Delete(csvFile);
            if (File.Exists(binFile)) File.Delete(binFile);

            if(filename.EndsWith(".bin"))
            {
                File.Copy(filename, binFile);
                await Execute("part.bin part.csv");
            }
            else if(filename.EndsWith(".csv"))
            {
                File.Copy(filename, csvFile);
                await Execute("part.csv part.bin");
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
