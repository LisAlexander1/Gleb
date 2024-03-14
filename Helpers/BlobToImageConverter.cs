using System.Globalization;
using System.Windows.Data;

namespace Gleb.Helpers;

public class BlobToImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var blobData = value as byte[];
        if (blobData == null || blobData.Length == 0) return null;

        try
        {
            return blobData.ToImage();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return default;
    }
}