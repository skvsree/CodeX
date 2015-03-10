namespace WindowsFormsUIHelper_test
{
    partial class Form1
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node2");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Node3");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Node4");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            this.btnProperties = new System.Windows.Forms.Button();
            this.cmbAnimation = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbForeColor = new System.Windows.Forms.ComboBox();
            this.cmbBackColor = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.nudAnimationSpeed = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.chkUACEnabled = new System.Windows.Forms.CheckBox();
            this.chkElevatedProcess = new System.Windows.Forms.CheckBox();
            this.chkApplyForCurrentForm = new System.Windows.Forms.CheckBox();
            this.btnShowConsole = new System.Windows.Forms.Button();
            this.cmbThemes = new System.Windows.Forms.ComboBox();
            this.cmbBorderColor = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.PersonName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Age = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Role = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.chkShowStatusBar = new System.Windows.Forms.CheckBox();
            this.chkShowStatusCountInICon = new System.Windows.Forms.CheckBox();
            this.btnShowDialog = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudAnimationSpeed)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnProperties
            // 
            this.btnProperties.Location = new System.Drawing.Point(363, 348);
            this.btnProperties.Name = "btnProperties";
            this.btnProperties.Size = new System.Drawing.Size(141, 43);
            this.btnProperties.TabIndex = 4;
            this.btnProperties.Text = "Show Test Form";
            this.btnProperties.UseVisualStyleBackColor = true;
            this.btnProperties.Click += new System.EventHandler(this.btnProperties_Click);
            // 
            // cmbAnimation
            // 
            this.cmbAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAnimation.FormattingEnabled = true;
            this.cmbAnimation.Location = new System.Drawing.Point(363, 111);
            this.cmbAnimation.Name = "cmbAnimation";
            this.cmbAnimation.Size = new System.Drawing.Size(148, 25);
            this.cmbAnimation.TabIndex = 5;
            this.cmbAnimation.SelectedIndexChanged += new System.EventHandler(this.cmbAnimation_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(265, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Animation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(261, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Fore Color";
            // 
            // cmbForeColor
            // 
            this.cmbForeColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbForeColor.FormattingEnabled = true;
            this.cmbForeColor.Location = new System.Drawing.Point(363, 158);
            this.cmbForeColor.Name = "cmbForeColor";
            this.cmbForeColor.Size = new System.Drawing.Size(148, 25);
            this.cmbForeColor.TabIndex = 8;
            this.cmbForeColor.SelectedIndexChanged += new System.EventHandler(this.cmbForeColor_SelectedIndexChanged);
            // 
            // cmbBackColor
            // 
            this.cmbBackColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBackColor.FormattingEnabled = true;
            this.cmbBackColor.Location = new System.Drawing.Point(363, 207);
            this.cmbBackColor.Name = "cmbBackColor";
            this.cmbBackColor.Size = new System.Drawing.Size(148, 25);
            this.cmbBackColor.TabIndex = 10;
            this.cmbBackColor.SelectedIndexChanged += new System.EventHandler(this.cmbBackColor_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(261, 211);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Back Color";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(293, 298);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Title";
            // 
            // txtTitle
            // 
            this.txtTitle.BackColor = System.Drawing.SystemColors.Window;
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTitle.Location = new System.Drawing.Point(363, 294);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(326, 25);
            this.txtTitle.TabIndex = 12;
            this.txtTitle.Text = "Death Star";
            // 
            // nudAnimationSpeed
            // 
            this.nudAnimationSpeed.Location = new System.Drawing.Point(658, 111);
            this.nudAnimationSpeed.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudAnimationSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAnimationSpeed.Name = "nudAnimationSpeed";
            this.nudAnimationSpeed.Size = new System.Drawing.Size(65, 25);
            this.nudAnimationSpeed.TabIndex = 13;
            this.nudAnimationSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAnimationSpeed.ValueChanged += new System.EventHandler(this.nudAnimationSpeed_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(544, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "Animation Speed";
            // 
            // chkUACEnabled
            // 
            this.chkUACEnabled.AutoSize = true;
            this.chkUACEnabled.ForeColor = System.Drawing.Color.White;
            this.chkUACEnabled.Location = new System.Drawing.Point(244, 491);
            this.chkUACEnabled.Name = "chkUACEnabled";
            this.chkUACEnabled.Size = new System.Drawing.Size(103, 21);
            this.chkUACEnabled.TabIndex = 15;
            this.chkUACEnabled.Text = "UAC Enabled";
            this.chkUACEnabled.UseVisualStyleBackColor = true;
            // 
            // chkElevatedProcess
            // 
            this.chkElevatedProcess.AutoSize = true;
            this.chkElevatedProcess.ForeColor = System.Drawing.Color.White;
            this.chkElevatedProcess.Location = new System.Drawing.Point(409, 491);
            this.chkElevatedProcess.Name = "chkElevatedProcess";
            this.chkElevatedProcess.Size = new System.Drawing.Size(125, 21);
            this.chkElevatedProcess.TabIndex = 16;
            this.chkElevatedProcess.Text = "Elevated Process";
            this.chkElevatedProcess.UseVisualStyleBackColor = true;
            // 
            // chkApplyForCurrentForm
            // 
            this.chkApplyForCurrentForm.AutoSize = true;
            this.chkApplyForCurrentForm.ForeColor = System.Drawing.Color.White;
            this.chkApplyForCurrentForm.Location = new System.Drawing.Point(244, 448);
            this.chkApplyForCurrentForm.Name = "chkApplyForCurrentForm";
            this.chkApplyForCurrentForm.Size = new System.Drawing.Size(158, 21);
            this.chkApplyForCurrentForm.TabIndex = 17;
            this.chkApplyForCurrentForm.Text = "Apply on current Form";
            this.chkApplyForCurrentForm.UseVisualStyleBackColor = true;
            this.chkApplyForCurrentForm.CheckedChanged += new System.EventHandler(this.chkApplyForCurrentForm_CheckedChanged);
            // 
            // btnShowConsole
            // 
            this.btnShowConsole.Location = new System.Drawing.Point(547, 348);
            this.btnShowConsole.Name = "btnShowConsole";
            this.btnShowConsole.Size = new System.Drawing.Size(141, 43);
            this.btnShowConsole.TabIndex = 18;
            this.btnShowConsole.Text = "Show Console";
            this.btnShowConsole.UseVisualStyleBackColor = true;
            this.btnShowConsole.Click += new System.EventHandler(this.btnShowConsole_Click);
            // 
            // cmbThemes
            // 
            this.cmbThemes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbThemes.FormattingEnabled = true;
            this.cmbThemes.Location = new System.Drawing.Point(12, 111);
            this.cmbThemes.Name = "cmbThemes";
            this.cmbThemes.Size = new System.Drawing.Size(241, 25);
            this.cmbThemes.TabIndex = 19;
            this.cmbThemes.SelectedIndexChanged += new System.EventHandler(this.cmbThemes_SelectedIndexChanged);
            // 
            // cmbBorderColor
            // 
            this.cmbBorderColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBorderColor.FormattingEnabled = true;
            this.cmbBorderColor.Location = new System.Drawing.Point(363, 253);
            this.cmbBorderColor.Name = "cmbBorderColor";
            this.cmbBorderColor.Size = new System.Drawing.Size(148, 25);
            this.cmbBorderColor.TabIndex = 21;
            this.cmbBorderColor.SelectedIndexChanged += new System.EventHandler(this.cmbBorderColor_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(247, 257);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 17);
            this.label6.TabIndex = 20;
            this.label6.Text = "Border Color";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(12, 91);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 17);
            this.label7.TabIndex = 22;
            this.label7.Text = "Theme";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(770, 111);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(711, 234);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PersonName,
            this.Age,
            this.Role});
            this.dataGridView1.Location = new System.Drawing.Point(26, 32);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(669, 183);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // PersonName
            // 
            this.PersonName.HeaderText = "Name";
            this.PersonName.Name = "PersonName";
            // 
            // Age
            // 
            this.Age.HeaderText = "Age";
            this.Age.Name = "Age";
            // 
            // Role
            // 
            this.Role.HeaderText = "Role";
            this.Role.Name = "Role";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 17;
            this.listBox1.Items.AddRange(new object[] {
            "test",
            "test1",
            "test2"});
            this.listBox1.Location = new System.Drawing.Point(595, 181);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 89);
            this.listBox1.TabIndex = 24;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(376, 65);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(45, 24);
            this.menuStrip1.TabIndex = 25;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(17, 160);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(213, 23);
            this.progressBar1.TabIndex = 26;
            this.progressBar1.Value = 50;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "one",
            "two",
            "three"});
            this.checkedListBox1.Location = new System.Drawing.Point(167, 277);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(120, 84);
            this.checkedListBox1.TabIndex = 27;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.Location = new System.Drawing.Point(521, 413);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(200, 100);
            this.tabControl1.TabIndex = 28;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(192, 70);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 70);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(17, 207);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node1";
            treeNode1.Text = "Node1";
            treeNode2.Name = "Node2";
            treeNode2.Text = "Node2";
            treeNode3.Name = "Node3";
            treeNode3.Text = "Node3";
            treeNode4.Name = "Node4";
            treeNode4.Text = "Node4";
            treeNode5.Name = "Node0";
            treeNode5.Text = "Node0";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5});
            this.treeView1.Size = new System.Drawing.Size(121, 201);
            this.treeView1.TabIndex = 29;
            // 
            // chkShowStatusBar
            // 
            this.chkShowStatusBar.AutoSize = true;
            this.chkShowStatusBar.ForeColor = System.Drawing.Color.White;
            this.chkShowStatusBar.Location = new System.Drawing.Point(916, 460);
            this.chkShowStatusBar.Name = "chkShowStatusBar";
            this.chkShowStatusBar.Size = new System.Drawing.Size(120, 21);
            this.chkShowStatusBar.TabIndex = 30;
            this.chkShowStatusBar.Text = "Show Status Bar";
            this.chkShowStatusBar.UseVisualStyleBackColor = true;
            this.chkShowStatusBar.CheckedChanged += new System.EventHandler(this.chkShowStatusBar_CheckedChanged);
            // 
            // chkShowStatusCountInICon
            // 
            this.chkShowStatusCountInICon.AutoSize = true;
            this.chkShowStatusCountInICon.ForeColor = System.Drawing.Color.White;
            this.chkShowStatusCountInICon.Location = new System.Drawing.Point(1080, 460);
            this.chkShowStatusCountInICon.Name = "chkShowStatusCountInICon";
            this.chkShowStatusCountInICon.Size = new System.Drawing.Size(177, 21);
            this.chkShowStatusCountInICon.TabIndex = 31;
            this.chkShowStatusCountInICon.Text = "Show Status Count in Icon";
            this.chkShowStatusCountInICon.UseVisualStyleBackColor = true;
            this.chkShowStatusCountInICon.CheckedChanged += new System.EventHandler(this.chkShowStatusCountInICon_CheckedChanged);
            // 
            // btnShowDialog
            // 
            this.btnShowDialog.Location = new System.Drawing.Point(733, 348);
            this.btnShowDialog.Name = "btnShowDialog";
            this.btnShowDialog.Size = new System.Drawing.Size(141, 43);
            this.btnShowDialog.TabIndex = 32;
            this.btnShowDialog.Text = "Show Dialog";
            this.btnShowDialog.UseVisualStyleBackColor = true;
            this.btnShowDialog.Click += new System.EventHandler(this.btnShowDialog_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1543, 585);
            this.Controls.Add(this.btnShowDialog);
            this.Controls.Add(this.chkShowStatusCountInICon);
            this.Controls.Add(this.chkShowStatusBar);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbBorderColor);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbThemes);
            this.Controls.Add(this.btnShowConsole);
            this.Controls.Add(this.chkApplyForCurrentForm);
            this.Controls.Add(this.chkElevatedProcess);
            this.Controls.Add(this.chkUACEnabled);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudAnimationSpeed);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbBackColor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbForeColor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbAnimation);
            this.Controls.Add(this.btnProperties);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.ShowMaximizeButton = true;
            this.ShowStatusBar = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.Controls.SetChildIndex(this.menuStrip1, 0);
            this.Controls.SetChildIndex(this.btnProperties, 0);
            this.Controls.SetChildIndex(this.cmbAnimation, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.cmbForeColor, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cmbBackColor, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtTitle, 0);
            this.Controls.SetChildIndex(this.nudAnimationSpeed, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.chkUACEnabled, 0);
            this.Controls.SetChildIndex(this.chkElevatedProcess, 0);
            this.Controls.SetChildIndex(this.chkApplyForCurrentForm, 0);
            this.Controls.SetChildIndex(this.btnShowConsole, 0);
            this.Controls.SetChildIndex(this.cmbThemes, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.cmbBorderColor, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.listBox1, 0);
            this.Controls.SetChildIndex(this.progressBar1, 0);
            this.Controls.SetChildIndex(this.checkedListBox1, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.Controls.SetChildIndex(this.treeView1, 0);
            this.Controls.SetChildIndex(this.chkShowStatusBar, 0);
            this.Controls.SetChildIndex(this.chkShowStatusCountInICon, 0);
            this.Controls.SetChildIndex(this.btnShowDialog, 0);
            ((System.ComponentModel.ISupportInitialize)(this.nudAnimationSpeed)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnProperties;
        private System.Windows.Forms.ComboBox cmbAnimation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbForeColor;
        private System.Windows.Forms.ComboBox cmbBackColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.NumericUpDown nudAnimationSpeed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkUACEnabled;
        private System.Windows.Forms.CheckBox chkElevatedProcess;
        private System.Windows.Forms.CheckBox chkApplyForCurrentForm;
        private System.Windows.Forms.Button btnShowConsole;
        private System.Windows.Forms.ComboBox cmbThemes;
        private System.Windows.Forms.ComboBox cmbBorderColor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PersonName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Age;
        private System.Windows.Forms.DataGridViewTextBoxColumn Role;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.CheckBox chkShowStatusBar;
        private System.Windows.Forms.CheckBox chkShowStatusCountInICon;
        private System.Windows.Forms.Button btnShowDialog;
    }
}

