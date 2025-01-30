using Microsoft.Win32;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton toggleButton && toggleButton.DataContext is FileItem fileItem)
            {
                _renameList.Add(fileItem);
                fileItem.Order = _renameList.IndexOf(fileItem) + 1;

            }
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton toggleButton && toggleButton.DataContext is FileItem fileItem)
            {
                _renameList.Remove(fileItem);
                fileItem.Order = null;
            }
        }

    }
}