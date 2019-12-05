using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace esp_tools_gui
{
    public partial class MainPage : Form
    {
        private static StringBuilder outStr;
        private ToolEfuse efuse;
        private ToolTool tool;
        private ToolSecure secure;
        private ToolPartition partition;
        private ToolPart partition2;
        private bool HasError = false;
        private bool isScrolling = false;

        private delegate void SafeCallDelegate(object sender, CustomEventArgs a);

        public MainPage()
        {
            InitializeComponent();

            outStr = new StringBuilder();
            richTextBox1.Text = "";

            efuse = new ToolEfuse();
            tool = new ToolTool();
            secure = new ToolSecure();
            partition = new ToolPartition();
            partition2 = new ToolPart();

            efuse.ConsoleEvent += HandleCustomEvent;
            tool.ConsoleEvent += HandleCustomEvent;
            secure.ConsoleEvent += HandleCustomEvent;
            partition.ConsoleEvent += HandleCustomEvent;
            partition2.ConsoleEvent += HandleCustomEvent;

            richTextBoxFlashHex.MouseWheel += RichTextBox_MouseWheel;
            richTextBoxFlashLine.MouseWheel += RichTextBox_MouseWheel;
            richTextBoxFlashData.MouseWheel += RichTextBox_MouseWheel;

            ExpertComboboxTool.SelectedIndex = 0;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Text += " - (v." + Application.ProductVersion + ")";
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM WIN32_SerialPort"))
            {
                string[] portnames = SerialPort.GetPortNames();
                var ports2 = searcher.Get().Cast<ManagementBaseObject>().ToList();
                var tList = (from n in portnames
                             join p in ports2 on n equals p["DeviceID"].ToString() into joinedList
                             from sub in joinedList.DefaultIfEmpty()
                             select new ComPort{ PortId=n, Info=sub==null?"":sub["Caption"].ToString() }).ToList();

                tList.ForEach((t)=> { comboBox1.Items.Add(t); });
            }
            comboBox1.SelectedIndex = 0;
            if(comboBox1.Items.Count > 0) Tool._com = ((ComPort)comboBox1.Items[0]).PortId;
            foreach (ComPort i in comboBox1.Items)
            {
                if(Properties.Settings.Default.comport == i.ToString())
                {
                    comboBox1.SelectedItem = i;
                    Tool._com = i.PortId;
                    break;
                }
            }
            comboBox2.Text = Properties.Settings.Default.combaud;

            Tool._baud = Properties.Settings.Default.combaud;

            Application.DoEvents();

            comboBox1.TextChanged += (s, ev) =>
            {
                Tool._com = ((s as ComboBox).SelectedItem as ComPort).PortId;
                Properties.Settings.Default.comport = (s as ComboBox).Text;
                Properties.Settings.Default.Save();
            };
            comboBox2.TextChanged += (s, ev) =>
            {
                Tool._baud = Properties.Settings.Default.combaud;
                Properties.Settings.Default.combaud = (s as ComboBox).Text;
                Properties.Settings.Default.Save();
            };
        }

        public class ComPort
        {
            public string PortId { get; set; }
            public string Info { get; set; }
            public override string ToString()
            {
                if (Info.Length > 1)
                {
                    return PortId + " - " + Info;
                }
                else
                {
                    return PortId;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HasError = false;
            splitContainer3.Panel2Collapsed = false;
            progressBar1.Value = 20;
            progressBar1.Visible = true;
            FillInfoTab();
            if (HasError)
            {
                return;
            }
            tabControl1.SelectedIndex = 0;
            progressBar1.Value = 50;
            Application.DoEvents();
            FillPartitionTab();
            if (HasError)
            {
                return;
            }
            progressBar1.Value = 80;
            tabControl1.SelectedIndex++;
            Application.DoEvents();
            FillEFuseTab();
            if (HasError)
            {
                return;
            }
            progressBar1.Value = 100;
            tabControl1.SelectedIndex++;
            Application.DoEvents();
            // todo: read something...
            progressBar1.Value = 100;
            Application.DoEvents();
            progressBar1.Visible = false;
            tabControl1.Enabled = true;
        }

        private void FillInfoTab()
        {
            tool.Parse("--after no_reset flash_id");
            infoTextboxChipType.Text = tool.ChipType;
            infoTextboxChip.Text = tool.Chip;
            infoTextboxFeature.Text = tool.Features;
            infoTextboxCrystal.Text = tool.Crystal;
            infoTextboxMac.Text = tool.MAC;
            infoTextboxFlash.Text = tool.Flash;
        }

        private void FillPartitionTab()
        {
            tool.ReadPartitionTable();
            partitionChart.Series[0].Points.Clear();
            partitionListview.Items.Clear();
            foreach (var p in tool.Partitions)
            {
                var lv = partitionListview.Items.Add(p.Name);
                lv.SubItems.Add(p.GetTypeName());
                lv.SubItems.Add(p.GetSubTypeName());
                lv.SubItems.Add("0x" + p.Offset.ToString("X"));
                lv.SubItems.Add(p.GetSize());
                lv.SubItems.Add("0x" + p.Flags.ToString("X"));

                var point = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0, p.Size);
                point.AxisLabel = p.Name;
                partitionChart.Series[0].Points.Add(point);
            }
        }

        private void FillEFuseTab()
        {
            efuse.Parse("summary");
            tableLayoutPanel1.RowCount = 11;
            tableLayoutPanel1.Controls.Clear();
            int row = 0;
            foreach (var f in efuse.Fuses)
            {
                if (row > 10) tableLayoutPanel1.RowCount++;
                var l = new Label { Text = f.Key + ":", Parent = tableLayoutPanel1, Dock = DockStyle.Fill, BackColor = Color.AliceBlue };
                tableLayoutPanel1.SetRow(l, row);
                tableLayoutPanel1.SetColumn(l, 0);
                tableLayoutPanel1.SetColumnSpan(l, 4);
                row++;
                foreach (var v in efuse.Fuses[f.Key])
                {
                    if (row > 10) tableLayoutPanel1.RowCount++;
                    var l2 = new Label { Text = v.Title, Parent = tableLayoutPanel1, Dock = DockStyle.Fill };
                    tableLayoutPanel1.SetRow(l2, row);
                    tableLayoutPanel1.SetColumn(l2, 0);
                    l2 = new Label { Text = v.Description, Parent = tableLayoutPanel1, Dock = DockStyle.Fill };
                    tableLayoutPanel1.SetRow(l2, row);
                    tableLayoutPanel1.SetColumn(l2, 1);
                    var t2 = new TextBox { Text = v.Value, Parent = tableLayoutPanel1, Dock = DockStyle.Fill, ReadOnly = true };
                    tableLayoutPanel1.SetRow(t2, row);
                    tableLayoutPanel1.SetColumn(t2, 2);
                    l2 = new Label { Text = v.ReadWrite, Parent = tableLayoutPanel1, Dock = DockStyle.Fill };
                    tableLayoutPanel1.SetRow(l2, row);
                    tableLayoutPanel1.SetColumn(l2, 3);
                    row++;
                }
            }
            tableLayoutPanel1.RowStyles.Clear();
        }

        
        private async void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                var text = (sender as ComboBox).Text;
                switch (ExpertComboboxTool.Text)
                {
                    case "esptool": await tool.Execute(text); break;
                    case "espefuse": await efuse.Execute(text); break;
                    case "espsecure": await secure.Execute(text); break;
                    case "gen_esp32part": await partition.Execute(text); break;
                    case "parttool": await partition2.Execute(text); break;
                }

                for(var k=0;k<comboBox3.Items.Count;k++)
                {
                    if(comboBox3.Items[k].ToString() == text)
                    {
                        if (k != 0) comboBox3.Items.RemoveAt(k);
                        else return;
                        break;
                    }
                }
                if(text.Length > 1) comboBox3.Items.Insert(0, text);
            }
        }

        void HandleCustomEvent(object sender, CustomEventArgs a)
        {
            if (richTextBox1.InvokeRequired)
            {
                this.BeginInvoke(new SafeCallDelegate(HandleCustomEvent), new object[] { sender, a });
                return;
            }
            if (a.input.Length > 0)
            {
                string txt;
                txt = "\r\n > " + a.input + "\r\n";
                richTextBox1.SelectionColor = Color.White;
                richTextBox1.AppendText(txt);
                richTextBox1.SelectionColor = richTextBox1.ForeColor;
                richTextBox1.ScrollToCaret(); 
            }
            if(a.error.Length > 0)
            {
                string txt;
                txt = "\r\n [E] " + a.error + "\r\n";
                richTextBox1.SelectionColor = Color.LightCoral;
                richTextBox1.AppendText(txt);
                richTextBox1.SelectionColor = richTextBox1.ForeColor;
                richTextBox1.ScrollToCaret();
                HasError = true;
            }
            if(a.output.Length > 0)
            {
                string txt;
                txt = a.output;
                if (txt.Contains("fatal error")) HasError = true;
                //else txt = a.output + "\r\n";
                richTextBox1.AppendText(txt);
                if(txt.Contains("\r\n")) richTextBox1.ScrollToCaret();
            }
        }

        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start((sender as LinkLabel).Text);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void readFlashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(partitionListview.SelectedIndices.Count == 1)
            {
                int index = partitionListview.SelectedIndices[0];
                Regex re = new Regex(@"(\d+)(\.\d+)?\s*");
                var m = re.Matches(partitionListview.Items[index].SubItems[4].Text);
                if (m.Count >= 1)
                {
                    int address = Convert.ToInt32(partitionListview.Items[index].SubItems[3].Text, 16);
                    int size = (int)(double.Parse(m[0].Value) * 1024);

                    numericUpDownFlashAddr.Value = address;
                    numericUpDownFlashCount.Value = size;

                    tabControl1.SelectedIndex = 3;
                    buttonReadFlash_Click(sender, e);
                }
            }
        }

        private void RichTextBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (vScrollBar1.Value < vScrollBar1.Maximum) vScrollBar1.Value++;
            }
            else if (e.Delta > 0)
            {
                if (vScrollBar1.Value > 0) vScrollBar1.Value--;
            }
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        private void vScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            if (isScrolling) return;
            int line = richTextBoxFlashLine.GetFirstCharIndexFromLine(vScrollBar1.Value);
            if (line < 0) return;
            richTextBoxFlashLine.Select(line, 8);
        }

        private void richTextBoxFlashData_SelectionChanged(object sender, EventArgs e)
        {
            if (isScrolling) return;
            isScrolling = true;
            int line = richTextBoxFlashData.GetLineFromCharIndex(richTextBoxFlashData.SelectionStart);
            int charX0 = richTextBoxFlashData.GetFirstCharIndexFromLine(line);
            //if (richTextBoxFlashHex.SelectionStart > 2) charX0 += 1;
            int charSelection = (richTextBoxFlashData.SelectionStart - charX0);
            richTextBoxFlashLine.Select(richTextBoxFlashLine.GetFirstCharIndexFromLine(line), 8);
            richTextBoxFlashHex.Select(richTextBoxFlashHex.GetFirstCharIndexFromLine(line) + charSelection * 3 + 1, 2);
            vScrollBar1.Value = line;
            Application.DoEvents();
            isScrolling = false;
        }

        private void richTextBoxFlashLine_SelectionChanged(object sender, EventArgs e)
        {
            if (isScrolling) return;
            isScrolling = true;
            int line = richTextBoxFlashLine.GetLineFromCharIndex(richTextBoxFlashLine.SelectionStart);
            int charX0 = richTextBoxFlashLine.GetFirstCharIndexFromLine(line);
            richTextBoxFlashData.SelectionStart = richTextBoxFlashData.GetFirstCharIndexFromLine(line);
            richTextBoxFlashHex.SelectionStart = richTextBoxFlashHex.GetFirstCharIndexFromLine(line);
            vScrollBar1.Value = line;
            Application.DoEvents();
            isScrolling = false;
        }

        private void richTextBoxFlashHex_SelectionChanged(object sender, EventArgs e)
        {
            if (isScrolling) return;
            isScrolling = true;
            int line = richTextBoxFlashHex.GetLineFromCharIndex(richTextBoxFlashHex.SelectionStart);
            int charX0 = richTextBoxFlashHex.GetFirstCharIndexFromLine(line);
            if (richTextBoxFlashHex.SelectionStart > 2) charX0 += 1;
            int charSelection = (richTextBoxFlashHex.SelectionStart - charX0) / 3;
            richTextBoxFlashLine.Select(richTextBoxFlashLine.GetFirstCharIndexFromLine(line), 8);
            richTextBoxFlashData.Select(richTextBoxFlashData.GetFirstCharIndexFromLine(line) + charSelection, 1);
            vScrollBar1.Value = line;
            Application.DoEvents();
            isScrolling = false;
        }

        private async void editPartitionTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Regex re = new Regex(@"(\d+)(\.\d+)?\s*");
            var m = re.Matches(tool.Flash);

            if (m.Count > 0)
            {
                PartTool pt = new PartTool(int.Parse(m[0].Value), partition);
                foreach (ListViewItem lv in partitionListview.Items)
                {
                    m = re.Matches(lv.SubItems[4].Text);
                    double size = double.Parse(m[0].Value);
                    if (m.Count > 1) double.Parse(m[0].Value + "." + m[1].Value);
                    if (lv.SubItems[4].Text.IndexOf("k") > 0) size *= 1024;
                    if (lv.SubItems[4].Text.IndexOf("M") > 0) size *= 1024 * 1024;
                    size = Convert.ToInt32(size);
                    switch (lv.Text.Replace("\0", "").Trim())
                    {
                        case "nvs":
                            pt.SetNvs(int.Parse(size.ToString()));
                            break;
                        case "eeprom":
                            pt.SetEeprom(int.Parse(size.ToString()));
                            break;
                        case "app0":
                            pt.SetOta0(int.Parse(size.ToString()), null);
                            break;
                        case "app1":
                            pt.SetOta1(int.Parse(size.ToString()));
                            break;
                        case "spiffs":
                            pt.SetSpiffs(int.Parse(size.ToString()));
                            break;
                    }
                }
                if (pt.ShowDialog() == DialogResult.Yes)
                {
                    var res = partition.CreatePartition(PartTool.Nvs, PartTool.Ota0, PartTool.Ota1, PartTool.Eeprom, PartTool.Spiffs);
                    await tool.Execute("--before default_reset --after hard_reset write_flash 0x8000 part.bin");
                    FillPartitionTab();
                }
            }
            else
            {
                MessageBox.Show("Unable to get flash size. Connect device before execute this function.", "Error");
            }
        }

        private void buttonReadFlash_Click(object sender, EventArgs e)
        {
            isScrolling = true;
            var startAddr = (int)numericUpDownFlashAddr.Value;
            var TotalRead = (int)numericUpDownFlashCount.Value;
            int ToRead = TotalRead;
            richTextBoxFlashHex.Clear();
            richTextBoxFlashLine.Clear();
            richTextBoxFlashData.Clear();

            vScrollBar1.Value = 1;
            vScrollBar1.Maximum = TotalRead / 16;

            int voids = -(startAddr % 16);
            int lineNumber = startAddr + voids;
            char t;

            //do
            {
                String s1 = "", s2 = "", s3 = "";
                if (ToRead > 0x10000) ToRead = 0x8000;
                var bytes = ReadFlashPart(startAddr, ToRead);
                TotalRead -= ToRead;
                if (voids > 0) voids = 0;
                int lines = ToRead / 16;

                for (var j = 0; j < lines; j++)
                {
                    s1 += "0x" + lineNumber.ToString("X6") + "\n";

                    for (var k = 0; k < 16; k++)
                    {
                        s2 += " ";
                        if (voids < 0)
                        {
                            s2 += "  ";
                        }
                        else if (voids > bytes.Length)
                        {
                            s2 += "  ";
                        }
                        else
                        {
                            s2 += bytes[voids].ToString("X2");
                        }
                        voids++;
                    }
                    s2 += "\n";

                    voids -= 16;
                    for (var k = 0; k < 16; k++)
                    {
                        if (voids < 0)
                        {
                            s3 += " ";
                        }
                        else if (voids > bytes.Length)
                        {
                            s3 += " ";
                        }
                        else
                        {
                            t = ((char)bytes[voids]);
                            if (t >= 0x20)
                                s3 += ((char)bytes[voids]).ToString();
                            else
                                s3 += ".";
                        }
                        voids++;
                    }
                    s3 += "\n";
                    lineNumber += 16;
                }

                richTextBoxFlashLine.AppendText(s1);
                richTextBoxFlashHex.AppendText(s2);
                richTextBoxFlashData.AppendText(s3);

                startAddr += ToRead;
                ToRead = TotalRead;
            }// while (ToRead > 0);

            int totalLines = richTextBoxFlashData.Lines.Length;
            double lineHeight = richTextBoxFlashData.PreferredSize.Height * 1.0 / totalLines;
            int visibleLines = (int)(richTextBoxFlashData.Height / lineHeight);
            vScrollBar1.Maximum = Math.Max(1, totalLines);
            isScrolling = false;
            vScrollBar1.Value = 0;
        }

        private Byte[] ReadFlashPart(int startAddr, int bytes)
        {
            return tool.ReadMemory(startAddr, bytes);
        }

        private async void ereasePartitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (partitionListview.SelectedIndices.Count == 1)
            {
                if(MessageBox.Show("Do you really want erease this partition? All data will be ereased.", "Clear partition", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int index = partitionListview.SelectedIndices[0];
                    Regex re = new Regex(@"(\d+)(\.\d+)?\s*");
                    var m = re.Matches(partitionListview.Items[index].SubItems[4].Text);
                    if (m.Count >= 1)
                    {
                        int address = Convert.ToInt32(partitionListview.Items[index].SubItems[3].Text, 16);
                        int size = (int)(double.Parse(m[0].Value) * 1024);

                        await tool.Execute("erase_region 0x" + address.ToString("X") + " 0x" + size.ToString("X"));
                    }
                }
            }
        }
    }

    public class CustomEventArgs
    {
        public string input { get; set; } = "";
        public string output { get; set; } = "";
        public string error { get; set; } = "";
    }
}
