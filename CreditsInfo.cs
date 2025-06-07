using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBR
{


    /// <summary>
    /// Object is storing information about credits - language code, language, translator, and hyperlink.
    /// List of this class is created and populated in UcLanguage.cs in FillCreditList()
    /// </summary>
    public class CreditsInfo
    {
        public string LanguageCode { get; set; }

        public string Language { get; set; }

        public string Translator { get; set; }

        public string HyperLink { get; set; }
  
    }
}
