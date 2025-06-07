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

namespace SBR.Forms
{
    public partial class UcAbout : UserControl
    {
        public UcAbout()
        {
            InitializeComponent();
            FormInit();
        }


        // ********************************************************************************************************************
        // ** Public Methods:
        // ********************************************************************************************************************
        public void ChangeLanguage()
        {
            // List of strings which are in current User Control (Windows Form) and which we want to change to different language.
            // There is such method in each User Control (Windows Form) which is called from LangChanger static class.
            lblAbout1.Text = LangChanger.GetString("An application which is tracking time you are using your computer and is reminding you to take a break.This application is free for any personal or commercial use.");
            lblAbout2.Text = LangChanger.GetString("For more information about this app please check online help page:");
            lblAbout3.Text = LangChanger.GetString("Credits:");
            lblAbout4.Text = LangChanger.GetString("Icons/flags:");
            lblAbout5.Text = LangChanger.GetString("Sounds:");
            lblAbout6.Text = LangChanger.GetString("Check also these free apps:");
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
            foreach (Control c in pnlAbout.Controls) // going thru all linkLabels
            {
                if (c is LinkLabel link)
                {
                    // Set linkLabel colors
                    Color linkColor = ControlPaint.Light(SystemColors.ControlText,0.7f);
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
