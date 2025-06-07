using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Resources;
using Microsoft.Win32.SafeHandles;
using SBR;
using System.Diagnostics;
using static System.Windows.Forms.LinkLabel;

namespace SBR.Forms
{
    public partial class UcLanguage : UserControl
    {
        private List<CreditsInfo> creditList;
        private FrmTranslators myForm;

        public UcLanguage()
        {
            InitializeComponent();
            EventHandlersInit();
            DataInit();
        }

        // ********************************************************************************************************************
        // ** Public Methods:
        // ********************************************************************************************************************

        /// <summary>
        /// Method will change strings in current User Control (Windows Form) to proper language.
        /// </summary>
        public void ChangeLanguage()
        {
            // List of strings which are in current User Control (Windows Form) and which we want to change to different language.
            // There is such method in each User Control (Windows Form) which is called from LangChanger static class.
            lblSelectLanguage.Text = LangChanger.GetString("Select your language:");
            lblCredits.Text = LangChanger.GetString("Credits:");
        }


        // ********************************************************************************************************************
        // ** Private Methods:
        // ********************************************************************************************************************


        private void EventHandlersInit()
        {
            cboLanguages.SelectedIndexChanged += cboLanguages_SelectedIndexChanged;
            btnMissingLanguage.Click += btnMissingLanguage_Click;
        }

        private void DataInit()
        {
            creditList = new List<CreditsInfo>();
            FillCreditList();
            PopulatePanelWithCredits();
        }


      
        private void UcLanguage_Load(object sender, EventArgs e)
        {
            // We can't call UpdateComboBoxWithLanguages() method in the constructor because it will:
            // immediately call cboLanguages_SelectedIndexChanged()
            // which will immediately call LangChanger.ChangeLangCode(credit.LanguageCode);
            // which will FAIL because LangChanger.Init() is not yet initialized.  
            UpdateComboBoxWithLanguages();
        }


        /// <summary>
        /// Method will populate pnlTableLang with flags, credits and hyperlinks.
        /// </summary>
        private void PopulatePanelWithCredits()
        {
            pnlTableLang.Controls.Clear();
            pnlTableLang.RowCount = 0;
            string translator = "";
            Image flag = null;

            for (int i = 0; i < creditList.Count; i++)
            {
                flag = FindFlag(creditList[i].LanguageCode);
                translator = String.Format($"{creditList[i].Language + "\n" + creditList[i].Translator}");
                LinkLabel linkLabel = CreateLinkLabel(i);

                // Add controls to the panel.   
                pnlTableLang.Controls.Add(new PictureBox() { Image = flag, Height = 30, SizeMode = PictureBoxSizeMode.Zoom, BorderStyle = BorderStyle.None }, 0, pnlTableLang.RowCount);
                pnlTableLang.Controls.Add(new Label() { Text = translator, Width = 400, Height = 35, Font = new Font("Segoe UI", 9.75f) }, 1, pnlTableLang.RowCount);
                pnlTableLang.Controls.Add(linkLabel, 2, pnlTableLang.RowCount);

                // No need to run rest of this code for last item, otherwise it will create extra blank table.
                if (i == (creditList.Count - 1)) break;

                pnlTableLang.RowCount++;  //increase panel rows count by one

                //Add a new RowStyle as a copy of the previous one.
                RowStyle temp = pnlTableLang.RowStyles[0];
                pnlTableLang.RowStyles.Add(new RowStyle(temp.SizeType, temp.Height));

                // Increase height of the panel.     
                if (pnlTableLang.Height <= 280)
                {
                    pnlTableLang.Height += 42;
                }
            }

            // Trick to disable horizontal scrollbar.
            int vertScrollWidth = SystemInformation.VerticalScrollBarWidth;
            pnlTableLang.Padding = new Padding(0, 0, vertScrollWidth, 0);
        }

        /// <summary>
        /// Method will create LinkLabel with hyperlink.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private LinkLabel CreateLinkLabel(int i)
        {
            Color linkColor = ControlPaint.Light(SystemColors.ControlText, 0.7f);

            // Hyperlink
            var linkLabel = new LinkLabel
            {
                Text = creditList[i].HyperLink,
                Width = 200,
                Height = 35,
                Tag = creditList[i].HyperLink,
                Font = new Font("Segoe UI", 9.75f, FontStyle.Underline),

                LinkColor = linkColor,
                ActiveLinkColor = linkColor,
                VisitedLinkColor = linkColor,
                ForeColor = linkColor,
                BackColor = SystemColors.Control,
                LinkBehavior = LinkBehavior.HoverUnderline,
            };

            // Hyperlink - event handler.
            linkLabel.LinkClicked += (sender, e) =>
            {
                var link = (sender as LinkLabel)?.Tag as string;
                if (!string.IsNullOrEmpty(link))
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = link,
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                }
            };
            return linkLabel;
        }

        /// <summary>
        /// Method will find flag by language code.
        /// </summary>
        /// <param name="languageCode"></param>
        /// <returns></returns>
        private Image FindFlag(string languageCode)
        {
            switch (languageCode)
            {
                case "en-gb":
                    return ResourcesFlagsDir.ResourcesFlags.en_gb;
                case "de-de":
                    return ResourcesFlagsDir.ResourcesFlags.de_de;
                case "el-gr":
                    return ResourcesFlagsDir.ResourcesFlags.el_gr;
                case "cs-cz":
                    return ResourcesFlagsDir.ResourcesFlags.cs_cz;
                //case "es-es":
                //    return ResourcesFlagsDir.ResourcesFlags.es_es;
                //case "pt-pt":
                //    return ResourcesFlagsDir.ResourcesFlags.pt_pt;
                //case "fr-fr":
                //    return ResourcesFlagsDir.ResourcesFlags.fr_fr;
                //case "it-it":
                //    return ResourcesFlagsDir.ResourcesFlags.it_it;
                //case "ko-kr":
                //    return ResourcesFlagsDir.ResourcesFlags.ko_kr;
                //case "ru-ru":
                //    return ResourcesFlagsDir.ResourcesFlags.ru_ru;
                //case "zh-cn":
                //    return ResourcesFlagsDir.ResourcesFlags.zh_cn;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Method will fill creditList with data.
        /// </summary>
        private void FillCreditList()
        {

            creditList.Add(new CreditsInfo()
            {
                LanguageCode = "en-gb",
                Language = "Britain - English",
                Translator = "British translation agency",
                HyperLink = "www.google.com"
            });

            creditList.Add(new CreditsInfo()
            {
                LanguageCode = "de-de",
                Language = "Germany - Deutsch",
                Translator = "German translation agency",
                HyperLink = "www.google.com",
            });

            creditList.Add(new CreditsInfo()
            {
                LanguageCode = "el-gr",
                Language = "Greek - Dελληνικά",
                Translator = "Greek translation agency",
                HyperLink = "www.seznam.cz"
            });

            creditList.Add(new CreditsInfo()
            {
                LanguageCode = "cs-cz",
                Language = "Cestina",
                Translator = "Cesky prekladatel",
                HyperLink = "www.seznam.cz"
            });



        }

        /// <summary>
        /// Method will update combobox with languages (from creditList).
        /// </summary>
        private void UpdateComboBoxWithLanguages()
        {
            cboLanguages.Items.Clear();
            foreach (var credit in creditList)
            {
                cboLanguages.Items.Add(credit.Language);
            }

            //Sort the list of languages in the combobox alphabetically.
            cboLanguages.Sorted = true;


            // Change the selected language in the combobox to the current language of the application.
            // This part is here because after we load setting file we want to have the correct language selected in the combobox.
            string langCode = MainForm.MainFormInstance?.cData?.LanguageCode;

            if (!string.IsNullOrEmpty(langCode))
            {
                cboLanguages.SelectedItem = creditList.FirstOrDefault(c => c.LanguageCode == langCode)?.Language;
            }
        }

        /// <summary>
        /// Method will change language and will update flag image when different language is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ensure SelectedItem is not null before using it
            if (cboLanguages.SelectedItem != null)
            {
                string selectedLanguage = cboLanguages.SelectedItem.ToString();
                foreach (var credit in creditList)
                {
                    if (credit.Language == selectedLanguage)
                    {
                        // Change the flag image.
                        picFlag.Image = FindFlag(credit.LanguageCode);

                        Debug.Print("Volame zmenu jazyka: " + selectedLanguage);

                        // Change the language in the application.
                        LangChanger.ChangeLangCode(credit.LanguageCode);

                        // hange the languageCode in the settings file.
                        MainForm.MainFormInstance.cData.LanguageCode = credit.LanguageCode;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Method will open FrmTranslators form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMissingLanguage_Click(object sender, EventArgs e)
        {
            if (myForm == null || myForm.IsDisposed)
            {
                myForm = new FrmTranslators();
                myForm.StartPosition = FormStartPosition.CenterParent;
                myForm.FormClosed += (s, args) => myForm = null; // reset myForm when the form is closed
                myForm.ShowDialog(this); // use ShowDialog instead of Show to ensure it is centered relative to the parent
            }
        }




    }
}
