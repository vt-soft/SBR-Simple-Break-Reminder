namespace SBR.Forms
{
    partial class UcLanguageTemp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcLanguageTemp));
            pnlTempLang = new Panel();
            llbContact = new LinkLabel();
            label1 = new Label();
            pnlTempLang.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTempLang
            // 
            pnlTempLang.AccessibleRole = AccessibleRole.None;
            pnlTempLang.BorderStyle = BorderStyle.FixedSingle;
            pnlTempLang.Controls.Add(llbContact);
            pnlTempLang.Controls.Add(label1);
            pnlTempLang.Location = new Point(30, 30);
            pnlTempLang.Name = "pnlTempLang";
            pnlTempLang.Size = new Size(758, 439);
            pnlTempLang.TabIndex = 1;
            // 
            // llbContact
            // 
            llbContact.AutoSize = true;
            llbContact.Font = new Font("Segoe UI", 9.75F);
            llbContact.Location = new Point(22, 102);
            llbContact.Name = "llbContact";
            llbContact.Size = new Size(150, 17);
            llbContact.TabIndex = 19;
            llbContact.TabStop = true;
            llbContact.Tag = "www.vt-soft.com/contact";
            llbContact.Text = "www.vt-soft.com/contact";
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label1.Location = new Point(22, 28);
            label1.Name = "label1";
            label1.Size = new Size(707, 74);
            label1.TabIndex = 1;
            label1.Text = resources.GetString("label1.Text");
            // 
            // UcLanguageTemp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlTempLang);
            Name = "UcLanguageTemp";
            Size = new Size(824, 509);
            pnlTempLang.ResumeLayout(false);
            pnlTempLang.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTempLang;
        private Label label1;
        private LinkLabel llbContact;
    }
}
