using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SBR.Forms
{

    public enum FormType
    {
        AddTask,
        EditTask,
        ReopenTask
    }
    public partial class UcToDo : UserControl
    {

        private enum FilterType
        {
            OnGoingTasks,
            ArchivedTasks,
            RecurringTasks
        }

        // Define a delegate for the event
        public delegate void TaskCountUpdatedEventHandler(int numberOfTasks, int toDoButtonColor);

        // Define the event
        public event TaskCountUpdatedEventHandler? TaskCountUpdated;

        private bool isMidnight = false;
        private DataToDoList tData;
        private ContextMenuStrip contextMenu;
        private string selectedTaskId; // Store the ID of the selected task for context menu actions

        public UcToDo()
        {
            InitializeComponent();
            DataGridViewInit();
            PopulateAndSortDataGridView();
            CalculateTaskRecurrenceShow();
        }

        private void UcToDo_Load(object sender, EventArgs e)
        {
            // It's a bit unfortunate to call DeleteOldArchivedTasksFromFile() after PopulateAndSortDataGridView()
            // but for the ToDo button text and color we need to call PopulateAndSortDataGridView() asap.
            // And I also want to call as minimum methods in constructor as possible.
            DeleteOldArchivedTasksFromFile();

            CalculateTaskRecurrenceShow();
            EventHandlersInit();
            ContextMenuInit();

            rdoOngoingTasks.Checked = true; // Set the default filter to Ongoing tasks.
            // No need to run FilterDataGridView(FilterType.OnGoingTasks) here. FilterRadioButtons_CheckedChanged() will do it.
            // Same for UpdateRowFontColors();

            if (MainForm.MainFormInstance.cData.HidePriorityColumn)
            {
                chkHideColumns.Checked = true;
            }
            else
            {
                chkHideColumns.Checked = false;
            }

        }


        // ********************************************************************************************************************
        // ** Public Methods:
        // ********************************************************************************************************************

        /// <summary>
        /// Counts the number of tasks in the DataGridView (excluding archived tasks) and also the color for the ToDo button text.
        /// </summary>
        public void UpdateNumberOfTasks()
        {
            int numberOfTasks = 0; // Reset the count
            int toDoButtonTextColor = 0; // Default white color

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Filter only for Ongoing tasks (normal + recurring where TaskRecurrenceShow = true)
                if (!row.IsNewRow &&
                     row.Cells["TaskArchived"].Value.ToString() == "False" && (
                     row.Cells["TaskType"].Value.ToString() == "N" ||
                     row.Cells["TaskRecurrenceShow"].Value.ToString() =="True"))
                {
                    numberOfTasks++;

                    // For the button color purposes:
                    // Parse the TaskDueDate using DateTime.ParseExact
                    if (DateTime.TryParseExact(
                            row.Cells["TaskDueDate"].Value?.ToString(),
                            "yyyy-MM-dd",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out DateTime taskDueDate))
                    {
                        // Calculate the difference in days between TaskDueDate and today's date.
                        int daysDifference = (taskDueDate - DateTime.Today).Days;


                        if (daysDifference < 4)
                        {
                            toDoButtonTextColor = 3;  // Less than 4 days: Red     
                        }
                        else if (daysDifference < 7)
                        {
                            if (toDoButtonTextColor <2)   // Less than 7 days: Orange
                                toDoButtonTextColor=2;
                        }
                        else if (daysDifference < 10)
                        {
                            if (toDoButtonTextColor <1)   // Less than 10 days: Blue
                                toDoButtonTextColor=1;
                        }
                    }
                    else
                    {
                        throw new Exception("Invalid or null TaskDueDate encountered.");
                    }
                }
            }

            lblTotalTasksNumber.Text = numberOfTasks.ToString();


            // Raise the TaskCountUpdated event.
            TaskCountUpdated?.Invoke(numberOfTasks, toDoButtonTextColor);

        }
        public void ChangeLanguage()
        {
            // List of strings which are in current User Control (Windows Form) and which we want to change to different language.
            // There is such method in each User Control (Windows Form) which is called from LangChanger static class.

            btnAddTask.Text = LangChanger.GetString("Add new task");
            lblTotalTasksText.Text = LangChanger.GetString("Tasks:");
            lblShow.Text = LangChanger.GetString("Show tasks:");
            lblRecurringTasks.Text = LangChanger.GetString("Recurring tasks:");
            lblArchivedTasks.Text = LangChanger.GetString("Archived tasks:");
            rdoOngoingTasks.Text = LangChanger.GetString("Ongoing");
            rdoRecurringTasks.Text = LangChanger.GetString("Recurring");
            rdoArchivedTasks.Text = LangChanger.GetString("Archived");
            chkHideColumns.Text = LangChanger.GetString("Hide priority, tag and status column.");

            dataGridView1.Columns["TaskDueDate"].HeaderText= LangChanger.GetString("Due date");
            dataGridView1.Columns["TaskPriority"].HeaderText= LangChanger.GetString("P");
            dataGridView1.Columns["TaskType"].HeaderText= LangChanger.GetString("T");
            dataGridView1.Columns["TaskName"].HeaderText= LangChanger.GetString("Name");
            dataGridView1.Columns["TaskStatus"].HeaderText= LangChanger.GetString("Status");
            dataGridView1.Columns["TaskTag"].HeaderText= LangChanger.GetString("Tag");
            dataGridView1.Columns["TaskNote"].HeaderText= LangChanger.GetString("Note");

            // For the localization purposes.
            lblTotalTasksNumber.Location = new Point(
                                               lblTotalTasksText.Location.X + lblTotalTasksText.Width,
                                               lblTotalTasksText.Location.Y - 2);


            rdoRecurringTasks.Location = new Point(
                                             rdoOngoingTasks.Location.X + rdoOngoingTasks.Width,
                                             rdoRecurringTasks.Location.Y);

            rdoArchivedTasks.Location = new Point(
                                            rdoRecurringTasks.Location.X + rdoRecurringTasks.Width,
                                            rdoArchivedTasks.Location.Y);

            lblShow.Location = new Point(
                                   rdoRecurrenceTasks.Location.X - lblShow.Width,
                                   lblShow.Location.Y);


            // We need to call this method here as well , because we are changing some texts in context menu.
            ContextMenuInit();

        }


        // ********************************************************************************************************************
        // ** Private Methods:
        // ********************************************************************************************************************

        private void EventHandlersInit()
        {
            btnAddTask.Click += BtnAddTask_Click;

            rdoOngoingTasks.CheckedChanged   += FilterRadioButtons_CheckedChanged;
            rdoArchivedTasks.CheckedChanged  += FilterRadioButtons_CheckedChanged;
            rdoRecurringTasks.CheckedChanged += FilterRadioButtons_CheckedChanged;

            chkHideColumns.CheckedChanged += ChkHideColumns_CheckedChanged;

            // Handle MouseDown to show the context menu
            dataGridView1.MouseDown += DataGridView1_MouseDown;
        }

        /// <summary>
        /// Initializes the DataGridView and its columns.
        /// </summary>
        private void DataGridViewInit()
        {
            // Add columns to dataGridView1
            dataGridView1.Columns.Add("TaskID", "ID");
            dataGridView1.Columns["TaskID"].Visible = false; // Hide the ID column
            dataGridView1.Columns.Add("TaskDueDate", "***Due date");
            dataGridView1.Columns.Add("TaskPriority", "***P");
            dataGridView1.Columns.Add("TaskArchived", "Archived");
            dataGridView1.Columns["TaskArchived"].Visible = false; // Hide the TaskArchived column
            dataGridView1.Columns.Add("TaskType", "***T");

            dataGridView1.Columns.Add("TaskRecurrenceType", "TaskRecurrenceType"); // daily/weekly/monthly
            dataGridView1.Columns["TaskRecurrenceType"].Visible = false; // Hide the TaskRecurrenceType column
            dataGridView1.Columns.Add("TaskRecurrenceTime", "TaskRecurrenceTime");
            dataGridView1.Columns["TaskRecurrenceTime"].Visible = false; // Hide the TaskRecurrenceTime column
            dataGridView1.Columns.Add("TaskRecurrenceReminderTime", "TaskRecurrenceReminder");
            dataGridView1.Columns["TaskRecurrenceReminderTime"].Visible = false; // Hide the TaskRecurrenceReminder column
            dataGridView1.Columns.Add("TaskRecurrenceShow", "TaskRecurrenceShow");
            dataGridView1.Columns["TaskRecurrenceShow"].Visible = false; // Hide the TaskRecurrenceShow column

            dataGridView1.Columns.Add("TaskName", "***Name");
            dataGridView1.Columns.Add("TaskStatus", "***Status");
            dataGridView1.Columns.Add("TaskTag", "***Tag");
            dataGridView1.Columns.Add("TaskNote", "***Note");


            // Set fixed widths for specific columns
            dataGridView1.Columns["TaskName"].Width = 150;
            dataGridView1.Columns["TaskStatus"].Width = 90;
            dataGridView1.Columns["TaskTag"].Width = 90;
            dataGridView1.Columns["TaskPriority"].Width = 90; 


            // Disable auto-sizing for these columns
            dataGridView1.Columns["TaskName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns["TaskStatus"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns["TaskTag"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns["TaskPriority"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;


            // Adjust column widths to match text size
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
       
            // Adjust header widths to match text size
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            // Prevent text wrapping in column headers
            dataGridView1.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            // Set the "Note" column to fill the remaining space
            dataGridView1.Columns["TaskNote"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Make the row header (blank) column non-resizable
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            // Hide the last (empty) row
            dataGridView1.AllowUserToAddRows = false;

            // Make the DataGridView read-only
            dataGridView1.ReadOnly = true;

            // Prevent users from resizing row heights
            dataGridView1.AllowUserToResizeRows = false;

            // Set the width of the first column (row header column)
            dataGridView1.RowHeadersWidth = 12;

            // Set the font size to 9.75
            dataGridView1.DefaultCellStyle.Font = new Font(dataGridView1.DefaultCellStyle.Font.FontFamily, 9.75f);

            // Set the font size for the headers
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.ColumnHeadersDefaultCellStyle.Font.FontFamily, 9.75f);

        }

        /// <summary>
        /// Initializes the context menu for the DataGridView.
        /// </summary>
        private void ContextMenuInit()
        {
            //// Create the ContextMenuStrip
            contextMenu = new ContextMenuStrip();
     

            // Determine colors based on the current mode
            Color backgroundColor = MainForm.MainFormInstance.DarkMode ? Color.FromArgb(80, 80, 80) : SystemColors.Control;
            Color textColor = MainForm.MainFormInstance.DarkMode ? Color.WhiteSmoke : SystemColors.ControlText;

            // Set the context menu's background and text colors
            contextMenu.BackColor = backgroundColor;
            contextMenu.ForeColor = textColor;

            // Add menu items
            if (rdoOngoingTasks.Checked)
            {
                var editTaskItem = new ToolStripMenuItem(LangChanger.GetString("Edit task"), null, (sender, e) => EditMenuItem_Click(sender, e, FormType.EditTask));
                var finishTaskItem = new ToolStripMenuItem(LangChanger.GetString("Finish task"), null, FinishMenuItem_Click);

                // Apply colors to menu items
                editTaskItem.BackColor = backgroundColor;
                editTaskItem.ForeColor = textColor;
                finishTaskItem.BackColor = backgroundColor;
                finishTaskItem.ForeColor = textColor;

                editTaskItem.Image = ResourcesIconsDir.ResourcesIcons.edit_icon;
                finishTaskItem.Image = ResourcesIconsDir.ResourcesIcons.finish_icon;

                contextMenu.Items.Add(editTaskItem);
                contextMenu.Items.Add(finishTaskItem);
            }
            else if (rdoRecurringTasks.Checked)
            {
                var editTaskItem = new ToolStripMenuItem(LangChanger.GetString("Edit task"), null, (sender, e) => EditMenuItem_Click(sender, e, FormType.EditTask));
                var archiveTaskItem = new ToolStripMenuItem(LangChanger.GetString("Archive task"), null, FinishMenuItem_Click);

                // Apply colors to menu items
                editTaskItem.BackColor = backgroundColor;
                editTaskItem.ForeColor = textColor;
                archiveTaskItem.BackColor = backgroundColor;
                archiveTaskItem.ForeColor = textColor;

                editTaskItem.Image = ResourcesIconsDir.ResourcesIcons.edit_icon;
                archiveTaskItem.Image = ResourcesIconsDir.ResourcesIcons.archive_icon;

                contextMenu.Items.Add(editTaskItem);
                contextMenu.Items.Add(archiveTaskItem);
            }
            else if (rdoArchivedTasks.Checked)
            {
                var reopenTaskItem = new ToolStripMenuItem(LangChanger.GetString("Reopen task"), null, (sender, e) => EditMenuItem_Click(sender, e, FormType.ReopenTask));

                // Apply colors to menu items
                reopenTaskItem.BackColor = backgroundColor;
                reopenTaskItem.ForeColor = textColor;

                // Add icon to the menu item
                reopenTaskItem.Image = ResourcesIconsDir.ResourcesIcons.reopen_icon;

                contextMenu.Items.Add(reopenTaskItem);
            }

            // Warning: This code caused that the context menu was shown even on header or bellow the table.
            // Assign the ContextMenuStrip to the DataGridView
            // dataGridView1.ContextMenuStrip = contextMenu;
        }

        /// <summary>
        /// Handles the MouseDown event for the DataGridView to show the context menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView1_MouseDown(object sender, MouseEventArgs e)
        {

            // Check if the right mouse button was clicked
            if (e.Button == MouseButtons.Right)
            {

                // Get the row under the mouse pointer
                var hitTestInfo = dataGridView1.HitTest(e.X, e.Y);
                //Debug.Print($"RowIndex: {hitTestInfo.RowIndex}, Type: {hitTestInfo.Type}");

                if (hitTestInfo.RowIndex >= 0 && hitTestInfo.Type == DataGridViewHitTestType.Cell)
                {
                    // Select the row under the mouse pointer
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[hitTestInfo.RowIndex].Selected = true;

                    // Store the TaskId of the selected row
                    selectedTaskId = dataGridView1.Rows[hitTestInfo.RowIndex].Cells["TaskID"].Value?.ToString();


                    // Maybe the DarkMode was changed so we have to call this mehtod to re-init the menu.
                    ContextMenuInit();

                    // Show the context menu
                    contextMenu.Show(dataGridView1, e.Location);
                }
            }

        }

        // ********************************************************************************************************************
        // ** Methods for Populating/Filtering/Counting/Coloring/Sorting/Deleting DataGridView:

        /// <summary>
        /// Populate (and sort) the DataGridView with the ALL (Ongoing/Archived/Recurring tasks) data from the tData object.
        /// </summary>
        private void PopulateAndSortDataGridView()
        {
            tData = MainForm.MainFormInstance.tData;

            dataGridView1.Rows.Clear();

            // Sort tasks by TaskDueDate in ascending order
            var sortedTasks = tData.ToDoTasks.OrderBy(task => task.TaskDueDate).ToList();


            // Add rows to the DataGridView based on the sorted tasks
            foreach (var task in sortedTasks)
            {
                dataGridView1.Rows.Add(
                    task.TaskId,

                    task.TaskDueDate,
                    task.TaskPriority,
                    task.TaskArchived,
                    task.TaskType,

                    task.TaskRecurrenceType,
                    task.TaskRecurrenceTime,
                    task.TaskRecurrenceReminderTime,
                    task.TaskRecurrenceShow,

                    task.TaskName,
                    task.TaskStatus,
                    task.TaskTag,
                    task.TaskNote
                );
            }
        }

        /// <summary>
        /// Sorts the DataGridView items by the TaskDueDate column in ascending order (oldest at the top).
        /// </summary>
        private void SortDataGridView()
        {
            // Ensure the TaskDueDate column exists and is sortable
            if (dataGridView1.Columns["TaskDueDate"] != null)
            {
                // Sort the DataGridView by the TaskDueDate column in ascending order
                dataGridView1.Sort(dataGridView1.Columns["TaskDueDate"], ListSortDirection.Ascending);
            }
            else
            {
                MessageBox.Show("The TaskDueDate column is missing or not sortable.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Filter for Ongoing/Archived/Recurring tasks.
        /// </summary>
        /// <param name="a"></param>
        private void FilterDataGridView(FilterType filterType)
        {
            if (filterType==FilterType.OnGoingTasks) // Filter only for Ongoing tasks (normal + recurring where TaskRecurrenceShow = true)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    if (!row.IsNewRow) // Exclude the last row
                    {
                        // The string representation of a bool in .NET is "True" or "False" (with a capital "T" or "F").
                        if (row.Cells["TaskArchived"].Value.ToString() == "False" && (
                            row.Cells["TaskType"].Value.ToString() == "N" ||
                            row.Cells["TaskRecurrenceShow"].Value.ToString() =="True"))
                        {
                            row.Visible = true;
                        }
                        else
                        {
                            row.Visible = false;
                        }
                    }
                }
            }

            if (filterType==FilterType.ArchivedTasks) // Filter only for Archived tasks
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow) // Exclude the last row
                    {
                        row.Visible = row.Cells["TaskArchived"].Value.ToString() == "True";
                    }
                }
            }

            if (filterType==FilterType.RecurringTasks) // Filter only for Recurring tasks (which are of course not Archived)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow) // Exclude the last row
                    {
                        if (row.Cells["TaskArchived"].Value.ToString() == "False" &&
                            row.Cells["TaskType"].Value.ToString() == "R")
                        {
                            row.Visible = true;
                        }
                        else
                        {
                            row.Visible = false;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Updates the font color of specific rows in the DataGridView based on a condition.
        /// </summary>
        private void UpdateRowFontColors()
        {
            // Debug.Print("UpdateRowFontColors()");
            // Disable visual styles for headers to allow custom styles
            dataGridView1.EnableHeadersVisualStyles = false;

            if (rdoArchivedTasks.Checked)
            {
                // If archived tasks are shown, set the font color to white
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow) // Exclude the last (empty) row
                    {
                        row.HeaderCell.Style.BackColor = Color.White; // Default background color
                    }
                }
            }
            else // For non archived tasks select apropriate color
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow) // Exclude the last (empty) row 
                    {
                        // Parse the TaskDueDate using DateTime.ParseExact
                        if (DateTime.TryParseExact(
                                row.Cells["TaskDueDate"].Value?.ToString(),
                                "yyyy-MM-dd",
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None,
                                out DateTime taskDueDate))
                        {
                            // Calculate the difference in days between TaskDueDate and today's date
                            int daysDifference = (taskDueDate - DateTime.Today).Days;

                            // Set background color of the row header cell based on the difference
                            if (daysDifference < 4)
                            {
                                row.HeaderCell.Style.BackColor = Color.FromArgb(255, 70, 81); // Less than 4 days: Red
                            }
                            else if (daysDifference < 7)
                            {
                                row.HeaderCell.Style.BackColor = Color.FromArgb(255, 165, 0); // Less than 7 days: Orange

                            }
                            else if (daysDifference < 10)
                            {
                                row.HeaderCell.Style.BackColor = Color.FromArgb(99, 183, 183); // Less than 10 days: Blue
                            }
                            else
                            {
                                row.HeaderCell.Style.BackColor = Color.White; // Default background color
                            }
                        }
                        else
                        {
                            // If TaskDueDate is invalid or null, use default background color
                            row.HeaderCell.Style.BackColor = Color.White;
                        }
                    }
                }
            }


        }

        /// <summary>
        /// Deletes archived tasks (TaskArchived = true) from tData if their DueDate is older than 30 days.
        /// Updates the DataGridView and the JSON file.
        /// </summary>
        private void DeleteOldArchivedTasksFromFile()
        {
            // Get the current date
            DateTime currentDate = DateTime.Today;

            // Filter out tasks that are archived and older than 30 days
            var tasksToKeep = tData.ToDoTasks.Where(task =>
            {
                if (task.TaskArchived && DateTime.TryParseExact(task.TaskDueDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dueDate))
                {
                    return (currentDate - dueDate).Days <= 30; // Keep tasks that are not older than 30 days
                }
                return true; // Keep non-archived tasks
            }).ToList();

            // Update tData with the filtered tasks
            tData.ToDoTasks = tasksToKeep;

            // Update the DataGridView
            PopulateAndSortDataGridView();

            // Save the updated task list to the JSON file
            JsonMethods.FileSaveTasks(tData);
        }


        /// <summary>
        /// Calculate if we can display Recurring task in the Ongoing tasks.
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void CalculateTaskRecurrenceShow()
        {
            // It is probably not necessary to update tData, dataGridView1 is enough.
            // Warning: We are not storing TaskRecurrenceShow real status in tData or json file. Just in dataGridView1.

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow &&
                     row.Cells["TaskType"].Value?.ToString() == "R" &&
                     row.Cells["TaskArchived"].Value?.ToString() == "False")
                {
                    if (DateTime.TryParseExact(row.Cells["TaskDueDate"].Value?.ToString(), "yyyy-MM-dd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime taskDueDate))
                    {
                        // if (Today > TaskDueDate - TaskRecurrenceReminderTime) then we can display the task in OnGoing tasks.
                        if (DateTime.Today >= taskDueDate.AddDays(-Convert.ToDouble(row.Cells["TaskRecurrenceReminderTime"].Value)))
                        {
                            row.Cells["TaskRecurrenceShow"].Value = true;
                        }
                        else
                        {
                            row.Cells["TaskRecurrenceShow"].Value = false;
                        }
                    }
                    else
                    {
                        throw new Exception("Invalid or null TaskDueDate encountered.");
                    }
                }
            }
        }

        // ********************************************************************************************************************
        // Mehtods for Adding/Editing/Finishing task:

        /// <summary>
        /// Handles the Add New Task (button click) event. 
        /// Mehtod will dark the background of the MainForm and then will call OpenAddTaskDialog() method to show the AddTaskForm.
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddTask_Click(object sender, EventArgs e)
        {
            // Dark the background of the MainForm and show the AddTaskForm  
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

                OpenAddTaskDialog(); // Open the AddTaskForm

                overlay.Close();
            }
        }

        /// <summary>
        /// Mehtod will open AddTaskForm and then will udpate DataGridView1, tData object and also will save the new task to the JSON file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenAddTaskDialog()
        {
            // Open the AddTaskForm
            using (var addTaskForm = new AddTaskForm(FormType.AddTask, String.Empty))
            {
                if (addTaskForm.ShowDialog() == DialogResult.OK)
                {
                    // Get the newTask from the Form
                    var newTask = addTaskForm.newTask;

                    // Add the newTask to the DataGridView
                    dataGridView1.Rows.Add(
                        newTask.TaskId,
                        newTask.TaskDueDate,
                        newTask.TaskPriority,
                        newTask.TaskArchived,
                        newTask.TaskType,
                        newTask.TaskRecurrenceType,
                        newTask.TaskRecurrenceTime,
                        newTask.TaskRecurrenceReminderTime,
                        newTask.TaskRecurrenceShow,
                        newTask.TaskName,
                        newTask.TaskStatus,
                        newTask.TaskTag,
                        newTask.TaskNote
                    );

                    // Update tData with the newTask
                    tData = MainForm.MainFormInstance.tData;
                    tData.ToDoTasks.Add(newTask);

                    // Save the json file
                    JsonMethods.FileSaveTasks(tData);

                    CalculateTaskRecurrenceShow();
                    FilterRadioButtons_CheckedChanged(null, EventArgs.Empty);
                    UpdateNumberOfTasks();
                    SortDataGridView();
                }
            }
        }

        /// <summary>
        /// Handles the Edit/Reopen Task (context menu click) event. 
        /// Mehtod will udpate DataGridView1, tData object and also will save the editedTask to the JSON file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditMenuItem_Click(object sender, EventArgs e, FormType formType)
        {
            // Open the AddTaskForm
            using (var addTaskForm = new AddTaskForm(formType, selectedTaskId))
            {
                if (addTaskForm.ShowDialog() == DialogResult.OK)
                {
                    // Get the udpated (edited) task (ToDoTask class) from the Form
                    var editedTask = addTaskForm.newTask;

                    // Now we need to update the DataGridView1 with the edited task:
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["TaskID"].Value?.ToString() == selectedTaskId)
                        {
                            // Update the row with the updatedTask data
                            row.Cells["TaskDueDate"].Value = editedTask.TaskDueDate;
                            row.Cells["TaskPriority"].Value = editedTask.TaskPriority;
                            row.Cells["TaskArchived"].Value = editedTask.TaskArchived;
                            row.Cells["TaskType"].Value = editedTask.TaskType;

                            row.Cells["TaskRecurrenceType"].Value = editedTask.TaskRecurrenceType;
                            row.Cells["TaskRecurrenceTime"].Value = editedTask.TaskRecurrenceTime;
                            row.Cells["TaskRecurrenceReminderTime"].Value = editedTask.TaskRecurrenceReminderTime;
                            row.Cells["TaskRecurrenceShow"].Value = editedTask.TaskRecurrenceShow;

                            row.Cells["TaskName"].Value = editedTask.TaskName;
                            row.Cells["TaskStatus"].Value = editedTask.TaskStatus;
                            row.Cells["TaskTag"].Value = editedTask.TaskTag;
                            row.Cells["TaskNote"].Value = editedTask.TaskNote;
                            break;
                        }
                    }

                    // Update tData with the editedTask
                    tData = MainForm.MainFormInstance.tData;
                    var taskToUpdate = tData.ToDoTasks.FirstOrDefault(task => task.TaskId == selectedTaskId);
                    if (taskToUpdate != null)
                    {
                        taskToUpdate.TaskDueDate = editedTask.TaskDueDate;
                        taskToUpdate.TaskPriority = editedTask.TaskPriority;
                        taskToUpdate.TaskArchived = editedTask.TaskArchived;
                        taskToUpdate.TaskType = editedTask.TaskType;

                        taskToUpdate.TaskRecurrenceType = editedTask.TaskRecurrenceType;
                        taskToUpdate.TaskRecurrenceTime = editedTask.TaskRecurrenceTime;
                        taskToUpdate.TaskRecurrenceReminderTime = editedTask.TaskRecurrenceReminderTime;
                        taskToUpdate.TaskRecurrenceShow = editedTask.TaskRecurrenceShow;

                        taskToUpdate.TaskName = editedTask.TaskName;
                        taskToUpdate.TaskStatus = editedTask.TaskStatus;
                        taskToUpdate.TaskTag = editedTask.TaskTag;
                        taskToUpdate.TaskNote = editedTask.TaskNote;
                    }

                    // Save the json file
                    JsonMethods.FileSaveTasks(tData); ;

                    CalculateTaskRecurrenceShow();
                    FilterRadioButtons_CheckedChanged(null, EventArgs.Empty);
                    // UpdateRowFontColors(); - this is already called in FilterRadioButtons_CheckedChanged()
                    UpdateNumberOfTasks();
                    SortDataGridView();

                }
            }
        }

        /// <summary>
        /// Handles the Finish task / Archive task (context menu click) event. 
        /// Mehtod will udpate DataGridView1, tData object and also will save the editedTask to the JSON file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinishMenuItem_Click(object sender, EventArgs e)
        {
            // In fact we are not deleting finished tasks, but we are archiving them for the next 30 days.

            tData = MainForm.MainFormInstance.tData;
            var taskToUpdate = tData.ToDoTasks.FirstOrDefault(task => task.TaskId == selectedTaskId);  // Get the sellected task

            if (taskToUpdate != null)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // For Normal tasks just set the TaskArchived to true and that is all.
                    if (row.Cells["TaskID"].Value?.ToString() == selectedTaskId &&
                        row.Cells["TaskType"].Value?.ToString()=="N")
                    {
                        row.Cells["TaskArchived"].Value = true;
                        taskToUpdate.TaskArchived = true;
                    }
                    // There are two options for R-tasks: 
                    // 1. We are calling this method from Ongoing tasks (Finish task).
                    //    In such case we need to recalculate the DueDate and we are not Archiving such task!
                    else if (row.Cells["TaskID"].Value?.ToString() == selectedTaskId &&
                             row.Cells["TaskType"].Value?.ToString() == "R" &&
                             rdoOngoingTasks.Checked)
                    {
                        DateTime taskDueDate = DateTime.ParseExact(row.Cells["TaskDueDate"].Value?.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        DateTime newTaskDueDate;

                        switch (Convert.ToInt32(row.Cells["TaskRecurrenceType"].Value))
                        {
                            case 1: // Daily
                                newTaskDueDate = taskDueDate.AddDays(Convert.ToDouble(row.Cells["TaskRecurrenceTime"].Value));
                                break;
                            case 2: // Weekly
                                newTaskDueDate = taskDueDate.AddDays(Convert.ToDouble(row.Cells["TaskRecurrenceTime"].Value) * 7);
                                break;
                            case 3: // Monthly
                                newTaskDueDate = taskDueDate.AddMonths(Convert.ToInt32(row.Cells["TaskRecurrenceTime"].Value));
                                break;
                            default:
                                throw new Exception("Invalid TaskRecurrenceType encountered.");
                        }

                        row.Cells["TaskDueDate"].Value = newTaskDueDate.ToString("yyyy-MM-dd");
                        taskToUpdate.TaskDueDate = newTaskDueDate.ToString("yyyy-MM-dd");
                    }
                    // There are two options for R-tasks: 
                    // 2. We are calling this method from Recurring tasks (Archive task).
                    //    In such case we are archiving such task. 
                    else if (row.Cells["TaskID"].Value?.ToString() == selectedTaskId &&
                             row.Cells["TaskType"].Value?.ToString() == "R" &&
                             rdoRecurringTasks.Checked)
                    {
                        row.Cells["TaskArchived"].Value = true;
                        taskToUpdate.TaskArchived = true;
                    }
                }

                // Save the updated task list to the JSON file
                JsonMethods.FileSaveTasks(tData);

                CalculateTaskRecurrenceShow();
                FilterRadioButtons_CheckedChanged(null, EventArgs.Empty);
                UpdateNumberOfTasks();
                SortDataGridView();
            }
            else
            {
                MessageBox.Show("Task not found FI.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        // **********************************************************************************************************************
        // ** Check boxes:

        /// <summary>
        /// Show/hide some columns in the DataGridView based on the checkbox state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkHideColumns_CheckedChanged(object? sender, EventArgs e)
        {
            dataGridView1.Columns["TaskTag"].Visible = !dataGridView1.Columns["TaskTag"].Visible;
            dataGridView1.Columns["TaskPriority"].Visible = !dataGridView1.Columns["TaskPriority"].Visible;
            dataGridView1.Columns["TaskStatus"].Visible = !dataGridView1.Columns["TaskStatus"].Visible;

            if (chkHideColumns.Checked)
            {
                MainForm.MainFormInstance.cData.HidePriorityColumn = true;
            }
            else
            {
                MainForm.MainFormInstance.cData.HidePriorityColumn = false;
            }
        }


        /// <summary>
        /// Show/hide archived tasks in the DataGridView based on the checkbox state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterRadioButtons_CheckedChanged(object? sender, EventArgs e)
        {
            // If the radio buttons are inside a GroupBox or Panel, changing the Checked state of one radio button 
            // will automatically uncheck the others, triggering their CheckedChanged events as well.
            if (sender is RadioButton radioButton && !radioButton.Checked)
            {
                // Ignore the event if the radio button is being unchecked
                return;
            }

            if (rdoOngoingTasks.Checked)
            {
                // Filter for ongoing tasks
                FilterDataGridView(FilterType.OnGoingTasks);

                btnAddTask.Visible = true;
                lblTotalTasksText.Visible = true;
                lblTotalTasksNumber.Visible = true;
                lblRecurringTasks.Visible = false;
                lblArchivedTasks.Visible = false;

            }
            else if (rdoRecurringTasks.Checked)
            {
                // Filter for recurring tasks
                FilterDataGridView(FilterType.RecurringTasks);

                btnAddTask.Visible = false;
                lblTotalTasksText.Visible = false;
                lblTotalTasksNumber.Visible = false;
                lblRecurringTasks.Location = new Point(30, 63);
                lblRecurringTasks.Visible = true;
                lblArchivedTasks.Visible = false;

            }
            else if (rdoArchivedTasks.Checked)
            {
                // Filter for archived tasks
                FilterDataGridView(FilterType.ArchivedTasks);

                btnAddTask.Visible = false;
                lblTotalTasksText.Visible = false;
                lblTotalTasksNumber.Visible = false;
                lblRecurringTasks.Visible = false;
                lblArchivedTasks.Location = new Point(30, 63);
                lblArchivedTasks.Visible = true;

            }

            // Adjust the context menu items based on the checkbox state.
            ContextMenuInit();

            // Adjust the colors
            UpdateRowFontColors();
        }

        // **********************************************************************************************************************
        // ** Others:

        private void MidnightDetection()
        {
            // Midnight is detected.
            if (!isMidnight && DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
            {
                isMidnight = true;

                CalculateTaskRecurrenceShow();
                UpdateNumberOfTasks();
                FilterRadioButtons_CheckedChanged(null, EventArgs.Empty);
            }

            if (isMidnight && DateTime.Now.Hour != 0 && DateTime.Now.Minute != 0)
            {
                isMidnight = false;
            }
        }

        private void tmrMidnight_Tick(object sender, EventArgs e)
        {
            MidnightDetection();
        }



    }
}
