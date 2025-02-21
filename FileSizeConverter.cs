using System.Globalization;
using System.Windows.Data;

namespace FileRenamer
{
    public class FileSizeConverter : IValueConverter
    {
        //TODO introduce constants for better readability
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is long size)
            {
                if (size >= 1 << 30)
                    return $"{size / (1 << 30)} GB";
                if (size >= 1 << 20)
                    return $"{size / (1 << 20)} MB"; 
                if (size >= 1 << 10)
                    return $"{size / (1 << 10)} KB";
                return $"{size} bytes";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
