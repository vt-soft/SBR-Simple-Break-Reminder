
namespace SBR.Forms
{
    partial class FrmPomodoro
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
            btnOk = new Button();
            lblText = new Label();
            SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.Location = new Point(302, 73);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 26);
            btnOk.TabIndex = 2;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOK_Click;
            // 
            // lblText
            // 
            lblText.AutoSize = true;
            lblText.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblText.Location = new Point(72, 19);
            lblText.Name = "lblText";
            lblText.Size = new Size(105, 17);
            lblText.TabIndex = 0;
            lblText.Text = "*** Pomodor 4/4";
            // 
            // FrmPomodoro
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(391, 107);
            Controls.Add(lblText);
            Controls.Add(btnOk);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmPomodoro";
            ShowIcon = false;
            Text = "***Pomodoro 4/4";
            ResumeLayout(false);
            PerformLayout();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
        private LinkLabel llbLink;
        private Button btnOk;
        private Label lblText;
    }
}