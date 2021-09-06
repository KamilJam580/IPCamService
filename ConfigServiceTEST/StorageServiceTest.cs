using Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConfigServiceTEST
{
    [TestClass]
    public class StorageServiceTest
    {
        string url1 = "http://169.254.2.93/video.mjpg";
        string url2 = "http://169.254.178.49/video2.mjpg";
        string url3 = "http://169.254.20.26/video2.mjpg";
        private void AddGrabber2()
        {
            ICamera camera = new Vivotek_HTTP_MJPG_GRABBER();
            camera.SetUrl(url1);
            StorageService.Add(camera);
        }

        private void AddGrabber1()
        {
            ICamera camera = new Vivotek_HTTP_MJPG_GRABBER();
            camera.SetUrl(url1);
            StorageService.Add(camera);
        }

        [TestMethod]
        public void AddGrabberTest()
        {
            // Arrange
            StorageService.RemoveAll();
            AddGrabber1();
            // Act
            var count = StorageService.GetCount();
            // Assert
            Assert.AreNotEqual(0, count);
        }

        [TestMethod]
        public void ReadConfigTest()
        {
            // Arrange
            StorageService.RemoveAll();
            AddGrabber1();
            AddGrabber2();
            // Act
            var count = StorageService.GetCount();
            // Assert
            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void RemoveByValidItemTest()
        {
            // Arrange
            StorageService.RemoveAll();
            ICamera camera = new Vivotek_HTTP_MJPG_GRABBER();
            camera.SetUrl(url3);
            // Act
            StorageService.Add(camera);

            bool status = StorageService.RemoveByItem(camera);
            // Assert
            Assert.AreEqual(true, status);
        }

        [TestMethod]
        public void RemoveByInvalidItemTest()
        {
            // Arrange
            StorageService.RemoveAll();
            ICamera camera = new Vivotek_HTTP_MJPG_GRABBER();
            camera.SetUrl(url3);

            ICamera camera2 = new Vivotek_HTTP_MJPG_GRABBER();
            camera2.SetUrl(url1);
            // Act
            StorageService.Add(camera);

            bool status = StorageService.RemoveByItem(camera2);
            // Assert
            Assert.AreEqual(false, status);
        }

        [TestMethod]
        public void RemoveByIdTestStatus()
        {
            // Arrange
            StorageService.RemoveAll();
            ICamera camera = new Vivotek_HTTP_MJPG_GRABBER();
            camera.SetUrl(url3);
            // Act
            StorageService.Add(camera);

            bool status = StorageService.RemoveByID(camera.id);
            // Assert
            Assert.AreEqual(true, status);
        }
        [TestMethod]
        public void RemoveByIdTestQuantity()
        {
            // Arrange
            StorageService.RemoveAll();
            ICamera camera = new Vivotek_HTTP_MJPG_GRABBER();
            camera.SetUrl(url3);
            // Act
            StorageService.Add(camera);

            bool status = StorageService.RemoveByID(camera.id);
            // Assert
            Assert.AreEqual(0, StorageService.GetCount());
        }

        [TestMethod]
        public void RemoveAllTest()
        {
            // Arrange
            ICamera camera = new Vivotek_HTTP_MJPG_GRABBER();
            camera.SetUrl(url3);
            // Act
            StorageService.Add(camera);

            StorageService.RemoveAll();
            // Assert
            Assert.AreEqual(0, StorageService.GetCount());
        }

        private void AddCamera(string url)
        {
            ICamera camera = new Vivotek_HTTP_MJPG_GRABBER();
            camera.SetUrl(url1);
            StorageService.Add(camera);
        }
        [TestMethod]
        public void MultiThreadTest()
        {
            // Arrange
            // Act
            Thread thread1 = new Thread(() => AddCamera(url1));
            thread1.Start();

            Thread thread2 = new Thread(() => AddCamera(url2));
            thread2.Start();

            Thread thread3 = new Thread(() => AddCamera(url3));
            thread3.Start();

            Parallel.For (0, 100, (i, state) =>
            {
                ICamera camera = new Vivotek_HTTP_MJPG_GRABBER();
                camera.SetUrl(url1);
                StorageService.Add(camera);
            });

            StorageService.RemoveAll();
            // Assert
            Assert.AreEqual(0, StorageService.GetCount());
        }




    }
}
