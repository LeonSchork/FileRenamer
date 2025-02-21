using System.Windows.Media;

namespace FileRenamer
{
    public class LanguageOption(string language, string culture, ImageSource icon)
    {
        public ImageSource Icon { get; set; } = icon;
        public string Culture { get; set; } = culture;
        public string Language { get; set; } = language;
    }
}
