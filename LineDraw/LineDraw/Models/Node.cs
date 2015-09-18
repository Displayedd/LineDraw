using LineDraw.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineDraw.Models
{
    /// <summary>
    /// This class defines a node in a search tree.
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Create a new instance of this class with default values;
        /// </summary>
        public Node()
        {
            this.Parent = null;
            this.Occupied = false;
            this.Distance = int.MaxValue;
        }

        public Node Parent { get; set; }
        public bool Occupied { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public double Distance { get; set; }

        /// <summary>
        /// Get IComparer for sorting by the Distance property.
        /// </summary>
        public static IComparer<Node> SortByDistance
        { get { return new DistanceComparer<Node>(); } }

    }
}
