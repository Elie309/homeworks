using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;

namespace BitmapPivot
{
    public class ImageHelper
    {

        [SupportedOSPlatform("windows")]
        public static string GetImageFileExtension(Image image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image), "Image cannot be null.");

            ImageFormat format = image.RawFormat;
            if (format == null)
                throw new ArgumentException("Invalid image format.", nameof(image));

            return GetImageExtensionFromFormat(format);
        }

        [SupportedOSPlatform("windows")]
        private static string GetImageExtensionFromFormat(ImageFormat format)
        {
            switch (format.Guid.ToString().ToLower())
            {
                case "b96b3caf-0728-11d3-9d7b-0000f81ef32e": // ImageFormat.Bmp.Guid
                    return ".bmp";
                case "b96b3cb0-0728-11d3-9d7b-0000f81ef32e": // ImageFormat.Jpeg.Guid
                    return ".jpg";
                case "b96b3cb1-0728-11d3-9d7b-0000f81ef32e": // ImageFormat.Gif.Guid
                    return ".gif";
                case "b96b3ca5-0728-11d3-9d7b-0000f81ef32e": // ImageFormat.Png.Guid
                    return ".png";
                case "b96b3cae-0728-11d3-9d7b-0000f81ef32e":
                    return ".jpeg";
                default:
                    throw new NotSupportedException($"Unsupported image format: {format}");
            }
        }

        [SupportedOSPlatform("windows")]
        public static long GetFileSize(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.Length;
            }
        }

        [SupportedOSPlatform("windows")]
        public static byte[,,] ReadPixelData(Image image)
        {
            Bitmap bitmap = new Bitmap(image);
            byte[,,] pixels = new byte[bitmap.Width, bitmap.Height, 3];
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    pixels[x, y, 0] = pixelColor.R;
                    pixels[x, y, 1] = pixelColor.G;
                    pixels[x, y, 2] = pixelColor.B;
                }
            }
            return pixels;
        }

    }


}