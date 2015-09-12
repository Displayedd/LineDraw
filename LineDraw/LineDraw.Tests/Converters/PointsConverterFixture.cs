using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LineDraw.Interfaces;
using LineDraw.Models;
using Moq;
using LineDraw.Converters;
using System.Windows.Media;

namespace LineDraw.Tests.Models
{
    [TestClass]
    public class PointsConverterFixture
    {
        [TestMethod]
        public void ShouldConvertToPointsArray()
        {
            //Prepare
            PointsConverter target = new PointsConverter();
            Point[] points = new Point[] { new Point { X = 1, Y = 2 }, new Point { X = 3, Y = 4 } };

            //Act
            PointCollection result = (PointCollection)target.Convert(points, null, null, null);

            //Verify
            Assert.IsInstanceOfType(result, typeof(PointCollection));
            Assert.AreEqual(1, result[0].X);
            Assert.AreEqual(2, result[0].Y);
            Assert.AreEqual(3, result[1].X);
            Assert.AreEqual(4, result[1].Y);
        }

        [TestMethod]
        public void ShouldConvertToPointsCollection()
        {
            //Prepare
            PointsConverter target = new PointsConverter();
            PointCollection points = new PointCollection();
            points.Add(new System.Windows.Point(1, 2));
            points.Add(new System.Windows.Point(3, 4));

            //Act
            Point[] result = (Point[])target.ConvertBack(points, null, null, null);

            //Verify
            Assert.IsInstanceOfType(result, typeof(Point[]));
            Assert.AreEqual(1, result[0].X);
            Assert.AreEqual(2, result[0].Y);
            Assert.AreEqual(3, result[1].X);
            Assert.AreEqual(4, result[1].Y);
        }
    }
}
