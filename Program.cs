using System;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace tiny_capture
{
    class Program
    {
        static int Main(string[] args)
        {
            if(args.Length != 2 || !Microsoft.VisualBasic.Information.IsNumeric(args[0]))
            {
                Console.WriteLine("Usage: tiny-capture <INDEX> <PATH>");
                return 1;
            }

            int index = int.Parse(args[0]);
            VideoCapture capture = new VideoCapture(index, VideoCaptureAPIs.DSHOW);
            if(!capture.IsOpened())
            {
                Console.WriteLine("The specified device index is invalid.");
                capture.Release();
                return 1;
            }

            capture.FrameWidth = 1920;
            capture.FrameHeight = 1080;

            Mat frame = new Mat();
            capture.Read(frame);

            string fn = args[1];

            try
            {
                BitmapConverter.ToBitmap(frame).Save(fn);
            }
            catch
            {
                Console.WriteLine("Failed to save image.");
                capture.Release();
                return 1;
            }

            Console.WriteLine(fn);

            capture.Release();

            return 0;
        }
    }
}
