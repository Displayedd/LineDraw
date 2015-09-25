using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LineDraw.Interfaces;
using LineDraw.Models;
using LineDraw.Services;
using Moq;
using LineDraw.Misc;

namespace LineDraw.Tests.Models
{
    [TestClass]
    public class LineCalculatorFactoryFixture
    {

        [TestMethod]
        public void WhenCreateCalled_ReturnsLineCalculator()
        {
            //Prepare
            ILineCalculatorFactory target = new LineCalculatorFactory();
            
            //Act
            ILineCalculator result1 = target.Create(PathAlgorithm.AStar);
            ILineCalculator result2 = target.Create(PathAlgorithm.BFS);
            ILineCalculator result3 = target.Create(PathAlgorithm.Dijkstra);            

            //Verify
            Assert.IsInstanceOfType(result1, typeof(AStarLineCalculator));
            Assert.IsInstanceOfType(result2, typeof(BFSLineCalculator));
            Assert.IsInstanceOfType(result3, typeof(DijkstraLineCalculator));
        }
    }
}
