using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfApp1
{
    public class StringToUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue && Uri.IsWellFormedUriString(stringValue, UriKind.RelativeOrAbsolute))
            {
                return new Uri(stringValue, UriKind.RelativeOrAbsolute);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}