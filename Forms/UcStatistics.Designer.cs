namespace SBR.Forms
{
    partial class UcStatistics
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
            components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            lblStats = new Label();
            chartMonths = new System.Windows.Forms.DataVisualization.Charting.Chart();
            tmrMonthChart = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)chartMonths).BeginInit();
            SuspendLayout();
            // 
            // lblStats
            // 
            lblStats.AutoSize = true;
            lblStats.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblStats.Location = new Point(30, 20);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(278, 21);
            lblStats.TabIndex = 1;
            lblStats.Text = "*Daily averages for the last 14 months:";
            // 
            // chartMonths
            // 
            chartArea2.BackColor = Color.White;
            chartArea2.Name = "ChartArea1";
            chartMonths.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            chartMonths.Legends.Add(legend2);
            chartMonths.Location = new Point(7, 66);
            chartMonths.Name = "chartMonths";
            series4.ChartArea = "ChartArea1";
            series4.Color = Color.Red;
            series4.Legend = "Legend1";
            series4.Name = "Ignored breaks";
            series4.YValuesPerPoint = 2;
            series5.ChartArea = "ChartArea1";
            series5.Color = Color.DodgerBlue;
            series5.CustomProperties = "DrawSideBySide=True";
            series5.Legend = "Legend1";
            series5.Name = "Working time";
            series5.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series5.YValuesPerPoint = 2;
            series6.ChartArea = "ChartArea1";
            series6.Color = Color.DarkKhaki;
            series6.Legend = "Legend1";
            series6.Name = "Total time";
            series6.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            chartMonths.Series.Add(series4);
            chartMonths.Series.Add(series5);
            chartMonths.Series.Add(series6);
            chartMonths.Size = new Size(803, 398);
            chartMonths.TabIndex = 40;
            chartMonths.Text = "chart1";
            chartMonths.Enter += chartMonths_Enter;
            // 
            // tmrMonthChart
            // 
            tmrMonthChart.Interval = 10000;
            // 
            // UcStatistics
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(chartMonths);
            Controls.Add(lblStats);
            Name = "UcStatistics";
            Size = new Size(824, 509);
            ((System.ComponentModel.ISupportInitialize)chartMonths).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblStats;
        public System.Windows.Forms.DataVisualization.Charting.Chart chartMonths;
        private System.Windows.Forms.Timer tmrMonthChart;
    }
}
