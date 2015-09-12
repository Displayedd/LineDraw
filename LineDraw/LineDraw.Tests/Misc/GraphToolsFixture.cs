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
    public class GraphToolsFixture
    {
        [TestMethod]
        public void WhenCreateGraphCalled_ReturnsMatrix()
        {
            //Prepare
            int height = 100;
            int width = 200;

            //Act
            Node[][] result = GraphTools<Node>.CreateGraph(height, width);

            //Verify
            Assert.AreEqual(width, result.Length);
            Assert.AreEqual(height, result[0].Length);
            for (int i = 0; i < result.Length; i++)
                for (int j = 0; j < result[i].Length; j++)
                {
                    Assert.IsInstanceOfType(result[i][j], typeof(Node));
                    Assert.IsFalse(result[i][j].Occupied);
                    Assert.IsNull(result[i][j].Parent);
                    Assert.AreEqual(i, result[i][j].X);
                    Assert.AreEqual(j, result[i][j].Y);
                }
        }

        [TestMethod]
        public void WhenSearchResetCalled_ResetsNodes()
        {
            //Prepare
            Node parent = new Node();
            int height = 100;
            int width = 200;
            Node[][] result = GraphTools<Node>.CreateGraph(height, width);
            for (int i = 0; i < result.Length; i++)
                for (int j = 0; j < result[i].Length; j++)
                {
                    result[i][j].Parent = parent;
                    result[i][j].Distance = i+j;
                }

            //Before the Act phase check that values have indeed been set.
            for (int i = 0; i < result.Length; i++)
                for (int j = 0; j < result[i].Length; j++)
                {
                    Assert.IsInstanceOfType(result[i][j], typeof(Node));
                    Assert.IsFalse(result[i][j].Occupied);
                    Assert.IsNotNull(result[i][j].Parent);
                    Assert.AreEqual(i, result[i][j].X);
                    Assert.AreEqual(j, result[i][j].Y);
                    Assert.AreEqual(i + j, result[i][j].Distance);
                }

            //Act
            GraphTools<Node>.SearchReset(result);

            //Verify
            Assert.AreEqual(width, result.Length);
            Assert.AreEqual(height, result[0].Length);
            for (int i = 0; i < result.Length; i++)
                for (int j = 0; j < result[i].Length; j++)
                {
                    Assert.IsInstanceOfType(result[i][j], typeof(Node));
                    Assert.IsFalse(result[i][j].Occupied);
                    Assert.IsNull(result[i][j].Parent);
                    Assert.AreEqual(i, result[i][j].X);
                    Assert.AreEqual(j, result[i][j].Y);
                    Assert.AreEqual(int.MaxValue, result[i][j].Distance);
                }
        }

        [TestMethod]
        public void WhenGetAdjacentElementsCalled_ReturnsNodes()
        {
            //Prepare
            int height = 10;
            int width = 10;
            Node[][] graph = GraphTools<Node>.CreateGraph(height, width);

            //Act
            // Element in the center
            Node[] result1 = GraphTools<Node>.GetAdjacentElements(graph, graph[5][5]);
            // Element in the top left corner
            Node[] result2 = GraphTools<Node>.GetAdjacentElements(graph, graph[0][0]);
            // Element in the bottom right corner
            Node[] result3 = GraphTools<Node>.GetAdjacentElements(graph, graph[9][9]);

            //Verify
            Assert.AreEqual(8, result1.Length);

            Assert.AreEqual(graph[5][4], result1[0]);
            Assert.AreEqual(graph[6][5], result1[1]);
            Assert.AreEqual(graph[5][6], result1[2]);
            Assert.AreEqual(graph[4][5], result1[3]);

            Assert.AreEqual(graph[4][4], result1[4]);
            Assert.AreEqual(graph[6][4], result1[5]);
            Assert.AreEqual(graph[6][6], result1[6]);
            Assert.AreEqual(graph[4][6], result1[7]);

            Assert.AreEqual(3, result2.Length);
            Assert.AreEqual(3, result3.Length);
        }

        [TestMethod]
        public void WhenDistanceCalled_ReturnsDouble()
        {
            //Prepare
            Node a = new Node { X = 2, Y = 2 };
            Node b = new Node { X = 3, Y = 3 };
            Node c = new Node { X = 0, Y = 0 };
            Node d = new Node { X = 1, Y = 1 };
            Node e = new Node { X = 1, Y = 0 };

            //Act
            double result1 = GraphTools<Node>.Distance(a, b);
            double result2 = GraphTools<Node>.Distance(b, a);
            double result3 = GraphTools<Node>.Distance(c, d);
            double result4 = GraphTools<Node>.Distance(d, d);
            double result5 = GraphTools<Node>.Distance(d, e);


            //Verify
            Assert.AreEqual(Math.Sqrt(2), result1);
            Assert.AreEqual(Math.Sqrt(2), result2);
            Assert.AreEqual(Math.Sqrt(2), result3);
            Assert.AreEqual(0, result4);
            Assert.AreEqual(1, result5);
        }
    }
}
