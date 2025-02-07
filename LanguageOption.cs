using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FileRenamer
{
    public class LanguageOption
    {
        public ImageSource Icon { get; set; }
        public string Culture { get; set; }
        public string Language { get; set; }

        public LanguageOption(string language, string culture, ImageSource icon)
        {
            Language = language;
            Culture = culture;
            Icon = icon;
        }
    }
    
}
