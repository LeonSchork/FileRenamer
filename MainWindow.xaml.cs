using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileRenamer
{

    public partial class MainWindow : Window
    {
        private FileItemContainer _fileItemContainer;
        private List<FileItem> _renameList = new();

        public MainWindow()
        {
            InitializeComponent();
            _fileItemContainer = new FileItemContainer();

        }

        private void OpenFolderDialogButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog folderDialog = new();
            if (folderDialog.ShowDialog() == true)
            {
                FolderLocationTextBlock.Text = Utility.ShortenPath(folderDialog.FolderName);

                if (_fileItemContainer.FileItems != null) _fileItemContainer.Clear();
                _fileItemContainer.AddFileItems(folderDialog.FolderName);

                FileListView.ItemsSource = _fileItemContainer.FileItems;
            }
        }

        private void FileSelectionTB_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton toggleButton && toggleButton.DataContext is FileItem fileItem)
            {
                _renameList.Add(fileItem);
                fileItem.Order = _renameList.IndexOf(fileItem) + 1;

            }
        }

        private void FileSelectionTB_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton toggleButton && toggleButton.DataContext is FileItem fileItem)
            {
                _renameList.Remove(fileItem);
                fileItem.Order = null;
            }
        }

        private void ExecuteRenameButton_Click(object sender, RoutedEventArgs e)
        {
            string namingText = NamingTextbox.Text;
            int initialNumber = 1;
            int increment = 1;
            
            try
            {
                initialNumber = int.Parse(NumberingTextbox.Text);
                increment = int.Parse(IncrementTextbox.Text);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Bitte fülle die Felder Start Nr. und Inkrement nur mit Zahlen", "Falsche Eingabe", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Utility.CounterOptions renameOptions =
                NumberingSwitch.IsChecked == true ? Utility.CounterOptions.Suffix : Utility.CounterOptions.Prefix;

            Utility.RenameFiles(_renameList, namingText, renameOptions, initialNumber, increment);

            _renameList.Clear();
            _fileItemContainer.ClearOrder();
            FileListView.ItemsSource = null;
            FileListView.ItemsSource = _fileItemContainer.FileItems;
        }

        private void MarkAllButton_Click(object sender, RoutedEventArgs e)
        {
            bool areEqual = _renameList.SequenceEqual(_fileItemContainer.FileItems);
            if (areEqual) 
            {
                _renameList.Clear();
                _fileItemContainer.ClearOrder();
            }
            else
            {
                foreach (FileItem file in _fileItemContainer.FileItems)
                {
                    _renameList.Add(file);
                    file.Order = _renameList.IndexOf(file) + 1;
                }
            }
        }
    }
}