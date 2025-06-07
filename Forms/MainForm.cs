using System.Windows.Forms;
using SBR.Forms;
using System.Resources;
using System.Drawing.Imaging;
using static System.Windows.Forms.LinkLabel;
using System.Diagnostics;
using System.Numerics;
using SBR;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;


// Project information: 
// Installed NugetPackages:  WinForms Data Visualization - for Charts
// 
// Please be aware that I am developer beginner so this code is not perfect and it is probbaly not following all best practices.
// However I still hope that you will find it useful and that it will help you to create your own application.
// for more projects please check this site: https://www.vt-soft.com/ 
//
// MainForm is the entry point of the application.
// Enjoy the code! :)




namespace SBR
{
    public partial class MainForm : Form
    {
        public static MainForm MainFormInstance;

        public static bool TimeIsUp = false;    // Just for color purposes.
        public static int PomoCounter = 0;

        public bool DarkMode { get; private set; } = false;    // Day/dark mode

        private Color navBarBacgroundColor1 = Color.FromArgb(41, 39, 40);   // Color for selected left NavBar button.
        private Color navBarBacgroundColor2 = Color.Gray;                   // Color for non-selected NavBar button.

        // We store all UserControl (Forms) in this dictionary.
        private Dictionary<string, UserControl> screens = new Dictionary<string, UserControl>();

        // This is the current User Control (Windows Form) which is loaded into the pnlFormLoader panel.
        private UserControl currentControl;

        // Here we store all necessary data. These objects are also then saved to json files.
        public DataConfig? cData;
        public DataToDoList? tData;

        public MainForm()
        {
            InitializeComponent();
            MainFormInstance = this;
        }

        // ********************************************************************************************************************
        // ** Public Methods:
        // ********************************************************************************************************************

        /// <summary>
        /// Method for adjusting the color of the horizontal and vertical strip.
        /// </summary>
        public void AdjustColor()
        {
            pnlVerticalStrip.BackColor = ColorTranslator.FromHtml(cData.SelectedColor);
            pnlHorizontalStrip.BackColor = ColorTranslator.FromHtml(cData.SelectedColor);
            btnChangeDayMode.ForeColor = ColorTranslator.FromHtml(cData.SelectedColor);
        }


        /// <summary>
        /// Method for changing strings in current User Control (Windows Form) to proper (selected) language.
        /// </summary>
        public void ChangeLanguage()
        {
            // List of strings which are in current User Control (Windows Form) and which we want to change to different language.
            // This method ChangeLanguage() in each User Control (Windows Form) and is called from LangChanger static class.
            btnAlarm.Text = "  " + LangChanger.GetString("Alarm");
            //btnToDo.Text = "m " + LangChanger.GetString("ToDo");
            ((UcToDo)screens["btnToDo"]).UpdateNumberOfTasks(); // This will also update the string in the button text.
            btnSettings.Text = "  " + LangChanger.GetString("Settings");
            btnStatistics.Text = "  " + LangChanger.GetString("Statistics");
            btnLanguage.Text = "  " + LangChanger.GetString("Language");
            btnAbout.Text = "  " + LangChanger.GetString("About");
        }

        /// <summary>
        /// Method for loading the correct Form (User Control) into the pnlFormLoader panel.
        /// </summary>
        /// <param name="buttonName"></param>
        public void LoadForm(string buttonName)
        {
            this.pnlFormLoader.SuspendLayout(); // Suspend layout to prevent redraw. Probably not necessary here :)

            //  Without this part of code there were some white flashes when switching between Forms (User Controls).
            if (DarkMode)
            {
                this.BackColor = Color.FromArgb(80, 80, 80); // dark mode
            }
            else
            {
                this.BackColor = SystemColors.Control; // day mode
            }

            this.pnlFormLoader.Controls.Clear();
            currentControl = screens[buttonName];     // Searching for the proper Form (User Control) in the Dictionary.
            currentControl.Dock = DockStyle.Fill;     // Ensure the UserControl is docked correctly.
            this.pnlFormLoader.Controls.Add(currentControl);
            this.pnlFormLoader.ResumeLayout(); // Resume layout after adding the control. Probably not necessary here :)
            currentControl.Show();

        }

        /// <summary>
        /// Method for changing the color of the button when it is enabled or disabled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ButtonEnabledChanged(object sender, EventArgs e)
        {
            // There is little chaos in Dark/Day mode for buttons. But I am rather not touching it anymore :)

            Button b = (Button)sender;

            if (DarkMode)
            {
                b.BackColor = Color.Gray;
                b.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                b.BackColor = SystemColors.ButtonHighlight;
                b.ForeColor = SystemColors.ControlText;

                if (b.Enabled == false)
                {
                    b.BackColor = SystemColors.ButtonFace;
                }
            }

        }

        // ********************************************************************************************************************
        // ** Private Methods:
        // ********************************************************************************************************************

        private void MainForm_Load(object sender, EventArgs e)
        {

            // Set the working directory to the application's directory
            // This is necessary for the application to work correctly when started from the Task Scheduler.
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Directory.SetCurrentDirectory(appDirectory);


            // All settings data  are loaded into cData object.
            cData = JsonMethods.FileLoadConfig();
            // All TODO tasks data are loaded into tData object.
            tData = JsonMethods.FileLoadTasks();


            // Method which will  populate the "screens" dictionary with all User Control (Win Forms).
            FormsAndButtonsInit();

            // Pass on references to the MainForm and other Forms to LangChanger static class
            LangChanger.Init(this, screens);

            // Adjust the language according to the value in the json file.
            LangChanger.ChangeLangCode(cData.LanguageCode);

            // Load correct Form (btnAlarm in this case) into the pnlFormLoader panel.
            LoadForm("btnAlarm");


            // Adjust the dark mode according to the value in the json file.
            if (cData.DarkMode)
            {
                DarkModeOn();
                btnChangeDayMode.Image = ResourcesIconsDir.ResourcesIcons.sun_icon;
            }
            else
            {
                DarkModeOff();
                btnChangeDayMode.Image = ResourcesIconsDir.ResourcesIcons.moon_icon;
            }

            // Adjust color of the horizontal and vertical strip.
            AdjustColor();

            // Adjust TODO button text and color.
            ((UcToDo)screens["btnToDo"]).UpdateNumberOfTasks();


        }

        /// <summary>
        /// Method for initializing the NavBar buttons and Forms (User Controls).    
        /// </summary>       
        private void FormsAndButtonsInit()
        {
            NavBarButton_Leave();

            // Initialize the vertical strip to first button (btnAlarm).
            pnlVerticalStrip.Height = btnAlarm.Height;
            pnlVerticalStrip.Top = btnAlarm.Top;
            pnlVerticalStrip.Left = 0;
            btnAlarm.BackColor = navBarBacgroundColor2;


            // For Event purposes  we are creating ucAlarm and ucSettings here in a bit different way.
            UcAlarm ucAlarm = new UcAlarm() { Dock = DockStyle.Fill };
            UcSettings ucSettings = new UcSettings() { Dock = DockStyle.Fill };
            UcToDo ucToDo = new UcToDo() { Dock = DockStyle.Fill };

            // Subscribe to the events
            ucAlarm.SubscribeToEvents(ucSettings);
            ucToDo.TaskCountUpdated += UcToDo_TaskCountUpdated;


            // Populate the dictionary list (Key-Value) with all Forms (User Control).
            // Key (like btnAlarm) must exactly match button name.
            screens.Add("btnAlarm", ucAlarm);
            screens.Add("btnToDo", ucToDo);
            screens.Add("btnSettings", ucSettings);
            screens.Add("btnStatistics", new UcStatistics() { Dock = DockStyle.Fill });
            screens.Add("btnLanguage", new UcLanguageTemp() { Dock = DockStyle.Fill });  // TODO - TEMP - then change back to UcLanguage
            screens.Add("btnAbout", new UcAbout() { Dock = DockStyle.Fill });

            // Load correct Form (btnAlarm in this case) into the pnlFormLoader panel.
            // LoadForm("btnAlarm");
        }

        /// <summary>
        /// Method for handling the NavBar button click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarButton_Click(Object sender, EventArgs e)
        {
            // Instead of having a separate event handler for each button we have one general event for all NavBar buttons.
            // This event is triggered when any NavBar button is clicked.

            Button button = (Button)sender;

            // Check if the form associated with the clicked button is already loaded
            if (currentControl == screens[button.Name])
            {
                return; // Form is already loaded, no need to reload
            }

            NavBarButton_Leave();

            // Adjust the NavBarPanel to the clicked button.
            pnlVerticalStrip.Height = button.Height;
            pnlVerticalStrip.Top = button.Top;
            pnlVerticalStrip.Left = 0;
            button.BackColor = navBarBacgroundColor2;

            // Load correct Form into the pnlFormLoader panel.
            LoadForm(button.Name);
        }

        /// <summary>
        /// Method for handling the NavBar button mouse enter event. 
        /// When mouse leaves the NavBar button, hide the gray vertical strip.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarButton_MouseLeave(object sender, EventArgs e)
        {
            pnlVerticalStrip2.Visible = false;
        }

        /// <summary>
        /// Method for handling the NavBar button mouse enter event.
        /// When mouse enters the NavBar button, show the gray vertical strip.   
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarButton_MouseEnter(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            pnlVerticalStrip2.Visible = true;
            pnlVerticalStrip2.Top = button.Top;
        }

        // <summary>
        // Method for resetting the colors of all NavBar buttons.
        // </summary>
        private void NavBarButton_Leave()
        {
            btnAlarm.BackColor = navBarBacgroundColor1;
            btnToDo.BackColor = navBarBacgroundColor1;
            btnSettings.BackColor = navBarBacgroundColor1;
            btnStatistics.BackColor = navBarBacgroundColor1;
            btnLanguage.BackColor = navBarBacgroundColor1;
            btnAbout.BackColor = navBarBacgroundColor1;
        }


        /// <summary>
        /// Handles the TaskCountUpdated event from UcToDo.
        /// Method is updatting the ToDo button text and color based on the number of tasks and the color code.
        /// </summary>
        /// <param name="numberOfTasks">The total number of tasks.</param>
        /// <param name="toDoButtonColor">The color code for the ToDo button text.</param>
        private void UcToDo_TaskCountUpdated(int numberOfTasks, int toDoButtonColor)
        {
            // Update the ToDo button text
            btnToDo.Text = "  " + LangChanger.GetString("ToDo") + $" ({numberOfTasks})";

            // Update the ToDo button color based on the color code
            switch (toDoButtonColor)
            {
                case 3: // Red
                    btnToDo.ForeColor = Color.FromArgb(255, 70, 81);
                    break;
                case 2: // Orange
                    btnToDo.ForeColor = Color.FromArgb(255, 165, 0);
                    break;
                case 1: // Blue
                    btnToDo.ForeColor = Color.FromArgb(99, 183, 183);
                    break;
                default: // Default white
                    btnToDo.ForeColor = Color.White;
                    break;
            }


            if (numberOfTasks == 0)
            {
                btnToDo.Font = new Font(btnToDo.Font, FontStyle.Regular);
            }
            else
            {
                btnToDo.Font = new Font(btnToDo.Font, FontStyle.Bold);
            }

        }

        // ************************************************************************************************
        // *  Methods for dark / day mode.
        // *

        /// <summary>
        /// Method for changing the application to dark mode and back to day mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangeDayMode_Click(object sender, EventArgs e)
        {
            DarkMode = !DarkMode;

            if (DarkMode)
            {
                btnChangeDayMode.Image = ResourcesIconsDir.ResourcesIcons.sun_icon;
                DarkModeOn();
            }
            else
            {
                btnChangeDayMode.Image = ResourcesIconsDir.ResourcesIcons.moon_icon;
                DarkModeOff();
            }
        }

        /// <summary>
        /// Method for changing the application to dark mode.
        /// </summary>
        private void DarkModeOn()
        {
            cData.DarkMode = DarkMode = true;
            Color myBackgroundColor = Color.FromArgb(80, 80, 80);
            Color myForeColor = Color.WhiteSmoke;

            foreach (var item in screens.Values)      // going through all Forms (User Controls)
            {
                item.BackColor = myBackgroundColor;   // color of the form (User Control) background

                foreach (Control c in item.Controls)  // going through all Controls in particular Form (User Control)
                {
                    if (c is Label)
                    {
                        Label l = (Label)c;
                        l.ForeColor = myForeColor;
                        l.BackColor = myBackgroundColor;

                        if (TimeIsUp && l.Name == "lblAlarmTime")
                        {
                            l.ForeColor = Color.FromArgb(238, 70, 90);
                        }
                    }
                    else if (c is Button)
                    {
                        Button b = (Button)c;
                        b.ForeColor = myForeColor;
                        b.BackColor = Color.Gray;

                        if (b.Enabled == false)
                        {
                            // b.BackColor = SystemColors.ControlDark;
                            b.ForeColor = Color.WhiteSmoke;
                        }

                        if (TimeIsUp && (b.Name == "btnBreakYes" || b.Name == "btnBreakNo"))
                        {
                            b.ForeColor = SystemColors.ControlText;

                            if (b.Name == "btnBreakYes")
                                b.BackColor = Color.LightGreen;
                            if (b.Name == "btnBreakNo")
                                b.BackColor = Color.FromArgb(255, 153, 163); // Light pink  
                        }
                        if (TimeIsUp && PomoCounter == 4 && b.Name == "btnPomodoro")
                        {
                            b.BackColor = Color.FromArgb(238, 70, 90);
                        }

                    }
                    else if (c is ComboBox)
                    {
                        ComboBox cb = (ComboBox)c;
                        cb.ForeColor = myForeColor;
                        cb.BackColor = myBackgroundColor;
                    }
                    else if (c is RichTextBox)
                    {
                        RichTextBox r = (RichTextBox)c;
                        r.ForeColor = myForeColor;
                        r.BackColor = myBackgroundColor;
                    }

                    else if (c is DataGridView)
                    {
                        DarkModeOnDataGridView((DataGridView)c, myForeColor, myBackgroundColor);
                    }
                    else if (c is Panel) // will work both for Panel and TableLayoutPanel
                    {
                        DarkModeOnTLP((Panel)c, myForeColor, myBackgroundColor);
                    }
                    else if (c is Chart)
                    {
                        DarkModeOnCharts((Chart)c, myForeColor, myBackgroundColor);
                    }

                }
            }
        }

        private void DarkModeOnDataGridView(DataGridView dataGridView, Color myForeColor, Color myBackgroundColor)
        {
            // Set the background color for the DataGridView
            dataGridView.BackgroundColor = myBackgroundColor;
            dataGridView.GridColor = Color.FromArgb(120, 120, 120); // Grid lines color

            // Set the default cell style
            dataGridView.DefaultCellStyle.BackColor = myBackgroundColor;
            dataGridView.DefaultCellStyle.ForeColor = myForeColor;
            dataGridView.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight; // Blue background for selection
            dataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText; // White text for selection

            // Set the column headers style
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60); // Darker header background
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = myForeColor;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = SystemColors.Highlight; // Blue background for header selection
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;

            // Set the row headers style
            dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60); // Darker header background
            dataGridView.RowHeadersDefaultCellStyle.ForeColor = myForeColor;
            dataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = SystemColors.Highlight; // Blue background for row header selection
            dataGridView.RowHeadersDefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;

            // Disable visual styles for headers to allow custom styles
            dataGridView.EnableHeadersVisualStyles = false;

            // Set alternating row styles for better readability
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(90, 90, 90); // Slightly lighter background for alternating rows
            dataGridView.AlternatingRowsDefaultCellStyle.ForeColor = myForeColor;
        }

        /// <summary>
        /// Method for changing the application to dark mode in TableLayoutPanel and Panel.
        /// </summary>
        /// <param name="tlp"></param>
        /// <param name="myForeColor2"></param>
        /// <param name="myBackgroundColor2"></param>
        private void DarkModeOnTLP(Panel tlp, Color myForeColor, Color myBackgroundColor)
        {
            Color myLinkForeColor = Color.Silver;

            foreach (Control c2 in tlp.Controls)
            {
                // Warning: as LinkLabel is derived from Label, it must be checked first!
                if (c2 is LinkLabel)
                {
                    LinkLabel ll = (LinkLabel)c2;
                    ll.LinkColor = myLinkForeColor;
                    ll.ActiveLinkColor = myLinkForeColor;
                    ll.VisitedLinkColor = myLinkForeColor;
                    ll.ForeColor = myLinkForeColor;
                    ll.BackColor = myBackgroundColor;
                }
                else if (c2 is Label)
                {
                    Label l = (Label)c2;
                    l.ForeColor = myForeColor;
                    l.BackColor = myBackgroundColor;
                }
                else if (c2 is RadioButton)
                {
                    RadioButton r = (RadioButton)c2;
                    r.ForeColor = myForeColor;
                    r.BackColor = myBackgroundColor;
                }
                else if (c2 is CheckBox)
                {
                    CheckBox ch = (CheckBox)c2;
                    ch.ForeColor = myForeColor;
                    ch.BackColor = myBackgroundColor;
                }
                else if (c2 is NumericUpDown)
                {
                    NumericUpDown n = (NumericUpDown)c2;
                    n.ForeColor = myForeColor;
                    n.BackColor = myBackgroundColor;
                }

            }
        }

        private void DarkModeOnCharts(Chart chart, Color myForeColor, Color myBackgroundColor)
        {

            Color lighterBColor = Color.FromArgb(
                Math.Min(myBackgroundColor.R + 20, 255), // Ensure the value doesn't exceed 255
                Math.Min(myBackgroundColor.G + 20, 255),
                Math.Min(myBackgroundColor.B + 20, 255)
            );

            // Set color of the chart background
            chart.BackColor = myBackgroundColor;
            chart.ChartAreas[0].BackColor = lighterBColor;

            // Set color of the axes 
            chart.ChartAreas[0].AxisX.LineColor = myForeColor;
            chart.ChartAreas[0].AxisY.LineColor = myForeColor;
            chart.ChartAreas[0].AxisY2.LineColor = myForeColor;

            // Set color of the axes labels
            chart.ChartAreas[0].AxisX.LabelStyle.ForeColor = myForeColor;
            chart.ChartAreas[0].AxisY.LabelStyle.ForeColor = myForeColor;
            chart.ChartAreas[0].AxisY2.LabelStyle.ForeColor = myForeColor;

            // Set color of the tick marks (next to the Y-axis labels)
            chart.ChartAreas[0].AxisY.MajorTickMark.LineColor = myForeColor;
            chart.ChartAreas[0].AxisY.MinorTickMark.LineColor = myForeColor;
            chart.ChartAreas[0].AxisY2.MajorTickMark.LineColor = myForeColor;
            chart.ChartAreas[0].AxisY2.MinorTickMark.LineColor = myForeColor;
            chart.ChartAreas[0].AxisX.MajorTickMark.LineColor = myForeColor;
            chart.ChartAreas[0].AxisX.MinorTickMark.LineColor = myForeColor;


            // Set color of the grid lines
            chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(120, 120, 120);
            chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(120, 120, 120);
            chart.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.FromArgb(120, 120, 120);

            // Set color of the legend
            chart.Legends[0].ForeColor = myForeColor;
            chart.Legends[0].BackColor = myBackgroundColor;

            chart.ChartAreas["ChartArea1"].AxisY2.TitleForeColor = myForeColor;
            chart.ChartAreas["ChartArea1"].AxisY.TitleForeColor = myForeColor;
        }

        /// <summary>
        /// Method for changing the application to day mode.
        /// </summary>
        private void DarkModeOff()
        {
            cData.DarkMode = DarkMode = false;
            Color myBackgroundColor = SystemColors.Control;
            Color myForeColor = SystemColors.ControlText;

            foreach (var item in screens.Values)         // going through all Forms (User Controls)
            {
                item.BackColor = SystemColors.Control;   // color of the form (User Control) background

                foreach (Control c in item.Controls)     // going through all Controls in particular Form (User Control)
                {
                    if (c is Label)
                    {
                        Label l = (Label)c;
                        l.ForeColor = myForeColor;
                        l.BackColor = myBackgroundColor;

                        if (TimeIsUp && l.Name == "lblAlarmTime")
                        {
                            l.ForeColor = Color.FromArgb(238, 70, 90);
                        }

                    }
                    else if (c is Button)
                    {
                        Button b = (Button)c;
                        b.ForeColor = myForeColor;
                        b.BackColor = SystemColors.ButtonHighlight;

                        if (b.Enabled == false)
                        {
                            b.BackColor = SystemColors.ButtonFace;
                        }


                        if (TimeIsUp && (b.Name == "btnBreakYes" || b.Name == "btnBreakNo"))
                        {
                            b.ForeColor = SystemColors.ControlText;

                            if (b.Name == "btnBreakYes")
                                b.BackColor = Color.LightGreen;
                            if (b.Name == "btnBreakNo")
                                b.BackColor = Color.FromArgb(255, 153, 163); // Light pink  
                        }
                        if (TimeIsUp && PomoCounter == 4 && b.Name == "btnPomodoro")
                        {
                            b.BackColor = Color.FromArgb(238, 70, 90);
                        }


                    }
                    else if (c is ComboBox)
                    {
                        ComboBox cb = (ComboBox)c;
                        cb.ForeColor = myForeColor;
                        cb.BackColor = Color.White;
                    }
                    else if (c is RichTextBox)
                    {
                        RichTextBox r = (RichTextBox)c;
                        r.ForeColor = myForeColor;
                        r.BackColor = myBackgroundColor;
                    }

                    else if (c is DataGridView)
                    {
                        DarkModeOffDataGridView((DataGridView)c, myForeColor, myBackgroundColor);
                    }

                    else if (c is Panel) // will work both for Panel and TableLayoutPanel
                    {
                        DarkModeOffTLP((Panel)c);
                    }
                    else if (c is Chart)
                    {
                        DarkModeOffCharts((Chart)c);
                    }

                }
            }
        }

        /// <summary>
        /// Reverts the DataGridView styling to light mode.
        /// </summary>
        /// <param name="dataGridView">The DataGridView to style.</param>
        /// <param name="myForeColor">The foreground color (text color).</param>
        /// <param name="myBackgroundColor">The background color.</param>
        private void DarkModeOffDataGridView(DataGridView dataGridView, Color myForeColor, Color myBackgroundColor)
        {
            // Set the background color for the DataGridView
            dataGridView.BackgroundColor = myBackgroundColor;
            dataGridView.GridColor = Color.LightGray; // Default grid lines color

            // Set the default cell style
            dataGridView.DefaultCellStyle.BackColor = myBackgroundColor;
            dataGridView.DefaultCellStyle.ForeColor = myForeColor;
            dataGridView.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight; // Default selection background
            dataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText; // Default selection text color

            // Set the column headers style
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(200, 200, 200); // Default header background
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText; // Default header text color
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = SystemColors.Highlight; // Default selection for headers
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;

            // Set the row headers style
            dataGridView.RowHeadersDefaultCellStyle.BackColor = SystemColors.Control; // Default header background
            dataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText; // Default header text color
            dataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = SystemColors.Highlight; // Default selection for row headers
            dataGridView.RowHeadersDefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;

            // Disable visual styles for headers to allow custom styles
            dataGridView.EnableHeadersVisualStyles = false;

            // Set alternating row styles for better readability
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 230, 230); // Light gray for alternating rows
            dataGridView.AlternatingRowsDefaultCellStyle.ForeColor = SystemColors.ControlText; // Default text color for alternating rows
        }

        /// <summary>
        /// Method for changing the application to day mode in TableLayoutPanel and Panel.
        /// </summary>
        /// <param name="tlp"></param>
        /// <param name="myForeColor2"></param>
        /// <param name="myBackgroundColor2"></param>
        private void DarkModeOffTLP(Panel tlp)
        {
            foreach (Control c2 in tlp.Controls)
            {
                // Warning: as LinkLabel is derived from Label, it must be checked first!
                if (c2 is LinkLabel)
                {
                    LinkLabel ll = (LinkLabel)c2;
                    ll.LinkColor = SystemColors.ControlText;
                    ll.ActiveLinkColor = SystemColors.ControlText;
                    ll.VisitedLinkColor = SystemColors.ControlText;
                    ll.ForeColor = SystemColors.ControlText;
                    ll.BackColor = SystemColors.Control;
                }
                else if (c2 is Label)
                {
                    Label l = (Label)c2;
                    l.ForeColor = SystemColors.ControlText;
                    l.BackColor = SystemColors.Control;
                }
                else if (c2 is RadioButton)
                {
                    RadioButton r = (RadioButton)c2;
                    r.ForeColor = SystemColors.ControlText;
                    r.BackColor = SystemColors.Control;
                }
                else if (c2 is CheckBox)
                {
                    CheckBox ch = (CheckBox)c2;
                    ch.ForeColor = SystemColors.ControlText;
                    ch.BackColor = SystemColors.Control;
                }
                else if (c2 is NumericUpDown)
                {
                    NumericUpDown n = (NumericUpDown)c2;
                    n.ForeColor = SystemColors.ControlText;
                    n.BackColor = SystemColors.Control;
                }
            }
        }

        private void DarkModeOffCharts(Chart chart)
        {
            // Set the chart background to the default light mode color
            chart.BackColor = SystemColors.Control;  //Color.White;
            chart.ChartAreas[0].BackColor = Color.White;

            // Set the axes line colors to the default light mode color
            chart.ChartAreas[0].AxisX.LineColor = Color.Black;
            chart.ChartAreas[0].AxisY.LineColor = Color.Black;
            chart.ChartAreas[0].AxisY2.LineColor = Color.Black;

            // Set the axes labels to the default light mode color
            chart.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
            chart.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
            chart.ChartAreas[0].AxisY2.LabelStyle.ForeColor = Color.Black;

            // Set the tick marks (next to the Y-axis labels) to the default light mode color
            chart.ChartAreas[0].AxisY.MajorTickMark.LineColor = Color.Black;
            chart.ChartAreas[0].AxisY.MinorTickMark.LineColor = Color.Black;
            chart.ChartAreas[0].AxisY2.MajorTickMark.LineColor = Color.Black;
            chart.ChartAreas[0].AxisY2.MinorTickMark.LineColor = Color.Black;
            chart.ChartAreas[0].AxisX.MajorTickMark.LineColor = Color.Black;
            chart.ChartAreas[0].AxisX.MinorTickMark.LineColor = Color.Black;

            // Set the grid lines to the default light mode color
            chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            chart.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.LightGray;

            // Set the legend colors to the default light mode color
            chart.Legends[0].ForeColor = SystemColors.ControlText;
            chart.Legends[0].BackColor = SystemColors.Control;

            // Set the axis titles to the default light mode color
            chart.ChartAreas["ChartArea1"].AxisY2.TitleForeColor = Color.Black;
            chart.ChartAreas["ChartArea1"].AxisY.TitleForeColor = Color.Black;
        }


        // ************************************************************************************************
        // * Another methods
        // *


        /// <summary>
        /// Save settings and graph data to json file every 10 minutes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrSaveData_Tick(object sender, EventArgs e)
        {
            // Save settings
            if (cData != null)
                JsonMethods.FileSaveConfig(cData);
            else throw new Exception("Data is null!");

            // Also if the random color is selected, change the color every 10 minutes.
            if (((UcSettings)screens["btnSettings"]).rdoRandom.Checked)
            {
                cData.SelectedColor = ColorTranslator.ToHtml(((UcSettings)screens["btnSettings"]).RandomColor());
                AdjustColor();
            }
        }

        /// <summary>
        /// Save settings and graph data to json files fefore the application is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cData != null)
                JsonMethods.FileSaveConfig(cData);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // In MainForm or wherever you open FrmPomodoro
            using (var overlay = new Form())
            {
                overlay.FormBorderStyle = FormBorderStyle.None;
                overlay.BackColor = Color.Black;
                overlay.Opacity = 0.55; // Adjust for desired dimness
                overlay.ShowInTaskbar = false;
                overlay.StartPosition = FormStartPosition.Manual;
                overlay.Location = this.Location;
                overlay.Size = this.Size;
                overlay.Owner = this;
                overlay.Show();
                overlay.Close();
            }
        }
    }
}
