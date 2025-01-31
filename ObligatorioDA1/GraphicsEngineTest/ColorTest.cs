﻿using System;
using GraphicsEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphicsEngineTest
{
    [TestClass]
    public class ColorTest
    {
        private Color colorVector;

        [TestInitialize]
        public void CreateVectorForColors()
        {
            colorVector = new Color
            {
                R = 0.3m,
                G = 0.6m,
                B = 0.9m
            };
        }


        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void CreateColor_InvalidColor_FAIL()
        {
            var c = new Color
            {
                R = 1.1m,
                G = -0.3m,
                B = 0.5m
            };
        }

        [TestMethod]
        public void GetColors_GetRed_OK()
        {
            var redAmount = colorVector.Red();
            Assert.AreEqual(77, redAmount);
        }

        [TestMethod]
        public void GetColors_GetGreen_OK()
        {
            var greenAmount = colorVector.Green();
            Assert.AreEqual(153, greenAmount);
        }

        [TestMethod]
        public void GetColors_GetBlue_OK()
        {
            var blueAmount = colorVector.Blue();
            Assert.AreEqual(230, blueAmount);
        }
    }
}