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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Partition Name");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Partitions", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PartTool));
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.trackBarOtaD = new System.Windows.Forms.TrackBar();
            this.checkBoxOtaD = new System.Windows.Forms.CheckBox();
            this.labelOtaD = new System.Windows.Forms.Label();
            this.labelFfat = new System.Windows.Forms.Label();
            this.checkBoxFfat = new System.Windows.Forms.CheckBox();
            this.trackBarFfat = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarNvs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarEeprom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpiffs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOta1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOtaD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFfat)).BeginInit();
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
            this.label1.Location = new System.Drawing.Point(30, 377);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Used / Total size:";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 393);
            this.progressBar1.MarqueeAnimationSpeed = 200;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(484, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 2;
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(381, 376);
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
            this.checkBoxNvs.Location = new System.Drawing.Point(12, 177);
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
            this.trackBarNvs.Location = new System.Drawing.Point(90, 164);
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
            this.checkBoxEeprom.Location = new System.Drawing.Point(12, 228);
            this.checkBoxEeprom.Name = "checkBoxEeprom";
            this.checkBoxEeprom.Size = new System.Drawing.Size(72, 17);
            this.checkBoxEeprom.TabIndex = 8;
            this.checkBoxEeprom.Text = "EEPROM";
            this.checkBoxEeprom.UseVisualStyleBackColor = true;
            this.checkBoxEeprom.CheckedChanged += new System.EventHandler(this.checkBoxEeprom_CheckedChanged);
            // 
            // trackBarEeprom
            // 
            this.trackBarEeprom.Enabled = false;
            this.trackBarEeprom.LargeChange = 1000;
            this.trackBarEeprom.Location = new System.Drawing.Point(90, 215);
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
            this.checkBoxSpiffs.Location = new System.Drawing.Point(12, 279);
            this.checkBoxSpiffs.Name = "checkBoxSpiffs";
            this.checkBoxSpiffs.Size = new System.Drawing.Size(62, 17);
            this.checkBoxSpiffs.TabIndex = 10;
            this.checkBoxSpiffs.Text = "SPIFFS";
            this.checkBoxSpiffs.UseVisualStyleBackColor = true;
            this.checkBoxSpiffs.CheckedChanged += new System.EventHandler(this.checkBoxSpiffs_CheckedChanged);
            // 
            // trackBarSpiffs
            // 
            this.trackBarSpiffs.Enabled = false;
            this.trackBarSpiffs.LargeChange = 1000;
            this.trackBarSpiffs.Location = new System.Drawing.Point(90, 266);
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
            this.labelNvs.Location = new System.Drawing.Point(465, 177);
            this.labelNvs.Name = "labelNvs";
            this.labelNvs.Size = new System.Drawing.Size(31, 13);
            this.labelNvs.TabIndex = 12;
            this.labelNvs.Text = "100k";
            // 
            // labelEeprom
            // 
            this.labelEeprom.AutoSize = true;
            this.labelEeprom.Location = new System.Drawing.Point(465, 228);
            this.labelEeprom.Name = "labelEeprom";
            this.labelEeprom.Size = new System.Drawing.Size(31, 13);
            this.labelEeprom.TabIndex = 13;
            this.labelEeprom.Text = "100k";
            // 
            // labelSpiffs
            // 
            this.labelSpiffs.AutoSize = true;
            this.labelSpiffs.Location = new System.Drawing.Point(465, 279);
            this.labelSpiffs.Name = "labelSpiffs";
            this.labelSpiffs.Size = new System.Drawing.Size(31, 13);
            this.labelSpiffs.TabIndex = 14;
            this.labelSpiffs.Text = "100k";
            // 
            // button1
            // 
            this.button1.Image = global::esp_tools_gui.Properties.Resources.export32;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(118, 433);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 55);
            this.button1.TabIndex = 15;
            this.button1.Text = "Export Table";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Image = global::esp_tools_gui.Properties.Resources.burn32;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.Location = new System.Drawing.Point(224, 433);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 55);
            this.button2.TabIndex = 16;
            this.button2.Text = "Burn Table";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 496);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(484, 30);
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
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "bin-File|*.bin|csv-File|*.csv";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Title = "Import Partition Table";
            // 
            // button4
            // 
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button4.Image = global::esp_tools_gui.Properties.Resources.import32;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button4.Location = new System.Drawing.Point(12, 433);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 55);
            this.button4.TabIndex = 22;
            this.button4.Text = "Import Table";
            this.button4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Image = global::esp_tools_gui.Properties.Resources.add32;
            this.button5.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button5.Location = new System.Drawing.Point(330, 433);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(166, 55);
            this.button5.TabIndex = 23;
            this.button5.Text = "Add to Arduino IDE";
            this.button5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Location = new System.Drawing.Point(531, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(485, 513);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Partitions";
            // 
            // button6
            // 
            this.button6.Image = global::esp_tools_gui.Properties.Resources.add32;
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(332, 461);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(146, 46);
            this.button6.TabIndex = 24;
            this.button6.Text = "Add to Arduino IDE";
            this.button6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(79, 487);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(247, 20);
            this.textBox2.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 490);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Description:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(79, 461);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(97, 20);
            this.textBox1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 464);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Table name:";
            // 
            // treeView1
            // 
            this.treeView1.ImageIndex = 5;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Indent = 20;
            this.treeView1.ItemHeight = 16;
            this.treeView1.Location = new System.Drawing.Point(6, 19);
            this.treeView1.Name = "treeView1";
            treeNode1.ImageIndex = 4;
            treeNode1.Name = "Knoten2";
            treeNode1.Text = "Partition Name";
            treeNode2.Name = "Knoten0";
            treeNode2.Text = "Partitions";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(472, 436);
            this.treeView1.TabIndex = 1;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "add.png");
            this.imageList1.Images.SetKeyName(1, "burn.png");
            this.imageList1.Images.SetKeyName(2, "export.png");
            this.imageList1.Images.SetKeyName(3, "import.png");
            this.imageList1.Images.SetKeyName(4, "kchart.png");
            this.imageList1.Images.SetKeyName(5, "kde-folder-open.png");
            // 
            // trackBarOtaD
            // 
            this.trackBarOtaD.Enabled = false;
            this.trackBarOtaD.LargeChange = 1000;
            this.trackBarOtaD.Location = new System.Drawing.Point(90, 113);
            this.trackBarOtaD.Maximum = 128;
            this.trackBarOtaD.Name = "trackBarOtaD";
            this.trackBarOtaD.Size = new System.Drawing.Size(377, 45);
            this.trackBarOtaD.SmallChange = 4;
            this.trackBarOtaD.TabIndex = 25;
            this.trackBarOtaD.TickFrequency = 80;
            this.trackBarOtaD.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // checkBoxOtaD
            // 
            this.checkBoxOtaD.AutoSize = true;
            this.checkBoxOtaD.Checked = true;
            this.checkBoxOtaD.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOtaD.Enabled = false;
            this.checkBoxOtaD.Location = new System.Drawing.Point(12, 123);
            this.checkBoxOtaD.Name = "checkBoxOtaD";
            this.checkBoxOtaD.Size = new System.Drawing.Size(71, 17);
            this.checkBoxOtaD.TabIndex = 26;
            this.checkBoxOtaD.Text = "OTAData";
            this.checkBoxOtaD.UseVisualStyleBackColor = true;
            // 
            // labelOtaD
            // 
            this.labelOtaD.AutoSize = true;
            this.labelOtaD.Location = new System.Drawing.Point(465, 127);
            this.labelOtaD.Name = "labelOtaD";
            this.labelOtaD.Size = new System.Drawing.Size(31, 13);
            this.labelOtaD.TabIndex = 27;
            this.labelOtaD.Text = "100k";
            // 
            // labelFfat
            // 
            this.labelFfat.AutoSize = true;
            this.labelFfat.Location = new System.Drawing.Point(465, 330);
            this.labelFfat.Name = "labelFfat";
            this.labelFfat.Size = new System.Drawing.Size(31, 13);
            this.labelFfat.TabIndex = 30;
            this.labelFfat.Text = "100k";
            // 
            // checkBoxFfat
            // 
            this.checkBoxFfat.AutoSize = true;
            this.checkBoxFfat.Location = new System.Drawing.Point(12, 330);
            this.checkBoxFfat.Name = "checkBoxFfat";
            this.checkBoxFfat.Size = new System.Drawing.Size(52, 17);
            this.checkBoxFfat.TabIndex = 29;
            this.checkBoxFfat.Text = "FFAT";
            this.checkBoxFfat.UseVisualStyleBackColor = true;
            this.checkBoxFfat.CheckedChanged += new System.EventHandler(this.checkBoxFfat_CheckedChanged);
            // 
            // trackBarFfat
            // 
            this.trackBarFfat.Enabled = false;
            this.trackBarFfat.LargeChange = 1000;
            this.trackBarFfat.Location = new System.Drawing.Point(90, 317);
            this.trackBarFfat.Maximum = 4000;
            this.trackBarFfat.Name = "trackBarFfat";
            this.trackBarFfat.Size = new System.Drawing.Size(377, 45);
            this.trackBarFfat.SmallChange = 4;
            this.trackBarFfat.TabIndex = 28;
            this.trackBarFfat.TickFrequency = 80;
            this.trackBarFfat.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarFfat.ValueChanged += new System.EventHandler(this.trackBarFfat_ValueChanged);
            // 
            // PartTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 547);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelFfat);
            this.Controls.Add(this.checkBoxFfat);
            this.Controls.Add(this.trackBarFfat);
            this.Controls.Add(this.labelOtaD);
            this.Controls.Add(this.checkBoxOtaD);
            this.Controls.Add(this.trackBarOtaD);
            this.Controls.Add(this.button5);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOtaD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFfat)).EndInit();
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
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBarOtaD;
        private System.Windows.Forms.CheckBox checkBoxOtaD;
        private System.Windows.Forms.Label labelOtaD;
        private System.Windows.Forms.Label labelFfat;
        private System.Windows.Forms.CheckBox checkBoxFfat;
        private System.Windows.Forms.TrackBar trackBarFfat;
    }
}