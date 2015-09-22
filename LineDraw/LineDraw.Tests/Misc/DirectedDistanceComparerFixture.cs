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
    public class DirectedDistanceComparerFixture
    {
        [TestMethod]
        public void WhenComparingEqual_ReturnsZero()
        {
            //Prepare
            Node a = new Node { Distance = 1, X = 1, Y = 1 };
            Node b = new Node { Distance = 1, X = 1, Y = 1 };
            Node goal = new Node { X = 5, Y = 5 };
            DirectedDistanceComparer<Node> target = new DirectedDistanceComparer<Node>(goal);

            //Act
            int result = target.Compare(a, b);

            //Verify
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void WhenComparingSmaller_ReturnsPostive()
        {
            //Prepare
            Node a = new Node { Distance = 1, X = 1, Y = 1 };
            Node b = new Node { Distance = 1, X = 2, Y = 2 };
            Node c = new Node { Distance = 1, X = 1, Y = 1 };
            Node d = new Node { Distance = 2, X = 1, Y = 1 };

            Node goal = new Node { X = 5, Y = 5 };
            DirectedDistanceComparer<Node> target = new DirectedDistanceComparer<Node>(goal);

            //Act
            int result1 = target.Compare(a, b);
            int result2 = target.Compare(d, c);

            //Verify
            Assert.IsTrue(result1 > 0);
            Assert.IsTrue(result2 > 0);
        }

        [TestMethod]
        public void WhenComparingLarger_ReturnsNegative()
        {
            //Prepare
            Node a = new Node { Distance = 1, X = 2, Y = 2 };
            Node b = new Node { Distance = 1, X = 1, Y = 1 };
            Node c = new Node { Distance = 1, X = 1, Y = 1 };
            Node d = new Node { Distance = 2, X = 1, Y = 1 };

            Node goal = new Node { X = 5, Y = 5 };
            DirectedDistanceComparer<Node> target = new DirectedDistanceComparer<Node>(goal);

            //Act
            int result1 = target.Compare(a, b);
            int result2 = target.Compare(c, d);

            //Verify
            Assert.IsTrue(result1 < 0);
            Assert.IsTrue(result2 < 0);
        }
    }
}
