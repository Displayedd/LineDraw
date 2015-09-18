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
    public class DijkstraLineCalculatorFixture
    {
        [TestMethod]
        public void WhenCalculateLine_ReturnsPath()
        {
            //Prepare
            int height = 100;
            int width = 200;
            PriorityQueueNode[][] graph = GraphTools<PriorityQueueNode>.CreateGraph(height, width);
            Point startPoint1 = new Point {X = 10, Y = 10};
            Point endPoint1 = new Point {X = 50, Y = 50};
            Point startPoint2 = new Point { X = 15, Y = 10 };
            Point endPoint2 = new Point { X = 60, Y = 50 };
            DijkstraLineCalculator target = new DijkstraLineCalculator();

            //Act
            Point[] result1 = target.CalculateLine(graph, startPoint1, endPoint1);
            Point[] result2 = target.CalculateLine(graph, startPoint2, endPoint2);

            //Verify
            Assert.IsNotNull(result1);
            Assert.AreEqual(startPoint1.X, result1[0].X);
            Assert.AreEqual(startPoint1.Y, result1[0].Y );
            Assert.AreEqual(endPoint1.X, result1[result1.Length - 1].X);
            Assert.AreEqual(endPoint1.Y, result1[result1.Length - 1].Y);

            Assert.IsNotNull(result2);
            Assert.AreEqual(startPoint2.X, result2[0].X);
            Assert.AreEqual(startPoint2.Y, result2[0].Y);
            Assert.AreEqual(endPoint2.X, result2[result2.Length - 1].X);
            Assert.AreEqual(endPoint2.Y, result2[result2.Length - 1].Y);
        }
    }
}
