using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FileRenamer
{
    class MainViewModel : INotifyPropertyChanged
    {
        private string _dirInputContent = "Verzeichnis Auswählen";

        public string dirInputContent
        {
            get => _dirInputContent;
            set => SetProperty(ref _dirInputContent, value);
        }

        private string _nameInputContent = "Dateiname";

        public string nameInputContent
        {
            get => _nameInputContent;
            set => SetProperty(ref _nameInputContent, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
