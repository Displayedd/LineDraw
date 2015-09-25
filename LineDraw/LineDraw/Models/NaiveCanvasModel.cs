using LineDraw.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineDraw.Models
{
    /// <summary>
    /// This class models a naive approach of computing lines between
    /// two points.
    /// </summary>
    public class NaiveCanvasModel : ICanvasModel
    {
        public NaiveCanvasModel()
        {
            this.Height = 500;
            this.Width = 500;
        }

        /// <summary>
        /// The canvas height in this model.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// The canvas width in this model.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Not implemented.
        /// </summary>
        public void ClearLines()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Computes a line between the submitted points.
        /// </summary>
        /// <param name="startPoint">The start point.</param>
        /// <param name="endPoint">The end point.</param>
        /// <returns>The computed line.</returns>
        public Point[] AddLine(Point startPoint, Point endPoint, PathAlgorithm algorithm)
        {
            // We don't check for intersecting lines and simply
            // return a line consisting of the start and end point.
            return new Point[] { startPoint, endPoint };
        }

        /// <summary>
        /// Checks whether the submitted point is occupied by a line.
        /// </summary>
        /// <param name="point">Point to check</param>
        /// <returns>True if submitted point is occupied by a line in the canvas model</returns>
        public bool IsOccupied(Point point)
        {
            return false;
        }
    }
}
