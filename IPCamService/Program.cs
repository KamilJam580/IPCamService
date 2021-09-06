using Core;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//http://169.254.2.93/video.mjpg


namespace IPCamService
{
    class Program
    {

        static void Main(string[] args)
        {
            string url1 = "http://169.254.2.93/video.mjpg";
            string url2 = "http://169.254.178.49/video2.mjpg";

            Console.WriteLine("Select action:");
            Console.WriteLine("1 - Add Camera");
            Console.WriteLine("2 - Show Cameras");
            var action = Console.ReadLine();
            if (action == "1")
            {
                Console.Write("URL: ");
                var url = Console.ReadLine();
                ICamera videoGrabber = new Vivotek_HTTP_MJPG_GRABBER();
                videoGrabber.SetUrl(url);
                StorageService.Add(videoGrabber);
            }


            List<ICamera> cameras = StorageService.GetAll();
            foreach (var camera in cameras)
            {
                camera.Init();
            }
            //handler.InitAll();
            while (true)
            {
                foreach (var camera in cameras)
                {
                    CvInvoke.Imshow(camera.GetUrl(), camera.GetFrame()); ;
                }
                CvInvoke.WaitKey(10);

            }


        }
    }
}
