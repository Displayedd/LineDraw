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
    public class DistanceComparerFixture
    {
        [TestMethod]
        public void WhenComparingEqual_ReturnsZero()
        {
            //Prepare
            Node a = new Node { Distance = 1 };
            Node b = new Node { Distance = 1 };
            DistanceComparer<Node> target = new DistanceComparer<Node>();

            //Act
            int result = target.Compare(a, b);

            //Verify
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void WhenComparingSmaller_ReturnsPostive()
        {
            //Prepare
            Node a = new Node { Distance = 2 };
            Node b = new Node { Distance = 1 };
            DistanceComparer<Node> target = new DistanceComparer<Node>();

            //Act
            int result = target.Compare(a, b);

            //Verify
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void WhenComparingLarger_ReturnsNegative()
        {
            //Prepare
            Node a = new Node { Distance = 1 };
            Node b = new Node { Distance = 2 };
            DistanceComparer<Node> target = new DistanceComparer<Node>();

            //Act
            int result = target.Compare(a, b);

            //Verify
            Assert.IsTrue(result < 0);
        }
    }
}
