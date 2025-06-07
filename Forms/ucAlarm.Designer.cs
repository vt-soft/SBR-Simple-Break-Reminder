using Microsoft.Win32;

namespace SBR.Forms
{
    partial class UcAlarm
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
            if (disposing)
            {
                // Unsubscribe from events here. This method was not generated (updated) by the designer!!!
                SystemEvents.PowerModeChanged -= SystemEvents_PowerModeChanged;

                if (components != null)
                {
                    components.Dispose();
                }
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
            components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            btnPomodoro = new Button();
            btnAlarmType = new Button();
            lblAlarmTime = new Label();
            lblAverageStats = new Label();
            lblIgnoredBreaks = new Label();
            lblWorkingTime = new Label();
            lblABreaks = new Label();
            lblATime = new Label();
            lblABreaksEmo = new Label();
            lblATimeEmo = new Label();
            btnSoundOff = new Button();
            tmrMainTimer = new System.Windows.Forms.Timer(components);
            btnAlarmReset = new Button();
            btnBreakYes = new Button();
            btnBreakNo = new Button();
            tmrActiveBreakButtons = new System.Windows.Forms.Timer(components);
            tmrDisableSoundButton = new System.Windows.Forms.Timer(components);
            lblStats3 = new Label();
            rtxStats = new RichTextBox();
            tipAverageStats = new ToolTip(components);
            tmrUpdateCharts = new System.Windows.Forms.Timer(components);
            chartAlarm = new System.Windows.Forms.DataVisualization.Charting.Chart();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)chartAlarm).BeginInit();
            SuspendLayout();
            // 
            // btnPomodoro
            // 
            btnPomodoro.BackColor = SystemColors.ButtonHighlight;
            btnPomodoro.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
            btnPomodoro.Location = new Point(30, 80);
            btnPomodoro.Name = "btnPomodoro";
            btnPomodoro.Size = new Size(42, 42);
            btnPomodoro.TabIndex = 6;
            btnPomodoro.Text = "1/4";
            btnPomodoro.UseVisualStyleBackColor = false;
            // 
            // btnAlarmType
            // 
            btnAlarmType.BackColor = SystemColors.ButtonHighlight;
            btnAlarmType.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
            btnAlarmType.Location = new Point(30, 30);
            btnAlarmType.Name = "btnAlarmType";
            btnAlarmType.Size = new Size(42, 42);
            btnAlarmType.TabIndex = 7;
            btnAlarmType.Text = "A1";
            btnAlarmType.UseVisualStyleBackColor = false;
            // 
            // lblAlarmTime
            // 
            lblAlarmTime.AutoSize = true;
            lblAlarmTime.Font = new Font("Microsoft Sans Serif", 72F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblAlarmTime.Location = new Point(76, 24);
            lblAlarmTime.Name = "lblAlarmTime";
            lblAlarmTime.Size = new Size(284, 108);
            lblAlarmTime.TabIndex = 8;
            lblAlarmTime.Text = "00:00";
            // 
            // lblAverageStats
            // 
            lblAverageStats.Anchor = AnchorStyles.Right;
            lblAverageStats.AutoSize = true;
            lblAverageStats.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblAverageStats.Location = new Point(675, 34);
            lblAverageStats.Name = "lblAverageStats";
            lblAverageStats.Size = new Size(105, 17);
            lblAverageStats.TabIndex = 9;
            lblAverageStats.Text = "***Average stats:";
            tipAverageStats.SetToolTip(lblAverageStats, "Daily averages for the last 10 days. Except today.");
            lblAverageStats.SizeChanged += lblAverageStats_SizeChanged;
            // 
            // lblIgnoredBreaks
            // 
            lblIgnoredBreaks.AutoSize = true;
            lblIgnoredBreaks.Font = new Font("Segoe UI", 9.75F);
            lblIgnoredBreaks.Location = new Point(541, 67);
            lblIgnoredBreaks.Name = "lblIgnoredBreaks";
            lblIgnoredBreaks.Size = new Size(115, 17);
            lblIgnoredBreaks.TabIndex = 10;
            lblIgnoredBreaks.Text = "***Ignored breaks:";
            // 
            // lblWorkingTime
            // 
            lblWorkingTime.AutoSize = true;
            lblWorkingTime.Font = new Font("Segoe UI", 9.75F);
            lblWorkingTime.Location = new Point(541, 100);
            lblWorkingTime.Name = "lblWorkingTime";
            lblWorkingTime.Size = new Size(103, 17);
            lblWorkingTime.TabIndex = 11;
            lblWorkingTime.Text = "***Wokring time:";
            // 
            // lblABreaks
            // 
            lblABreaks.AutoSize = true;
            lblABreaks.Font = new Font("Segoe UI", 9.75F);
            lblABreaks.Location = new Point(696, 67);
            lblABreaks.Name = "lblABreaks";
            lblABreaks.Size = new Size(32, 17);
            lblABreaks.TabIndex = 12;
            lblABreaks.Text = "2.55";
            // 
            // lblATime
            // 
            lblATime.AutoSize = true;
            lblATime.Font = new Font("Segoe UI", 9.75F);
            lblATime.Location = new Point(696, 100);
            lblATime.Name = "lblATime";
            lblATime.Size = new Size(56, 17);
            lblATime.TabIndex = 13;
            lblATime.Text = "08:40:20";
            // 
            // lblABreaksEmo
            // 
            lblABreaksEmo.AutoSize = true;
            lblABreaksEmo.Font = new Font("Segoe UI", 15.75F);
            lblABreaksEmo.Location = new Point(756, 59);
            lblABreaksEmo.Name = "lblABreaksEmo";
            lblABreaksEmo.Size = new Size(24, 30);
            lblABreaksEmo.TabIndex = 14;
            lblABreaksEmo.Text = ":)";
            // 
            // lblATimeEmo
            // 
            lblATimeEmo.AutoSize = true;
            lblATimeEmo.Font = new Font("Segoe UI", 15.75F);
            lblATimeEmo.Location = new Point(756, 92);
            lblATimeEmo.Name = "lblATimeEmo";
            lblATimeEmo.Size = new Size(24, 30);
            lblATimeEmo.TabIndex = 15;
            lblATimeEmo.Text = ":)";
            // 
            // btnSoundOff
            // 
            btnSoundOff.BackColor = SystemColors.ButtonHighlight;
            btnSoundOff.Enabled = false;
            btnSoundOff.Image = ResourcesIconsDir.ResourcesIcons.s1;
            btnSoundOff.Location = new Point(541, 30);
            btnSoundOff.Name = "btnSoundOff";
            btnSoundOff.Size = new Size(62, 26);
            btnSoundOff.TabIndex = 16;
            btnSoundOff.UseVisualStyleBackColor = false;
            // 
            // tmrMainTimer
            // 
            tmrMainTimer.Interval = 1000;
            // 
            // btnAlarmReset
            // 
            btnAlarmReset.BackColor = SystemColors.ButtonHighlight;
            btnAlarmReset.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAlarmReset.ForeColor = SystemColors.ControlText;
            btnAlarmReset.Location = new Point(362, 30);
            btnAlarmReset.Name = "btnAlarmReset";
            btnAlarmReset.Size = new Size(157, 26);
            btnAlarmReset.TabIndex = 23;
            btnAlarmReset.Text = "***Reset alarm";
            btnAlarmReset.UseVisualStyleBackColor = false;
            // 
            // btnBreakYes
            // 
            btnBreakYes.BackColor = SystemColors.ControlLight;
            btnBreakYes.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBreakYes.Location = new Point(362, 63);
            btnBreakYes.Name = "btnBreakYes";
            btnBreakYes.Size = new Size(157, 26);
            btnBreakYes.TabIndex = 24;
            btnBreakYes.Text = "***Break YES";
            btnBreakYes.UseVisualStyleBackColor = false;
            // 
            // btnBreakNo
            // 
            btnBreakNo.BackColor = SystemColors.ButtonHighlight;
            btnBreakNo.Location = new Point(362, 96);
            btnBreakNo.Name = "btnBreakNo";
            btnBreakNo.Padding = new Padding(0, 0, 0, 2);
            btnBreakNo.Size = new Size(157, 26);
            btnBreakNo.TabIndex = 25;
            btnBreakNo.Text = "***Break NO";
            btnBreakNo.UseVisualStyleBackColor = false;
            // 
            // tmrActiveBreakButtons
            // 
            tmrActiveBreakButtons.Interval = 180000;
            // 
            // tmrDisableSoundButton
            // 
            tmrDisableSoundButton.Interval = 16000;
            // 
            // lblStats3
            // 
            lblStats3.AutoSize = true;
            lblStats3.Location = new Point(298, 165);
            lblStats3.Name = "lblStats3";
            lblStats3.Size = new Size(0, 17);
            lblStats3.TabIndex = 37;
            // 
            // rtxStats
            // 
            rtxStats.BorderStyle = BorderStyle.None;
            rtxStats.Location = new Point(30, 140);
            rtxStats.Multiline = false;
            rtxStats.Name = "rtxStats";
            rtxStats.ReadOnly = true;
            rtxStats.ScrollBars = RichTextBoxScrollBars.None;
            rtxStats.Size = new Size(777, 29);
            rtxStats.TabIndex = 38;
            rtxStats.TabStop = false;
            rtxStats.Text = "***Today's statistics :   Ignored breaks:  4    Total time 10:38:04    Working time 08:32:55    Iddle time 02:05:22";
            rtxStats.Enter += rtxStats_Enter;
            // 
            // tipAverageStats
            // 
            tipAverageStats.IsBalloon = true;
            // 
            // tmrUpdateCharts
            // 
            tmrUpdateCharts.Enabled = true;
            tmrUpdateCharts.Interval = 60000;
            // 
            // chartAlarm
            // 
            chartArea1.BackColor = Color.White;
            chartArea1.Name = "ChartArea1";
            chartAlarm.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartAlarm.Legends.Add(legend1);
            chartAlarm.Location = new Point(10, 184);
            chartAlarm.Name = "chartAlarm";
            series1.ChartArea = "ChartArea1";
            series1.Color = Color.Red;
            series1.Legend = "Legend1";
            series1.Name = "Ignored breaks";
            series1.YValuesPerPoint = 2;
            series2.ChartArea = "ChartArea1";
            series2.Color = Color.DodgerBlue;
            series2.CustomProperties = "DrawSideBySide=True";
            series2.Legend = "Legend1";
            series2.Name = "Working time";
            series2.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series2.YValuesPerPoint = 2;
            series3.ChartArea = "ChartArea1";
            series3.Color = Color.DarkKhaki;
            series3.Legend = "Legend1";
            series3.Name = "Total time";
            series3.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            chartAlarm.Series.Add(series1);
            chartAlarm.Series.Add(series2);
            chartAlarm.Series.Add(series3);
            chartAlarm.Size = new Size(777, 294);
            chartAlarm.TabIndex = 39;
            chartAlarm.Text = "chart1";
            chartAlarm.Enter += chartAlarm_Enter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(742, 485);
            label1.Name = "label1";
            label1.Size = new Size(43, 17);
            label1.TabIndex = 40;
            label1.Text = "label1";
            // 
            // UcAlarm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label1);
            Controls.Add(chartAlarm);
            Controls.Add(rtxStats);
            Controls.Add(lblStats3);
            Controls.Add(btnBreakNo);
            Controls.Add(btnBreakYes);
            Controls.Add(btnAlarmReset);
            Controls.Add(btnSoundOff);
            Controls.Add(lblATimeEmo);
            Controls.Add(lblABreaksEmo);
            Controls.Add(lblATime);
            Controls.Add(lblABreaks);
            Controls.Add(lblWorkingTime);
            Controls.Add(lblIgnoredBreaks);
            Controls.Add(lblAverageStats);
            Controls.Add(lblAlarmTime);
            Controls.Add(btnAlarmType);
            Controls.Add(btnPomodoro);
            Font = new Font("Segoe UI", 9.75F);
            Name = "UcAlarm";
            Size = new Size(824, 509);
            ((System.ComponentModel.ISupportInitialize)chartAlarm).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ListBox listBox1;
        private Button btnPomodoro;
        private Button btnAlarmType;
        private Label lblAlarmTime;
        private Label lblAverageStats;
        private Label lblIgnoredBreaks;
        private Label lblWorkingTime;
        private Label lblABreaks;
        private Label lblATime;
        private Label lblABreaksEmo;
        private Label lblATimeEmo;
        private Button btnSoundOff;
        private System.Windows.Forms.Timer tmrMainTimer;
        private Button btnAlarmReset;
        private Button btnBreakYes;
        private Button btnBreakNo;
        private System.Windows.Forms.Timer tmrActiveBreakButtons;
        private System.Windows.Forms.Timer tmrDisableSoundButton;
        private Label lblStats3;
        private RichTextBox rtxStats;
        private ToolTip tipAverageStats;
        private System.Windows.Forms.Timer tmrUpdateCharts;
        public System.Windows.Forms.DataVisualization.Charting.Chart chartAlarm;
        private Label label1;
    }
}
