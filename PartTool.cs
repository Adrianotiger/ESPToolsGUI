using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace esp_tools_gui
{
    public partial class PartTool : Form
    {
        static int flashSizeBytes = 0;
        static int flashSizeKB = 0;

        private ToolPartition partition;

        public static int BaseAddress = 0x9000;

        public static int Eeprom = 0;
        public static int Nvs = 0;
        public static int Ota0 = 0;
        public static int Ota1 = 0;
        public static int Otad = 8 * 1024;
        public static bool OtaEnabled = false;
        public static int Spiffs = 0;

        public PartTool(int flashMB, ToolPartition partitionTool)
        {
            InitializeComponent();
            flashSizeBytes = flashMB * 1024 * 1024;
            flashSizeKB = flashMB * 1024;

            progressBar1.Value = 0;
            progressBar1.Maximum = flashSizeKB;

            partition = partitionTool;
        }

        private void PartTool_Load(object sender, EventArgs e)
        {
            trackBarOta.SmallChange = 64;
            trackBarOta1.SmallChange = 64;
            trackBarOta.TickFrequency = 256;
            trackBarOta1.TickFrequency = 256;

            trackBarEeprom.SmallChange = 4;
            trackBarNvs.SmallChange = 4;
            trackBarEeprom.TickFrequency = 4;
            trackBarNvs.TickFrequency = 4;

            trackBarSpiffs.SmallChange = 4;
            trackBarSpiffs.TickFrequency = 128;
            Calculate();
        }

        public int Calculate()
        {
            int total = 0;
            int free = 0;
            total = BaseAddress;
            if (checkBoxNvs.Checked) total += Nvs;
            if (checkBoxOta.Checked) total += Otad + Ota0 + Ota1;
            else total += Ota0;
            if (checkBoxEeprom.Checked) total += Eeprom;            
            if (checkBoxSpiffs.Checked) total += Spiffs;
            total /= 1024;
            free = flashSizeKB - total;

            progressBar1.Value = Math.Min(progressBar1.Maximum, total);
            if(total > progressBar1.Maximum)
            {
                labelProgress.ForeColor = Color.DarkRed;
            }
            else
            {
                labelProgress.ForeColor = Color.Black;
            }
            labelProgress.Text = total + "kb / " + flashSizeKB + "kb";

            labelOta.Text = trackBarOta.Value + "kb";
            if (checkBoxOtaLock.Checked)
            {
                labelOta1.Text = trackBarOta.Value + "kb";
                trackBarOta1.Value = trackBarOta.Value;
            }
            else
            {
                labelOta1.Text = trackBarOta1.Value + "kb";
            }
            labelNvs.Text = (checkBoxNvs.Checked ? trackBarNvs.Value.ToString() : "0") + "kb";
            labelEeprom.Text = (checkBoxEeprom.Checked ? trackBarEeprom.Value.ToString() : "0") + "kb";
            labelSpiffs.Text = (checkBoxSpiffs.Checked ? trackBarSpiffs.Value.ToString() : "0") + "kb";

            if (free < 0) return free;

            trackBarEeprom.Maximum = trackBarEeprom.Value + free;
            trackBarNvs.Maximum = trackBarNvs.Value + free;
            if (checkBoxOta.Checked)
                trackBarOta.Maximum = trackBarOta.Value + free / 2;
            else
                trackBarOta.Maximum = trackBarOta.Value + free;
            trackBarOta1.Maximum = trackBarOta1.Value + free;
            trackBarSpiffs.Maximum = trackBarSpiffs.Value + free;

            return free;
        }

        public void SetEeprom(int size)
        {
            Eeprom = size;
            if (size / 1024 > trackBarEeprom.Maximum) trackBarEeprom.Maximum = size / 1024;
            trackBarEeprom.Value = size / 1024;
            Calculate();
        }
        public void SetNvs(int size)
        {
            Nvs = size;
            if (size / 1024 > trackBarNvs.Maximum) trackBarNvs.Maximum = size / 1024;
            trackBarNvs.Value = size / 1024;
            Calculate();
        }
        public void SetOta0(int size, bool? useOta)
        {
            Ota0 = size;
            if (size / 1024 > trackBarOta.Maximum) trackBarOta.Maximum = size / 1024;
            trackBarOta.Value = size / 1024;
            if (useOta != null)
            {
                OtaEnabled = (bool)useOta;
                checkBoxOta.Checked = (bool)useOta;
                if (useOta == true)
                {
                    Ota1 = size;
                }
                else
                {
                    Ota1 = 0;
                }
            }
            Calculate();
        }
        public void SetOta1(int size)
        {
            Ota1 = size;
            if (size / 1024 > trackBarOta1.Maximum) trackBarOta1.Maximum = size / 1024;
            trackBarOta1.Value = size / 1024;
            checkBoxOta.Checked = true;
            checkBoxOtaLock.Checked = (Ota1 == Ota0);
            OtaEnabled = true;
            Calculate();
        }

        public void SetSpiffs(int size)
        {
            Spiffs = size;
            if (size / 1024 > trackBarSpiffs.Maximum) trackBarSpiffs.Maximum = size / 1024;
            trackBarSpiffs.Value = size / 1024;
            Calculate();
        }

        private void trackBarOta_ValueChanged(object sender, EventArgs e)
        {
            Ota0 = (trackBarOta.Value - trackBarOta.Value % 64) * 1024;
            trackBarOta.Value = Ota0 / 1024;
            if (checkBoxOtaLock.Checked)
            {
                Ota1 = Ota0;
                if (Ota0 / 1024 > trackBarOta1.Maximum) trackBarOta1.Maximum = Ota0 / 1024;
                trackBarOta1.Value = Ota0 / 1024;
                if (trackBarOta.Value > trackBarOta1.Maximum) trackBarOta1.Maximum = trackBarOta.Value;
                trackBarOta1.Value = trackBarOta.Value;
            }
            Calculate();
        }

        private void trackBarOta1_ValueChanged(object sender, EventArgs e)
        {
            if (!checkBoxOtaLock.Checked)
            {
                Ota1 = (trackBarOta1.Value - (trackBarOta1.Value % 64)) * 1024;
                trackBarOta1.Value = Ota1 / 1024;
                Calculate();
            }
        }

        private void trackBarNvs_ValueChanged(object sender, EventArgs e)
        {
            Nvs = (trackBarNvs.Value - trackBarNvs.Value % 4) * 1024;
            trackBarNvs.Value = Nvs / 1024;
            Calculate();
        }

        private void trackBarEeprom_ValueChanged(object sender, EventArgs e)
        {
            Eeprom = (trackBarEeprom.Value - trackBarEeprom.Value % 4) * 1024;
            trackBarEeprom.Value = Eeprom / 1024;
            Calculate();
        }

        private void trackBarSpiffs_ValueChanged(object sender, EventArgs e)
        {
            Spiffs = (trackBarSpiffs.Value - trackBarSpiffs.Value % 4) * 1024;
            trackBarSpiffs.Value = Spiffs / 1024;
            Calculate();
        }

        private void checkBoxOta_CheckedChanged(object sender, EventArgs e)
        {
            OtaEnabled = checkBoxOta.Checked;
            checkBoxOtaLock.Enabled = checkBoxOta.Checked;
            if (checkBoxOta.Checked)
            {
                trackBarOta1.Enabled = !checkBoxOtaLock.Checked;
                Ota1 = Ota0;
                Calculate();
            }
            else
            {
                trackBarOta1.Enabled = false;
                Ota1 = 0;
                Calculate();
            }
        }

        private void checkBoxNvs_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxNvs.Checked)
            {
                Nvs = trackBarNvs.Value * 1024;
                trackBarNvs.Enabled = true;
                Calculate();
            }
            else
            {
                Nvs = 0;
                trackBarNvs.Enabled = false;
                Calculate();
            }
        }

        private void checkBoxEeprom_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEeprom.Checked)
            {
                Eeprom = trackBarEeprom.Value * 1024;
                trackBarEeprom.Enabled = true;
                Calculate();
            }
            else
            {
                Eeprom = 0;
                trackBarEeprom.Enabled = false;
                Calculate();
            }
        }

        private void checkBoxSpiffs_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSpiffs.Checked)
            {
                Spiffs = trackBarSpiffs.Value * 1024;
                trackBarSpiffs.Enabled = true;
                Calculate();
            }
            else
            {
                Spiffs = 0;
                trackBarSpiffs.Enabled = false;
                Calculate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void trackBarOta_Scroll(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SavePartition(saveFileDialog1.FileName);
            }
        }

        private async void SavePartition(string filename)
        {
            await partition.CreatePartition(Nvs, Ota0, Ota1, Eeprom, Spiffs);
            File.Copy(partition.GetPartitionPath(filename.EndsWith(".bin")), filename, true);
        }

        private void checkBoxOtaLock_CheckedChanged(object sender, EventArgs e)
        {
            trackBarOta1.Enabled = !checkBoxOtaLock.Checked;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if(await partition.ImportTable(openFileDialog1.FileName))
                {
                    var csv = File.ReadAllLines(partition.GetPartitionPath(false));
                    foreach(var line in csv)
                    {
                        if (line.StartsWith("#")) continue;
                        var items = line.Split(',');
                        if (items.Count() < 5) continue;
                        switch(items[0].Trim())
                        {
                            case "nvs": SetNvs(Convert.ToInt32(items[4].Trim(), 16)); break;
                            case "app0": SetOta0(Convert.ToInt32(items[4].Trim(), 16), null); break;
                            case "app1": SetOta1(Convert.ToInt32(items[4].Trim(), 16)); break;
                            case "eeprom": SetEeprom(Convert.ToInt32(items[4].Trim(), 16)); break;
                            case "spiffs": SetSpiffs(Convert.ToInt32(items[4].Trim(), 16)); break;
                            case "otadata": Otad = Convert.ToInt32(items[4].Trim(), 16); break;
                            default: MessageBox.Show("Error: can't import this partition: " + items[0].Trim() + " - it will be ignored", "Import partitions"); break;
                        }

                        /*
                            nvs, data, nvs, 0x9000, 0x5000,
                            otadata, data, ota, 0xe000, 0x2000,
                            app0, app, ota_0, 0x10000, 0x180000,
                            app1, app, ota_1, 0x190000, 0x100000,
                            eeprom, data, 0x99, 0x310000, 0x1000,
                            spiffs, data, spiffs, 0x311000, 0x16f000,
                         */
                    }
                }
                else
                {
                    MessageBox.Show("Unable to import partition table " + openFileDialog1.FileName, "Import");
                }
            }
        }
    }
}
