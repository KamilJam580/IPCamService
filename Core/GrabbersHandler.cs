using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class GrabbersHandler
    {
        //List<IVideoGrabber> list = ConfigService.LoadGrabbersConfig();
        List<IVideoGrabber> videoGrabbers = new List<IVideoGrabber>();

        public void Add(IVideoGrabber grabber)
        {
            videoGrabbers.Clear();
            videoGrabbers = ConfigService.LoadGrabbersFromFile();
            videoGrabbers.Add(grabber);
            ConfigService.SaveGrabbersToFile(videoGrabbers);
        }
        public void Add(List<IVideoGrabber> grabbers)
        {
            foreach (var item in grabbers)
            {
                item.Create(item.GetUrl());
                videoGrabbers.Add(item);
            }
        }
        public List<IVideoGrabber> GetAll()
        {
            videoGrabbers.Clear();
            videoGrabbers = ConfigService.LoadGrabbersFromFile();
            return videoGrabbers;
        }

        public IVideoGrabber GetByID(int id)
        {
            return videoGrabbers.ElementAt(id);
        }

        public void RemoveByID(Guid id)
        {

            //videoGrabbers.RemoveAt(id);
        }

        public bool RemoveByItem(IVideoGrabber item)
        {
            videoGrabbers.Clear();
            videoGrabbers = ConfigService.LoadGrabbersFromFile();

            Guid guid = item.id;
            Console.WriteLine("GUID TO DELETE: " + guid.ToString());
            Console.WriteLine("URL TO DELETE: " + item.url);

            var item2del = videoGrabbers.Find(x => x.id.Equals(item.id));
            bool status = videoGrabbers.Remove(item2del);
            Console.WriteLine("Removed: " + status);
            Console.WriteLine("Video grabbers count: " + videoGrabbers.Count);
            ConfigService.SaveGrabbersToFile(videoGrabbers);
            return status;
            
        }
        /// <summary>
        /// It will also remove grabbers data from file!!! 
        /// Be carefour !!!!
        /// </summary>
        public void RemoveAll()
        {
            videoGrabbers.Clear();
            ConfigService.SaveGrabbersToFile(videoGrabbers);
        }

        public void InitAll()
        {
            foreach (var item in videoGrabbers)
            {
                item.Init();
            }
        }

        public int GetCount()
        {
            videoGrabbers.Clear();
            videoGrabbers = ConfigService.LoadGrabbersFromFile();
            return videoGrabbers.Count;
        }

        public GrabbersHandler()
        {

        }

    }
}
