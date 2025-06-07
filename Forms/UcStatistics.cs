using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SBR.Forms
{
    public partial class UcStatistics : UserControl
    {
        public UcStatistics()
        {
            InitializeComponent();

            MonthChartInit();
            MonthChartUpdate();

            chartMonths.GetToolTipText += MonthChart_GetToolTipText;
            tmrMonthChart.Tick += tmrMonthChart_Tick;
            tmrMonthChart.Enabled = true; // Start the timer to update the chart every 10 minutes.
        }


        // ********************************************************************************************************************
        // ** Public Methods:
        // ********************************************************************************************************************

        public void ChangeLanguage()
        {

            // List of strings which are in current User Control (Windows Form) and which we want to change to different language.
            // There is such method in each User Control (Windows Form) which is called from LangChanger static class.
            lblStats.Text = LangChanger.GetString("Daily averages for the last 14 months:");

            chartMonths.Series["Total time"].LegendText = LangChanger.GetString("Total time");
            chartMonths.Series["Working time"].LegendText = LangChanger.GetString("Working time");
            chartMonths.Series["Ignored breaks"].LegendText = LangChanger.GetString("Ignored breaks");

            chartMonths.ChartAreas["ChartArea1"].AxisY.Title = LangChanger.GetString("Ignored breaks [-]");
            chartMonths.ChartAreas["ChartArea1"].AxisY2.Title = LangChanger.GetString("Working time, total time [hours]");
        }


        // ********************************************************************************************************************
        // ** Private Methods:
        // ********************************************************************************************************************


        // Update the chart data every 10 minutes.
        private void tmrMonthChart_Tick(object? sender, EventArgs e)
        {
            MonthChartUpdate();
        }

        /// <summary>
        /// Initialize the chart design and settings.
        /// </summary>
        private void MonthChartInit()
        {
            // Adjust the design
            chartMonths.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            chartMonths.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;

            chartMonths.ChartAreas["ChartArea1"].AxisY.Title = "***Ignored breaks [-]"; // Chart Y Axis Title 
            chartMonths.ChartAreas["ChartArea1"].AxisY.TitleAlignment = StringAlignment.Center;  // Chart Y axis Text 
            chartMonths.ChartAreas["ChartArea1"].AxisY.TextOrientation =TextOrientation.Rotated270;
            chartMonths.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;

            chartMonths.ChartAreas["ChartArea1"].AxisY2.Title = "***Working time, total time [hours]"; // Chart Y Axis Title 
            chartMonths.ChartAreas["ChartArea1"].AxisY2.TitleAlignment = StringAlignment.Center;  // Chart Y axis Text 
            chartMonths.ChartAreas["ChartArea1"].AxisY2.TextOrientation =TextOrientation.Rotated270;
            chartMonths.ChartAreas["ChartArea1"].AxisY2.MajorGrid.LineColor = Color.Gainsboro;

            // Set chart types
            chartMonths.Series["Ignored breaks"].ChartType = SeriesChartType.StackedColumn;
            chartMonths.Series["Total time"].ChartType = SeriesChartType.StackedColumn;
            chartMonths.Series["Working time"].ChartType = SeriesChartType.StackedColumn;

            // Set colors
            chartMonths.Series["Ignored breaks"].Color = Color.FromArgb(238, 70, 90); // Red
            chartMonths.Series["Total time"].Color = Color.DarkGray;                  // Gray
            chartMonths.Series["Working time"].Color = ColorTranslator.FromHtml("#0180C0");      // 

            // Set axes
            chartMonths.Series["Ignored breaks"].XAxisType = AxisType.Primary;
            chartMonths.Series["Ignored breaks"].YAxisType = AxisType.Primary;
            chartMonths.Series["Total time"].XAxisType = AxisType.Primary;
            chartMonths.Series["Total time"].YAxisType = AxisType.Secondary;
            chartMonths.Series["Working time"].XAxisType = AxisType.Primary;
            chartMonths.Series["Working time"].YAxisType = AxisType.Secondary;

            // Set x-axis label format
            chartMonths.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "MM-dd";

            // Rename legend entries
            chartMonths.Series["Ignored breaks"].LegendText = "***Ignored Breaks";
            chartMonths.Series["Total time"].LegendText = "***Total Time";
            chartMonths.Series["Working time"].LegendText = "***Working Time";


        }

        /// <summary>
        /// Update the chart data.
        /// </summary>
        private void MonthChartUpdate()
        {
            foreach (var series in chartMonths.Series)
            {
                series.Points.Clear();           // Clear existing series.
                series.IsXValueIndexed = true;   // Ensure x-values are indexed to only display data points - otherwise there would be also tomorrow's day in the chart.
                series.ToolTip = "#VALY";        // Display the y-value of the data point
            }


            // Variable to track the maximum value of "Ignored breaks"
            // It doesn't look nice if the Ignored breaks (red bar) is accross whole chart height. 
            // With this trick we are reducing Y-axis for Ignored break to 25% of the chart height.
            double maxIgnoredBreaks = 0;

            // Add data to the chart.
            int monthCount = MainForm.MainFormInstance.cData.Months.Count-1;
            for (int i = monthCount; i >= 0; i--)
            {
                var month = MainForm.MainFormInstance.cData.Months[i];
                DateTime xMonthValue = DateTime.ParseExact(month.MonthDate, "yyyy-MM", CultureInfo.InvariantCulture);

                // Format xMonthValue as "MM-yyyy" for the x-axis
                string sxMonthValue = xMonthValue.ToString("yyyy-MM");
                
                chartMonths.Series["Ignored breaks"].Points.AddXY(sxMonthValue, (float)month.MonthIgnoredBreaks);
                // Because I have no idea how to display "Total time" behind the "Working time", I am using this trick:
                // I am diplaying "IdleTime" in "Total time" series. (Total time is the sum of working time and idle time)
                // For this reason we also have to recalculate MonthChart_GetToolTipTextt() method.
                chartMonths.Series["Total time"].Points.AddXY(sxMonthValue, (double)month.MonthIdleTime / 3600);
                chartMonths.Series["Working time"].Points.AddXY(sxMonthValue, (double)month.MonthWorkingTime / 3600);

                // (Trick to reduce Y-axis) Track the maximum value of "Ignored breaks".
                if (month.MonthIgnoredBreaks > maxIgnoredBreaks)
                {
                    maxIgnoredBreaks = month.MonthIgnoredBreaks;
                }
            }

            // (Trick to reduce Y-axis) Calculate the maximum Y-axis value to ensure it is at most 40% of the chart height.
            double chartHeight = chartMonths.ChartAreas["ChartArea1"].AxisY.ScaleView.Size; // Get the chart height.
            double adjustedMaxY = maxIgnoredBreaks / 0.25; // Ensure maxIgnoredBreaks is at most 20% of the chart height.

            // (Trick to reduce Y-axis) Set the Y-axis maximum value.
            chartMonths.ChartAreas["ChartArea1"].AxisY.Maximum = Math.Max(maxIgnoredBreaks, adjustedMaxY);


            // Refresh the chart
            chartMonths.Invalidate();
            chartMonths.Update();
        }

        /// <summary>
        /// Get tooltip text for the chart data points.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonthChart_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            if (e.HitTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                var dataPoint = e.HitTestResult.Series.Points[e.HitTestResult.PointIndex];
                var seriesName = e.HitTestResult.Series.Name;
                var xValue = dataPoint.AxisLabel; 
                string yValue;

                // Format yValue based on the series
                if (seriesName == "Total time")
                {
                    // Sum "Total time" and "Working time" for yValue. This is because of the trick in MonthChartUpdates().
                    double totalTime = chartMonths.Series["Total time"].Points[e.HitTestResult.PointIndex].YValues[0];
                    double workingTime = chartMonths.Series["Working time"].Points[e.HitTestResult.PointIndex].YValues[0];
                    yValue = TimeSpan.FromHours(totalTime + workingTime).ToString(@"hh\:mm"); // Format as hh:mm
                }
                else if (seriesName == "Working time")
                {
                    yValue = TimeSpan.FromHours(dataPoint.YValues[0]).ToString(@"hh\:mm"); // Format as hh:mm
                }

                else if (seriesName == "Ignored breaks")
                {
                    yValue = dataPoint.YValues[0].ToString("0.00"); // Format as a float number
                }
                else
                {
                    yValue = dataPoint.YValues[0].ToString(); // Default formatting
                }

                // Set the tooltip text
                e.Text = $"{xValue}, {yValue}";
            }
        }

        private void chartMonths_Enter(object sender, EventArgs e)
        {
            // Trick how to get rid of the focus rectangle around the chart.
            lblStats.Focus();
        }
    }
}
