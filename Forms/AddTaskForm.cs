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
 
    public partial class AddTaskForm : Form
    {
        private string taskEditId;
        private bool checksPassed;
        public ToDoTask newTask { get; private set; }  // Define a property to hold the task details.

        public AddTaskForm(FormType formType, string taskEditId)
        {
            this.taskEditId = taskEditId;

            InitializeComponent();
            EventHandlersInit();
            FormInit(formType,taskEditId);
            ChangeLanguage();

            ApplyDarkMode();
        }

        private void ApplyDarkMode()
        {
            // Apply dark mode only if enabled in MainForm
            if (MainForm.MainFormInstance.DarkMode)
            {
                Color myBackgroundColor = Color.FromArgb(80, 80, 80);
                Color myForeColor = Color.WhiteSmoke;

                // Set the background color of the form
                this.BackColor = myBackgroundColor;

                // Iterate through all controls in the form
                foreach (Control control in this.Controls)
                {
                    if (control is Label lbl)
                    {
                        lbl.ForeColor = myForeColor;
                        lbl.BackColor = myBackgroundColor;
                    }
                    else if (control is TextBox txt)
                    {
                        txt.ForeColor = myForeColor;
                        txt.BackColor = Color.FromArgb(100, 100, 100); // Slightly darker background for text boxes
                    }
                    else if (control is Button btn)
                    {
                        btn.ForeColor = myForeColor;
                        btn.BackColor = Color.Gray;
                    }
                    else if (control is ComboBox cmb)
                    {
                        cmb.ForeColor = myForeColor;
                        cmb.BackColor = myBackgroundColor;
                    }
                    else if (control is NumericUpDown nud)
                    {
                        nud.ForeColor = myForeColor;
                        nud.BackColor = myBackgroundColor;
                    }
                    else if (control is CheckBox chk)
                    {
                        chk.ForeColor = myForeColor;
                        chk.BackColor = myBackgroundColor;
                    }

                    // Not working and it would be probably too dificult to fix it :)
                    // The DateTimePicker control in Windows Forms has limited styling options, and some of its visual elements
                    // (like the dropdown calendar) are controlled by the operating system's theme,
                    // which makes it challenging to fully customize its appearance for dark mode.
                    //else if (control is DateTimePicker dtp)
                    //{
                    //    dtp.CalendarForeColor = myForeColor;
                    //    dtp.CalendarMonthBackground = myBackgroundColor;
                    //    dtp.CalendarTitleBackColor = Color.Gray;
                    //    dtp.CalendarTitleForeColor = myForeColor;
                    //}
                }
            }
        }


        // ********************************************************************************************************************
        // ** Public Methods:
        // ********************************************************************************************************************
        public void ChangeLanguage()
        {
            // List of strings which are in current User Control (Windows Form) and which we want to change to different language.
            // There is such method in each User Control (Windows Form) which is called from LangChanger static class.

            lblTaskDueDate.Text = LangChanger.GetString("Due date:");
            lblTaskPriority.Text = LangChanger.GetString("Priority:");
            lblRecTask.Text = LangChanger.GetString("Recurring task:");
            lblRTaskRepeat.Text = LangChanger.GetString("Repeat every");
            lblRTaskRemind.Text = LangChanger.GetString("Remind");
            lblRTaskDaysAfter.Text = LangChanger.GetString("days before due date.");
            lblTaskName.Text = LangChanger.GetString("Name:");
            lblTaskStatus.Text = LangChanger.GetString("Status:");
            lblTaskTag.Text = LangChanger.GetString("Tag:");
            lblTaskNote.Text = LangChanger.GetString("Note:");

            // Because of different language string sizes, we need to adjust the position of the controls
            AdjustLabelPositions();
        }

        // ********************************************************************************************************************
        // ** Private Methods:
        // ********************************************************************************************************************

        private void FormInit(FormType formType, string taskEditId)
        {
            // Populate the combo box 
            cmbRTask.Items.Add(LangChanger.GetString("day/days"));
            cmbRTask.Items.Add(LangChanger.GetString("week/weeks"));
            cmbRTask.Items.Add(LangChanger.GetString("month/months"));

            // Hide R-task controls by default
            lblRTaskRepeat.Visible = false;
            nudRTaskRepeatTime.Visible = false;
            cmbRTask.Visible = false;
            lblRTaskRemind.Visible = false;
            nudRTaskRemindTime.Visible = false;
            lblRTaskDaysAfter.Visible = false;

            // Adujst the position of the controls below the R-Task controls
            this.Height = 490; // Adjust the form height
            lblTaskName.Location = new Point(lblTaskName.Location.X, 135);
            txtTaskName.Location = new Point(txtTaskName.Location.X, 135);
            lblTaskStatus.Location = new Point(lblTaskStatus.Location.X, 170);
            txtTaskStatus.Location = new Point(txtTaskStatus.Location.X, 170);
            lblTaskTag.Location = new Point(lblTaskTag.Location.X, 205);
            txtTaskTag.Location = new Point(txtTaskTag.Location.X, 205);
            lblTaskNote.Location = new Point(lblTaskNote.Location.X, 240);
            txtTaskNote.Location = new Point(txtTaskNote.Location.X, 240);
            btnAddRecord.Location = new Point(btnAddRecord.Location.X, 400);

            if (formType == FormType.AddTask)
            {
                // Set the form title and button text for adding a task
                this.Text = LangChanger.GetString("Add new task");
                btnAddRecord.Text = LangChanger.GetString("Add new task");
            }
            else if (formType == FormType.EditTask)
            {
                // Set the form title and button text for editing a task
                this.Text = LangChanger.GetString("Edit task");
                btnAddRecord.Text = LangChanger.GetString("Edit task");

                // Populate the form fields with the existing task details
                LoadTaskDetails(taskEditId);
            }
            else if (formType == FormType.ReopenTask)
            {
                // Set the form title and button text for editing a task
                this.Text = LangChanger.GetString("Reopen task");
                btnAddRecord.Text = LangChanger.GetString("Reopen task");

                // Populate the form fields with the existing task details
                LoadTaskDetails(taskEditId);
            }   
        }
        private void EventHandlersInit()
        {
            btnAddRecord.Click += BtnAddRecord_Click;
            chkRTask.CheckedChanged += ChkRTask_CheckedChanged;

            // Validating event handlers
            txtTaskName.Validating += TxtTaskName_Validating;
            txtTaskNote.Validating += TxtTaskNote_Validating;
            dateTimePicker1.Validating += DateTimePicker1_Validating;
            nudRTaskRepeatTime.Validating += NudRTaskRepeatTime_Validating;
            cmbRTask.Validating += CmbRTask_Validating;
            nudRTaskRemindTime.Validating += NudRTaskRemindTime_Validating;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chkRTask checkbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkRTask_CheckedChanged(object? sender, EventArgs e)
        {
            if (chkRTask.Checked) 
            {
                // Show R-Task controls
                lblRTaskRepeat.Visible = true;
                nudRTaskRepeatTime.Visible = true;
                cmbRTask.Visible = true;
                lblRTaskRemind.Visible = true;
                nudRTaskRemindTime.Visible = true;
                lblRTaskDaysAfter.Visible = true;

                // Adujst the position of the controls below the R-Task controls
                this.Height = 525; // Adjust the form height
                lblTaskName.Location = new Point(lblTaskName.Location.X, 170); 
                txtTaskName.Location = new Point(txtTaskName.Location.X, 170); 
                lblTaskStatus.Location = new Point(lblTaskStatus.Location.X, 205); 
                txtTaskStatus.Location = new Point(txtTaskStatus.Location.X, 205); 
                lblTaskTag.Location = new Point(lblTaskTag.Location.X, 240); 
                txtTaskTag.Location = new Point(txtTaskTag.Location.X, 240); 
                lblTaskNote.Location = new Point(lblTaskNote.Location.X, 275); 
                txtTaskNote.Location = new Point(txtTaskNote.Location.X, 275); 
                btnAddRecord.Location = new Point(btnAddRecord.Location.X, 435);       
            }
            else 
            {
                // Hide R-Task controls
                lblRTaskRepeat.Visible = false;
                nudRTaskRepeatTime.Visible = false;
                cmbRTask.Visible = false;
                lblRTaskRemind.Visible = false;
                nudRTaskRemindTime.Visible = false;
                lblRTaskDaysAfter.Visible = false;

                // Adujst the position of the controls below the R-Task controls
                this.Height = 490; // Adjust the form height
                lblTaskName.Location = new Point(lblTaskName.Location.X, 135);
                txtTaskName.Location = new Point(txtTaskName.Location.X, 135);
                lblTaskStatus.Location = new Point(lblTaskStatus.Location.X, 170);
                txtTaskStatus.Location = new Point(txtTaskStatus.Location.X, 170);
                lblTaskTag.Location = new Point(lblTaskTag.Location.X, 205);
                txtTaskTag.Location = new Point(txtTaskTag.Location.X, 205);
                lblTaskNote.Location = new Point(lblTaskNote.Location.X, 240);
                txtTaskNote.Location = new Point(txtTaskNote.Location.X, 240);
                btnAddRecord.Location = new Point(btnAddRecord.Location.X, 400);
            }
        }

        /// <summary>
        /// Loads the task details into the form fields for editing.
        /// </summary>
        /// <param name="taskEditId"></param>
        private void LoadTaskDetails(string taskEditId)
        {
            // Retrieve the task from the data source
            var tasks = MainForm.MainFormInstance.tData;
            var task = tasks.ToDoTasks.FirstOrDefault(task => task.TaskId == taskEditId);

            if (task != null)
            {
                // Populate the form fields with the task details
                dateTimePicker1.Value = DateTime.Parse(task.TaskDueDate);
                txtTaskPriority.Text = task.TaskPriority;

                txtTaskName.Text = task.TaskName;
                txtTaskStatus.Text = task.TaskStatus;
                txtTaskTag.Text = task.TaskTag;
                txtTaskNote.Text = task.TaskNote;

                chkRTask.Checked = (task.TaskType.ToString() == "R") ? true : false; 

                if (chkRTask.Checked) 
                {
                    nudRTaskRepeatTime.Text = task.TaskRecurrenceTime.ToString(); 
                    switch (task.TaskRecurrenceType)
                    {
                        case 1:
                            cmbRTask.SelectedIndex = 0; // Daily
                            cmbRTask.Text = "***Daily";
                            break;
                        case 2:
                            cmbRTask.SelectedIndex = 1; // Weekly
                            cmbRTask.Text = "***Weekly";
                            break;
                        case 3:
                            cmbRTask.SelectedIndex = 2; // Monthly
                            cmbRTask.Text = "***Monthly";
                            break;
                        default:
                            cmbRTask.SelectedIndex = -1; // No item is selected
                            break;
                    }
                    nudRTaskRemindTime.Text = task.TaskRecurrenceReminderTime.ToString();
                }
            }
            else
            {
                MessageBox.Show("Task not found. AddTaskForm.cs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Validates the date in the DateTimePicker control. Date can't never be older than today.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimePicker1_Validating(object? sender, CancelEventArgs e)
        {
            if (dateTimePicker1.Value.Date < DateTime.Today)
            {
                errorProvider1.SetError(dateTimePicker1, "!");
                checksPassed = false;
            }
            else
            {
                errorProvider1.SetError(dateTimePicker1, string.Empty); // Clear the error
            }
        }

        /// <summary>
        /// Validates the task name input. It can't be empty.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtTaskName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaskName.Text))
            {
                errorProvider1.SetError(txtTaskName, "!");
                checksPassed = false;
                e.Cancel = false; // Cancel the event to prevent focus loss
            }
            else
            {
                errorProvider1.SetError(txtTaskName, string.Empty); // Clear the error      
            }
        }

        /// <summary>
        /// Validates the task note input. It can't be empty.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtTaskNote_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaskNote.Text))
            {
                errorProvider1.SetError(txtTaskNote, "!");
                checksPassed = false;
            }
            else
            {
                errorProvider1.SetError(txtTaskNote, string.Empty); // Clear the error
            }
        }

        /// <summary>
        /// Validates the task repeat time input. It can't be zero.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NudRTaskRepeatTime_Validating(object? sender, CancelEventArgs e)
        {
            if (chkRTask.Checked && nudRTaskRepeatTime.Value==0)
            {
                errorProvider1.SetError(nudRTaskRepeatTime, "!");
                checksPassed = false;
            }
            else
            {
                errorProvider1.SetError(nudRTaskRepeatTime, string.Empty); // Clear the error
            }
        }

        /// <summary>
        /// Validates the selected recurrence type in the combo box. It can't be empty.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbRTask_Validating(object? sender, CancelEventArgs e)
        {
            if (chkRTask.Checked && cmbRTask.SelectedIndex == -1)
            {
                errorProvider1.SetError(cmbRTask, "!");
                checksPassed = false;
            }
            else
            {
                errorProvider1.SetError(cmbRTask, string.Empty); // Clear the error
            }
        }

        /// <summary>
        /// Validates the reminder time input. It can't be zero.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NudRTaskRemindTime_Validating(object? sender, CancelEventArgs e)
        {
            if (chkRTask.Checked && nudRTaskRemindTime.Value==0)
            {
                errorProvider1.SetError(nudRTaskRemindTime, "!");
                checksPassed = false;
            }
            else
            {
                errorProvider1.SetError(nudRTaskRemindTime, string.Empty); // Clear the error
            }
        }

        /// <summary>
        /// Handles the Cancel button click event. Sets the dialog result to Cancel and closes the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelRecord_Click(object? sender, EventArgs e)
        {
            // Set the dialog result to Cancel to indicate the operation was canceled.
            DialogResult = DialogResult.Cancel;

            // Close the form.
            Close();
        }

        /// <summary>
        /// If all validations are passed, create a new task (or edit existing task) and close the form with an OK result.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddRecord_Click(object sender, EventArgs e)
        {
            // We need to run the validation also here. If there is any issue then the methods bellow will set checkPassed to false.
            checksPassed = true;

            DateTimePicker1_Validating(sender, new CancelEventArgs());
            TxtTaskName_Validating(sender, new CancelEventArgs());
            TxtTaskNote_Validating(sender, new CancelEventArgs());
            NudRTaskRepeatTime_Validating(sender, new CancelEventArgs());
            CmbRTask_Validating(sender, new CancelEventArgs());
            NudRTaskRemindTime_Validating(sender, new CancelEventArgs());

            // If all fields are valid, proceed
            if (checksPassed)
            {
                // Reseting the values of the R-Task controls if the checkbox is not checked
                if (!chkRTask.Checked)
                {
                    nudRTaskRepeatTime.Value =0;
                    nudRTaskRemindTime.Value =0;
                    cmbRTask.SelectedIndex = -1; // No item is selected
                }

                // Create a new ToDoTask object based on the form inputs
                // newTask is public property so we can use it easily in UcToDo.cs

                newTask = new ToDoTask
                {
                    // Generate a unique ID for the task but only in case of adding a new task, otherwise use the existing ID
                    TaskId = string.IsNullOrEmpty(taskEditId) ? Guid.NewGuid().ToString() : taskEditId, 

                    TaskDueDate = dateTimePicker1.Value.ToString("yyyy-MM-dd"),
                    TaskPriority = txtTaskPriority.Text,
                    TaskArchived = false, // Set to false by default when adding a new task
                    TaskType = (chkRTask.Checked) ? 'R' : 'N', // R - recurring, N - normal
                    
                    TaskRecurrenceType = cmbRTask.SelectedIndex + 1,            // 0 - Not recurring task, 1 - daily, 2 - weekly, 3 - monthly.
                    TaskRecurrenceTime = (int)nudRTaskRepeatTime.Value,         // In days/weeks/months based on TaskRecurrenceType.
                    TaskRecurrenceReminderTime =(int)nudRTaskRemindTime.Value,  // Remind (show in the ongoing tasks) in X day before task due date.
                    TaskRecurrenceShow = false,                                 // Real value will be counted in separate method in UcToDo.cs

                    TaskName = txtTaskName.Text,
                    TaskStatus = txtTaskStatus.Text,
                    TaskTag = txtTaskTag.Text,
                    TaskNote = txtTaskNote.Text
                };

                // Close the form with an OK result
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        // **************************************************************************************************************************
        // ** Methods for localization purposes:                                                                                 

        /// <summary>
        /// Adjusts the positions of the labels in the form based on their corresponding controls.
        /// This method is for localization purposes, as the size of the labels may change based on the language.
        /// </summary>
        private void AdjustLabelPositions()
        {
            // PART 1: Align the labels to the right of the corresponding text boxes and controls
            const int c = 5;
            lblTaskDueDate.Location = new Point(dateTimePicker1.Location.X - lblTaskDueDate.Width - c, lblTaskDueDate.Location.Y);
            lblTaskPriority.Location = new Point(txtTaskPriority.Location.X - lblTaskPriority.Width - c, lblTaskPriority.Location.Y);
            lblRecTask.Location = new Point(chkRTask.Location.X - lblRecTask.Width -c, lblRecTask.Location.Y);

            nudRTaskRepeatTime.Location = new Point(lblRTaskRepeat.Location.X + lblRTaskRepeat.Width + 2*c, nudRTaskRepeatTime.Location.Y);
            cmbRTask.Location = new Point(nudRTaskRepeatTime.Location.X + nudRTaskRepeatTime.Width + 4*c, cmbRTask.Location.Y);

            nudRTaskRemindTime.Location = new Point(lblRTaskRemind.Location.X + lblRTaskRemind.Width + 2*c, nudRTaskRemindTime.Location.Y);
            lblRTaskDaysAfter.Location = new Point(nudRTaskRemindTime.Location.X + nudRTaskRemindTime.Width +4*c, lblRTaskDaysAfter.Location.Y);

            lblTaskName.Location = new Point(txtTaskName.Location.X - lblTaskName.Width - c, lblTaskName.Location.Y);
            lblTaskStatus.Location = new Point(txtTaskStatus.Location.X - lblTaskStatus.Width - c, lblTaskStatus.Location.Y);
            lblTaskTag.Location = new Point(txtTaskTag.Location.X - lblTaskTag.Width - c, lblTaskTag.Location.Y);
            lblTaskNote.Location = new Point(txtTaskNote.Location.X - lblTaskNote.Width - c, lblTaskNote.Location.Y);

            // PART 2: Find out the minumum-x value for the controls
            int minX = GetExtremeXPosition("Left");

            // PART 3: Add deltaX to all the controls, this will move them to the left.
            int deltaX = 30 - minX;

            foreach (Control control in this.Controls)
            {
                if (control is not Button)
                {
                    // Adjust the X position of the control
                    control.Location = new Point(control.Location.X + deltaX, control.Location.Y);
                }
            }

            // PART 4: Find out the maximum-x value (+width) for the cotnrols
            int maxX = GetExtremeXPosition("Right");

            // PART 5: Adjust the width of the form
            this.Width = maxX + 50; // Add some padding to the right side of the form

        }

        /// <summary>
        /// Finds the extreme X position of the controls in the form based on the specified direction ("Left" or "Right").
        /// This method is for localization purposes.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        private int GetExtremeXPosition(string direction)
        {
            // For "Left", initialize result to int.MaxValue and update it with the smallest Location.X value. For the "Right" the opposite.
            int result = direction == "Left" ? int.MaxValue : int.MinValue;

            foreach (Control control in this.Controls)
            {

                if (direction == "Left" && control is not Button)
                {
                    // Find the control with the lowest X position
                    if (control.Location.X < result)
                    {
                        result = control.Location.X;
                    }
                }
                else if (direction == "Right" && control is not Button)
                {
                    // Find the control with the maximum X + Width position
                    if ((control.Location.X + control.Width) > result)
                    {
                        result = control.Location.X + control.Width;
                    }
                }
            }

            return result;
        }








    }
}

