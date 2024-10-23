using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

public class ScreenCaptureService
{
    public byte[] CaptureScreen(Rectangle bounds)
    {
        using (var bmp = new Bitmap(bounds.Width, bounds.Height))
        {
            using (var g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(bounds.Location, System.Drawing.Point.Empty, bounds.Size);
            }

            using (var ms = new MemoryStream())
            {
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }

    public BitmapSource ByteArrayToImageSource(byte[] byteArray)
    {
        using (var ms = new MemoryStream(byteArray))
        {
            var bitmap = new Bitmap(ms);
            IntPtr hBitmap = bitmap.GetHbitmap();
            var imageSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(hBitmap); // Libera o handle
            return imageSource;
        }
    }

    [System.Runtime.InteropServices.DllImport("gdi32.dll")]
    private static extern bool DeleteObject(IntPtr hObject);
}
