using System.IO;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace Gleb.Helpers;
public class BlobToImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        var blobData = value as byte[];
        if (blobData == null || blobData.Length == 0)
        {
            return null;
        }

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
        
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return default;
    }
}