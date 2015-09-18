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
    /// This class implements comparison functionality for the PriorityQueueWrapper class.
    /// </summary>
    /// <typeparam name="T">A class inhereting from the PriorityQueueWrapper class.</typeparam>
    public class DistanceComparer<T> : IComparer<T> where T : Node
    {
        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less 
        /// than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(T x, T y)
        {
            return x.Distance.CompareTo(y.Distance);            
        }
    }
}
