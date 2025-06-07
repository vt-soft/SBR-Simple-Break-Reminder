namespace SBR.Forms
{
    partial class UcLanguage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcLanguage));
            cboLanguages = new ComboBox();
            imageList1 = new ImageList(components);
            picFlag = new PictureBox();
            pnlTableLang = new TableLayoutPanel();
            btnMissingLanguage = new Button();
            lblSelectLanguage = new Label();
            lblCredits = new Label();
            pnlLine = new Panel();
            ((System.ComponentModel.ISupportInitialize)picFlag).BeginInit();
            SuspendLayout();
            // 
            // cboLanguages
            // 
            cboLanguages.BackColor = Color.White;
            cboLanguages.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            cboLanguages.FormattingEnabled = true;
            cboLanguages.Location = new Point(161, 60);
            cboLanguages.Name = "cboLanguages";
            cboLanguages.Size = new Size(225, 24);
            cboLanguages.TabIndex = 1;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth24Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "cs-cz.png");
            imageList1.Images.SetKeyName(1, "de-de.png");
            imageList1.Images.SetKeyName(2, "en-gb.png");
            imageList1.Images.SetKeyName(3, "es-es.png");
            imageList1.Images.SetKeyName(4, "fr-fr.png");
            imageList1.Images.SetKeyName(5, "it-it.png");
            imageList1.Images.SetKeyName(6, "ja-jp.png");
            imageList1.Images.SetKeyName(7, "ko-kr.png");
            imageList1.Images.SetKeyName(8, "ru-ru.png");
            imageList1.Images.SetKeyName(9, "zh-cn.png");
            // 
            // picFlag
            // 
            picFlag.Image = ResourcesFlagsDir.ResourcesFlags.cs_cz;
            picFlag.Location = new Point(30, 30);
            picFlag.Name = "picFlag";
            picFlag.Size = new Size(112, 71);
            picFlag.TabIndex = 5;
            picFlag.TabStop = false;
            // 
            // pnlTableLang
            // 
            pnlTableLang.AutoScroll = true;
            pnlTableLang.BackColor = Color.Transparent;
            pnlTableLang.ColumnCount = 3;
            pnlTableLang.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            pnlTableLang.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300F));
            pnlTableLang.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            pnlTableLang.Location = new Point(30, 170);
            pnlTableLang.Name = "pnlTableLang";
            pnlTableLang.RowCount = 1;
            pnlTableLang.RowStyles.Add(new RowStyle(SizeType.Absolute, 43F));
            pnlTableLang.Size = new Size(773, 66);
            pnlTableLang.TabIndex = 8;
            // 
            // btnMissingLanguage
            // 
            btnMissingLanguage.Font = new Font("Segoe UI", 9.75F);
            btnMissingLanguage.Location = new Point(677, 30);
            btnMissingLanguage.Name = "btnMissingLanguage";
            btnMissingLanguage.Size = new Size(126, 50);
            btnMissingLanguage.TabIndex = 2;
            btnMissingLanguage.Text = "Is your language missing here? ";
            btnMissingLanguage.UseVisualStyleBackColor = true;
            // 
            // lblSelectLanguage
            // 
            lblSelectLanguage.AutoSize = true;
            lblSelectLanguage.Font = new Font("Segoe UI", 9.75F);
            lblSelectLanguage.Location = new Point(161, 32);
            lblSelectLanguage.Name = "lblSelectLanguage";
            lblSelectLanguage.Size = new Size(138, 17);
            lblSelectLanguage.TabIndex = 11;
            lblSelectLanguage.Text = "*Select your language:";
            // 
            // lblCredits
            // 
            lblCredits.AutoSize = true;
            lblCredits.Font = new Font("Segoe UI", 9.75F);
            lblCredits.Location = new Point(32, 136);
            lblCredits.Name = "lblCredits";
            lblCredits.Size = new Size(57, 17);
            lblCredits.TabIndex = 12;
            lblCredits.Text = "*Credits:";
            // 
            // pnlLine
            // 
            pnlLine.BorderStyle = BorderStyle.FixedSingle;
            pnlLine.Location = new Point(30, 113);
            pnlLine.Margin = new Padding(0);
            pnlLine.Name = "pnlLine";
            pnlLine.Size = new Size(773, 1);
            pnlLine.TabIndex = 0;
            // 
            // UcLanguage
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlLine);
            Controls.Add(lblCredits);
            Controls.Add(lblSelectLanguage);
            Controls.Add(btnMissingLanguage);
            Controls.Add(pnlTableLang);
            Controls.Add(picFlag);
            Controls.Add(cboLanguages);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            Name = "UcLanguage";
            Size = new Size(824, 509);
            Load += UcLanguage_Load;
            ((System.ComponentModel.ISupportInitialize)picFlag).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox cboLanguages;
        private ImageList imageList1;
        private PictureBox picFlag;
        private TableLayoutPanel pnlTableLang;
        private Button btnMissingLanguage;
        private Label lblSelectLanguage;
        private Label lblCredits;
        private Panel pnlLine;
    }
}
