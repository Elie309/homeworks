using BitmapPivot;
using System.Drawing;
using System.Runtime.Versioning;

class Program
{


    [SupportedOSPlatform("windows")]
    static void Main(string[] args)
    {

        string PATH = "C:\\Users\\User\\Desktop\\";

        string FileName = "tree.jpg";

        Image image = Image.FromFile(PATH + FileName);

        MyImage myImage = new MyImage(image);

        Console.Write(myImage.ToString());

        myImage.From_Image_To_File(PATH + "output_file.bmp");

        int toConvert = image.Width;

        byte[] endian = MyImage.Convertir_Int_To_Endian(toConvert);
        int convertedBack = MyImage.Convertir_Endian_To_Int(endian);

        Console.WriteLine($"Value to convert: {toConvert}");


        Console.Write("Value converted to endian: ");
        foreach (byte b in endian)
        {
            Console.Write($"{b:X2} ");
        }
        Console.WriteLine();


        Console.WriteLine($"Value Converted Back: {convertedBack}");
    }
 }
