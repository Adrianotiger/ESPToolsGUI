using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public static int Ffat = 0;

        public PartTool(int flashMB, ToolPartition partitionTool)
        {
            InitializeComponent();
            flashSizeBytes = flashMB * 1024 * 1024;
            flashSizeKB = flashMB * 1024;

            progressBar1.Value = 0;
            progressBar1.Maximum = flashSizeKB;

            partition = partitionTool;

            this.Width /= 2;
        }

        private void PartTool_Load(object sender, EventArgs e)
        {
            trackBarOta.SmallChange = 64;
            trackBarOta1.SmallChange = 64;
            trackBarOtaD.SmallChange = 1;
            trackBarOta.TickFrequency = 256;
            trackBarOta1.TickFrequency = 256;
            trackBarOtaD.TickFrequency = 8;

            trackBarEeprom.SmallChange = 4;
            trackBarNvs.SmallChange = 4;
            trackBarEeprom.TickFrequency = 4;
            trackBarNvs.TickFrequency = 4;

            trackBarSpiffs.SmallChange = 4;
            trackBarSpiffs.TickFrequency = 128;
            trackBarFfat.SmallChange = 4;
            trackBarFfat.TickFrequency = 128;
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
            if (checkBoxFfat.Checked) total += Ffat;
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
            labelOtaD.Text = trackBarOtaD.Value + "kb";
            labelNvs.Text = (checkBoxNvs.Checked ? trackBarNvs.Value.ToString() : "0") + "kb";
            labelEeprom.Text = (checkBoxEeprom.Checked ? trackBarEeprom.Value.ToString() : "0") + "kb";
            labelSpiffs.Text = (checkBoxSpiffs.Checked ? trackBarSpiffs.Value.ToString() : "0") + "kb";
            labelFfat.Text = (checkBoxFfat.Checked ? trackBarFfat.Value.ToString() : "0") + "kb";

            if (free < 0) return free;

            trackBarEeprom.Maximum = trackBarEeprom.Value + free;
            trackBarNvs.Maximum = trackBarNvs.Value + free;
            if (checkBoxOta.Checked)
                trackBarOta.Maximum = trackBarOta.Value + free / 2;
            else
                trackBarOta.Maximum = trackBarOta.Value + free;
            trackBarOta1.Maximum = trackBarOta1.Value + free;
            trackBarSpiffs.Maximum = trackBarSpiffs.Value + free;
            trackBarFfat.Maximum = trackBarFfat.Value + free;

            return free;
        }

        public void SetEeprom(int size)
        {
            Eeprom = size;
            if (!trackBarEeprom.Enabled) checkBoxEeprom.Checked = true;
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
        public void SetOtaD(int size)
        {
            //Otad = size;
            if (size / 1024 > trackBarOtaD.Maximum) trackBarOtaD.Maximum = size / 1024;
            trackBarOtaD.Minimum = 0;
            trackBarOtaD.Value = size / 1024;
            checkBoxOtaD.Checked = true;
            Calculate();
        }

        public void SetSpiffs(int size)
        {
            Spiffs = size;
            if (!trackBarSpiffs.Enabled) checkBoxSpiffs.Checked = true;
            if (size / 1024 > trackBarSpiffs.Maximum) trackBarSpiffs.Maximum = size / 1024;
            trackBarSpiffs.Value = size / 1024;
            Calculate();
        }

        public void SetFfat(int size)
        {
            Ffat = size;
            if (!trackBarFfat.Enabled) checkBoxFfat.Checked = true;
            if (size / 1024 > trackBarFfat.Maximum) trackBarFfat.Maximum = size / 1024;
            trackBarFfat.Value = size / 1024;
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

        private void trackBarFfat_ValueChanged(object sender, EventArgs e)
        {
            Ffat = (trackBarFfat.Value - trackBarFfat.Value % 4) * 1024;
            trackBarFfat.Value = Ffat / 1024;
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

        private void checkBoxFfat_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFfat.Checked)
            {
                Ffat = trackBarFfat.Value * 1024;
                trackBarFfat.Enabled = true;
                Calculate();
            }
            else
            {
                Ffat = 0;
                trackBarFfat.Enabled = false;
                Calculate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (groupBox1.Left == progressBar1.Left)
            {
                groupBox1.Left = this.Width + 100;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
                this.Close();
            }
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
            await partition.CreatePartition(Nvs, Ota0, Ota1, Eeprom, Spiffs, Ffat);
            File.Copy(partition.GetPartitionPath(filename.EndsWith(".bin")), filename, true);
        }

        private void checkBoxOtaLock_CheckedChanged(object sender, EventArgs e)
        {
            trackBarOta1.Enabled = !checkBoxOtaLock.Checked;
        }

        private int convertCSVInteger(string inString)
        {
            if(inString.StartsWith("0x"))
            {
                return Convert.ToInt32(inString.Trim(), 16);
            }
            else
            {
                int multiplier = 0;
                if(inString.Contains("K"))
                {
                    inString = inString.Replace("K", "");
                    multiplier = 1024;
                }
                else if (inString.Contains("M"))
                {
                    inString = inString.Replace("M", "");
                    multiplier = 1024 * 1024;
                }
                return Convert.ToInt32(inString.Trim(), 10) * multiplier;
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Import partition table";

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

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
                        case "nvs": SetNvs(convertCSVInteger(items[4])); break;
                        case "app0": SetOta0(convertCSVInteger(items[4]), null); break;
                        case "app1": SetOta1(convertCSVInteger(items[4])); break;
                        case "eeprom": SetEeprom(convertCSVInteger(items[4])); break;
                        case "ffat": SetFfat(convertCSVInteger(items[4])); break;
                        case "spiffs": SetSpiffs(convertCSVInteger(items[4])); break;
                        case "otadata": Otad = convertCSVInteger(items[4]); break;
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

        private void button5_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            openFileDialog1.Title = "Select partition file to insert into Arduino IDE:";
            openFileDialog1.FilterIndex = 1;

            if (sender == button6)
            {
                // do not open the dialog again
            }
            else
            {
                if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            }

            textBox1.Text = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);

            var appData = EnterPartitionFolder();
            if (appData == "") return;

            var dirs = Directory.GetDirectories(appData);
            foreach(var d in dirs)
            {
                var boardsFile = File.ReadAllLines(Path.Combine(d, "boards.txt"));
                TreeNode tn = treeView1.Nodes.Add(Path.GetFileName(d), Path.GetFileName(d), 5);
                List<string> boards = new List<string>();
                foreach(var l in boardsFile)
                {
                    var m = Regex.Match(l, @"([a-zA-Z0-9_]*).name=");
                    if (m.Groups.Count > 1)
                    {
                        boards.Add(m.Groups[1].Value);
                        tn.Nodes.Add(m.Groups[1].Value, m.Groups[1].Value, 5);
                    }
                }
                boards.ForEach(x => {
                    List<TreeNode> tn2 = new List<TreeNode>();
                    foreach (var l in boardsFile)
                    {
                        var m = Regex.Match(l, @"^" + x + @".menu.PartitionScheme.([a-zA-Z0-9_]*)=(.*)");
                        if (m.Groups.Count > 2)
                        {
                            tn2.Add(tn.Nodes[x].Nodes.Add(m.Groups[1].Value, m.Groups[2].Value, 5));
                            //tn.Nodes.Add(m.Groups[1].Value, m.Groups[1].Value, 5);
                        }
                    }
                    if(tn2.Count > 0)
                    {
                        foreach (var l in boardsFile)
                        {
                            tn2.ForEach(y =>
                            {
                                var m = Regex.Match(l, @"^" + x + @".menu.PartitionScheme." + y.Name + @"\." + @"([a-zA-Z0-9_.]*)=(.*)");
                                if (m.Groups.Count > 2)
                                {
                                    y.Nodes.Add(m.Groups[1].Value, m.Groups[1].Value + ":" + m.Groups[2].Value, 4);
                                    //tn.Nodes.Add(m.Groups[1].Value, m.Groups[1].Value, 5);
                                }
                            });
                        }
                    }
                    else
                    {
                        tn.Nodes[x].Remove();
                    }
                });

            }

            groupBox1.Visible = true;
            groupBox1.Left = progressBar1.Left;
        }

        private string EnterSubFolder(string mainFolder, string subFolder, bool showError = true)
        {
            if(!Directory.Exists(Path.Combine(mainFolder, subFolder)))
            {
                if(showError) MessageBox.Show("Folder " + Path.Combine(mainFolder, subFolder) + " not found");
                return "";
            }
            return Path.Combine(mainFolder, subFolder);
        }

        private string EnterPartitionFolder()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var isLocal = EnterSubFolder(appData, "local", false);
            if (isLocal == "") appData = EnterSubFolder(appData, "../local");
            else appData = isLocal;
            appData = EnterSubFolder(appData, "Arduino15");
            if (appData != "") appData = EnterSubFolder(appData, "packages");
            if (appData != "") appData = EnterSubFolder(appData, "esp32");
            if (appData != "") appData = EnterSubFolder(appData, "hardware");
            if (appData != "") appData = EnterSubFolder(appData, "esp32");
            return appData;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length < 3)
            {
                MessageBox.Show("Add a description for your partition", "No description", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (treeView1.Nodes.Count == 0 || treeView1.Nodes[0].Nodes.Count == 0)
            {
                MessageBox.Show("No board found inside this directory.", "No board", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            TreeNode editNode = null;
            int partitionSize = GetSizeFromFile(openFileDialog1.FileName);

            for (var j = 0; j < treeView1.Nodes[0].Nodes.Count; j++)
            {
                if (treeView1.Nodes[0].Nodes[j].IsSelected)
                {
                    editNode = treeView1.Nodes[0].Nodes[j];
                    break;
                }
            }
            if (editNode == null)
            {
                MessageBox.Show("Please select the board to copy the new partition.", "No board", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (partitionSize == 0)
            {
                MessageBox.Show("Unable to detect partition size of OTA0", "invalid partition file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var appData = EnterPartitionFolder();
            appData = EnterSubFolder(appData, editNode.Parent.Text);
            if (appData == "") return;
            var boardsFile = File.ReadAllLines(Path.Combine(appData, "boards.txt")).ToList();
            List<string> boards = new List<string>();
            bool boardFound = false;
            bool partitionsFound = false;
            int lineIndex = 0;
            int index = 0;
            boardsFile.ForEach(l => {
                index++;
                if(lineIndex > 0)
                {

                }
                else if (!boardFound)
                {
                    var m = Regex.Match(l, @"([a-zA-Z0-9_]*).name=");
                    if (m.Groups.Count > 1 && m.Groups[1].Value == editNode.Text)
                    {
                        boardFound = true;
                    }
                }
                else
                {
                    if (!partitionsFound)
                    {
                        if (Regex.IsMatch(l, @"^" + editNode.Text + @".menu.PartitionScheme."))
                        {
                            partitionsFound = true;
                        }
                    }
                    else
                    {
                        if (!Regex.IsMatch(l, @"^" + editNode.Text + @".menu.PartitionScheme."))
                        {
                            lineIndex = index - 1;
                        }
                    }
                }
            });
            if(lineIndex > 0)
            {
                boardsFile.Insert(lineIndex, editNode.Text + ".menu.PartitionScheme." + textBox1.Text + ".upload.maximum_size=" + partitionSize);
                boardsFile.Insert(lineIndex, editNode.Text + ".menu.PartitionScheme." + textBox1.Text + ".build.partitions=" + textBox1.Text);
                boardsFile.Insert(lineIndex, editNode.Text + ".menu.PartitionScheme." + textBox1.Text + "=" + textBox2.Text);

                File.WriteAllLines(Path.Combine(appData, "boards.txt"), boardsFile);

                button5_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Unable to find partition scheme on this board", "index not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private int GetSizeFromFile(string file)
        {
            if (file.EndsWith(".bin"))
            {
                var bytes = File.ReadAllBytes(file);
                for (var j = 0; j < 16; j++)
                {
                    if(Encoding.ASCII.GetString(bytes, 12 + 32 * j, 10).Trim('\0') == "app0")
                    {
                        int size = 0;
                        for(var k=0;k<4;k++)
                        {
                            size = (size << 8) + bytes[11 + 32 * j - k];
                        }
                        return size;
                    }
                }
            }
            else
            {
                var lines = File.ReadAllLines(file);
            }
            return 0;
        }
    }
}
