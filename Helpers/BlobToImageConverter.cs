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

        BitmapImage image = new BitmapImage();
        image.BeginInit();
        try
        {
            using var ms = new MemoryStream(blobData);
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = ms;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            image.EndInit();
        }

        return image;
    }
        
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return default;
    }
}