using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace FileRenamer
{
    public class FileItemContainer
    {
        private ObservableCollection<FileItem> _fileItems;

        public FileItemContainer()
        {
            _fileItems = new ObservableCollection<FileItem>();
            FileItem.OrderChanged += (s, e) => UpdateOrders();
        }

        public ObservableCollection<FileItem> FileItems => _fileItems;

        private void AddFileItem(FileItem fileItem)
        {
            if (fileItem == null)
                throw new ArgumentNullException(nameof(fileItem));

            _fileItems.Add(fileItem);
        }

        public void AddFileItems(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            //TODO localize exception message
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException($"The directory '{path}' does not exist.");

            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                AddFileItem(new FileItem
                {
                    Name = Path.GetFileNameWithoutExtension(file),
                    Extension = Path.GetExtension(file),
                    Size = fileInfo.Length,
                    FolderPath = path
                });
            }
        }

        private void UpdateOrders()
        {
            var orderedItems = _fileItems.Where(fi => fi.Order.HasValue)
                                         .OrderBy(fi => fi.Order)
                                         .ToList();

            for (int i = 0; i < orderedItems.Count; i++)
            {
                orderedItems[i].Order = i + 1;
            }

            var unOrderedItems = _fileItems.Where(fi => !fi.Order.HasValue).ToList();
            foreach (var item in unOrderedItems)
            {
                item.Order = null;
            }
        }

        public void ClearOrder()
        {
            foreach (var fileItem in FileItems)
            {
                fileItem.Order = null;
            }
        }

        public void Clear()
        {
            _fileItems.Clear();
        }
    }
}
