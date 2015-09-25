using LineDraw.Interfaces;
using LineDraw.Misc;
using LineDraw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineDraw.Services
{
    /// <summary>
    /// This class is a factory for ILineCalculator instances.
    /// </summary>
    public class LineCalculatorFactory : ILineCalculatorFactory
    {
        /// <summary>
        /// Create a new instance of a ILineCalculator class based
        /// on submitted pathfinding algorithm.
        /// </summary>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        public ILineCalculator Create(PathAlgorithm algorithm)
        {
            switch (algorithm)
            {
                case PathAlgorithm.AStar:
                    return new AStarLineCalculator();
                case PathAlgorithm.BFS:
                    return new BFSLineCalculator();
                case PathAlgorithm.Dijkstra:
                    return new DijkstraLineCalculator();
                default:
                    throw new ArgumentException(string.Format(
                        "Unable to create a ILineCalculator using {0} algorithm.", algorithm));
            }
        }
    }
}
