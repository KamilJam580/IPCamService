using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Core
{
    public static class GrabberType
    {
        public const string
            Vivotek_HTTP_MJPG_GRABBER = "Vivotek_HTTP_MJPG_GRABBER",
            Vivotek_HTTP_RTSP_GRABBER = "Vivotek_RTSP_MJPG_GRABBER";
    }
    public static class ConfigService
    {

        static string GrabberConfigPath = "GrabbersConfig.json";

        public static List<IVideoGrabber> LoadGrabbersFromFile()
        {
            string readText = File.ReadAllText(GrabberConfigPath);
            try
            {
                var deserialized = JsonConvert.DeserializeObject(readText);
                JArray jArray = (JArray)deserialized;
                List<IVideoGrabber> grabbers = new List<IVideoGrabber>();

                foreach (JObject jobject in jArray)
                {
                    string type = (string)jobject["type"];

                    IVideoGrabber item = GrabberFactory(jobject, type);
                    grabbers.Add(item);
                }
                return grabbers;
            }
            catch (Exception)
            {
                return new List<IVideoGrabber>();
            }

        }

        private static IVideoGrabber GrabberFactory(JObject jobject, string type)
        {
            IVideoGrabber item = null;
            if (type == GrabberType.Vivotek_HTTP_MJPG_GRABBER)
            {
                item = jobject.ToObject<Vivotek_HTTP_MJPG_GRABBER>();
            }
            return item;
        }

        public static void SaveGrabbersToFile(List<IVideoGrabber> grabbers)
        {
            var jsonString = JsonConvert.SerializeObject(grabbers, Formatting.Indented);
            using (StreamWriter writer = new StreamWriter(GrabberConfigPath))
            {
                writer.WriteLine(jsonString);
            }
        }


    }


}
