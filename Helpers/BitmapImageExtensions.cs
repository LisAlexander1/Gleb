﻿using System.IO;
using System.Windows.Media.Imaging;

namespace Gleb.Helpers;

public static class BitmapImageExtensions
{
    public static BitmapImage ToImage(this byte[] array)
    {
        using (var ms = new MemoryStream(array))
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad; // here
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }
}