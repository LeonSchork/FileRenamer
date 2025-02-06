using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FileRenamer
{
    public class FileItem : INotifyPropertyChanged
    {
        private int? _order;
        public string Name { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
        public string FolderPath { get; set; }

        public int? Order
        {
            get => _order;
            set
            {
                if (_order != value)
                {
                    _order = value;
                    OnPropertyChanged(nameof(Order));
                    OrderChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public static event EventHandler OrderChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string FullPath()
        {
            return Path.Combine(FolderPath, Name, Extension);
        }
    }
}
