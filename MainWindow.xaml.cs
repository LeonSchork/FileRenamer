using FileRenamer.Languages;
using Microsoft.Win32;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FileRenamer
{

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private FileItemContainer _fileItemContainer;
        private List<FileItem> _renameList = new();
        private bool _isAnyItemMarked;
        private bool _isStartup = true;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            _fileItemContainer = new FileItemContainer();
            DataContext = this;
            FileItem.OrderChanged += FileItem_OrderChanged;

            LanguageComboBox.ItemsSource = LanguageManager.LanguageSelections;

            string savedCulture = ConfigurationManager.AppSettings["SelectedLanguage"];
            if (!string.IsNullOrEmpty(savedCulture))
            {
                foreach (LanguageOption item in LanguageManager.LanguageSelections)
                {
                    if (item.Culture == savedCulture)
                    {
                        LanguageComboBox.SelectedItem = item;
                        break;
                    }
                }
            }
            else
            {
                CultureInfo currentCulture = CultureInfo.CurrentCulture;
                string[] cultureName = currentCulture.Name.Split('-');
                foreach (LanguageOption item in LanguageManager.LanguageSelections)
                {
                    if (item.Culture.ToString() == cultureName[0])
                    {
                        LanguageComboBox.SelectedItem = item;
                        break;
                    }
                }
            }

            _isStartup = false;
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

        private void FileSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var fileItem = (FileItem)button.DataContext;

            if(fileItem.Order == null)
            {
                _renameList.Add(fileItem);
                    fileItem.Order = _renameList.IndexOf(fileItem) + 1;
            }
            else
            {
                _renameList.Remove(fileItem);
                fileItem.Order = null;
            }
        }

        public bool IsAnyItemMarked
        {
            get { return _isAnyItemMarked; }
            set
            {
                _isAnyItemMarked = value;
                OnPropertyChanged(nameof(IsAnyItemMarked));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void FileItem_OrderChanged(object sender, EventArgs e)
        {
            UpdateSelectAllButton();
        }

        private void UpdateSelectAllButton()
        {
            IsAnyItemMarked = _renameList.Any();
            if (IsAnyItemMarked)
            {
                MarkAllButton.Visibility = Visibility.Collapsed;
                ClearAllButton.Visibility = Visibility.Visible;
            }
            else if (!IsAnyItemMarked)
            {
                MarkAllButton.Visibility = Visibility.Visible;
                ClearAllButton.Visibility = Visibility.Collapsed;
            }
        }

        private void ExecuteRenameButton_Click(object sender, RoutedEventArgs e)
        {
            string namingText = NamingTextbox.Text;
            int initialNumber;
            int increment;

            try
            {
                initialNumber = string.IsNullOrEmpty(NumberingTextbox.Text) ? 1 : int.Parse(NumberingTextbox.Text);
                increment = string.IsNullOrEmpty(IncrementTextbox.Text) ? 1 : int.Parse(IncrementTextbox.Text);

            }
            catch (FormatException ex)
            {
                MessageBox.Show(Resource.ExecuteRenameErrorMessage, Resource.InvalideInputTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                NumberingTextbox.Text = null;
                IncrementTextbox.Text = null;
                return;
            }

            Utility.CounterOptions renameOptions =
                NumberingSwitch.IsChecked == true ? Utility.CounterOptions.Suffix : Utility.CounterOptions.Prefix;

            Utility.RenameFiles(_renameList, namingText, renameOptions, initialNumber, increment);

            _renameList.Clear();
            _fileItemContainer.ClearOrder();
            FileListView.ItemsSource = null;
            FileListView.ItemsSource = _fileItemContainer.FileItems;
            UpdateSelectAllButton();
        }

        private void MarkAllButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (FileItem file in _fileItemContainer.FileItems)
            {
                _renameList.Add(file);
                file.Order = _renameList.IndexOf(file) + 1;
            }
            UpdateSelectAllButton();
        }

        private void ClearAllButton_Click(object sender, RoutedEventArgs e)
        {
            _renameList.Clear();
            _fileItemContainer.ClearOrder();
            UpdateSelectAllButton();
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBoxResult result = MessageBoxResult.None;
            if (!_isStartup)
            {
                result = MessageBox.Show(Resource.RestartMessage, Resource.RestartTitle, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            }
            if(result == MessageBoxResult.Yes || result == MessageBoxResult.None)
            {
                LanguageOption selectedLanguage = (LanguageOption)LanguageComboBox.SelectedItem;

                LanguageManager.UpdateLanguageSetting(selectedLanguage.Culture, _isStartup);

            }
        }

    }
}