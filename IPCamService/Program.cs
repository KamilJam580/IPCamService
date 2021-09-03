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
            GrabbersHandler grabbers = new GrabbersHandler();
            /*
            string url = "http://169.254.2.93/video.mjpg";
            IVideoGrabber grabber1 = new Vivotek_HTTP_MJPG_GRABBER();
            grabber1.Create(url);

            string url2 = "http://169.254.178.49/video2.mjpg";
            IVideoGrabber grabber2 = new Vivotek_HTTP_MJPG_GRABBER();
            grabber2.Create(url2);

 
            grabbers.Add(grabber1);
            grabbers.Add(grabber2);

            ConfigService.SaveGrabbersConfig(grabbers.GetAll());
            */

            
            List<IVideoGrabber> list =  ConfigService.LoadGrabbersFromFile();
            grabbers.Add(list);

            //grabbers.InitAll();

            while (true)
            {
                List<IVideoGrabber> videoGrabbers = grabbers.GetAll();
                foreach (var grabber in videoGrabbers)
                {
                    //Console.WriteLine(grabber.GetName());
                    Mat frame = new Mat();
                    CvInvoke.Imshow(grabber.GetUrl(), grabber.GetFrame()); ;
                }
                CvInvoke.WaitKey(10);

            }


        }
    }
}
