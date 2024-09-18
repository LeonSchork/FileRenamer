using Microsoft.Win32;
using System.ComponentModel;
using System.Windows;
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
                txtBoxPath.ToolTip = folderName;
            }
        }

        private void nameInput_GotFocus(object sender, RoutedEventArgs e)
        {
            if(nameInput.Text == "Dateiname")
            {
                nameInput.Clear();
            }
        }

        private void extensionInput_GotFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}