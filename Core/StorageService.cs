using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class StorageService
    {


        public static void Add(ICamera grabber)
        {
            List<ICamera> videoGrabbers = JsonFileHander.LoadGrabbersFromFile();
            videoGrabbers.Add(grabber);
            JsonFileHander.SaveGrabbersToFile(videoGrabbers);
        }
        public static void Add(List<ICamera> grabbers)
        {
            List<ICamera> videoGrabbers = JsonFileHander.LoadGrabbersFromFile();
            foreach (var item in grabbers)
            {
                item.SetUrl(item.GetUrl());
                videoGrabbers.Add(item);
            }
            JsonFileHander.SaveGrabbersToFile(videoGrabbers);
        }
        public static List<ICamera> GetAll()
        {
            List<ICamera> videoGrabbers = JsonFileHander.LoadGrabbersFromFile();
            return videoGrabbers;
        }

        public static ICamera GetByID(Guid id)
        {
            List<ICamera> videoGrabbers = JsonFileHander.LoadGrabbersFromFile();
            var item2del = videoGrabbers.Find(x => x.id.Equals(id));
            return item2del;
        }

        public static bool RemoveByID(Guid id)
        {
            List<ICamera> videoGrabbers = JsonFileHander.LoadGrabbersFromFile();
            var item2del = videoGrabbers.Find(x => x.id.Equals(id));
            bool status = videoGrabbers.Remove(item2del);
            JsonFileHander.SaveGrabbersToFile(videoGrabbers);
            Console.WriteLine("Removed: " + status);
            Console.WriteLine("Video grabbers count: " + videoGrabbers.Count);
            return status;
        }

        public static bool RemoveByItem(ICamera item)
        {
            List<ICamera> videoGrabbers = JsonFileHander.LoadGrabbersFromFile();

            Guid guid = item.id;
            Console.WriteLine("GUID TO DELETE: " + guid.ToString());
            Console.WriteLine("URL TO DELETE: " + item.url);

            var item2del = videoGrabbers.Find(x => x.id.Equals(item.id));
            bool status = videoGrabbers.Remove(item2del);
            JsonFileHander.SaveGrabbersToFile(videoGrabbers);
            Console.WriteLine("Removed: " + status);
            Console.WriteLine("Video grabbers count: " + videoGrabbers.Count);
            return status;
            
        }
        /// <summary>
        /// It will also remove grabbers data from file!!! 
        /// Be carefour !!!!
        /// </summary>
        public static void RemoveAll()
        {
            List<ICamera> videoGrabbers = new List<ICamera>();
            JsonFileHander.SaveGrabbersToFile(videoGrabbers);
        }


        public static int GetCount()
        {
            List<ICamera> videoGrabbers = JsonFileHander.LoadGrabbersFromFile();
            return videoGrabbers.Count;
        }


    }
}
