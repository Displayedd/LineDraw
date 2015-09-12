using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LineDraw.Interfaces;
using LineDraw.Models;
using Moq;

namespace LineDraw.Tests.Models
{
    [TestClass]
    public class GraphCanvasModelFixture
    {
        [TestMethod]
        public void WhenConstructed_InitializesValues()
        {
            //Prepare
            Mock<ILineCalculator> mockedLineCalculator = new Mock<ILineCalculator>();

            //Act
            GraphCanvasModel target = new GraphCanvasModel(mockedLineCalculator.Object);

            //Verify
            Assert.IsInstanceOfType(target, typeof(ICanvasModel));
            Assert.AreEqual(500, target.Height);
            Assert.AreEqual(500, target.Width);
            Assert.IsInstanceOfType(target.Graph, typeof(Node[][]));
            Assert.AreEqual(500, target.Graph.Length);
            Assert.AreEqual(500, target.Graph[0].Length);
        }

        [TestMethod]
        public void WhenAddLineCalled_ReturnsAddedLine()
        {
            //Prepare
            Point startPoint = new Point { X = 10, Y = 10 };
            Point endPoint = new Point { X = 50, Y = 50 };
            Point[] line = new Point[] { startPoint, endPoint };

            Mock<ILineCalculator> mockedLineCalculator = new Mock<ILineCalculator>();
            mockedLineCalculator.Setup(x => x.CalculateLine(It.IsAny<Node[][]>(),It.IsAny<Point>(),
                It.IsAny<Point>())).Returns(line).Verifiable();

            GraphCanvasModel target = new GraphCanvasModel(mockedLineCalculator.Object);

            //Act
            Point[] result = target.AddLine(startPoint, endPoint);

            //Verify
            mockedLineCalculator.VerifyAll();
            Assert.AreEqual(result[0].X, startPoint.X);
            Assert.AreEqual(result[0].Y, startPoint.Y);
            Assert.AreEqual(result[1].X, endPoint.X);
            Assert.AreEqual(result[1].Y, endPoint.Y);
            Assert.IsTrue(target.Graph[startPoint.X][startPoint.Y].Occupied);
            Assert.IsTrue(target.Graph[endPoint.X][endPoint.Y].Occupied);
        }

        [TestMethod]
        public void WhenClearLinesCalled_LinesCleared()
        {
            //Prepare
            Point startPoint = new Point { X = 10, Y = 10 };
            Point endPoint = new Point { X = 50, Y = 50 };
            Point[] line = new Point[] { startPoint, endPoint };

            Mock<ILineCalculator> mockedLineCalculator = new Mock<ILineCalculator>();
            mockedLineCalculator.Setup(x => x.CalculateLine(It.IsAny<Node[][]>(), It.IsAny<Point>(),
                It.IsAny<Point>())).Returns(line).Verifiable();

            GraphCanvasModel target = new GraphCanvasModel(mockedLineCalculator.Object);
            target.AddLine(startPoint, endPoint);

            //Act
            target.ClearLines();

            //Verify
            mockedLineCalculator.VerifyAll();
            Assert.IsFalse(target.Graph[startPoint.X][startPoint.Y].Occupied);
            Assert.IsFalse(target.Graph[endPoint.X][endPoint.Y].Occupied);
        }
    }
}
