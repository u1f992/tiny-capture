using System;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace tiny_capture
{
    class Program
    {
        static int Main(string[] args)
        {
            if(args.Length != 1 || !Microsoft.VisualBasic.Information.IsNumeric(args[0]))
            {
                Console.WriteLine("Usage: .\\tiny-capture.exe <INDEX>");
                return 1;
            }

            VideoCapture capture = new VideoCapture(args[0]);
            if(!capture.IsOpened())
            {
                Console.WriteLine("The specified device index is invalid.");
                capture.Dispose();
                return 1;
            }

            capture.FrameWidth = 1920;
            capture.FrameHeight = 1080;

            Mat frame = new Mat();
            capture.Read(frame);

            string fn = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";

            try
            {
                BitmapConverter.ToBitmap(frame).Save(fn);
            }
            catch
            {
                Console.WriteLine("Failed to save image.");
                capture.Dispose();
                frame.Dispose();
                return 1;
            }

            Console.WriteLine(fn);
            capture.Dispose();
            frame.Dispose();

            return 0;
        }
    }
}
