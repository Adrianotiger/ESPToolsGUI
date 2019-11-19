namespace esp_tools_gui
{
    partial class PartTool
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.trackBarOta = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelProgress = new System.Windows.Forms.Label();
            this.checkBoxOta = new System.Windows.Forms.CheckBox();
            this.checkBoxNvs = new System.Windows.Forms.CheckBox();
            this.trackBarNvs = new System.Windows.Forms.TrackBar();
            this.checkBoxEeprom = new System.Windows.Forms.CheckBox();
            this.trackBarEeprom = new System.Windows.Forms.TrackBar();
            this.checkBoxSpiffs = new System.Windows.Forms.CheckBox();
            this.trackBarSpiffs = new System.Windows.Forms.TrackBar();
            this.labelOta = new System.Windows.Forms.Label();
            this.labelNvs = new System.Windows.Forms.Label();
            this.labelEeprom = new System.Windows.Forms.Label();
            this.labelSpiffs = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.labelOta1 = new System.Windows.Forms.Label();
            this.trackBarOta1 = new System.Windows.Forms.TrackBar();
            this.checkBoxOtaLock = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarNvs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarEeprom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpiffs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOta1)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarOta
            // 
            this.trackBarOta.LargeChange = 1000;
            this.trackBarOta.Location = new System.Drawing.Point(90, 20);
            this.trackBarOta.Maximum = 4000;
            this.trackBarOta.Name = "trackBarOta";
            this.trackBarOta.Size = new System.Drawing.Size(377, 45);
            this.trackBarOta.SmallChange = 4;
            this.trackBarOta.TabIndex = 0;
            this.trackBarOta.TickFrequency = 80;
            this.trackBarOta.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarOta.Scroll += new System.EventHandler(this.trackBarOta_Scroll);
            this.trackBarOta.ValueChanged += new System.EventHandler(this.trackBarOta_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 282);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Used / Total size:";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 298);
            this.progressBar1.MarqueeAnimationSpeed = 200;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(484, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 2;
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(381, 281);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(54, 13);
            this.labelProgress.TabIndex = 3;
            this.labelProgress.Text = "0 / 4120k";
            // 
            // checkBoxOta
            // 
            this.checkBoxOta.AutoSize = true;
            this.checkBoxOta.Checked = true;
            this.checkBoxOta.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOta.Location = new System.Drawing.Point(12, 48);
            this.checkBoxOta.Name = "checkBoxOta";
            this.checkBoxOta.Size = new System.Drawing.Size(48, 17);
            this.checkBoxOta.TabIndex = 4;
            this.checkBoxOta.Text = "OTA";
            this.checkBoxOta.UseVisualStyleBackColor = true;
            this.checkBoxOta.CheckedChanged += new System.EventHandler(this.checkBoxOta_CheckedChanged);
            // 
            // checkBoxNvs
            // 
            this.checkBoxNvs.AutoSize = true;
            this.checkBoxNvs.Checked = true;
            this.checkBoxNvs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxNvs.Location = new System.Drawing.Point(12, 126);
            this.checkBoxNvs.Name = "checkBoxNvs";
            this.checkBoxNvs.Size = new System.Drawing.Size(48, 17);
            this.checkBoxNvs.TabIndex = 6;
            this.checkBoxNvs.Text = "NVS";
            this.checkBoxNvs.UseVisualStyleBackColor = true;
            this.checkBoxNvs.CheckedChanged += new System.EventHandler(this.checkBoxNvs_CheckedChanged);
            // 
            // trackBarNvs
            // 
            this.trackBarNvs.LargeChange = 1000;
            this.trackBarNvs.Location = new System.Drawing.Point(90, 113);
            this.trackBarNvs.Maximum = 4000;
            this.trackBarNvs.Name = "trackBarNvs";
            this.trackBarNvs.Size = new System.Drawing.Size(377, 45);
            this.trackBarNvs.SmallChange = 4;
            this.trackBarNvs.TabIndex = 5;
            this.trackBarNvs.TickFrequency = 80;
            this.trackBarNvs.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarNvs.ValueChanged += new System.EventHandler(this.trackBarNvs_ValueChanged);
            // 
            // checkBoxEeprom
            // 
            this.checkBoxEeprom.AutoSize = true;
            this.checkBoxEeprom.Checked = true;
            this.checkBoxEeprom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEeprom.Location = new System.Drawing.Point(12, 177);
            this.checkBoxEeprom.Name = "checkBoxEeprom";
            this.checkBoxEeprom.Size = new System.Drawing.Size(72, 17);
            this.checkBoxEeprom.TabIndex = 8;
            this.checkBoxEeprom.Text = "EEPROM";
            this.checkBoxEeprom.UseVisualStyleBackColor = true;
            this.checkBoxEeprom.CheckedChanged += new System.EventHandler(this.checkBoxEeprom_CheckedChanged);
            // 
            // trackBarEeprom
            // 
            this.trackBarEeprom.LargeChange = 1000;
            this.trackBarEeprom.Location = new System.Drawing.Point(90, 164);
            this.trackBarEeprom.Maximum = 4000;
            this.trackBarEeprom.Name = "trackBarEeprom";
            this.trackBarEeprom.Size = new System.Drawing.Size(377, 45);
            this.trackBarEeprom.SmallChange = 4;
            this.trackBarEeprom.TabIndex = 7;
            this.trackBarEeprom.TickFrequency = 80;
            this.trackBarEeprom.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarEeprom.ValueChanged += new System.EventHandler(this.trackBarEeprom_ValueChanged);
            // 
            // checkBoxSpiffs
            // 
            this.checkBoxSpiffs.AutoSize = true;
            this.checkBoxSpiffs.Checked = true;
            this.checkBoxSpiffs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSpiffs.Location = new System.Drawing.Point(12, 228);
            this.checkBoxSpiffs.Name = "checkBoxSpiffs";
            this.checkBoxSpiffs.Size = new System.Drawing.Size(62, 17);
            this.checkBoxSpiffs.TabIndex = 10;
            this.checkBoxSpiffs.Text = "SPIFFS";
            this.checkBoxSpiffs.UseVisualStyleBackColor = true;
            this.checkBoxSpiffs.CheckedChanged += new System.EventHandler(this.checkBoxSpiffs_CheckedChanged);
            // 
            // trackBarSpiffs
            // 
            this.trackBarSpiffs.LargeChange = 1000;
            this.trackBarSpiffs.Location = new System.Drawing.Point(90, 215);
            this.trackBarSpiffs.Maximum = 4000;
            this.trackBarSpiffs.Name = "trackBarSpiffs";
            this.trackBarSpiffs.Size = new System.Drawing.Size(377, 45);
            this.trackBarSpiffs.SmallChange = 4;
            this.trackBarSpiffs.TabIndex = 9;
            this.trackBarSpiffs.TickFrequency = 80;
            this.trackBarSpiffs.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarSpiffs.ValueChanged += new System.EventHandler(this.trackBarSpiffs_ValueChanged);
            // 
            // labelOta
            // 
            this.labelOta.AutoSize = true;
            this.labelOta.Location = new System.Drawing.Point(465, 34);
            this.labelOta.Name = "labelOta";
            this.labelOta.Size = new System.Drawing.Size(31, 13);
            this.labelOta.TabIndex = 11;
            this.labelOta.Text = "100k";
            // 
            // labelNvs
            // 
            this.labelNvs.AutoSize = true;
            this.labelNvs.Location = new System.Drawing.Point(465, 126);
            this.labelNvs.Name = "labelNvs";
            this.labelNvs.Size = new System.Drawing.Size(31, 13);
            this.labelNvs.TabIndex = 12;
            this.labelNvs.Text = "100k";
            // 
            // labelEeprom
            // 
            this.labelEeprom.AutoSize = true;
            this.labelEeprom.Location = new System.Drawing.Point(465, 177);
            this.labelEeprom.Name = "labelEeprom";
            this.labelEeprom.Size = new System.Drawing.Size(31, 13);
            this.labelEeprom.TabIndex = 13;
            this.labelEeprom.Text = "100k";
            // 
            // labelSpiffs
            // 
            this.labelSpiffs.AutoSize = true;
            this.labelSpiffs.Location = new System.Drawing.Point(465, 228);
            this.labelSpiffs.Name = "labelSpiffs";
            this.labelSpiffs.Size = new System.Drawing.Size(31, 13);
            this.labelSpiffs.TabIndex = 14;
            this.labelSpiffs.Text = "100k";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 379);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 35);
            this.button1.TabIndex = 15;
            this.button1.Text = "Export Table";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(33, 420);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(141, 35);
            this.button2.TabIndex = 16;
            this.button2.Text = "Burn Table";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(355, 358);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(141, 35);
            this.button3.TabIndex = 17;
            this.button3.Text = "Cancel";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "bin-File|*.bin|csv-File|*.csv";
            this.saveFileDialog1.RestoreDirectory = true;
            this.saveFileDialog1.Title = "Save Partition Table";
            // 
            // labelOta1
            // 
            this.labelOta1.AutoSize = true;
            this.labelOta1.Location = new System.Drawing.Point(465, 76);
            this.labelOta1.Name = "labelOta1";
            this.labelOta1.Size = new System.Drawing.Size(31, 13);
            this.labelOta1.TabIndex = 20;
            this.labelOta1.Text = "100k";
            // 
            // trackBarOta1
            // 
            this.trackBarOta1.Enabled = false;
            this.trackBarOta1.LargeChange = 1000;
            this.trackBarOta1.Location = new System.Drawing.Point(90, 62);
            this.trackBarOta1.Maximum = 4000;
            this.trackBarOta1.Name = "trackBarOta1";
            this.trackBarOta1.Size = new System.Drawing.Size(377, 45);
            this.trackBarOta1.SmallChange = 4;
            this.trackBarOta1.TabIndex = 18;
            this.trackBarOta1.TickFrequency = 80;
            this.trackBarOta1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarOta1.ValueChanged += new System.EventHandler(this.trackBarOta1_ValueChanged);
            // 
            // checkBoxOtaLock
            // 
            this.checkBoxOtaLock.AutoSize = true;
            this.checkBoxOtaLock.Checked = true;
            this.checkBoxOtaLock.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOtaLock.Location = new System.Drawing.Point(33, 72);
            this.checkBoxOtaLock.Name = "checkBoxOtaLock";
            this.checkBoxOtaLock.Size = new System.Drawing.Size(50, 17);
            this.checkBoxOtaLock.TabIndex = 21;
            this.checkBoxOtaLock.Text = "Lock";
            this.checkBoxOtaLock.UseVisualStyleBackColor = true;
            this.checkBoxOtaLock.CheckedChanged += new System.EventHandler(this.checkBoxOtaLock_CheckedChanged);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(33, 338);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(141, 35);
            this.button4.TabIndex = 22;
            this.button4.Text = "Import Table";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "bin-File|*.bin|csv-File|*.csv";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Title = "Import Partition Table";
            // 
            // PartTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 468);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.checkBoxOtaLock);
            this.Controls.Add(this.labelOta1);
            this.Controls.Add(this.trackBarOta1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelSpiffs);
            this.Controls.Add(this.labelEeprom);
            this.Controls.Add(this.labelNvs);
            this.Controls.Add(this.labelOta);
            this.Controls.Add(this.checkBoxSpiffs);
            this.Controls.Add(this.trackBarSpiffs);
            this.Controls.Add(this.checkBoxEeprom);
            this.Controls.Add(this.trackBarEeprom);
            this.Controls.Add(this.checkBoxNvs);
            this.Controls.Add(this.trackBarNvs);
            this.Controls.Add(this.checkBoxOta);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBarOta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PartTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PartTool";
            this.Load += new System.EventHandler(this.PartTool_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarNvs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarEeprom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpiffs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOta1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarOta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.CheckBox checkBoxOta;
        private System.Windows.Forms.CheckBox checkBoxNvs;
        private System.Windows.Forms.TrackBar trackBarNvs;
        private System.Windows.Forms.CheckBox checkBoxEeprom;
        private System.Windows.Forms.TrackBar trackBarEeprom;
        private System.Windows.Forms.CheckBox checkBoxSpiffs;
        private System.Windows.Forms.TrackBar trackBarSpiffs;
        private System.Windows.Forms.Label labelOta;
        private System.Windows.Forms.Label labelNvs;
        private System.Windows.Forms.Label labelEeprom;
        private System.Windows.Forms.Label labelSpiffs;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label labelOta1;
        private System.Windows.Forms.TrackBar trackBarOta1;
        private System.Windows.Forms.CheckBox checkBoxOtaLock;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}