using System.Globalization;
using System.Windows.Data;

namespace Gleb.Helpers;

public class MultiBoolToVisibilityConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        return values.Any(b => (bool)b) ? Visibility.Visible : Visibility.Collapsed;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        return default!;
    }
}