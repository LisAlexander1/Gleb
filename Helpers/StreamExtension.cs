﻿using System.IO;

namespace Gleb.Helpers;

public static class StreamExtension
{
    public static byte[] ReadFully(this Stream input)
    {
        var buffer = new byte[16 * 1024];
        using (var ms = new MemoryStream())
        {
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0) ms.Write(buffer, 0, read);
            return ms.ToArray();
        }
    }
}