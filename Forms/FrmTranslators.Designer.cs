
namespace SBR.Forms
{
    partial class FrmTranslators
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
            llbLink = new LinkLabel();
            btnCancel = new Button();
            lblText = new Label();
            SuspendLayout();
            // 
            // llbLink
            // 
            llbLink.AutoSize = true;
            llbLink.Location = new Point(39, 132);
            llbLink.Name = "llbLink";
            llbLink.Size = new Size(60, 15);
            llbLink.TabIndex = 1;
            llbLink.TabStop = true;
            llbLink.Text = "linkLabel1";
            llbLink.LinkClicked += llbLink_LinkClicked;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(400, 128);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // lblText
            // 
            lblText.AutoSize = true;
            lblText.Location = new Point(39, 21);
            lblText.Name = "lblText";
            lblText.Size = new Size(38, 15);
            lblText.TabIndex = 0;
            lblText.Text = "label1";
            // 
            // FrmTranslators
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(487, 159);
            Controls.Add(lblText);
            Controls.Add(btnCancel);
            Controls.Add(llbLink);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmTranslators";
            ShowIcon = false;
            Text = "Looking for translators";
            ResumeLayout(false);
            PerformLayout();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
        private LinkLabel llbLink;
        private Button btnCancel;
        private Label lblText;
    }
}