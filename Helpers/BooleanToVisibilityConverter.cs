using System.Globalization;
using System.Windows.Data;

namespace Gleb.Helpers;

public class BooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ^ parameter is true ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((Visibility)Enum.Parse(typeof(Visibility), (string)value) == Visibility.Visible) ^ parameter is true;
    }
}