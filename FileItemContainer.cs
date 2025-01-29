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
        }

        public ObservableCollection<FileItem> FileItems => _fileItems;

        /// <summary>
        /// Adds a FileItem object to the collection.
        /// </summary>
        /// <param name="fileItem">The FileItem object to add.</param>
        /// <exception cref="ArgumentNullException">Thrown when the fileItem is null.</exception>
        private void AddFileItem(FileItem fileItem)
        {
            if (fileItem == null)
                throw new ArgumentNullException(nameof(fileItem));

            _fileItems.Add(fileItem);
        }

        /// <summary>
        /// Adds FileItem objects to the collection from the specified directory path.
        /// </summary>
        /// <param name="path">The directory path containing files to add.</param>
        /// <exception cref="ArgumentNullException">Thrown when the path is null or empty.</exception>
        /// <exception cref="DirectoryNotFoundException">Thrown when the specified path does not exist.</exception>
        public void AddFileItems(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

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

        public void Clear()
        {
            _fileItems.Clear();
        }
    }
}
