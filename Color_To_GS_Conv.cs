using System;
using System.Drawing;
using System.Drawing.Imaging;

public class GrayScaleConvert{

    public static bool ConvertToGrayScale(Bitmap b){
        
        Rectangle rect = new Rectangle(0, 0, b.Width, b.Height);
        BitmapData BData = b.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

        IntPtr ptr = BData.Scan0;

        int bytes = Math.Abs(BData.Stride) * b.Height;
        byte[] rgbValues = new byte[bytes];

        System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

        for(int i = 0; i < rgbValues.Length;i+=3){
            byte gray = (byte)(0.299 * rgbValues[i] + 0.587 * rgbValues[i+1] + 0.114 * rgbValues[i+2]);

            rgbValues[i] = gray;
            rgbValues[i+1] = gray;
            rgbValues[i+2] = gray;
        }

        System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

        
        b.UnlockBits(BData);

        return true;
    }

    public static void Main(string[] args){

        if(args.Length != 1){
            Console.WriteLine("Usage: Color_To_GS_Conv.exe <input_file> ");
            return;
        }

        string InputPath = args[0];
        string uuid = Guid.NewGuid().ToString();
        string OutputPath = "/home/rashid/Desktop/Assignments/Part1 Week1/Training Assignment 5/GrayScaleParrots" + uuid + ".jpeg";

        Bitmap b = new Bitmap(InputPath);
        ConvertToGrayScale(b);
        b.Save(OutputPath, ImageFormat.Jpeg);
    }
}