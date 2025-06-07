using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace SBR.Forms
{
    public partial class UcLanguageTemp : UserControl
    {
        public UcLanguageTemp()
        {
            InitializeComponent();
            FormInit();
        }

        public void ChangeLanguage()
        {
        }

        // ********************************************************************************************************************
        // ** Private Methods:
        // ********************************************************************************************************************

        /// <summary>
        /// Method will activate the hyperlink for each linkLabel in pnlAbout panel and will set their colors.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormInit()
        {
            foreach (Control c in pnlTempLang.Controls) // going thru all linkLabels
            {
                if (c is LinkLabel link)
                {
                    // Set linkLabel colors
                    Color linkColor = ControlPaint.Light(SystemColors.ControlText, 0.7f);
                    link.LinkColor = linkColor;
                    link.ActiveLinkColor = linkColor;
                    link.VisitedLinkColor = linkColor;
                    link.ForeColor = linkColor;
                    link.BackColor = SystemColors.Control;

                    // Link behavior
                    link.LinkBehavior = LinkBehavior.HoverUnderline;

                    // Event handler 
                    link.LinkClicked += (s, ev) =>
                    {
                        var linkTag = (s as LinkLabel)?.Tag as string;
                        if (!string.IsNullOrEmpty(linkTag))
                        {
                            var psi = new ProcessStartInfo
                            {
                                FileName = linkTag,
                                UseShellExecute = true
                            };
                            Process.Start(psi);
                        }
                    };
                }
            }
        }

    }
}
