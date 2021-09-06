using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public abstract class ICamera
    {
 
        public abstract void SetUrl(string URL);
        public abstract Mat GetFrame();
        public abstract string GetUrl();
        public abstract Guid GetId();
        public abstract void Init();

        public Guid id;
        public string url;


    }

    public class Vivotek_HTTP_MJPG_GRABBER : ICamera
    {

        public string type = CameraType.Vivotek_HTTP_MJPG_GRABBER;
        VideoCapture capture;
        Mat frame;

        public Vivotek_HTTP_MJPG_GRABBER()
        {
            this.id = Guid.NewGuid();
        }


        public override void SetUrl(string URL)
        {
            this.url = URL;
        }

        public override void Init()
        {
            this.capture = new VideoCapture(url);
            this.frame = new Mat();
        }

        public override Mat GetFrame()
        {
            capture.Read(frame);
            return frame;
        }

        public override string GetUrl()
        {
            return this.url;
        }

        public override Guid GetId()
        {
            return this.id;
        }
    }

}
