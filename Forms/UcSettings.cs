using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SBR.Forms
{
    public partial class UcSettings : UserControl
    {

        public event EventHandler AlarmNumericUpDownValuesChanged;
        public event EventHandler AlarmCheckPomodoroChanged;
        public event EventHandler AlarmCheckEmoticonsChanged;

        //List of pastel colors for random color selection.
        List<String> colorList = new List<String> { "#E0A3E0",
                                                    "#EE465A",
                                                    "#A9A9A9",
                                                    "#77DD77",
                                                    "#8BC6FC",
                                                    "#FFD700",
                                                    "#E0A3E0",
                                                    "#FF99A3",
                                                    "#FFDDB3",
                                                    "#EBCCFF",
                                                    "#BEDDF1",
                                                    "#B0E9D5",
                                                    "#A5E3E0",
                                                    "#B4D9EF",
                                                    "#E7D27C",
                                                    "#F6B8D0",
                                                    "#F1BEB5",
                                                    "#F8C57C",
                                                    "#A4D8D8",
                                                    "#FFA4A9",
                                                    "#AFC0EA",
                                                    "#FFB347",
                                                    "#FFA38C",
                                                    "#9ECB91",
                                                    "#E99FAA",
                                                    "#BCBC82",
                                                    "#D05C39",
                                                    "#8686AF",
                                                    "#63B7B7",
                                                    "#E5C768",
                                                    "#0080C0", // Nice blue color, so it is here twice :)
                                                    "#0080C0"

        };

        public UcSettings()
        {
            InitializeComponent();

            FormInit();
            EventHandlersInit(); // I think it is safer to run this method after FormInit() method. FormInt method can run some events and they can cause us problems.

            // In case somebody will update json file manually - we have to run these methods:
            UpdateWindowsRegistry();
        }

        // ********************************************************************************************************************
        // ** Public Methods:
        // ********************************************************************************************************************

        /// <summary>
        /// This method will return a random color from the colorList.
        /// </summary>
        /// <returns></returns>
        public Color RandomColor()
        {
            Random random = new Random();
            int index = random.Next(0, colorList.Count);
            return ColorTranslator.FromHtml(colorList[index]);
        }

        public void ChangeLanguage()
        {

            // List of strings which are in current User Control (Windows Form) and which we want to change to different language.
            // There is such method (ChangeLanguage()) in each User Control (Windows Form) which is called from LangChanger static class.
            lblAlarm1.Text = LangChanger.GetString("Alarm 1 time [min]:");
            lblAlarm2.Text = LangChanger.GetString("Alarm 2 time [min]:");
            lblAlarm3.Text = LangChanger.GetString("Alarm 3 time [min]:");
            lblAlarm4.Text = LangChanger.GetString("Alarm 4 time [min]:");
            chkStartUp.Text = LangChanger.GetString("Run at startup");
            chkPlaySound.Text = LangChanger.GetString("Sound alarm");
            chkEmoticons.Text = LangChanger.GetString("Emoticons :) (:");
            chkPomodoro.Text = LangChanger.GetString("Pomodoro 4/4");
            chkPomoLongBreak.Text = LangChanger.GetString("Extra alert for long break");
            rdoRed.Text = LangChanger.GetString("Red");
            rdoGreen.Text = LangChanger.GetString("Green");
            rdoBlue.Text = LangChanger.GetString("Blue");
            rdoGray.Text = LangChanger.GetString("Gray");
            rdoYellow.Text = LangChanger.GetString("Yellow");
            rdoPink.Text = LangChanger.GetString("Pink");
            rdoOrange.Text = LangChanger.GetString("Orange");
            rdoPurple.Text = LangChanger.GetString("Purple");
            rdoRandom.Text = LangChanger.GetString("Random colour");
            rdoCustom.Text = LangChanger.GetString("Custom colour");

        }


        // ********************************************************************************************************************
        // ** Private Methods:
        // ********************************************************************************************************************

        private void EventHandlersInit()
        {
            // checkBoxes
            chkStartUp.CheckedChanged += new System.EventHandler(CheckBoxCheckedChanged);
            chkPlaySound.CheckedChanged += new System.EventHandler(CheckBoxCheckedChanged);
            chkEmoticons.CheckedChanged += new System.EventHandler(CheckBoxCheckedChanged);
            chkPomodoro.CheckedChanged += new System.EventHandler(CheckBoxCheckedChanged);
            chkPomoLongBreak.CheckedChanged += new System.EventHandler(CheckBoxCheckedChanged);

            // radioButtons Sound
            rdoS1.CheckedChanged += new System.EventHandler(RadioButtonsCheckChanged);
            rdoS2.CheckedChanged += new System.EventHandler(RadioButtonsCheckChanged);
            rdoS3.CheckedChanged += new System.EventHandler(RadioButtonsCheckChanged);

            // radioButtons Color
            rdoRed.CheckedChanged += new System.EventHandler(RadioButtonsCheckChanged);
            rdoGreen.CheckedChanged += new System.EventHandler(RadioButtonsCheckChanged);
            rdoBlue.CheckedChanged += new System.EventHandler(RadioButtonsCheckChanged);
            rdoGray.CheckedChanged += new System.EventHandler(RadioButtonsCheckChanged);
            rdoYellow.CheckedChanged += new System.EventHandler(RadioButtonsCheckChanged);
            rdoPink.CheckedChanged += new System.EventHandler(RadioButtonsCheckChanged);
            rdoOrange.CheckedChanged += new System.EventHandler(RadioButtonsCheckChanged);
            rdoPurple.CheckedChanged += new System.EventHandler(RadioButtonsCheckChanged);
            rdoRandom.CheckedChanged += new System.EventHandler(RadioButtonsCheckChanged);
            rdoCustom.CheckedChanged += new System.EventHandler(RadioButtonsCheckChanged);


            // numericUpDowns
            // I think it is better to use Leave event instead of ValueChanged event because
            // ValueChanged event is raised every time when the value is changed.
            nudAlarm1.Leave += new System.EventHandler(NumericUpDownValueChanged);
            nudAlarm2.Leave += new System.EventHandler(NumericUpDownValueChanged);
            nudAlarm3.Leave += new System.EventHandler(NumericUpDownValueChanged);
            nudAlarm4.Leave += new System.EventHandler(NumericUpDownValueChanged);

            // cursor (on the custom color label)   
            lblCustom.MouseEnter += new EventHandler(lblCustom_MouseEnter);
            lblCustom.MouseLeave += new EventHandler(lblCustom_MouseLeave);
        }

        /// <summary>
        /// This method will update settings (checkboxes/radiobuttons ...) according to the data from the json file.
        /// </summary>
        private void FormInit()
        {
            // Update pnlCustom color according to the data from the json file.
            pnlCustom.BackColor = ColorTranslator.FromHtml(MainForm.MainFormInstance.cData.CustomColor);

            // Select(check) the radio buttons in Color panel according to the data from the json file.
            foreach (Control SelectedButton in pnlColors.Controls)
            {
                if (SelectedButton is RadioButton)
                {
                    if (SelectedButton.Name.ToString() == MainForm.MainFormInstance.cData.SelectedRadioButton)
                    {
                        RadioButton r = (RadioButton)SelectedButton;
                        r.Checked = true;
                        break;
                    }
                }
            }

            // Select(check) the radio buttons in Settings panel according to the data from the json file.
            foreach (Control SelectedButton in pnlSettings.Controls)
            {
                if (SelectedButton is RadioButton)
                {
                    if (SelectedButton.Name.ToString() == MainForm.MainFormInstance.cData.PlaySoundRButton)
                    {
                        RadioButton r = (RadioButton)SelectedButton;
                        r.Checked = true;
                        break;
                    }
                }
            }


            // Update numeric up-down controls according to the data from the json file.
            nudAlarm1.Value = MainForm.MainFormInstance.cData.AlarmTime1;
            nudAlarm2.Value = MainForm.MainFormInstance.cData.AlarmTime2;
            nudAlarm3.Value = MainForm.MainFormInstance.cData.AlarmTime3;
            nudAlarm4.Value = MainForm.MainFormInstance.cData.AlarmTime4;

            // Update check-boxes according to the data from the json file.
            chkStartUp.Checked = MainForm.MainFormInstance.cData.StartUp;
            chkPlaySound.Checked = MainForm.MainFormInstance.cData.PlaySound;
            chkEmoticons.Checked = MainForm.MainFormInstance.cData.Emoticons;
            chkPomodoro.Checked = MainForm.MainFormInstance.cData.Pomodoro;
            chkPomoLongBreak.Checked = MainForm.MainFormInstance.cData.PomodoroLongBreak;

            EnableDisableSoundRButtons();
            EnableDisablePomoLongBreak();
        }

        /// <summary>
        /// This method will update the Windows registry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateWindowsRegistry()
        {
            // Define the registry key path for the "Run" section in the current user's registry hive
            string runKey = System.IO.Path.Combine("SOFTWARE", "Microsoft", "Windows", "CurrentVersion", "Run");

            // Open the registry key for reading.
            RegistryKey startupKey = Registry.CurrentUser.OpenSubKey(runKey);

            if (chkStartUp.Checked)
            {
                // If the registry value (entry) exists but the data (application path) is incorrect or outdated, delete the value.
                if (startupKey.GetValue("SBR_vtsoft") != null && startupKey.GetValue("SBR_vtsoft").ToString() != "\"" + Application.ExecutablePath + "\"")
                {        
                    startupKey = Registry.CurrentUser.OpenSubKey(runKey, true);
                    startupKey.DeleteValue("SBR_vtsoft", false);
                }

                // If the registry value (entry) does not exist, create it and set the application path as its data.
                if (startupKey.GetValue("SBR_vtsoft") == null)
                {
                    startupKey = Registry.CurrentUser.OpenSubKey(runKey, true);
                    startupKey.SetValue("SBR_vtsoft", "\"" + Application.ExecutablePath + "\"");            
                }
            }
            else // If the checkbox is unchecked, delete the registry value (entry).
            {
                startupKey = Registry.CurrentUser.OpenSubKey(runKey, true);
                startupKey.DeleteValue("SBR_vtsoft", false);
           
            }

            startupKey.Close();
        }

        private void CheckBoxCheckedChanged(object? sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            switch (chk.Name)
            {
                case "chkStartUp":
                    MainForm.MainFormInstance.cData.StartUp = chkStartUp.Checked;
                    UpdateWindowsRegistry();
                    break;
                case "chkPlaySound":
                    MainForm.MainFormInstance.cData.PlaySound = chkPlaySound.Checked;
                    EnableDisableSoundRButtons();
                    break;
                case "chkEmoticons":
                    MainForm.MainFormInstance.cData.Emoticons = chkEmoticons.Checked;
                    // Raise the event (We need to run UpdateAndShowAverageStats() method in UcAlarm.cs)
                    AlarmCheckEmoticonsChanged?.Invoke(this, EventArgs.Empty);
                    break;
                case "chkPomodoro":
                    MainForm.MainFormInstance.cData.Pomodoro = chkPomodoro.Checked;
                    EnableDisablePomoLongBreak();
                    // Raise the event (We need to run CheckPomodoro method in UcAlarm.cs)
                    AlarmCheckPomodoroChanged?.Invoke(this, EventArgs.Empty); 
                    break;
                case "chkPomoLongBreak":
                    MainForm.MainFormInstance.cData.PomodoroLongBreak = chkPomoLongBreak.Checked;
                    break;

            }
        }

        private void EnableDisablePomoLongBreak()
        {
            if (chkPomodoro.Checked)
            {
                chkPomoLongBreak.Visible = true;
            }
            else
            {
                chkPomoLongBreak.Visible = false;
                chkPomoLongBreak.Checked = false;
            }
        }

        private void EnableDisableSoundRButtons()
        {
            if (chkPlaySound.Checked)
            {
                rdoS1.Visible = true;
                rdoS2.Visible = true;
                rdoS3.Visible = true;
            }
            else
            {
                rdoS1.Visible = false;
                rdoS2.Visible = false;
                rdoS3.Visible = false;
            }         
        }

        private void NumericUpDownValueChanged(object? sender, EventArgs e)
        {
            NumericUpDown nud = sender as NumericUpDown;
            int selectedAlarm = MainForm.MainFormInstance.cData.SelectedAlarm;
            switch (nud.Name)
            {
                case "nudAlarm1":
                    MainForm.MainFormInstance.cData.AlarmTime1 = (int)nud.Value;
                    if (selectedAlarm == 1)
                    {
                        // Raise the event, but only if the selected (running) alarm is the same as the alarm that was changed.
                        AlarmNumericUpDownValuesChanged?.Invoke(this, EventArgs.Empty);
                    }
                    break;
                case "nudAlarm2":
                    MainForm.MainFormInstance.cData.AlarmTime2 = (int)nud.Value;
                    if (selectedAlarm == 2)
                    {
                        AlarmNumericUpDownValuesChanged?.Invoke(this, EventArgs.Empty);
                    }
                    break;
                case "nudAlarm3":
                    MainForm.MainFormInstance.cData.AlarmTime3 = (int)nud.Value;
                    if (selectedAlarm == 3)
                    {
                        AlarmNumericUpDownValuesChanged?.Invoke(this, EventArgs.Empty);
                    }
                    break;
                case "nudAlarm4":
                    MainForm.MainFormInstance.cData.AlarmTime4 = (int)nud.Value;
                    if (selectedAlarm == 4)
                    {
                        AlarmNumericUpDownValuesChanged?.Invoke(this, EventArgs.Empty);
                    }
                    break;
            }
        }

        private void RadioButtonsCheckChanged(object sender, EventArgs e)
        {
            // Settings radioButtons
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null && radioButton.Checked)
            {
                switch (radioButton.Name)
                {
                    case "rdoS1":
                        MainForm.MainFormInstance.cData.PlaySoundRButton = "rdoS1";
                        break;
                    case "rdoS2":
                        MainForm.MainFormInstance.cData.PlaySoundRButton = "rdoS2";
                        break;
                    case "rdoS3":
                        MainForm.MainFormInstance.cData.PlaySoundRButton = "rdoS3";
                        break;
                }
            }

            // Color radioButtons
            Color color = Color.Empty; // Initialize color to avoid CS0165
            radioButton = sender as RadioButton;
            if (radioButton != null && radioButton.Checked)
            {
                switch (radioButton.Name)
                {
                    case "rdoRed":
                        color = Color.FromArgb(238, 70, 90);
                        break;
                    case "rdoGreen":
                        color = Color.FromArgb(119, 221, 119);
                        break;
                    case "rdoBlue":
                        color = Color.FromArgb(139, 198, 252);
                        break;
                    case "rdoGray":
                        color = Color.DarkGray;
                        break;
                    case "rdoPurple":
                        color = Color.FromArgb(224, 163, 224);
                        break;
                    case "rdoYellow":
                        color = Color.Gold;
                        break;
                    case "rdoPink":
                        color = Color.FromArgb(255, 153, 163);
                        break;
                    case "rdoOrange":
                        color = Color.Orange;
                        break;
                    case "rdoRandom":
                        color = RandomColor();
                        break;
                    case "rdoCustom":
                        color = pnlCustom.BackColor;
                        break;
                }

                if (color != Color.Empty)
                {
                    MainForm.MainFormInstance.cData.SelectedColor = ColorTranslator.ToHtml(color);
                    MainForm.MainFormInstance.cData.SelectedRadioButton = radioButton.Name;
                    MainForm.MainFormInstance.AdjustColor();
                }
            }
        }

        private void lblCustom_MouseEnter(object sender, EventArgs e)
        {
            lblCustom.Cursor = Cursors.Hand; // Change cursor to hand when mouse enters
        }

        private void lblCustom_MouseLeave(object sender, EventArgs e)
        {
            lblCustom.Cursor = Cursors.Default; // Change cursor back to default when mouse leaves
        }

        /// <summary>
        /// Select custom color.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblCustom_Click(object sender, EventArgs e)
        {
            ColorDialog colorPicker = new ColorDialog();
            if (colorPicker.ShowDialog() == DialogResult.OK)
            {
                Color customColor = colorPicker.Color;
                pnlCustom.BackColor = customColor;      // Change color of the panel rectangle to the selected color.

                MainForm.MainFormInstance.cData.CustomColor = ColorTranslator.ToHtml(customColor);

                if (rdoCustom.Checked) // If also radio button is checked then also change color of the application.
                {
                    MainForm.MainFormInstance.cData.SelectedColor = ColorTranslator.ToHtml(customColor);
                    MainForm.MainFormInstance.AdjustColor();
                }
            }
        }

 
    }
}
