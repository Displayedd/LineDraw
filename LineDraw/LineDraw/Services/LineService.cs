using LineDraw.Interfaces;
using LineDraw.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LineDraw.Services
{
    /// <summary>
    /// This class provides facilities for a viewmodel to 
    /// communicate with the model.
    /// </summary>
    public class LineService : ILineService
    {
        // Model used for the underlying line computations
        private readonly ICanvasModel canvasModel;

        /// <summary>
        /// Create a new instance of this class.
        /// </summary>
        /// <param name="model">Model to use for computing lines.</param>
        public LineService(ICanvasModel model)
        {
            this.canvasModel = model;
        }

        /// <summary>
        /// Clear all lines in the underlying canvas model associated with this instance.
        /// </summary>
        public void ClearLines()
        {
            try
            {
                this.canvasModel.ClearLines();
            }
            catch (Exception ex)
            {
                Debug.Print(string.Format("Unable to clear lines: {0}", ex.Message));
            }
        }
        
        /// <summary>
        /// Compute and add a line to the underlying canvas model
        /// based on the submitted start and end points using selected .
        /// </summary>
        /// <param name="startPoint">The line start point.</param>
        /// <param name="endPoint">The line end point.</param>
        /// <param name="algorithm">Pathfinding algorithm to use, defualt is BFS.</param>
        /// <returns>The result of the operation.</returns>
        public LineQueryResult AddLine(Point startPoint, Point endPoint, PathAlgorithm algorithm = PathAlgorithm.BFS)
        {
            LineQueryResult result = new LineQueryResult();
            try
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Point[] computedLine = this.canvasModel.AddLine(startPoint, endPoint, algorithm);
                timer.Stop();
                result.Result = computedLine;
                result.Success = true;
                result.Time = timer.ElapsedMilliseconds;
            }
            catch (Exception ex)
            {
                Debug.Print(string.Format("Unable to add line: {0}", ex.Message));
                result.Message = string.Format("Unable to find a path between points using {0} algorithm.", algorithm);
                result.Success = false;
            }
            return result;
        }

        /// <summary>
        /// Compute and add a line to the underlying canvas model
        /// based on the submitted start and end points using selected
        /// asynchronously.
        /// </summary>
        /// <param name="startPoint">The line start point.</param>
        /// <param name="endPoint">The line end point.</param>
        /// <param name="algorithm">Pathfinding algorithm to use, defualt is BFS.</param>
        /// <param name="token">Cancellation token for the async operation.</param>
        /// <returns>The result of the operation.</returns>
        public async Task<LineQueryResult> AddLineAsync(Point startPoint, Point endPoint,
            PathAlgorithm algorithm = PathAlgorithm.BFS, CancellationToken token = new CancellationToken())
        {
            LineQueryResult result = await Task.Run(() => this.AddLine(startPoint, endPoint, algorithm), token);
            return result;
        }

        /// <summary>
        /// Select the submitted point in the underlying canvas model.
        /// </summary>
        /// <param name="point">Point to select</param>
        /// <returns>The result of the operation.</returns>
        public PointQueryResult SelectPoint(Point point)
        {
            PointQueryResult result = new PointQueryResult();
            try
            {
                bool occupied = this.canvasModel.IsOccupied(point);
                if (occupied)
                {
                    result.Success = false;
                    result.Message = "Selected point is occupied.";
                }
                else
                {
                    result.Result = point;
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(string.Format("Unable to select point: {0}", ex.Message));
                result.Message = "Unable to select point.";
                result.Success = false;
            }
            return result;
        }

        /// <summary>
        /// Get the size of the canvas in the underlying canvas model.
        /// </summary>
        /// <returns>The canvas size of the underlying canvas model.</returns>
        public Size GetCanvasSize()
        {
            return new Size { Height = this.canvasModel.Height, Width = this.canvasModel.Width };
        }
    }
}
