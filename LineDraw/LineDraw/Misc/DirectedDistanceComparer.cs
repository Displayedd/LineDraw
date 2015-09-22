using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C5;
using LineDraw.Models;

namespace LineDraw.Misc
{
    /// <summary>
    /// This class implements comparison functionality for the Node class.
    /// The Nodes are compared by their Distance + the estimated distance
    /// to the goal node. The distance to the goal node is estimated as 
    /// the straight line distance to the node.
    /// </summary>
    /// <typeparam name="T">A class inhereting from the <see cref="Node"/> class.</typeparam>
    public class DirectedDistanceComparer<T> : IComparer<T> where T : Node
    {
        private Node goal;

        /// <summary>
        /// Create a new instance of this class with the submitted
        /// node as the goal node.
        /// </summary>
        /// <param name="goal">The goal node</param>
        public DirectedDistanceComparer(T goal)
        {
            this.goal = goal;
        }

        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less 
        /// than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare</param>
        /// <param name="y">The second object to compare</param>
        /// <returns>An integer, zero if x and y are equal, positive value if x is 
        /// larger than y, a negative value if y is large than x.</returns>
        public int Compare(T x, T y)
        {
            double xToGoal = GraphTools<Node>.Distance(x, this.goal);
            double yToGoal = GraphTools<Node>.Distance(y, this.goal);
            return (x.Distance + xToGoal).CompareTo(y.Distance + yToGoal);            
        }
    }
}
