using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace BitmapPivot
{
    class MyImage
    {

         public string TypeImage { get; private set; }
        public long TailleFichier { get; private set; }
        public int Decalage { get; private set; }
        public int Largeur { get; private set; }
        public int Hauteur { get; private set; }
        public int BitsParCouleur { get; private set; }
        public byte[,,] Pixels { get; private set; }


        [SupportedOSPlatform("windows")]
        public MyImage(Image image)
        {
            TypeImage = ImageHelper.GetImageFileExtension(image);
            TailleFichier = ImageHelper.GetFileSize(image);
            Decalage = 0;
            Largeur = image.Width;
            Hauteur = image.Height;
            BitsParCouleur = Image.GetPixelFormatSize(image.PixelFormat);
            Pixels = ImageHelper.ReadPixelData(image);

        }

        public void From_Image_To_File(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }

            using (FileStream fs = new FileStream(file, FileMode.CreateNew))
            {
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    for (int y = 0; y < Hauteur; y++)
                    {
                        for (int x = 0; x < Largeur; x++)
                        {
                            writer.Write(Pixels[x, y, 0]);
                            writer.Write(Pixels[x, y, 1]);
                            writer.Write(Pixels[x, y, 2]);
                        }
                    }
                }
            }
        }


        public static int Convertir_Endian_To_Int(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                throw new ArgumentException("Byte array cannot be null or empty.", nameof(bytes));

            if (bytes.Length < 4)
                throw new ArgumentException("Byte array does not contain enough bytes to convert to an integer.", nameof(bytes));

            int value = bytes[0] | (bytes[1] << 8) | (bytes[2] << 16) | (bytes[3] << 24);

            return value;
        }

        public static byte[] Convertir_Int_To_Endian(int val)
        {
            byte[] bytes = new byte[4]; // Int32 size is 4 bytes

            // Convert integer to little-endian bytes
            bytes[0] = (byte)(val & 0xFF);
            bytes[1] = (byte)((val >> 8) & 0xFF);
            bytes[2] = (byte)((val >> 16) & 0xFF);
            bytes[3] = (byte)((val >> 24) & 0xFF);

            return bytes;
        }

        public override string? ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"TypeImage: {TypeImage}");
            sb.AppendLine($"TailleFichier: {TailleFichier}");
            sb.AppendLine($"Decalage: {Decalage}");
            sb.AppendLine($"Largeur: {Largeur}");
            sb.AppendLine($"Hauteur: {Hauteur}");
            sb.AppendLine($"BitsParCouleur: {BitsParCouleur}");


            return sb.ToString();
        }
    }
}
