using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LineDraw.Interfaces;
using LineDraw.Models;
using LineDraw.Canvas;
using Moq;
using System.Windows.Input;
using System.Collections.ObjectModel;
using LineDraw.Misc;

namespace LineDraw.Tests.Models
{
    [TestClass]
    public class BFSLineCalculatorFixture
    {
        [TestMethod]
        public void WhenCalculateLine_ReturnsPath()
        {
            //Prepare
            int height = 500;
            int width = 500;
            Node[][] graph = GraphTools<Node>.CreateGraph(height, width);
            Point startPoint = new Point {X = 10, Y = 10};
            Point endPoint = new Point { X = 350, Y = 450 };
            BFSLineCalculator target = new BFSLineCalculator();

            //Act
            Point[] result = target.CalculateLine(graph, startPoint, endPoint);

            //Verify
            Assert.IsNotNull(result);
            Assert.AreEqual(startPoint.X, result[0].X);
            Assert.AreEqual(startPoint.Y, result[0].Y );
            Assert.AreEqual(endPoint.X, result[result.Length - 1].X);
            Assert.AreEqual(endPoint.Y, result[result.Length - 1].Y);
        }
    }
}
