﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LineDraw.Interfaces;
using LineDraw.Models;
using LineDraw.Services;
using Moq;

namespace LineDraw.Tests.Models
{
    [TestClass]
    public class LineServiceFixture
    {
        [TestMethod]
        public void WhenConstructed_InitializesValues()
        {
            //Prepare
            Point[] mockedResult = new Point[] { new Point{X=1,Y=1}, new Point{X=2, Y=2} };
            Mock<ICanvasModel> mockedCanvasModel = new Mock<ICanvasModel>();
            //Act
            ILineService target = new LineService(mockedCanvasModel.Object);

            //Verify
            Assert.IsInstanceOfType(target, typeof(ILineService));
        }

        [TestMethod]
        public void WhenAddLineCalled_QuerySuccessfull()
        {
            //Prepare
            Point startPoint = new Point { X = 1, Y = 1 };
            Point endPoint = new Point { X = 2, Y = 2 };
            Point[] mockedResult = new Point[] { startPoint, endPoint };
            Mock<ICanvasModel> mockedCanvasModel = new Mock<ICanvasModel>();
            mockedCanvasModel.Setup(x => x.AddLine(It.IsAny<Point>(), It.IsAny<Point>())).Returns(mockedResult).Verifiable();
            ILineService target = new LineService(mockedCanvasModel.Object);

            //Act
            LineQueryResult result = target.AddLine(startPoint, endPoint);

            //Verify
            mockedCanvasModel.VerifyAll();
            Assert.AreEqual(true, result.Success);
            Assert.AreEqual(startPoint.X, result.Result[0].X);
            Assert.AreEqual(startPoint.Y, result.Result[0].Y);
            Assert.AreEqual(endPoint.X, result.Result[1].X);
            Assert.AreEqual(endPoint.Y, result.Result[1].Y);
        }

        [TestMethod]
        public void WhenAddLineCalled_QueryUnsuccessfull()
        {
            //Prepare
            Point startPoint = new Point { X = 1, Y = 1 };
            Point endPoint = new Point { X = 2, Y = 2 };
            Point[] mockedResult = new Point[] { startPoint, endPoint };
            Mock<ICanvasModel> mockedCanvasModel = new Mock<ICanvasModel>();
            mockedCanvasModel.Setup(x => x.AddLine(It.IsAny<Point>(), It.IsAny<Point>())).Throws(new Exception("Failed")).Verifiable();
            ILineService target = new LineService(mockedCanvasModel.Object);

            //Act
            LineQueryResult result = target.AddLine(startPoint, endPoint);

            //Verify
            mockedCanvasModel.VerifyAll();
            Assert.AreEqual(false, result.Success);
            Assert.IsFalse(string.IsNullOrEmpty(result.Message));
        }

        [TestMethod]
        public void WhenClearLinesCalled_LinesCleared()
        {
            //Prepare
            Mock<ICanvasModel> mockedCanvasModel = new Mock<ICanvasModel>();
            mockedCanvasModel.Setup(x => x.ClearLines()).Verifiable();
            ILineService target = new LineService(mockedCanvasModel.Object);

            //Act
            target.ClearLines();

            //Verify
            mockedCanvasModel.VerifyAll();
        }

        [TestMethod]
        public void WhenGetCanvasSizeCalled_SizeReturned()
        {
            //Prepare
            Mock<ICanvasModel> mockedCanvasModel = new Mock<ICanvasModel>();
            mockedCanvasModel.SetupGet(x => x.Height).Returns(500).Verifiable();
            mockedCanvasModel.SetupGet(x => x.Width).Returns(600).Verifiable();
            ILineService target = new LineService(mockedCanvasModel.Object);

            //Act
            Size result = target.GetCanvasSize();

            //Verify
            mockedCanvasModel.VerifyAll();
            Assert.AreEqual(500, result.Height);
            Assert.AreEqual(600, result.Width);
        }
    }
}
