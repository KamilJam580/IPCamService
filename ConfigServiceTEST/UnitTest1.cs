using Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ConfigServiceTEST
{
    [TestClass]
    public class UnitTest1
    {
        string url = "http://169.254.2.93/video.mjpg";
        string url2 = "http://169.254.178.49/video2.mjpg";
        string url3 = "http://169.254.20.26/video2.mjpg";
        private void AddGrabber2(GrabbersHandler grabbersHandler)
        {
            IVideoGrabber grabber2 = new Vivotek_HTTP_MJPG_GRABBER();
            grabber2.Create(url);
            grabbersHandler.Add(grabber2);
        }

        private void AddGrabber1(GrabbersHandler grabbersHandler)
        {
            IVideoGrabber grabber1 = new Vivotek_HTTP_MJPG_GRABBER();
            grabber1.Create(url);
            grabbersHandler.Add(grabber1);
        }

        [TestMethod]
        public void AddGrabberTest()
        {
            // Arrange
            GrabbersHandler grabbersHandler = new GrabbersHandler();
            grabbersHandler.RemoveAll();
            AddGrabber1(grabbersHandler);

            // Act
            var count = grabbersHandler.GetCount();
            // Assert
            Assert.AreNotEqual(0, count);
        }

        [TestMethod]
        public void ReadConfigTest()
        {
            // Arrange
            GrabbersHandler grabbersHandler = new GrabbersHandler();
            grabbersHandler.RemoveAll();

            AddGrabber1(grabbersHandler);
            AddGrabber2(grabbersHandler);
            // Act
            var count = grabbersHandler.GetCount();
            // Assert
            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void RemoveByValidItemTest()
        {
            // Arrange
            GrabbersHandler grabbersHandler = new GrabbersHandler();
            grabbersHandler.RemoveAll();
            IVideoGrabber grabber = new Vivotek_HTTP_MJPG_GRABBER();
            grabber.Create(url3);
            // Act
            grabbersHandler.Add(grabber);

            bool status = grabbersHandler.RemoveByItem(grabber);
            // Assert
            Assert.AreEqual(true, status);
        }

        [TestMethod]
        public void RemoveByInvalidItemTest()
        {
            // Arrange
            GrabbersHandler grabbersHandler = new GrabbersHandler();
            grabbersHandler.RemoveAll();
            IVideoGrabber grabber = new Vivotek_HTTP_MJPG_GRABBER();
            grabber.Create(url3);

            IVideoGrabber grabber2 = new Vivotek_HTTP_MJPG_GRABBER();
            grabber2.Create(url);
            // Act
            grabbersHandler.Add(grabber);

            bool status = grabbersHandler.RemoveByItem(grabber2);
            // Assert
            Assert.AreEqual(false, status);
        }



    }
}
