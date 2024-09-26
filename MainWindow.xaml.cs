using Microsoft.Win32;
using System.ComponentModel;
using System.Windows;
using System.Collections;
using System.IO;
using FileRenamer;


namespace FileRenamer
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            this.DataContext = _viewModel;
        }

        private void buttonSet_Click(object sender, RoutedEventArgs e)
        {
            var folderDialog = new OpenFolderDialog();
            folderDialog.Title = "Ordner Auswählen";

            if (folderDialog.ShowDialog() == true)
            {
                var folderName = folderDialog.FolderName;
                // Shorten the path to show only the lowest 3 directory names
                _viewModel.dirInputContent = HelperMethods.ShortenPathToLowestDirectories(folderName);
                // Set the full path as a ToolTip
                btnSetPath.ToolTip = folderName;

                string[] folderContent = Directory.GetFiles(folderName);
                
            }
        }

        private void nameInput_GotFocus(object sender, RoutedEventArgs e)
        {
            if(nameInput.Text == "Dateiname")
            {
                nameInput.Clear();
            }
        }


        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }


        private void prefixToggle_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void prefixToggle_Unchecked(object sender, RoutedEventArgs e)
        {

        }
        private void suffixToggle_Checked(object sender, RoutedEventArgs e)
        {
            if (prefixToggle.IsChecked == true)
            {
                prefixToggle.IsChecked = false;
            }
        }

        private void suffixToggle_Unchecked(object sender, RoutedEventArgs e)
        {

        }       
    }
}