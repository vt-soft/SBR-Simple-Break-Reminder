namespace SBR
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            pnlMenu = new Panel();
            btnToDo = new Button();
            btnChangeDayMode = new Button();
            pnlVerticalStrip2 = new Panel();
            pnlVerticalStrip = new Panel();
            btnAbout = new Button();
            btnLanguage = new Button();
            btnStatistics = new Button();
            btnSettings = new Button();
            btnAlarm = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            pnlHorizontalStrip = new Panel();
            pnlFormLoader = new Panel();
            tmrSaveData = new System.Windows.Forms.Timer(components);
            pnlMenu.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = Color.FromArgb(41, 39, 40);
            pnlMenu.Controls.Add(btnToDo);
            pnlMenu.Controls.Add(btnChangeDayMode);
            pnlMenu.Controls.Add(pnlVerticalStrip2);
            pnlMenu.Controls.Add(pnlVerticalStrip);
            pnlMenu.Controls.Add(btnAbout);
            pnlMenu.Controls.Add(btnLanguage);
            pnlMenu.Controls.Add(btnStatistics);
            pnlMenu.Controls.Add(btnSettings);
            pnlMenu.Controls.Add(btnAlarm);
            pnlMenu.Controls.Add(flowLayoutPanel1);
            pnlMenu.Dock = DockStyle.Left;
            pnlMenu.Location = new Point(0, 0);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(183, 516);
            pnlMenu.TabIndex = 0;
            // 
            // btnToDo
            // 
            btnToDo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnToDo.FlatAppearance.BorderSize = 0;
            btnToDo.FlatStyle = FlatStyle.Flat;
            btnToDo.Font = new Font("Nirmala UI", 12.75F);
            btnToDo.ForeColor = Color.White;
            btnToDo.Image = ResourcesIconsDir.ResourcesIcons.tasklist_icon;
            btnToDo.ImageAlign = ContentAlignment.MiddleLeft;
            btnToDo.Location = new Point(12, 127);
            btnToDo.Name = "btnToDo";
            btnToDo.Size = new Size(173, 45);
            btnToDo.TabIndex = 10;
            btnToDo.Text = "*** ToDo (16)";
            btnToDo.TextAlign = ContentAlignment.MiddleLeft;
            btnToDo.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnToDo.UseVisualStyleBackColor = true;
            btnToDo.Click += NavBarButton_Click;
            btnToDo.MouseEnter += NavBarButton_MouseEnter;
            btnToDo.MouseLeave += NavBarButton_MouseLeave;
            // 
            // btnChangeDayMode
            // 
            btnChangeDayMode.Anchor = AnchorStyles.None;
            btnChangeDayMode.FlatStyle = FlatStyle.Flat;
            btnChangeDayMode.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnChangeDayMode.ForeColor = Color.White;
            btnChangeDayMode.Image = ResourcesIconsDir.ResourcesIcons.moon_icon;
            btnChangeDayMode.Location = new Point(19, 470);
            btnChangeDayMode.Name = "btnChangeDayMode";
            btnChangeDayMode.Size = new Size(25, 25);
            btnChangeDayMode.TabIndex = 9;
            btnChangeDayMode.UseVisualStyleBackColor = true;
            btnChangeDayMode.Click += btnChangeDayMode_Click;
            // 
            // pnlVerticalStrip2
            // 
            pnlVerticalStrip2.BackColor = Color.WhiteSmoke;
            pnlVerticalStrip2.ForeColor = SystemColors.ActiveBorder;
            pnlVerticalStrip2.Location = new Point(0, 354);
            pnlVerticalStrip2.Name = "pnlVerticalStrip2";
            pnlVerticalStrip2.Size = new Size(12, 45);
            pnlVerticalStrip2.TabIndex = 8;
            pnlVerticalStrip2.Visible = false;
            // 
            // pnlVerticalStrip
            // 
            pnlVerticalStrip.BackColor = Color.Red;
            pnlVerticalStrip.ForeColor = SystemColors.ActiveBorder;
            pnlVerticalStrip.Location = new Point(0, 303);
            pnlVerticalStrip.Name = "pnlVerticalStrip";
            pnlVerticalStrip.Size = new Size(12, 45);
            pnlVerticalStrip.TabIndex = 7;
            // 
            // btnAbout
            // 
            btnAbout.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnAbout.FlatAppearance.BorderSize = 0;
            btnAbout.FlatStyle = FlatStyle.Flat;
            btnAbout.Font = new Font("Nirmala UI", 12.75F);
            btnAbout.ForeColor = Color.White;
            btnAbout.Image = ResourcesIconsDir.ResourcesIcons.about_icon;
            btnAbout.ImageAlign = ContentAlignment.MiddleLeft;
            btnAbout.Location = new Point(12, 307);
            btnAbout.Name = "btnAbout";
            btnAbout.Size = new Size(173, 45);
            btnAbout.TabIndex = 6;
            btnAbout.Text = "  ***About";
            btnAbout.TextAlign = ContentAlignment.MiddleLeft;
            btnAbout.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAbout.UseVisualStyleBackColor = true;
            btnAbout.Click += NavBarButton_Click;
            btnAbout.MouseEnter += NavBarButton_MouseEnter;
            btnAbout.MouseLeave += NavBarButton_MouseLeave;
            // 
            // btnLanguage
            // 
            btnLanguage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnLanguage.FlatAppearance.BorderSize = 0;
            btnLanguage.FlatStyle = FlatStyle.Flat;
            btnLanguage.Font = new Font("Nirmala UI", 12.75F);
            btnLanguage.ForeColor = Color.White;
            btnLanguage.Image = ResourcesIconsDir.ResourcesIcons.language_icon2;
            btnLanguage.ImageAlign = ContentAlignment.MiddleLeft;
            btnLanguage.Location = new Point(12, 262);
            btnLanguage.Name = "btnLanguage";
            btnLanguage.Size = new Size(173, 45);
            btnLanguage.TabIndex = 5;
            btnLanguage.Text = "  ***Language";
            btnLanguage.TextAlign = ContentAlignment.MiddleLeft;
            btnLanguage.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnLanguage.UseVisualStyleBackColor = true;
            btnLanguage.Click += NavBarButton_Click;
            btnLanguage.MouseEnter += NavBarButton_MouseEnter;
            btnLanguage.MouseLeave += NavBarButton_MouseLeave;
            // 
            // btnStatistics
            // 
            btnStatistics.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnStatistics.FlatAppearance.BorderSize = 0;
            btnStatistics.FlatStyle = FlatStyle.Flat;
            btnStatistics.Font = new Font("Nirmala UI", 12.75F);
            btnStatistics.ForeColor = Color.White;
            btnStatistics.Image = ResourcesIconsDir.ResourcesIcons.chart_icon;
            btnStatistics.ImageAlign = ContentAlignment.MiddleLeft;
            btnStatistics.Location = new Point(12, 217);
            btnStatistics.Name = "btnStatistics";
            btnStatistics.Size = new Size(173, 45);
            btnStatistics.TabIndex = 4;
            btnStatistics.Text = "  ***Statistics";
            btnStatistics.TextAlign = ContentAlignment.MiddleLeft;
            btnStatistics.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnStatistics.UseVisualStyleBackColor = true;
            btnStatistics.Click += NavBarButton_Click;
            btnStatistics.MouseEnter += NavBarButton_MouseEnter;
            btnStatistics.MouseLeave += NavBarButton_MouseLeave;
            // 
            // btnSettings
            // 
            btnSettings.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.Font = new Font("Nirmala UI", 12.75F);
            btnSettings.ForeColor = Color.White;
            btnSettings.Image = ResourcesIconsDir.ResourcesIcons.settings_icon;
            btnSettings.ImageAlign = ContentAlignment.MiddleLeft;
            btnSettings.Location = new Point(12, 172);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(173, 45);
            btnSettings.TabIndex = 3;
            btnSettings.Text = "  ***Settings";
            btnSettings.TextAlign = ContentAlignment.MiddleLeft;
            btnSettings.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += NavBarButton_Click;
            btnSettings.MouseEnter += NavBarButton_MouseEnter;
            btnSettings.MouseLeave += NavBarButton_MouseLeave;
            // 
            // btnAlarm
            // 
            btnAlarm.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnAlarm.BackColor = Color.FromArgb(41, 31, 40);
            btnAlarm.FlatAppearance.BorderSize = 0;
            btnAlarm.FlatStyle = FlatStyle.Flat;
            btnAlarm.Font = new Font("Nirmala UI", 12.75F);
            btnAlarm.ForeColor = Color.White;
            btnAlarm.Image = ResourcesIconsDir.ResourcesIcons.timer_icon;
            btnAlarm.ImageAlign = ContentAlignment.MiddleLeft;
            btnAlarm.Location = new Point(12, 82);
            btnAlarm.Name = "btnAlarm";
            btnAlarm.Size = new Size(173, 45);
            btnAlarm.TabIndex = 2;
            btnAlarm.Text = "  ***Alarm";
            btnAlarm.TextAlign = ContentAlignment.MiddleLeft;
            btnAlarm.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAlarm.UseVisualStyleBackColor = false;
            btnAlarm.Click += NavBarButton_Click;
            btnAlarm.MouseEnter += NavBarButton_MouseEnter;
            btnAlarm.MouseLeave += NavBarButton_MouseLeave;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(10, 9, 0, 0);
            flowLayoutPanel1.Size = new Size(183, 82);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // pnlHorizontalStrip
            // 
            pnlHorizontalStrip.Dock = DockStyle.Top;
            pnlHorizontalStrip.Location = new Point(183, 0);
            pnlHorizontalStrip.Name = "pnlHorizontalStrip";
            pnlHorizontalStrip.Size = new Size(824, 12);
            pnlHorizontalStrip.TabIndex = 1;
            // 
            // pnlFormLoader
            // 
            pnlFormLoader.Dock = DockStyle.Fill;
            pnlFormLoader.Location = new Point(183, 12);
            pnlFormLoader.Name = "pnlFormLoader";
            pnlFormLoader.Size = new Size(824, 504);
            pnlFormLoader.TabIndex = 2;
            // 
            // tmrSaveData
            // 
            tmrSaveData.Enabled = true;
            tmrSaveData.Interval = 600000;
            tmrSaveData.Tick += tmrSaveData_Tick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1007, 516);
            Controls.Add(pnlFormLoader);
            Controls.Add(pnlHorizontalStrip);
            Controls.Add(pnlMenu);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SBR - Simple Break Reminder, v 1.00";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            pnlMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMenu;
        private FlowLayoutPanel flowLayoutPanel1;
        private Panel pnlFormLoader;
        private Panel pnlVerticalStrip2;
        internal Button btnChangeDayMode;
        private System.Windows.Forms.Timer tmrSaveData;
        public Panel pnlHorizontalStrip;
        public Panel pnlVerticalStrip;
        public Button btnAbout;
        public Button btnLanguage;
        public Button btnStatistics;
        public Button btnSettings;
        public Button btnAlarm;
        public Button btnToDo;
    }

}
