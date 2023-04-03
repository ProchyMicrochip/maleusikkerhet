using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Media.Imaging;

namespace måleusikkerhet.Infrastructure;

public static class ImageToBitmap
{
    public static Bitmap ToBitmap(Image image)
    {
        throw new NotImplementedException();
    }

    public static Bitmap ToBitmap(byte[] array)
    {
        using var ms = new MemoryStream();
        ms.Write(array);
        ms.Position = 0;
        return Bitmap.DecodeToHeight(ms, 128);
    }
    public static Image ToImage(Bitmap bitmap)
    {
        throw new NotImplementedException();
    }

    public static byte[] ToByteArray(Bitmap bitmap)
    {
        throw new NotImplementedException();
    }
}