namespace SBR.Forms
{
    partial class UcSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlAlarm = new Panel();
            nudAlarm4 = new NumericUpDown();
            nudAlarm3 = new NumericUpDown();
            nudAlarm2 = new NumericUpDown();
            nudAlarm1 = new NumericUpDown();
            lblAlarm4 = new Label();
            lblAlarm2 = new Label();
            lblAlarm3 = new Label();
            lblAlarm1 = new Label();
            pnlSettings = new Panel();
            chkPomoLongBreak = new CheckBox();
            rdoS3 = new RadioButton();
            rdoS2 = new RadioButton();
            rdoS1 = new RadioButton();
            chkPomodoro = new CheckBox();
            chkEmoticons = new CheckBox();
            chkPlaySound = new CheckBox();
            chkStartUp = new CheckBox();
            pnlColors = new Panel();
            rdoPurple = new RadioButton();
            pnlPurple = new Panel();
            rdoCustom = new RadioButton();
            rdoRandom = new RadioButton();
            rdoOrange = new RadioButton();
            pnlCustom = new Panel();
            lblCustom = new Label();
            pnlRandom = new Panel();
            pnlOrange = new Panel();
            rdoPink = new RadioButton();
            rdoYellow = new RadioButton();
            rdoGray = new RadioButton();
            pnlPink = new Panel();
            pnlYellow = new Panel();
            pnlGrey = new Panel();
            rdoBlue = new RadioButton();
            rdoGreen = new RadioButton();
            rdoRed = new RadioButton();
            pnlBlue = new Panel();
            pnlGreen = new Panel();
            pnlRed = new Panel();
            pnlAlarm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudAlarm4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudAlarm3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudAlarm2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudAlarm1).BeginInit();
            pnlSettings.SuspendLayout();
            pnlColors.SuspendLayout();
            pnlCustom.SuspendLayout();
            SuspendLayout();
            // 
            // pnlAlarm
            // 
            pnlAlarm.BorderStyle = BorderStyle.FixedSingle;
            pnlAlarm.Controls.Add(nudAlarm4);
            pnlAlarm.Controls.Add(nudAlarm3);
            pnlAlarm.Controls.Add(nudAlarm2);
            pnlAlarm.Controls.Add(nudAlarm1);
            pnlAlarm.Controls.Add(lblAlarm4);
            pnlAlarm.Controls.Add(lblAlarm2);
            pnlAlarm.Controls.Add(lblAlarm3);
            pnlAlarm.Controls.Add(lblAlarm1);
            pnlAlarm.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            pnlAlarm.Location = new Point(30, 30);
            pnlAlarm.Name = "pnlAlarm";
            pnlAlarm.Size = new Size(275, 181);
            pnlAlarm.TabIndex = 0;
            pnlAlarm.TabStop = true;
            // 
            // nudAlarm4
            // 
            nudAlarm4.Location = new Point(199, 140);
            nudAlarm4.Maximum = new decimal(new int[] { 55, 0, 0, 0 });
            nudAlarm4.Name = "nudAlarm4";
            nudAlarm4.Size = new Size(40, 25);
            nudAlarm4.TabIndex = 4;
            // 
            // nudAlarm3
            // 
            nudAlarm3.Location = new Point(199, 100);
            nudAlarm3.Maximum = new decimal(new int[] { 55, 0, 0, 0 });
            nudAlarm3.Name = "nudAlarm3";
            nudAlarm3.Size = new Size(40, 25);
            nudAlarm3.TabIndex = 3;
            // 
            // nudAlarm2
            // 
            nudAlarm2.Location = new Point(199, 60);
            nudAlarm2.Maximum = new decimal(new int[] { 55, 0, 0, 0 });
            nudAlarm2.Name = "nudAlarm2";
            nudAlarm2.Size = new Size(40, 25);
            nudAlarm2.TabIndex = 2;
            // 
            // nudAlarm1
            // 
            nudAlarm1.Location = new Point(199, 20);
            nudAlarm1.Maximum = new decimal(new int[] { 55, 0, 0, 0 });
            nudAlarm1.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            nudAlarm1.Name = "nudAlarm1";
            nudAlarm1.Size = new Size(40, 25);
            nudAlarm1.TabIndex = 1;
            nudAlarm1.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // lblAlarm4
            // 
            lblAlarm4.AutoSize = true;
            lblAlarm4.Location = new Point(23, 142);
            lblAlarm4.Name = "lblAlarm4";
            lblAlarm4.Size = new Size(133, 17);
            lblAlarm4.TabIndex = 0;
            lblAlarm4.Text = "***Alarm 4 time [min]:";
            // 
            // lblAlarm2
            // 
            lblAlarm2.AutoSize = true;
            lblAlarm2.Location = new Point(23, 62);
            lblAlarm2.Name = "lblAlarm2";
            lblAlarm2.Size = new Size(133, 17);
            lblAlarm2.TabIndex = 0;
            lblAlarm2.Text = "***Alarm 2 time [min]:";
            // 
            // lblAlarm3
            // 
            lblAlarm3.AutoSize = true;
            lblAlarm3.Location = new Point(23, 102);
            lblAlarm3.Name = "lblAlarm3";
            lblAlarm3.Size = new Size(133, 17);
            lblAlarm3.TabIndex = 0;
            lblAlarm3.Text = "***Alarm 3 time [min]:";
            // 
            // lblAlarm1
            // 
            lblAlarm1.AutoSize = true;
            lblAlarm1.Location = new Point(23, 22);
            lblAlarm1.Name = "lblAlarm1";
            lblAlarm1.Size = new Size(133, 17);
            lblAlarm1.TabIndex = 0;
            lblAlarm1.Text = "***Alarm 1 time [min]:";
            // 
            // pnlSettings
            // 
            pnlSettings.BorderStyle = BorderStyle.FixedSingle;
            pnlSettings.Controls.Add(chkPomoLongBreak);
            pnlSettings.Controls.Add(rdoS3);
            pnlSettings.Controls.Add(rdoS2);
            pnlSettings.Controls.Add(rdoS1);
            pnlSettings.Controls.Add(chkPomodoro);
            pnlSettings.Controls.Add(chkEmoticons);
            pnlSettings.Controls.Add(chkPlaySound);
            pnlSettings.Controls.Add(chkStartUp);
            pnlSettings.Location = new Point(330, 30);
            pnlSettings.Name = "pnlSettings";
            pnlSettings.Size = new Size(466, 181);
            pnlSettings.TabIndex = 1;
            pnlSettings.TabStop = true;
            // 
            // chkPomoLongBreak
            // 
            chkPomoLongBreak.AutoSize = true;
            chkPomoLongBreak.Location = new Point(180, 141);
            chkPomoLongBreak.Name = "chkPomoLongBreak";
            chkPomoLongBreak.Size = new Size(189, 21);
            chkPomoLongBreak.TabIndex = 8;
            chkPomoLongBreak.Text = "***Extra alert for long break";
            chkPomoLongBreak.UseVisualStyleBackColor = true;
            // 
            // rdoS3
            // 
            rdoS3.AutoSize = true;
            rdoS3.Location = new Point(290, 60);
            rdoS3.Name = "rdoS3";
            rdoS3.Size = new Size(40, 21);
            rdoS3.TabIndex = 7;
            rdoS3.TabStop = true;
            rdoS3.Text = "S3";
            rdoS3.UseVisualStyleBackColor = true;
            // 
            // rdoS2
            // 
            rdoS2.AutoSize = true;
            rdoS2.Location = new Point(241, 60);
            rdoS2.Name = "rdoS2";
            rdoS2.Size = new Size(40, 21);
            rdoS2.TabIndex = 6;
            rdoS2.TabStop = true;
            rdoS2.Text = "S2";
            rdoS2.UseVisualStyleBackColor = true;
            // 
            // rdoS1
            // 
            rdoS1.AutoSize = true;
            rdoS1.Location = new Point(189, 60);
            rdoS1.Name = "rdoS1";
            rdoS1.Size = new Size(40, 21);
            rdoS1.TabIndex = 5;
            rdoS1.TabStop = true;
            rdoS1.Text = "S1";
            rdoS1.UseVisualStyleBackColor = true;
            // 
            // chkPomodoro
            // 
            chkPomodoro.AutoSize = true;
            chkPomodoro.Location = new Point(21, 142);
            chkPomodoro.Name = "chkPomodoro";
            chkPomodoro.Size = new Size(128, 21);
            chkPomodoro.TabIndex = 4;
            chkPomodoro.Text = "***Pomodoro 4/4";
            chkPomodoro.UseVisualStyleBackColor = true;
            // 
            // chkEmoticons
            // 
            chkEmoticons.AutoSize = true;
            chkEmoticons.Location = new Point(21, 102);
            chkEmoticons.Name = "chkEmoticons";
            chkEmoticons.Size = new Size(146, 21);
            chkEmoticons.TabIndex = 3;
            chkEmoticons.Text = "***Emoticons  🙂🙁";
            chkEmoticons.UseVisualStyleBackColor = true;
            // 
            // chkPlaySound
            // 
            chkPlaySound.AutoSize = true;
            chkPlaySound.Location = new Point(21, 62);
            chkPlaySound.Name = "chkPlaySound";
            chkPlaySound.Size = new Size(116, 21);
            chkPlaySound.TabIndex = 2;
            chkPlaySound.Text = "***Sound alarm";
            chkPlaySound.UseVisualStyleBackColor = true;
            // 
            // chkStartUp
            // 
            chkStartUp.AutoSize = true;
            chkStartUp.BackColor = SystemColors.Control;
            chkStartUp.Location = new Point(21, 22);
            chkStartUp.Name = "chkStartUp";
            chkStartUp.Size = new Size(124, 21);
            chkStartUp.TabIndex = 1;
            chkStartUp.Text = "***Run at startup";
            chkStartUp.UseVisualStyleBackColor = false;
            // 
            // pnlColors
            // 
            pnlColors.BorderStyle = BorderStyle.FixedSingle;
            pnlColors.Controls.Add(rdoPurple);
            pnlColors.Controls.Add(pnlPurple);
            pnlColors.Controls.Add(rdoCustom);
            pnlColors.Controls.Add(rdoRandom);
            pnlColors.Controls.Add(rdoOrange);
            pnlColors.Controls.Add(pnlCustom);
            pnlColors.Controls.Add(pnlRandom);
            pnlColors.Controls.Add(pnlOrange);
            pnlColors.Controls.Add(rdoPink);
            pnlColors.Controls.Add(rdoYellow);
            pnlColors.Controls.Add(rdoGray);
            pnlColors.Controls.Add(pnlPink);
            pnlColors.Controls.Add(pnlYellow);
            pnlColors.Controls.Add(pnlGrey);
            pnlColors.Controls.Add(rdoBlue);
            pnlColors.Controls.Add(rdoGreen);
            pnlColors.Controls.Add(rdoRed);
            pnlColors.Controls.Add(pnlBlue);
            pnlColors.Controls.Add(pnlGreen);
            pnlColors.Controls.Add(pnlRed);
            pnlColors.Location = new Point(30, 237);
            pnlColors.Name = "pnlColors";
            pnlColors.Size = new Size(766, 174);
            pnlColors.TabIndex = 2;
            pnlColors.TabStop = true;
            // 
            // rdoPurple
            // 
            rdoPurple.AutoSize = true;
            rdoPurple.Location = new Point(163, 137);
            rdoPurple.Name = "rdoPurple";
            rdoPurple.Size = new Size(78, 21);
            rdoPurple.TabIndex = 17;
            rdoPurple.TabStop = true;
            rdoPurple.Text = "***Purple";
            rdoPurple.UseVisualStyleBackColor = true;
            // 
            // pnlPurple
            // 
            pnlPurple.BackColor = Color.FromArgb(224, 163, 224);
            pnlPurple.BorderStyle = BorderStyle.FixedSingle;
            pnlPurple.Location = new Point(163, 101);
            pnlPurple.Name = "pnlPurple";
            pnlPurple.Size = new Size(42, 30);
            pnlPurple.TabIndex = 18;
            // 
            // rdoCustom
            // 
            rdoCustom.AutoSize = true;
            rdoCustom.Location = new Point(580, 137);
            rdoCustom.Name = "rdoCustom";
            rdoCustom.Size = new Size(119, 21);
            rdoCustom.TabIndex = 8;
            rdoCustom.TabStop = true;
            rdoCustom.Text = "***Custom color";
            rdoCustom.UseVisualStyleBackColor = true;
            // 
            // rdoRandom
            // 
            rdoRandom.AutoSize = true;
            rdoRandom.Location = new Point(580, 58);
            rdoRandom.Name = "rdoRandom";
            rdoRandom.Size = new Size(124, 21);
            rdoRandom.TabIndex = 7;
            rdoRandom.TabStop = true;
            rdoRandom.Text = "***Random color";
            rdoRandom.UseVisualStyleBackColor = true;
            // 
            // rdoOrange
            // 
            rdoOrange.AutoSize = true;
            rdoOrange.Location = new Point(163, 58);
            rdoOrange.Name = "rdoOrange";
            rdoOrange.Size = new Size(85, 21);
            rdoOrange.TabIndex = 6;
            rdoOrange.TabStop = true;
            rdoOrange.Text = "***Orange";
            rdoOrange.UseVisualStyleBackColor = true;
            // 
            // pnlCustom
            // 
            pnlCustom.BackColor = Color.White;
            pnlCustom.BorderStyle = BorderStyle.FixedSingle;
            pnlCustom.Controls.Add(lblCustom);
            pnlCustom.Location = new Point(580, 100);
            pnlCustom.Name = "pnlCustom";
            pnlCustom.Size = new Size(42, 30);
            pnlCustom.TabIndex = 14;
            // 
            // lblCustom
            // 
            lblCustom.AutoSize = true;
            lblCustom.BackColor = Color.Transparent;
            lblCustom.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblCustom.Location = new Point(4, -14);
            lblCustom.Name = "lblCustom";
            lblCustom.Size = new Size(35, 37);
            lblCustom.TabIndex = 0;
            lblCustom.Text = "...";
            lblCustom.Click += lblCustom_Click;
            // 
            // pnlRandom
            // 
            pnlRandom.BackColor = Color.Transparent;
            pnlRandom.BackgroundImage = ResourcesIconsDir.ResourcesIcons.color_spectrum_icon;
            pnlRandom.BorderStyle = BorderStyle.FixedSingle;
            pnlRandom.Location = new Point(580, 22);
            pnlRandom.Name = "pnlRandom";
            pnlRandom.Size = new Size(42, 30);
            pnlRandom.TabIndex = 16;
            // 
            // pnlOrange
            // 
            pnlOrange.BackColor = Color.Orange;
            pnlOrange.BorderStyle = BorderStyle.FixedSingle;
            pnlOrange.Location = new Point(163, 22);
            pnlOrange.Name = "pnlOrange";
            pnlOrange.Size = new Size(42, 30);
            pnlOrange.TabIndex = 15;
            // 
            // rdoPink
            // 
            rdoPink.AutoSize = true;
            rdoPink.Location = new Point(24, 137);
            rdoPink.Name = "rdoPink";
            rdoPink.Size = new Size(64, 21);
            rdoPink.TabIndex = 5;
            rdoPink.TabStop = true;
            rdoPink.Text = "***Pink";
            rdoPink.UseVisualStyleBackColor = true;
            // 
            // rdoYellow
            // 
            rdoYellow.AutoSize = true;
            rdoYellow.Location = new Point(302, 58);
            rdoYellow.Name = "rdoYellow";
            rdoYellow.Size = new Size(77, 21);
            rdoYellow.TabIndex = 4;
            rdoYellow.TabStop = true;
            rdoYellow.Text = "***Yellow";
            rdoYellow.UseVisualStyleBackColor = true;
            // 
            // rdoGray
            // 
            rdoGray.AutoSize = true;
            rdoGray.Location = new Point(302, 137);
            rdoGray.Name = "rdoGray";
            rdoGray.Size = new Size(68, 21);
            rdoGray.TabIndex = 3;
            rdoGray.TabStop = true;
            rdoGray.Text = "***Gray";
            rdoGray.UseVisualStyleBackColor = true;
            // 
            // pnlPink
            // 
            pnlPink.BackColor = Color.FromArgb(255, 153, 163);
            pnlPink.BorderStyle = BorderStyle.FixedSingle;
            pnlPink.Location = new Point(24, 100);
            pnlPink.Name = "pnlPink";
            pnlPink.Size = new Size(42, 30);
            pnlPink.TabIndex = 14;
            // 
            // pnlYellow
            // 
            pnlYellow.BackColor = Color.Gold;
            pnlYellow.BorderStyle = BorderStyle.FixedSingle;
            pnlYellow.Location = new Point(302, 22);
            pnlYellow.Name = "pnlYellow";
            pnlYellow.Size = new Size(42, 30);
            pnlYellow.TabIndex = 13;
            // 
            // pnlGrey
            // 
            pnlGrey.BackColor = Color.DarkGray;
            pnlGrey.BorderStyle = BorderStyle.FixedSingle;
            pnlGrey.Location = new Point(302, 100);
            pnlGrey.Name = "pnlGrey";
            pnlGrey.Size = new Size(42, 30);
            pnlGrey.TabIndex = 12;
            // 
            // rdoBlue
            // 
            rdoBlue.AutoSize = true;
            rdoBlue.Location = new Point(441, 137);
            rdoBlue.Name = "rdoBlue";
            rdoBlue.Size = new Size(65, 21);
            rdoBlue.TabIndex = 2;
            rdoBlue.TabStop = true;
            rdoBlue.Text = "***Blue";
            rdoBlue.UseVisualStyleBackColor = true;
            // 
            // rdoGreen
            // 
            rdoGreen.AutoSize = true;
            rdoGreen.Location = new Point(441, 58);
            rdoGreen.Name = "rdoGreen";
            rdoGreen.Size = new Size(76, 21);
            rdoGreen.TabIndex = 1;
            rdoGreen.TabStop = true;
            rdoGreen.Text = "***Green";
            rdoGreen.UseVisualStyleBackColor = true;
            // 
            // rdoRed
            // 
            rdoRed.AutoSize = true;
            rdoRed.Location = new Point(24, 58);
            rdoRed.Name = "rdoRed";
            rdoRed.Size = new Size(64, 21);
            rdoRed.TabIndex = 0;
            rdoRed.TabStop = true;
            rdoRed.Text = "***Red";
            rdoRed.UseVisualStyleBackColor = true;
            // 
            // pnlBlue
            // 
            pnlBlue.BackColor = Color.FromArgb(139, 198, 252);
            pnlBlue.BorderStyle = BorderStyle.FixedSingle;
            pnlBlue.Location = new Point(441, 101);
            pnlBlue.Name = "pnlBlue";
            pnlBlue.Size = new Size(42, 30);
            pnlBlue.TabIndex = 11;
            // 
            // pnlGreen
            // 
            pnlGreen.BackColor = Color.FromArgb(119, 221, 119);
            pnlGreen.BorderStyle = BorderStyle.FixedSingle;
            pnlGreen.Location = new Point(441, 22);
            pnlGreen.Name = "pnlGreen";
            pnlGreen.Size = new Size(42, 30);
            pnlGreen.TabIndex = 10;
            // 
            // pnlRed
            // 
            pnlRed.BackColor = Color.FromArgb(238, 70, 90);
            pnlRed.BorderStyle = BorderStyle.FixedSingle;
            pnlRed.Location = new Point(24, 22);
            pnlRed.Name = "pnlRed";
            pnlRed.Size = new Size(42, 30);
            pnlRed.TabIndex = 9;
            // 
            // UcSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlColors);
            Controls.Add(pnlSettings);
            Controls.Add(pnlAlarm);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            Name = "UcSettings";
            Size = new Size(824, 509);
            pnlAlarm.ResumeLayout(false);
            pnlAlarm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudAlarm4).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudAlarm3).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudAlarm2).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudAlarm1).EndInit();
            pnlSettings.ResumeLayout(false);
            pnlSettings.PerformLayout();
            pnlColors.ResumeLayout(false);
            pnlColors.PerformLayout();
            pnlCustom.ResumeLayout(false);
            pnlCustom.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private NumericUpDown nudAlarm1;
        private Label lblAlarm4;
        private Label lblAlarm2;
        private Label lblAlarm3;
        private Label lblAlarm1;
        private NumericUpDown nudAlarm4;
        private NumericUpDown nudAlarm3;
        private NumericUpDown nudAlarm2;
        private CheckBox chkPomodoro;
        private CheckBox chkEmoticons;
        private CheckBox chkPlaySound;
        private CheckBox chkStartUp;
        private Panel pnlRed;
        private RadioButton rdoBlue;
        private RadioButton rdoGreen;
        private RadioButton rdoRed;
        private Panel pnlBlue;
        private Panel pnlGreen;
        private RadioButton rdoCustom;
        private RadioButton rdoOrange;
        private Panel pnlCustom;
        private Panel pnlRandom;
        private Panel pnlOrange;
        private RadioButton rdoPink;
        private RadioButton rdoYellow;
        private RadioButton rdoGray;
        private Panel pnlPink;
        private Panel pnlYellow;
        private Panel pnlGrey;
        private Label lblCustom;
        public RadioButton rdoRandom;
        private RadioButton rdoS3;
        private RadioButton rdoS2;
        private RadioButton rdoS1;
        private RadioButton rdoPurple;
        private Panel pnlPurple;
        public Panel pnlAlarm;
        public Panel pnlSettings;
        public Panel pnlColors;
        private CheckBox chkPomoLongBreak;
    }
}
