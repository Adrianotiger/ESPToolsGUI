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
using System.Text;
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

            efuse.ConsoleEvent += HandleCustomEvent;
            tool.ConsoleEvent += HandleCustomEvent;
            secure.ConsoleEvent += HandleCustomEvent;
            partition.ConsoleEvent += HandleCustomEvent;

            ExpertComboboxTool.SelectedIndex = 0;
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
            foreach(ComPort i in comboBox1.Items)
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
            progressBar1.Value = 10;
            progressBar1.Visible = true;
            tool.Parse("flash_id");
            tabControl1.SelectedIndex = 0;
            progressBar1.Value = 20;
            infoTextboxChipType.Text = tool.ChipType;
            infoTextboxChip.Text = tool.Chip;
            infoTextboxFeature.Text = tool.Features;
            infoTextboxCrystal.Text = tool.Crystal;
            infoTextboxMac.Text = tool.MAC;
            infoTextboxFlash.Text = tool.Flash;
            Application.DoEvents();
            tool.Parse("read_flash 0x8000 0x400 temp.bin");
            progressBar1.Value = 50;
            partitionChart.Series[0].Points.Clear();
            partitionListview.Items.Clear();
            tabControl1.SelectedIndex++;
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
            Application.DoEvents();
            efuse.Parse("summary");
            progressBar1.Value = 70;
            tableLayoutPanel1.RowCount = 11;
            tableLayoutPanel1.Controls.Clear();
            int row = 0;
            tabControl1.Visible = false;
            tabControl1.SelectedIndex++;
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
                    var t2 = new TextBox { Text = v.Value, Parent = tableLayoutPanel1, Dock = DockStyle.Fill, ReadOnly=true };
                    tableLayoutPanel1.SetRow(t2, row);
                    tableLayoutPanel1.SetColumn(t2, 2);
                    l2 = new Label { Text = v.ReadWrite, Parent = tableLayoutPanel1, Dock = DockStyle.Fill };
                    tableLayoutPanel1.SetRow(l2, row);
                    tableLayoutPanel1.SetColumn(l2, 3);
                    row++;
                }
            }
            tableLayoutPanel1.RowStyles.Clear();
            //for(var k=0;k<tableLayoutPanel1.RowCount;k++)
            //{
            //    tableLayoutPanel1.RowStyles.Add(new RowStyle { Height = 20, SizeType = SizeType.Absolute });
            //}
            tabControl1.Visible = true;
            Application.DoEvents();
            // todo: read something...
            progressBar1.Value = 100;
            Application.DoEvents();
            progressBar1.Visible = false;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '\r')
            {
                switch(ExpertComboboxTool.Text)
                {
                    case "esptool": tool.Execute((sender as TextBox).Text); break;
                    case "espefuse": efuse.Execute((sender as TextBox).Text); break;
                    case "espsecure": secure.Execute((sender as TextBox).Text); break;
                    case "gen_esp32part": partition.Execute((sender as TextBox).Text); break;
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

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
                string txt = " > " + a.input + "\r\n";
                richTextBox1.SelectionColor = Color.White;
                richTextBox1.AppendText(txt);
                richTextBox1.SelectionColor = richTextBox1.ForeColor;
                richTextBox1.ScrollToCaret(); 
            }
            if(a.error.Length > 0)
            {
                string txt = " [E] " + a.error + "\r\n";
                richTextBox1.SelectionColor = Color.LightCoral;
                richTextBox1.AppendText(txt);
                richTextBox1.SelectionColor = richTextBox1.ForeColor;
                richTextBox1.ScrollToCaret();
            }
            if(a.output.Length > 0)
            {
                string txt = a.output + "\r\n";
                richTextBox1.AppendText(txt);
                richTextBox1.ScrollToCaret();
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
