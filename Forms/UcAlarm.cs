using Microsoft.Win32;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.Design;


namespace SBR.Forms
{
    public partial class UcAlarm : UserControl
    {

        // ********************************************************************************************************************

        private UcSettings ucSettings;

        private Alarm myAlarm, myAlarmTotalTime, myAlarmIdleTime;
        private int selectedAlarm;
        private int alarmTimeSec;

        private string alarmLabel;
        private string totalTimeLabel;
        private string breaksLabel;
        string workingTimeLabel = "0";
        string idleTimeLabel = "0";

        private bool idleTimeOn = false;
        private bool isMidnight = false;
        private bool osSuspended = false; // PC is going to sleep

        private DateTime osSuspendedStartTime;
        private DateTime osSuspendedEndTime;

        private SoundPlayer soundPlayer;

        private string sTodayStats, sIgnoredBreaks, sTotalTime, sWorkingTime, sIdleTime, sPomodoroTitle, sPomodoroText;

        public UcAlarm()
        {
            InitializeComponent();
            FormInit();
            EventHandlersInit();
            AlarmChartInit();
        }


        // ********************************************************************************************************************
        // ** Public Methods:
        // ********************************************************************************************************************

        /// <summary>
        /// Subscribe to the event from UcSettings.
        /// </summary>
        /// <param name="settings"></param>
        public void SubscribeToEvents(UcSettings settings)
        {
            settings.AlarmNumericUpDownValuesChanged += (sender, e) => AdjustAndRunAlarm();
            settings.AlarmCheckPomodoroChanged += (sender, e) => CheckPomodoro();
            settings.AlarmCheckEmoticonsChanged += (sender, e) => UpdateAndShowAverageStats();

            ucSettings = settings; // This is for another purpose, not for the event purpose
        }

        /// <summary>
        /// Method for changing strings in current User Control (Windows Form) to proper (selected) language.
        /// </summary>
        public void ChangeLanguage()
        {

            // List of strings which are in current User Control (Windows Form) and which we want to change to different language.
            // This method ChangeLanguage() in each User Control (Windows Form) and is called from LangChanger static class.
            btnAlarmReset.Text = LangChanger.GetString("Reset alarm");
            btnBreakYes.Text = LangChanger.GetString("Break YES");
            btnBreakNo.Text = LangChanger.GetString("Break NO");
            lblAverageStats.Text = LangChanger.GetString("Average stats:");

            lblIgnoredBreaks.Text = LangChanger.GetString("Ignored breaks") + ":";
            lblWorkingTime.Text = LangChanger.GetString("Working time") + ":";

            sTodayStats = LangChanger.GetString("Today's stats:");
            sIgnoredBreaks = LangChanger.GetString("Ignored breaks");
            sTotalTime = LangChanger.GetString("Total time");
            sWorkingTime = LangChanger.GetString("Working time");
            sIdleTime = LangChanger.GetString("Idle time");

            sPomodoroTitle = LangChanger.GetString("Pomodoro 4/4");
            sPomodoroText = LangChanger.GetString("Pomodoro 4/4. Take a long break.");

            tipAverageStats.SetToolTip(lblAverageStats, LangChanger.GetString("Daily averages for the last 10 days. Except today."));

            // Chart strings:
            chartAlarm.Series["Ignored breaks"].LegendText = sIgnoredBreaks;
            chartAlarm.Series["Total time"].LegendText = sTotalTime;
            chartAlarm.Series["Working time"].LegendText = sWorkingTime;
            chartAlarm.ChartAreas["ChartArea1"].AxisY.Title = LangChanger.GetString("Ignored breaks [-]");
            chartAlarm.ChartAreas["ChartArea1"].AxisY2.Title = LangChanger.GetString("Working time, total time [hours]");



        }


        // ********************************************************************************************************************
        // ** Private Methods:
        // ********************************************************************************************************************


        private void FormInit()
        {
            selectedAlarm = MainForm.MainFormInstance.cData.SelectedAlarm;
            tmrMainTimer.Enabled = true;

            myAlarm = new Alarm();
            AdjustAndRunAlarm();

            SetButtonsAlarmOff();
            CheckPomodoro();

            // Set and lunch other alamrs
            myAlarmTotalTime = new Alarm(); // for counting total_time
            myAlarmTotalTime.Offset = MainForm.MainFormInstance.cData.Days[0].DayTotalTime; // offset is in fact the value from the json file
            myAlarmTotalTime.Restart();

            myAlarmIdleTime = new Alarm(); // for counting idle_time
            myAlarmIdleTime.Offset = MainForm.MainFormInstance.cData.Days[0].DayIdleTime; // offset is in fact the value from the json file
        
            chartAlarm.TabStop = false; // Disable tab stop on the chart control
        }
        private void EventHandlersInit()
        {
            // This is for detection of the system power mode (sleep/wake up).
            // When closing the app we have to unsubscribe this event in Dispose method which is in ucAlarm.Designer.cs.
            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;

            tmrMainTimer.Tick += TmrMainTimer_Tick;
            tmrActiveBreakButtons.Tick += TmrActiveBreakButtons_Tick;
            tmrDisableSoundButton.Tick += TmrDisableSoundButton_Tick;
            tmrUpdateCharts.Tick += TmrUpdateCharts_Tick;

            btnAlarmReset.Click += BtnAlarmReset_Click;
            btnBreakYes.Click += BtnBreakYesOrNo_Click;
            btnBreakNo.Click += BtnBreakYesOrNo_Click;

            btnAlarmType.Click += BtnAlarmType_Click;
            btnPomodoro.Click += BtnPomodoro_Click;
            btnSoundOff.Click += BtnSoundOff_Click;

            btnAlarmReset.EnabledChanged += BtnEnabledChanged;
            btnBreakYes.EnabledChanged += BtnEnabledChanged;
            btnBreakNo.EnabledChanged += BtnEnabledChanged;
            btnSoundOff.EnabledChanged += BtnEnabledChanged;
            btnAlarmType.EnabledChanged += BtnEnabledChanged;

            chartAlarm.GetToolTipText += AlarmChart_GetToolTipText;

            this.Load += UcAlarm_Load;

        }

        private void UcAlarm_Load(object sender, EventArgs e)
        {
            TmrMainTimer_Tick(null, EventArgs.Empty);
            AlarmChartUpdate();
        }

        private void BtnEnabledChanged(object? sender, EventArgs e)
        {
            MainForm.MainFormInstance.ButtonEnabledChanged(sender, e);
        }


        /// <summary>
        /// Click the button to select and run another alarm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAlarmType_Click(object? sender, EventArgs e)
        {
            selectedAlarm = (selectedAlarm % 4) + 1; // Select another alarm. Cycle through 1 to 4.
            AdjustAndRunAlarm();
        }

        /// <summary>
        /// This method will:
        /// - update btnAlarmType.Text
        /// - update alarmTimeSec
        /// - update cData.SelectedAlarm
        /// - and finally, will run (reset) the alarm
        /// based on selectedAlarm value.
        /// </summary>
        private void AdjustAndRunAlarm()
        {
            if (selectedAlarm == 1)
            {
                btnAlarmType.Text = "A1";
                alarmTimeSec = MainForm.MainFormInstance.cData.AlarmTime1 * 60;
                MainForm.MainFormInstance.cData.SelectedAlarm = 1;
            }
            if (selectedAlarm == 2)
            {
                // In case Alarm2 is not set (AlarmTime=0), we will skip it and will go to the next alarm. Same for Alarm3 and Alarm4.
                if (MainForm.MainFormInstance.cData.AlarmTime2 == 0)
                {
                    selectedAlarm++;
                }
                else
                {
                    btnAlarmType.Text = "A2";
                    alarmTimeSec = MainForm.MainFormInstance.cData.AlarmTime2 * 60;
                    MainForm.MainFormInstance.cData.SelectedAlarm = 2;
                }
            }
            if (selectedAlarm == 3)
            {
                if (MainForm.MainFormInstance.cData.AlarmTime3 == 0)
                {
                    selectedAlarm++;
                }
                else
                {
                    btnAlarmType.Text = "A3";
                    alarmTimeSec = MainForm.MainFormInstance.cData.AlarmTime3 * 60;
                    MainForm.MainFormInstance.cData.SelectedAlarm = 3;
                }
            }
            if (selectedAlarm == 4)
            {
                if (MainForm.MainFormInstance.cData.AlarmTime4 == 0)
                {
                    selectedAlarm = 1;
                    btnAlarmType.Text = "A1";
                    alarmTimeSec = MainForm.MainFormInstance.cData.AlarmTime1 * 60;
                    MainForm.MainFormInstance.cData.SelectedAlarm = 1;
                }
                else
                {
                    btnAlarmType.Text = "A4";
                    alarmTimeSec = MainForm.MainFormInstance.cData.AlarmTime4 * 60;
                    MainForm.MainFormInstance.cData.SelectedAlarm = 4;
                }
            }
            myAlarm.AlarmTimeSec = alarmTimeSec;
            myAlarm.Restart();

            // Becasue this timer is called every 1 sec, we need to run it manually now, otherwise there will be almost 1 sec delay when changing alarm type (A1-A4)
            if (myAlarm != null && myAlarmTotalTime != null)
            {
                TmrMainTimer_Tick(null, EventArgs.Empty);
            }


        }

        /// <summary>
        /// This method will be called when the time is up.
        /// </summary>
        private void TimeIsUp()
        {
            MainForm.TimeIsUp = true;

            myAlarm.Reset();

            BringMainWindowToFront();

            if (MainForm.MainFormInstance.cData.PlaySound)
            {
                PlaySound();
            }


            if (MainForm.MainFormInstance.cData.Pomodoro)
            {
                MainForm.PomoCounter++;
                CheckPomodoro();
            }

            tmrActiveBreakButtons.Enabled = true;
            SetButtonsAlarmOn();
        }

        /// <summary>
        /// If user forget to press BreakYes or BreakNo, this timer will bring the main application window to the foreground.
        /// Every 3 miutes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmrActiveBreakButtons_Tick(object? sender, EventArgs e)
        {

            // Handle of the main application window.
            IntPtr mainWindowHandle = MainForm.MainFormInstance.Handle;

            // Retrieve the handle of the currently active window.
            IntPtr foregroundWindow = GetForegroundWindow();

            if (foregroundWindow != mainWindowHandle)
            {
                BringMainWindowToFront();
            }
        }


        // ********************************************************************************************************************
        // ** tmrMainTimer and "its" methods

        /// <summary>
        /// Timer for dispalying alarms. Updating statistics and so on. Calling every 1 second.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmrMainTimer_Tick(object? sender, EventArgs e)
        {
            UpdateAndShowTtWtIt();
            UpdateAndShowMainAlarm();
            IdleTimeDetection();
            MidnightDetection();
            if (myAlarmTotalTime.ElapsedSec > 5) UpdateAndShowTodaysStats();

            // There is no need to run this method so often, however because to ensure the color of emtoticon is updated correctly we have not other choice here :(
            UpdateAndShowAverageStats();
        }

        /// <summary>
        /// Update and show TotalTime WorkingTime and IdleTime labels.
        /// </summary>
        private void UpdateAndShowTtWtIt()
        {

            // Ignored  Breaks Label:
            breaksLabel = (MainForm.MainFormInstance.cData.Days[0].DayIgnoredBreaks).ToString();

            // Total time Label:
            int tTime = (int)myAlarmTotalTime.ElapsedSec;
            MainForm.MainFormInstance.cData.Days[0].DayTotalTime = tTime;
            totalTimeLabel = string.Format("{0:hh\\:mm\\:ss}", TimeSpan.FromSeconds(tTime));

            // Idle time Label:
            int iTime = (int)myAlarmIdleTime.ElapsedSec;
            MainForm.MainFormInstance.cData.Days[0].DayIdleTime = iTime;
            idleTimeLabel = string.Format("{0:hh\\:mm\\:ss}", TimeSpan.FromSeconds(iTime));

            // Working time = Total time - Idle time :  
            int wTime = tTime - iTime;
            MainForm.MainFormInstance.cData.Days[0].DayWorkingTime = wTime;

            // Because there are troubles (+/-1 sec) when subtracting two alarms, so we are updating this label only if idleTime is not ON. 
            if (!idleTimeOn)
            {
                if (wTime <= 1)
                {
                    wTime = 0;
                }
                workingTimeLabel = string.Format("{0:hh\\:mm\\:ss}", TimeSpan.FromSeconds(wTime));

            }
        }

        /// <summary>
        /// Update the colors of mainAlarm and display the alarm value on the form.
        /// </summary>
        private void UpdateAndShowMainAlarm()
        {
            // This if statement will prevent "reseting" label alarm value when TimeIsUp.
            if (myAlarm.IsRunning)
            {
                alarmLabel = string.Format("{0:mm\\:ss}", TimeSpan.FromSeconds(myAlarm.RemainingSec));
                lblAlarmTime.Text = alarmLabel;
                MainForm.MainFormInstance.Text = "SBR - Simple Break Reminder  " + alarmLabel;

                if (myAlarm.RemainingSec <= 0 && !osSuspended)
                {
                    lblAlarmTime.Text = "00:00";
                    TimeIsUp();
                    return;
                }

                // For color purposes:
                if (myAlarm.RemainingSec > 60 && myAlarm.RemainingSec <= 300)
                {
                    lblAlarmTime.ForeColor = Color.FromArgb(255, 165, 0); // orange
                }
                else if (myAlarm.RemainingSec <= 60)
                {
                    lblAlarmTime.ForeColor = Color.FromArgb(238, 70, 90); // red
                }
                else
                {
                    if (MainForm.MainFormInstance.DarkMode)
                    {
                        lblAlarmTime.ForeColor = Color.WhiteSmoke;
                    }
                    else
                    {
                        lblAlarmTime.ForeColor = Color.Black;
                    }
                }
            }
        }

        /// <summary>
        /// Check if the idle time is detected. If yes, then run alarm 
        /// </summary>
        private void IdleTimeDetection()
        {
            if (!osSuspended)
            {
                // Start measuring idle time after 600 seconds of inactivity.
                const int idleTimeLimit = 600;

                // Idle time detected
                if (!idleTimeOn && SBR.IdleTimeDetection.GetIdleTime() > idleTimeLimit)
                {

                    Debug.Print(DateTime.Now.ToString() + "   I1: Idle time start: " + SBR.IdleTimeDetection.GetIdleTime());

                    // Time difference between the current time and the midnight time.
                    TimeSpan timeDifference = DateTime.Now - new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                    Debug.Print("Time difference: " + timeDifference.ToString());
                    Debug.Print("Time difference: " + timeDifference.TotalSeconds);

                    if (timeDifference.TotalMinutes <= 10)
                    {
                        myAlarmIdleTime.Offset += (int)timeDifference.TotalSeconds;
                        Debug.Print("Pod deset minut");
                    }
                    else
                    {
                        myAlarmIdleTime.Offset += idleTimeLimit;
                        Debug.Print("Nad deset minut");
                    }

                    myAlarmIdleTime.Start();

                    UpdateAndShowTtWtIt();
                    AlarmChartUpdate();

                    // It is necessary to have this command (idleTimeOn = true) here at the bottom. 
                    // Once idleTimeOn = true, updating of workingTimeLabel will be skipped in UpdateAndShowTtWtIt().
                    idleTimeOn = true;
                }

                // Back in work :)
                if (idleTimeOn && SBR.IdleTimeDetection.GetIdleTime() < 4)
                {
                    idleTimeOn = false;
                    myAlarmIdleTime.Stop();
                    Debug.Print(DateTime.Now.ToString() + "   I2: Idle time stop:   " + SBR.IdleTimeDetection.GetIdleTime());
                }
            }
        }

        /// <summary>
        /// This method will be called when the system power mode is changed. When PC is going to sleep or wake up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            // Suspended time is not included in any statistics

            if (e.Mode == PowerModes.Suspend) // PC is entering sleep mode.
            {
                osSuspended = true;
                osSuspendedStartTime = DateTime.Now;

                myAlarmTotalTime.Stop();
                myAlarmIdleTime.Stop(); // Just in case IdleTime is already counting.

                Debug.Print(DateTime.Now.ToString() + "   S1: PC is going to sleep");
                Debug.Print(DateTime.Now.ToString() + "   S1: Idle time:" + SBR.IdleTimeDetection.GetIdleTime());
            }
            else if (e.Mode == PowerModes.Resume) // PC is resuming from sleep mode.
            {
                osSuspended = false;
                osSuspendedEndTime = DateTime.Now;
                TimeSpan sleepDuration = osSuspendedEndTime - osSuspendedStartTime;

                idleTimeOn = true; // This will prevent adding any other offset to idle time in case IdleTimeDetection.GetIdleTime() is still > 600

                if (osSuspendedStartTime.Date != osSuspendedEndTime.Date) // PC was sleeping thru midnight
                {
                    SaveAndLoadData();
                    Debug.Print("S2: PC was sleeping for more than 24 hours. Data will be saved and loaded again.");
                    Debug.Print("S2: sStartTime: " + osSuspendedStartTime.ToString() + "  sEndTime: " + osSuspendedEndTime.ToString());
                }

                Debug.Print(DateTime.Now.ToString() + "   S2: PC has resumed from sleep: ");
                Debug.Print(DateTime.Now.ToString() + "   S2: PC was sleeping for: " + sleepDuration.ToString(@"mm\:ss") + " (mm:ss)");
                Debug.Print(DateTime.Now.ToString() + "   S2: Idle time:" + SBR.IdleTimeDetection.GetIdleTime());

                myAlarmTotalTime.Start();
                myAlarm.Restart();

                // Just in case Resume mode is executed two times in a row (Without corresponding Suspend mode).
                // This is what already happened to me: Suspend - Resume - Resume
                osSuspendedStartTime = DateTime.Now;

            }
        }



        /// <summary>
        /// If midnight is detected, then save the data to the json file and reset the idle time and total time and load the json file again.
        /// </summary>
        private void MidnightDetection()
        {
            // Midnight is detected.
            if (!isMidnight && DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
            {
                Debug.Print(DateTime.Now.ToString() + "   MidnightDetection()---------------------");
                isMidnight = true;
                SaveAndLoadData();

            }

            if (isMidnight && DateTime.Now.Hour != 0 && DateTime.Now.Minute != 0)
            {
                isMidnight = false;
            }
        }

        /// <summary>
        /// Call this method when Midnight is detected or if the PC was sleeping during the Midnight.
        /// </summary>
        private void SaveAndLoadData()
        {

            Debug.Print(DateTime.Now.ToString() + "   SaveAndLoadData() - IdleTimeAlarm Status: " + myAlarmIdleTime.IsRunning);

            // FileSave method  will also run UpdateMonthlyChartData mehtod. So data for the monthly chart will be updated.
            JsonMethods.FileSaveConfig(MainForm.MainFormInstance.cData);

            // Loading the json file again will adjust the correct day date.
            MainForm.MainFormInstance.cData = JsonMethods.FileLoadConfig();

            // Reset/restart the alarms:
            myAlarmIdleTime.Offset = 0;

            if (myAlarmIdleTime.IsRunning)
            {
                myAlarmIdleTime.Restart();
            }
            else
            {
                myAlarmIdleTime.Reset();
            }

            myAlarmTotalTime.Offset = 0;
            myAlarmTotalTime.Restart();


            UpdateAndShowAverageStats(); // No need to run this method more often.
            AlarmChartUpdate();
            UpdateAndShowTtWtIt();

            if (idleTimeOn) // If  idleTimeOn=true then workingTimeLabel will be not updated in UpdateAndShowTtWtIt(), so we have to "reset" it here.
            {
                workingTimeLabel = "00:00:00";
                Debug.Print(DateTime.Now.ToString() + "   Reseting WT to 00:00:00");
            }

        }


        /// <summary>
        /// Update and show TODAYS statistics.
        /// </summary>
        private void UpdateAndShowTodaysStats()
        {
            // Color of DayIgnoredBreaks depends on the number of this value.
            Color ingoredBreaksColor;
            if (MainForm.MainFormInstance.cData.Days[0].DayIgnoredBreaks > 3)
            {
                ingoredBreaksColor = Color.FromArgb(238, 70, 90);  // Red
            }
            else if (MainForm.MainFormInstance.cData.Days[0].DayIgnoredBreaks > 2)
            {
                ingoredBreaksColor = Color.FromArgb(255, 165, 0); // Orange
            }
            else
            {
                // Green color
                ingoredBreaksColor = Color.MediumSeaGreen;
            }

            // Color of DayWorkingTime depends on the number of this value.
            Color dayWorkingTimeColor;
            if (MainForm.MainFormInstance.cData.Days[0].DayWorkingTime >= 32400) // >=9 hours
            {
                dayWorkingTimeColor = Color.FromArgb(238, 70, 90);
            }
            else if (MainForm.MainFormInstance.cData.Days[0].DayWorkingTime >= 30600) // >=8.5 hours  
            {
                dayWorkingTimeColor = Color.FromArgb(255, 165, 0);
            }
            else
            {
                // Green color
                dayWorkingTimeColor = Color.MediumSeaGreen;
            }


            rtxStats.Clear();
            rtxStats.SelectionFont = new Font(rtxStats.Font, FontStyle.Regular);
            rtxStats.AppendText(sTodayStats + "   " + sIgnoredBreaks + ": ");

            rtxStats.SelectionFont = new Font(rtxStats.Font.FontFamily, 12, FontStyle.Bold); // Bold and color font for ignored breaks
            rtxStats.SelectionColor = ingoredBreaksColor;
            rtxStats.AppendText(breaksLabel);

            rtxStats.SelectionFont = new Font(rtxStats.Font, FontStyle.Regular);
            rtxStats.SelectionColor = rtxStats.ForeColor;
            rtxStats.AppendText($"   " + sTotalTime + $": {totalTimeLabel}   " + sWorkingTime + ": ");


            rtxStats.SelectionFont = new Font(rtxStats.Font.FontFamily, 12, FontStyle.Bold); // Bold and color font for working time
            rtxStats.SelectionColor = dayWorkingTimeColor;
            rtxStats.AppendText(workingTimeLabel);

            rtxStats.SelectionFont = new Font(rtxStats.Font, FontStyle.Regular);
            rtxStats.SelectionColor = rtxStats.ForeColor;
            rtxStats.AppendText($"   " + sIdleTime + $": {idleTimeLabel}");

        }

        // ********************************************************************************************************************
        // ** TmrUpdateCharts and "its" methods

        /// <summary>
        /// Timer for updating charts and average statistics. We are calling it every minute.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void TmrUpdateCharts_Tick(object? sender, EventArgs e)
        {
            AlarmChartUpdate();
        }

        private void UpdateAndShowAverageStats()
        {
            // Data for the top-rigt corner of the app.

            Double aBreaks = 0, aTime = 0;
            int numberOfDays = 9;

            // This is to count average values correctly. If there are no data for some day then decreasing numberOfDays by one day.
            for (int i = 1; i <= 9; i++)
            {
                if (MainForm.MainFormInstance.cData.Days[i].DayWorkingTime == 0)
                    numberOfDays--;
            }

            if (numberOfDays != 0)
            {
                // Average number of breaks within last 9 days (excluding today, so index is starting at 1)
                for (int i = 1; i <= 9; i++)
                {
                    aBreaks = aBreaks + MainForm.MainFormInstance.cData.Days[i].DayIgnoredBreaks;
                }
                aBreaks = aBreaks / numberOfDays;
                lblABreaks.Text = string.Format("{0:0.##}", aBreaks);

                // Average number of working time within last 9 days (excluding today, so index is starting at 1)
                for (int i = 1; i <= 9; i++)
                {
                    aTime = aTime + MainForm.MainFormInstance.cData.Days[i].DayWorkingTime;
                }
                aTime = aTime / numberOfDays;
                lblATime.Text = string.Format("{0:hh\\:mm\\:ss}", TimeSpan.FromSeconds(aTime));


                if (MainForm.MainFormInstance.cData.Emoticons)
                {
                    // emoticons - breaks
                    if (aBreaks > 3) // bad emoticon
                    {
                        lblABreaksEmo.Text = "🙁";
                        lblABreaksEmo.ForeColor = Color.FromArgb(238, 70, 90);
                    }
                    else if (aBreaks > 2) // neutral emoticon
                    {
                        lblABreaksEmo.Text = "😐";
                        lblABreaksEmo.ForeColor = Color.FromArgb(255, 165, 0);
                    }
                    else  // good emoticon
                    {
                        lblABreaksEmo.Text = "🙂";
                        lblABreaksEmo.ForeColor = Color.MediumSeaGreen;
                    }

                    // emoticons - time
                    if (aTime >= 32400) // bad emoticon > 9.5 hours
                    {
                        lblATimeEmo.Text = "🙁";
                        lblATimeEmo.ForeColor = Color.FromArgb(238, 70, 90);
                    }
                    else if (aTime >= 30600) // neutral emoticon > 8.5 hours
                    {
                        lblATimeEmo.Text = "😐";
                        lblATimeEmo.ForeColor = Color.FromArgb(255, 165, 0);
                    }
                    else  // good emoticon
                    {
                        lblATimeEmo.Text = "🙂";
                        lblATimeEmo.ForeColor = Color.MediumSeaGreen;
                    }
                }
                else
                {
                    lblABreaksEmo.Text = "";
                    lblATimeEmo.Text = "";
                }

            }
            else // If there are no data.
            {
                lblABreaksEmo.Text = "";
                lblATimeEmo.Text = "";
                lblABreaks.Text = "NA";
                lblATime.Text = "NA";
            }
        }

        // ********************************************************************************************************************
        // ** Mehtods for sound management:
        private void PlaySound()
        {
            string fileName = "";

            switch (MainForm.MainFormInstance.cData.PlaySoundRButton)
            {
                case "rdoS1":
                    fileName = "Sounds\\sound1.wav";
                    break;
                case "rdoS2":
                    fileName = "Sounds\\sound2.wav";
                    break;
                case "rdoS3":
                    fileName = "Sounds\\sound3.wav";
                    break;
            }

            // Set the working directory to the application's directory
            //string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

            //MessageBox.Show(appDirectory+fileName);


            soundPlayer = new SoundPlayer(fileName);
            soundPlayer.Play();
            btnSoundOff.Enabled = true;
            tmrDisableSoundButton.Enabled = true;
        }

        private void StopSound()
        {
            soundPlayer.Stop();
            btnSoundOff.Enabled = false;
        }

        private void BtnSoundOff_Click(object? sender, EventArgs e)
        {
            StopSound();
        }

        /// <summary>
        /// Timer for disabling the btnSoundOff button after 16 seconds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmrDisableSoundButton_Tick(object? sender, EventArgs e)
        {
            tmrDisableSoundButton.Enabled = false;
            btnSoundOff.Enabled = false;
        }

        // ********************************************************************************************************************


        /// <summary>
        /// Bring the main application window to the foreground.
        /// </summary>

        // Import necessary functions from user32.dll
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_SHOWWINDOW = 0x0040;

        private void BringMainWindowToFront()
        {

            // Windows has restrictions to prevent applications from stealing focus from the currently active window.
            // These restrictions are enforced more strictly when running the application outside of a debugger!!!

            // Click on the btnAlarm button in the main form
            MainForm.MainFormInstance.btnAlarm.PerformClick();

            // Activate the application window
            MainForm.MainFormInstance.WindowState = FormWindowState.Minimized;
            MainForm.MainFormInstance.Activate();
            MainForm.MainFormInstance.WindowState = FormWindowState.Normal;

            // The next code is here just in case previous 3 line of code do not work:

            // Fallback mechanism to ensure the window is brought to the foreground
            IntPtr mainWindowHandle = MainForm.MainFormInstance.Handle;

            // Check if the window is already in the foreground
            IntPtr foregroundWindow = GetForegroundWindow();
            if (foregroundWindow != mainWindowHandle)
            {
                // Attempt to set the window to the foreground
                if (!SetForegroundWindow(mainWindowHandle))
                {
                    // Fallback: Force the window to the top of the Z-order
                    SetWindowPos(mainWindowHandle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
                    SetWindowPos(mainWindowHandle, HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
                }
            }
        }


        /// <summary>
        /// Disable some controls while the alarm is on
        /// </summary>
        private void SetButtonsAlarmOn()
        {
            btnAlarmReset.Enabled = false;
            btnAlarmType.Enabled = false;
            btnPomodoro.Enabled = false;

            btnBreakYes.Enabled = true;
            btnBreakNo.Enabled = true;
            // btnSoundOff.Enabled = true; We can't enable it here, we must to it in PlaySound method.

            btnBreakYes.BackColor = Color.LightGreen;
            btnBreakNo.BackColor = Color.FromArgb(255, 153, 163); // Light pink  
            btnBreakYes.ForeColor = SystemColors.ControlText;
            btnBreakNo.ForeColor = SystemColors.ControlText;


            if (ucSettings != null)
            {
                ucSettings.pnlAlarm.Enabled = false;
                ucSettings.pnlSettings.Enabled = false;
                ucSettings.pnlColors.Enabled = false;
            }
        }

        /// <summary>
        /// Enable controls which were disabled when the alarm was on
        /// </summary>
        private void SetButtonsAlarmOff()
        {
            btnAlarmReset.Enabled = true;
            btnAlarmType.Enabled = true;
            btnPomodoro.Enabled = true;

            btnBreakYes.Enabled = false;
            btnBreakNo.Enabled = false;
            btnSoundOff.Enabled = false;

            if (MainForm.MainFormInstance.DarkMode)
            {
                btnBreakYes.BackColor = Color.Gray;
                btnBreakNo.BackColor = Color.Gray;
            }
            else
            {
                btnBreakYes.BackColor = SystemColors.Control;
                btnBreakNo.BackColor = SystemColors.Control;
            }

            if (ucSettings != null)
            {
                ucSettings.pnlAlarm.Enabled = true;
                ucSettings.pnlSettings.Enabled = true;
                ucSettings.pnlColors.Enabled = true;
            }


            if (ucSettings != null)
            {
                ucSettings.pnlAlarm.Enabled = true;
                ucSettings.pnlSettings.Enabled = true;
                ucSettings.pnlColors.Enabled = true;
            }
        }

        /// <summary>
        /// This method will be called when the user click on the BreakYes or BreakNo button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBreakYesOrNo_Click(object? sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Name == "btnBreakNo")
            {
                MainForm.MainFormInstance.cData.Days[0].DayIgnoredBreaks++; // Number of ignored breaks
                TmrMainTimer_Tick(null, EventArgs.Empty);
                AlarmChartUpdate();
            }

            // Pomodoro 4/4 -> 0/4
            if (MainForm.MainFormInstance.cData.Pomodoro && MainForm.PomoCounter == 4)
            {
                MainForm.PomoCounter++;
                CheckPomodoro();
            }

            if (soundPlayer != null)
            {
                StopSound();
            }

            tmrActiveBreakButtons.Enabled = false;
            SetButtonsAlarmOff();
            MainForm.TimeIsUp = false;
            myAlarm.Restart();
        }


        /// <summary>
        /// Reset the alarm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAlarmReset_Click(object? sender, EventArgs e)
        {
            myAlarm.Restart();
            TmrMainTimer_Tick(null, EventArgs.Empty); // We need to update the labels immediately. We can't wait for the timer to do it.
        }

        /// <summary>
        /// Reset the Pomodoro counter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPomodoro_Click(object? sender, EventArgs e)
        {
            MainForm.PomoCounter = 0;
            CheckPomodoro();
        }

        /// <summary>
        /// Update the Pomodoro button (colors/number) based on the pomodoro counter.
        /// </summary>
        private void CheckPomodoro()
        {
            if (MainForm.MainFormInstance.cData.Pomodoro)
            {
                if (btnPomodoro.Visible == false)
                    btnPomodoro.Visible = true;

                if (MainForm.PomoCounter == 4)
                {
                    btnPomodoro.Text = String.Format("{0}/4", MainForm.PomoCounter);
                    btnPomodoro.BackColor = Color.FromArgb(238, 70, 90);
                    if (MainForm.MainFormInstance.cData.PomodoroLongBreak)
                    {
                        // Dark the background of the MainForm and show the Pomodoro dialog  
                        using (var overlay = new Form())
                        {
                            overlay.FormBorderStyle = FormBorderStyle.None;
                            overlay.BackColor = Color.Black;
                            overlay.Opacity = 0.50; // Adjust for desired dimness
                            overlay.ShowInTaskbar = false;
                            overlay.StartPosition = FormStartPosition.Manual;
                            overlay.Location = new Point(
                                MainForm.MainFormInstance.Location.X + 7,
                                MainForm.MainFormInstance.Location.Y
                            );
                            overlay.Size = new Size(
                                MainForm.MainFormInstance.Size.Width - 14,
                                MainForm.MainFormInstance.Size.Height - 7
                            );
                            overlay.Owner = MainForm.MainFormInstance;
                            overlay.Show();
                            using (var dlg = new FrmPomodoro(sPomodoroText, sPomodoroTitle))
                            {
                                dlg.StartPosition = FormStartPosition.CenterParent;
                                dlg.ShowDialog(MainForm.MainFormInstance);
                            }
                            overlay.Close();
                        }
                    }
                }
                else if (MainForm.PomoCounter == 5)
                {
                    MainForm.PomoCounter = 0;
                    if (MainForm.MainFormInstance.DarkMode)
                    {
                        btnPomodoro.BackColor = Color.Gray;
                    }
                    else
                    {
                        btnPomodoro.BackColor = SystemColors.ButtonHighlight;
                    }
                }
                else
                {
                    if (MainForm.MainFormInstance.DarkMode)
                    {
                        btnPomodoro.BackColor = Color.Gray;
                    }
                    else
                    {
                        btnPomodoro.BackColor = SystemColors.ButtonHighlight;
                    }
                }
                btnPomodoro.Text = String.Format("{0}/4", MainForm.PomoCounter);
            }
            else
            {
                btnPomodoro.Visible = false;
                MainForm.PomoCounter = 0;
            }
        }


        // ********************************************************************************************************************
        // ** Chart methods:


        /// <summary>
        /// Initialize the chart.
        /// </summary>
        private void AlarmChartInit()
        {
            // Rename legend entries
            chartAlarm.Series["Ignored breaks"].LegendText = "***Ignored Breaks";
            chartAlarm.Series["Total time"].LegendText = "***Total Time";
            chartAlarm.Series["Working time"].LegendText = "***Working Time";

            // Adjust the design
            chartAlarm.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            chartAlarm.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;

            chartAlarm.ChartAreas["ChartArea1"].AxisY.Title = "***Ignored breaks [-]"; // Chart Y Axis Title 
            chartAlarm.ChartAreas["ChartArea1"].AxisY.TitleAlignment = StringAlignment.Center;  // Chart Y axis Text 
            chartAlarm.ChartAreas["ChartArea1"].AxisY.TextOrientation = TextOrientation.Rotated270;
            chartAlarm.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;

            chartAlarm.ChartAreas["ChartArea1"].AxisY2.Title = "***Working time, total time [hours]"; // Chart Y Axis Title 
            chartAlarm.ChartAreas["ChartArea1"].AxisY2.TitleAlignment = StringAlignment.Center;  // Chart Y axis Text 
            chartAlarm.ChartAreas["ChartArea1"].AxisY2.TextOrientation = TextOrientation.Rotated270;
            chartAlarm.ChartAreas["ChartArea1"].AxisY2.MajorGrid.LineColor = Color.Gainsboro;

            // Set chart types
            chartAlarm.Series["Ignored breaks"].ChartType = SeriesChartType.StackedColumn;
            chartAlarm.Series["Total time"].ChartType = SeriesChartType.StackedColumn;
            chartAlarm.Series["Working time"].ChartType = SeriesChartType.StackedColumn;

            // Set colors
            chartAlarm.Series["Ignored breaks"].Color = Color.FromArgb(238, 70, 90); // Red
            chartAlarm.Series["Total time"].Color = Color.DarkGray;                  // Gray
            chartAlarm.Series["Working time"].Color = ColorTranslator.FromHtml("#0180C0");      // 

            // Set axes
            chartAlarm.Series["Ignored breaks"].XAxisType = AxisType.Primary;
            chartAlarm.Series["Ignored breaks"].YAxisType = AxisType.Primary;
            chartAlarm.Series["Total time"].XAxisType = AxisType.Primary;
            chartAlarm.Series["Total time"].YAxisType = AxisType.Secondary;
            chartAlarm.Series["Working time"].XAxisType = AxisType.Primary;
            chartAlarm.Series["Working time"].YAxisType = AxisType.Secondary;

            // Set x-axis label format
            chartAlarm.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "MM-dd";
        }

        private void AlarmChartUpdate()
        {
            foreach (var series in chartAlarm.Series)
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
            for (int i = 9; i >= 0; i--)
            {
                var day = MainForm.MainFormInstance.cData.Days[i];
                DateTime xDayValue = DateTime.ParseExact(day.DayDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                chartAlarm.Series["Ignored breaks"].Points.AddXY(xDayValue, day.DayIgnoredBreaks);
                // Because I have no idea how to display "Total time" behind the "Working time", I am using this trick:
                // I am diplaying "IdleTime" in "Total time" series. (Total time is the sum of working time and idle time)
                // For this reason we also have to recalculate ChartAlarm_GetToolTipText() method.
                chartAlarm.Series["Total time"].Points.AddXY(xDayValue, (double)day.DayIdleTime / 3600);
                chartAlarm.Series["Working time"].Points.AddXY(xDayValue, (double)day.DayWorkingTime / 3600);

                // (Trick to reduce Y-axis) Track the maximum value of "Ignored breaks".
                if (day.DayIgnoredBreaks > maxIgnoredBreaks)
                {
                    maxIgnoredBreaks = day.DayIgnoredBreaks;
                }
            }

            // (Trick to reduce Y-axis) Calculate the maximum Y-axis value to ensure it is at most 40% of the chart height.
            double chartHeight = chartAlarm.ChartAreas["ChartArea1"].AxisY.ScaleView.Size; // Get the chart height.
            double adjustedMaxY = maxIgnoredBreaks / 0.25; // Ensure maxIgnoredBreaks is at most 20% of the chart height.

            // (Trick to reduce Y-axis) Set the Y-axis maximum value.
            chartAlarm.ChartAreas["ChartArea1"].AxisY.Maximum = Math.Max(maxIgnoredBreaks, adjustedMaxY);

            // Refresh the chart
            chartAlarm.Invalidate();
            chartAlarm.Update();
        }

        /// <summary>
        /// Get tooltip text for the chart data points.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AlarmChart_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            if (e.HitTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                var dataPoint = e.HitTestResult.Series.Points[e.HitTestResult.PointIndex];
                var seriesName = e.HitTestResult.Series.Name;
                var xValue = DateTime.FromOADate(dataPoint.XValue).ToString("MM-dd"); // Format xValue as MM-dd
                string yValue;

                // Format yValue based on the series
                if (seriesName == "Total time")
                {
                    // Sum "Total time" and "Working time" for yValue. This is because of the trick in UpdateAndShowCharts().
                    double totalTime = chartAlarm.Series["Total time"].Points[e.HitTestResult.PointIndex].YValues[0];
                    double workingTime = chartAlarm.Series["Working time"].Points[e.HitTestResult.PointIndex].YValues[0];
                    yValue = TimeSpan.FromHours(totalTime + workingTime).ToString(@"hh\:mm"); // Format as hh:mm
                }
                else if (seriesName == "Working time")
                {
                    yValue = TimeSpan.FromHours(dataPoint.YValues[0]).ToString(@"hh\:mm"); // Format as hh:mm
                }

                else if (seriesName == "Ignored breaks")
                {
                    yValue = dataPoint.YValues[0].ToString("0"); // Format as a whole number
                }
                else
                {
                    yValue = dataPoint.YValues[0].ToString(); // Default formatting
                }

                // Set the tooltip text
                e.Text = $"{xValue}, {yValue}";
            }
        }







        /// <summary>
        /// Method for changing the position of the lblAverageStats label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblAverageStats_SizeChanged(object sender, EventArgs e)
        {
            int newLeft = 790 - lblAverageStats.Width;
            lblAverageStats.Left = newLeft;

        }

        /// <summary>
        /// Shift focus to another control (e.g., a label)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rtxStats_Enter(object sender, EventArgs e)
        {
            // With this trick we will get rid of vertical line (cursor) displaying at the end of the rtxStats text (Which is displayed here if we click on rtxStats control). 
            lblAverageStats.Focus();
        }

        private void chartAlarm_Enter(object sender, EventArgs e)
        {
            // Trick how to get rid of the focus rectangle around the chart.
            lblAverageStats.Focus();
        }

      
    }

}
