namespace SBR.Forms
{
    partial class AddTaskForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddTaskForm));
            lblTaskDueDate = new Label();
            lblTaskPriority = new Label();
            lblRecTask = new Label();
            lblTaskName = new Label();
            lblTaskStatus = new Label();
            lblTaskTag = new Label();
            lblTaskNote = new Label();
            btnAddRecord = new Button();
            txtTaskNote = new TextBox();
            txtTaskName = new TextBox();
            txtTaskPriority = new TextBox();
            txtTaskTag = new TextBox();
            txtTaskStatus = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            errorProvider1 = new ErrorProvider(components);
            chkRTask = new CheckBox();
            lblRTaskRepeat = new Label();
            nudRTaskRepeatTime = new NumericUpDown();
            cmbRTask = new ComboBox();
            nudRTaskRemindTime = new NumericUpDown();
            lblRTaskRemind = new Label();
            lblRTaskDaysAfter = new Label();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudRTaskRepeatTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudRTaskRemindTime).BeginInit();
            SuspendLayout();
            // 
            // lblTaskDueDate
            // 
            resources.ApplyResources(lblTaskDueDate, "lblTaskDueDate");
            lblTaskDueDate.Name = "lblTaskDueDate";
            // 
            // lblTaskPriority
            // 
            resources.ApplyResources(lblTaskPriority, "lblTaskPriority");
            lblTaskPriority.Name = "lblTaskPriority";
            // 
            // lblRecTask
            // 
            resources.ApplyResources(lblRecTask, "lblRecTask");
            lblRecTask.Name = "lblRecTask";
            // 
            // lblTaskName
            // 
            resources.ApplyResources(lblTaskName, "lblTaskName");
            lblTaskName.Name = "lblTaskName";
            // 
            // lblTaskStatus
            // 
            resources.ApplyResources(lblTaskStatus, "lblTaskStatus");
            lblTaskStatus.Name = "lblTaskStatus";
            // 
            // lblTaskTag
            // 
            resources.ApplyResources(lblTaskTag, "lblTaskTag");
            lblTaskTag.Name = "lblTaskTag";
            // 
            // lblTaskNote
            // 
            resources.ApplyResources(lblTaskNote, "lblTaskNote");
            lblTaskNote.Name = "lblTaskNote";
            // 
            // btnAddRecord
            // 
            resources.ApplyResources(btnAddRecord, "btnAddRecord");
            btnAddRecord.Name = "btnAddRecord";
            btnAddRecord.UseVisualStyleBackColor = true;
            // 
            // txtTaskNote
            // 
            resources.ApplyResources(txtTaskNote, "txtTaskNote");
            txtTaskNote.Name = "txtTaskNote";
            // 
            // txtTaskName
            // 
            resources.ApplyResources(txtTaskName, "txtTaskName");
            txtTaskName.Name = "txtTaskName";
            // 
            // txtTaskPriority
            // 
            resources.ApplyResources(txtTaskPriority, "txtTaskPriority");
            txtTaskPriority.Name = "txtTaskPriority";
            // 
            // txtTaskTag
            // 
            resources.ApplyResources(txtTaskTag, "txtTaskTag");
            txtTaskTag.Name = "txtTaskTag";
            // 
            // txtTaskStatus
            // 
            resources.ApplyResources(txtTaskStatus, "txtTaskStatus");
            txtTaskStatus.Name = "txtTaskStatus";
            // 
            // dateTimePicker1
            // 
            resources.ApplyResources(dateTimePicker1, "dateTimePicker1");
            dateTimePicker1.Name = "dateTimePicker1";
            // 
            // errorProvider1
            // 
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider1.ContainerControl = this;
            // 
            // chkRTask
            // 
            resources.ApplyResources(chkRTask, "chkRTask");
            chkRTask.Name = "chkRTask";
            chkRTask.UseVisualStyleBackColor = true;
            // 
            // lblRTaskRepeat
            // 
            resources.ApplyResources(lblRTaskRepeat, "lblRTaskRepeat");
            lblRTaskRepeat.Name = "lblRTaskRepeat";
            // 
            // nudRTaskRepeatTime
            // 
            resources.ApplyResources(nudRTaskRepeatTime, "nudRTaskRepeatTime");
            nudRTaskRepeatTime.Name = "nudRTaskRepeatTime";
            // 
            // cmbRTask
            // 
            cmbRTask.FormattingEnabled = true;
            resources.ApplyResources(cmbRTask, "cmbRTask");
            cmbRTask.Name = "cmbRTask";
            // 
            // nudRTaskRemindTime
            // 
            resources.ApplyResources(nudRTaskRemindTime, "nudRTaskRemindTime");
            nudRTaskRemindTime.Name = "nudRTaskRemindTime";
            // 
            // lblRTaskRemind
            // 
            resources.ApplyResources(lblRTaskRemind, "lblRTaskRemind");
            lblRTaskRemind.Name = "lblRTaskRemind";
            // 
            // lblRTaskDaysAfter
            // 
            resources.ApplyResources(lblRTaskDaysAfter, "lblRTaskDaysAfter");
            lblRTaskDaysAfter.Name = "lblRTaskDaysAfter";
            // 
            // AddTaskForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblRTaskDaysAfter);
            Controls.Add(lblRTaskRemind);
            Controls.Add(nudRTaskRemindTime);
            Controls.Add(cmbRTask);
            Controls.Add(nudRTaskRepeatTime);
            Controls.Add(lblRTaskRepeat);
            Controls.Add(chkRTask);
            Controls.Add(dateTimePicker1);
            Controls.Add(txtTaskStatus);
            Controls.Add(txtTaskTag);
            Controls.Add(txtTaskPriority);
            Controls.Add(txtTaskName);
            Controls.Add(txtTaskNote);
            Controls.Add(btnAddRecord);
            Controls.Add(lblTaskNote);
            Controls.Add(lblTaskTag);
            Controls.Add(lblTaskStatus);
            Controls.Add(lblTaskName);
            Controls.Add(lblRecTask);
            Controls.Add(lblTaskPriority);
            Controls.Add(lblTaskDueDate);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddTaskForm";
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudRTaskRepeatTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudRTaskRemindTime).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTaskDueDate;
        private Label lblTaskPriority;
        private Label lblRecTask;
        private Label lblTaskName;
        private Label lblTaskStatus;
        private Label lblTaskTag;
        private Label lblTaskNote;
        private Button btnAddRecord;
        private TextBox txtTaskNote;
        private TextBox txtTaskName;
        private TextBox txtTaskPriority;
        private TextBox txtTaskTag;
        private TextBox txtTaskStatus;
        private DateTimePicker dateTimePicker1;
        private ErrorProvider errorProvider1;
        private ComboBox cmbRTask;
        private NumericUpDown nudRTaskRepeatTime;
        private Label lblRTaskRepeat;
        private CheckBox chkRTask;
        private Label lblRTaskDaysAfter;
        private Label lblRTaskRemind;
        private NumericUpDown nudRTaskRemindTime;
    }
}