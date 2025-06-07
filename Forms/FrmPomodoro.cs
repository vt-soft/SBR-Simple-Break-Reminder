using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SBR;
using static System.Windows.Forms.LinkLabel;

namespace SBR.Forms
{
    public partial class FrmPomodoro : Form
    {

        public FrmPomodoro(string sPomodoroText, string sPomodoroTitle)
        {
            InitializeComponent();

            lblText.Text = sPomodoroText;
            this.Text = sPomodoroTitle;

            // Set the form's icon
            var pictureBoxIcon = new PictureBox
            {
                Image = SystemIcons.Exclamation.ToBitmap(),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Location = new Point(16, 16) // Adjust as needed
            };
            this.Controls.Add(pictureBoxIcon);

             ApplyDarkMode();
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
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
          
                }
            }
        }


    }
}
