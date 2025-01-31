﻿using System;
using GraphicsEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphicsEngineTest
{
    [TestClass]
    public class PPMImageTest
    {
        [TestMethod]
        public void SavePixel_ValidPositions_OK()
        {
            var image = new PPMImage(3);
            var pixelColor = new Color
            {
                R = 0.5m,
                G = 1,
                B = 0
            };
            image.SavePixel(1, 0, pixelColor);
            Assert.AreEqual(pixelColor, image.PixelData[0, 0]);
        }

        [TestMethod]
        public void SavePixel_OtherValidPosition_OK()
        {
            var image = new PPMImage(3);
            var pixelColor = new Color
            {
                R = 0.5m,
                G = 1,
                B = 0
            };
            image.SavePixel(0, 0, pixelColor);
            Assert.AreEqual(pixelColor, image.PixelData[1, 0]);
        }

        [ExpectedException(typeof(OverflowException))]
        [TestMethod]
        public void SavePixel_InvalidPosition_FAIL()
        {
            var image = new PPMImage(3);
            var pixelColor = new Color
            {
                R = 0.5m,
                G = 1,
                B = 0
            };
            image.SavePixel(-1, 0, pixelColor);
        }
    }
}