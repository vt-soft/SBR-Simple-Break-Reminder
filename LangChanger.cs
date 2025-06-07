using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SBR.Forms;

namespace SBR
{
    public static class LangChanger
    {
        private static readonly ResourceManager rm;
        private static MainForm mainForm;

        // We store all UserControl (Forms) in this dictionary.
        private static Dictionary<string, UserControl> screens = new Dictionary<string, UserControl>();

        static LangChanger()
        {
            // SBR.Languages.Language = namespace + resource file name
            rm = new ResourceManager("SBR.Languages.Localization", Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// This method will initialize the LangChanger class with neccesarry refereces to mainForm and other Forms.
        /// </summary>
        /// <param name="aMainForm"></param>
        /// <param name="aScreens"></param>
        public static void Init(MainForm aMainForm, Dictionary<string,UserControl> aScreens)
        {
            mainForm = aMainForm;
            screens = aScreens;
        }

        /// <summary>
        /// Get string from resource file
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string? GetString(string name)
        {
            return rm.GetString(name);
        }

        /// <summary>
        /// This method will change language. In "language" string we are expecting language code like en-gb, fr-fr, etc.
        /// </summary>
        /// <param name="language"></param>
        public static void ChangeLangCode(string language)
        {
            Debug.Print("Changing language to: " + language);

            // Set proper culture for the application.
            var cultureInfo = new CultureInfo(language);
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            // Set proper language (based on the selected culture) for MainForm.
            mainForm.ChangeLanguage();

            // Set proper language (based on the selected culture) for all Forms (User Controls).
            ((UcAlarm)screens["btnAlarm"]).ChangeLanguage();
            ((UcToDo)screens["btnToDo"]).ChangeLanguage();
            ((UcSettings)screens["btnSettings"]).ChangeLanguage();
            ((UcStatistics)screens["btnStatistics"]).ChangeLanguage();
            ((UcLanguageTemp)screens["btnLanguage"]).ChangeLanguage(); // TODO - TEMP - then change back to UcLanguage
            ((UcAbout)screens["btnAbout"]).ChangeLanguage();

       

        }
    }
}
