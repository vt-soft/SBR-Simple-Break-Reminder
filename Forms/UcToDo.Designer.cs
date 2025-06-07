namespace SBR.Forms
{
    partial class UcToDo
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
            dataGridView1 = new DataGridView();
            chkHideColumns = new CheckBox();
            btnAddTask = new Button();
            rdoRecurrenceTasks = new Panel();
            rdoArchivedTasks = new RadioButton();
            rdoRecurringTasks = new RadioButton();
            rdoOngoingTasks = new RadioButton();
            lblTotalTasksText = new Label();
            lblTotalTasksNumber = new Label();
            lblShow = new Label();
            lblRecurringTasks = new Label();
            lblArchivedTasks = new Label();
            tmrMidnight = new System.Windows.Forms.Timer(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            rdoRecurrenceTasks.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(30, 92);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(759, 380);
            dataGridView1.TabIndex = 5;
            // 
            // chkHideColumns
            // 
            chkHideColumns.AutoSize = true;
            chkHideColumns.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            chkHideColumns.Location = new Point(7, 39);
            chkHideColumns.Name = "chkHideColumns";
            chkHideColumns.Size = new Size(253, 21);
            chkHideColumns.TabIndex = 7;
            chkHideColumns.Text = "***Hide Priority, Tag and Status column";
            chkHideColumns.UseVisualStyleBackColor = true;
            // 
            // btnAddTask
            // 
            btnAddTask.FlatStyle = FlatStyle.Flat;
            btnAddTask.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            btnAddTask.Location = new Point(30, 28);
            btnAddTask.Name = "btnAddTask";
            btnAddTask.Size = new Size(151, 27);
            btnAddTask.TabIndex = 8;
            btnAddTask.Text = "***Add new task";
            btnAddTask.UseVisualStyleBackColor = true;
            // 
            // rdoRecurrenceTasks
            // 
            rdoRecurrenceTasks.Controls.Add(rdoArchivedTasks);
            rdoRecurrenceTasks.Controls.Add(rdoRecurringTasks);
            rdoRecurrenceTasks.Controls.Add(rdoOngoingTasks);
            rdoRecurrenceTasks.Controls.Add(chkHideColumns);
            rdoRecurrenceTasks.Location = new Point(373, 22);
            rdoRecurrenceTasks.Name = "rdoRecurrenceTasks";
            rdoRecurrenceTasks.Size = new Size(425, 64);
            rdoRecurrenceTasks.TabIndex = 11;
            // 
            // rdoArchivedTasks
            // 
            rdoArchivedTasks.AutoSize = true;
            rdoArchivedTasks.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            rdoArchivedTasks.Location = new Point(254, 6);
            rdoArchivedTasks.Name = "rdoArchivedTasks";
            rdoArchivedTasks.Size = new Size(91, 21);
            rdoArchivedTasks.TabIndex = 10;
            rdoArchivedTasks.Text = "***Archived";
            rdoArchivedTasks.TextAlign = ContentAlignment.TopLeft;
            rdoArchivedTasks.UseVisualStyleBackColor = true;
            // 
            // rdoRecurringTasks
            // 
            rdoRecurringTasks.AutoSize = true;
            rdoRecurringTasks.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            rdoRecurringTasks.Location = new Point(127, 6);
            rdoRecurringTasks.Name = "rdoRecurringTasks";
            rdoRecurringTasks.Size = new Size(97, 21);
            rdoRecurringTasks.TabIndex = 9;
            rdoRecurringTasks.Text = "***Recurring";
            rdoRecurringTasks.TextAlign = ContentAlignment.TopLeft;
            rdoRecurringTasks.UseVisualStyleBackColor = true;
            // 
            // rdoOngoingTasks
            // 
            rdoOngoingTasks.AutoSize = true;
            rdoOngoingTasks.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            rdoOngoingTasks.Location = new Point(7, 6);
            rdoOngoingTasks.Name = "rdoOngoingTasks";
            rdoOngoingTasks.Size = new Size(92, 21);
            rdoOngoingTasks.TabIndex = 8;
            rdoOngoingTasks.Text = "***Ongoing";
            rdoOngoingTasks.TextAlign = ContentAlignment.TopLeft;
            rdoOngoingTasks.UseVisualStyleBackColor = true;
            // 
            // lblTotalTasksText
            // 
            lblTotalTasksText.AutoSize = true;
            lblTotalTasksText.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblTotalTasksText.Location = new Point(30, 61);
            lblTotalTasksText.Name = "lblTotalTasksText";
            lblTotalTasksText.Size = new Size(57, 17);
            lblTotalTasksText.TabIndex = 12;
            lblTotalTasksText.Text = "***Tasks:";
            // 
            // lblTotalTasksNumber
            // 
            lblTotalTasksNumber.AutoSize = true;
            lblTotalTasksNumber.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblTotalTasksNumber.Location = new Point(90, 61);
            lblTotalTasksNumber.Name = "lblTotalTasksNumber";
            lblTotalTasksNumber.Size = new Size(25, 20);
            lblTotalTasksNumber.TabIndex = 13;
            lblTotalTasksNumber.Text = "55";
            // 
            // lblShow
            // 
            lblShow.AutoSize = true;
            lblShow.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblShow.Location = new Point(282, 30);
            lblShow.Name = "lblShow";
            lblShow.Size = new Size(90, 17);
            lblShow.TabIndex = 14;
            lblShow.Text = "***Show tasks:";
            // 
            // lblRecurringTasks
            // 
            lblRecurringTasks.AutoSize = true;
            lblRecurringTasks.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblRecurringTasks.Location = new Point(159, 1);
            lblRecurringTasks.Name = "lblRecurringTasks";
            lblRecurringTasks.Size = new Size(115, 17);
            lblRecurringTasks.TabIndex = 15;
            lblRecurringTasks.Text = "***Recurring tasks:";
            // 
            // lblArchivedTasks
            // 
            lblArchivedTasks.AutoSize = true;
            lblArchivedTasks.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblArchivedTasks.Location = new Point(30, 1);
            lblArchivedTasks.Name = "lblArchivedTasks";
            lblArchivedTasks.Size = new Size(109, 17);
            lblArchivedTasks.TabIndex = 16;
            lblArchivedTasks.Text = "***Archived tasks:";
            // 
            // tmrMidnight
            // 
            tmrMidnight.Enabled = true;
            tmrMidnight.Interval = 1000;
            tmrMidnight.Tick += tmrMidnight_Tick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // UcToDo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblArchivedTasks);
            Controls.Add(lblRecurringTasks);
            Controls.Add(lblShow);
            Controls.Add(lblTotalTasksNumber);
            Controls.Add(lblTotalTasksText);
            Controls.Add(rdoRecurrenceTasks);
            Controls.Add(btnAddTask);
            Controls.Add(dataGridView1);
            Name = "UcToDo";
            Size = new Size(824, 509);
            Load += UcToDo_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            rdoRecurrenceTasks.ResumeLayout(false);
            rdoRecurrenceTasks.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dataGridView1;
        private Button button6;
        private CheckBox chkHideColumns;
        private Button btnAddTask;
        private Panel rdoRecurrenceTasks;
        private Label lblTotalTasksText;
        private Label lblTotalTasksNumber;
        private RadioButton rdoArchivedTasks;
        private RadioButton rdoRecurringTasks;
        private RadioButton rdoOngoingTasks;
        private Label lblShow;
        private Label lblRecurringTasks;
        private Label lblArchivedTasks;
        private System.Windows.Forms.Timer tmrMidnight;
        private ContextMenuStrip contextMenuStrip1;
    }
}
