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
            Point startPoint = new Point {X = 10, Y = 10};
            Point endPoint = new Point {X = 50, Y = 50};
            DijkstraLineCalculator target = new DijkstraLineCalculator();

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
