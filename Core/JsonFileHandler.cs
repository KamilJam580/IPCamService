using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Core
{
    public static class CameraType
    {
        public const string
            Vivotek_HTTP_MJPG_GRABBER = "Vivotek_HTTP_MJPG_GRABBER",
            Vivotek_HTTP_RTSP_GRABBER = "Vivotek_RTSP_MJPG_GRABBER";
    }
    public static class JsonFileHander
    {

        static string GrabberConfigPath = "GrabbersConfig.json";
        private static readonly object fileLock = new object();
        public static List<ICamera> LoadGrabbersFromFile()
        {
            string readText;
            lock (fileLock)
            {
                readText = File.ReadAllText(GrabberConfigPath);
            }
            try
            {
                var deserialized = JsonConvert.DeserializeObject(readText);
                JArray jArray = (JArray)deserialized;
                List<ICamera> grabbers = new List<ICamera>();

                foreach (JObject jobject in jArray)
                {
                    string type = (string)jobject["type"];

                    ICamera item = CameraFactory(jobject, type);
                    grabbers.Add(item);
                }
                return grabbers;
            }
            catch (Exception)
            {
                return new List<ICamera>();
            }



        }

        private static ICamera CameraFactory(JObject jobject, string type)
        {
            ICamera item = null;
            if (type == CameraType.Vivotek_HTTP_MJPG_GRABBER)
            {
                item = jobject.ToObject<Vivotek_HTTP_MJPG_GRABBER>();
            }
            return item;
        }

        public static void SaveGrabbersToFile(List<ICamera> grabbers)
        {
            var jsonString = JsonConvert.SerializeObject(grabbers, Formatting.Indented);
            lock (fileLock)
            {
                using (StreamWriter writer = new StreamWriter(GrabberConfigPath))
                {
                    writer.WriteLine(jsonString);
                }
            }

        }


    }


}
