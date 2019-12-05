using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace esp_tools_gui
{
    public partial class Connecting : Form
    {
        private static bool Stop = false;
        public Connecting(string tool, string text)
        {
            InitializeComponent();

            Opacity = 0.0;
            TitleLabel.Text = tool;
            ConnectionLabel.Text = text;
            progressBar1.Value = 0;
            timer1.Enabled = true;
            Stop = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Opacity < 1.0) Opacity += 0.05;
            if (progressBar1.Value < progressBar1.Maximum) progressBar1.Value++;
            else if (linkLabel1.Visible == false) linkLabel1.Visible = true;
            if (Stop)
            {
                timer1.Enabled = false;
                this.Close();
            }
        }

        public static void Terminate()
        {
            Stop = true;
        }

        private void Connecting_Load(object sender, EventArgs e)
        {
            Top -= 200;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Stop = true;
        }
    }
}
