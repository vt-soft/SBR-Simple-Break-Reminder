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
using SBR;
using static System.Windows.Forms.LinkLabel;

namespace SBR.Forms
{
    public partial class FrmTranslators : Form
    {

        public FrmTranslators()
        {
            InitializeComponent();

            lblText.Text = "Is your language missing here? Are you native speaker?\nDo you want to participate in this project?\nIf yes, please clik the link to get more information.\nThere are only few words in this project, so translation is matter of few minutes :)";

            llbLink.Text = "www.example.com";
        }

        /// <summary>
        /// Open the link in the default browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void llbLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = llbLink.Text,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
