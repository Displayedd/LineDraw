using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LineDraw.Interfaces;
using LineDraw.Models;

namespace LineDraw.Tests.Models
{
    [TestClass]
    public class NaiveCanvasModelFixture
    {
        [TestMethod]
        public void WhenConstructed_InitializesValues()
        {
            //Prepare

            //Act
            NaiveCanvasModel target = new NaiveCanvasModel();

            //Verify
            Assert.IsInstanceOfType(target, typeof(ICanvasModel));
            Assert.AreEqual(500, target.Height);
            Assert.AreEqual(500, target.Width);
        }

        [TestMethod]
        public void WhenAddLineCalled_ReturnsAddedLine()
        {
            //Prepare
            NaiveCanvasModel target = new NaiveCanvasModel();
            Point startPoint = new Point { X = 0, Y = 1 };
            Point endPoint = new Point { X = 1, Y = 1 };

            //Act
            Point[] result = target.AddLine(startPoint, endPoint, PathAlgorithm.BFS);

            //Verify
            Assert.AreEqual(result[0].X, startPoint.X);
            Assert.AreEqual(result[0].Y, startPoint.Y);
            Assert.AreEqual(result[1].X, endPoint.X);
            Assert.AreEqual(result[1].Y, endPoint.Y);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void WhenClearLinesCalled_ThrowsException()
        {
            //Prepare
            NaiveCanvasModel target = new NaiveCanvasModel();

            //Act
            target.ClearLines();

            //Verify
        }
    }
}
