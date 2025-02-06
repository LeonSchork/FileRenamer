using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows;

namespace FileRenamer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ApplyLanguageSetting();
        }

        private void ApplyLanguageSetting()
        {
            string culture = ConfigurationManager.AppSettings["SelectedLanguage"];
            if (!string.IsNullOrEmpty(culture))
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            }
        }
    }

}
