using LineDraw.Interfaces;
using LineDraw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineDraw.Services
{
    /// <summary>
    /// This class provides facilities to add lines to a canvas.
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

            }            
        }

        /// <summary>
        /// Compute and add a line in the underlying canvas model
        /// based on the submitted start and end points.
        /// </summary>
        /// <param name="startPoint">The line start point.</param>
        /// <param name="endPoint">The line end point.</param>
        /// <returns>The result of the operation.</returns>
        public LineQueryResult AddLine(Point startPoint, Point endPoint)
        {
            LineQueryResult result = new LineQueryResult();
            try
            {
                Point[] computedLine = this.canvasModel.AddLine(startPoint, endPoint);
                result.Result = computedLine;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
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
            return new Size {Height = this.canvasModel.Height, Width = this.canvasModel.Width };
        }
    }
}
