using FileRenamer.Languages;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Imaging;

namespace FileRenamer
{
    public static class LanguageManager
    {
        private static readonly List<LanguageSelection> languageSelections = new List<LanguageSelection>
        {
            new LanguageSelection("English", "en", new BitmapImage(new Uri("/FileRenamer;component/Icons/en.png", UriKind.Relative))) ,
            new LanguageSelection("German", "de", new BitmapImage(new Uri("/FileRenamer;component/Icons/de.png", UriKind.Relative)))
        };

        public static List<LanguageSelection> LanguageSelections => languageSelections;

        public static void UpdateLanguageSetting(string culture, bool isStartup)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings["SelectedLanguage"] != null)
            {
                config.AppSettings.Settings["SelectedLanguage"].Value = culture;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                if (!isStartup) Utility.RestartApplication();
            }
            else
            {
                MessageBox.Show("SelectedLanguage key is missing in the configuration file. English will be applied as default");
            }
        }
    }
}
