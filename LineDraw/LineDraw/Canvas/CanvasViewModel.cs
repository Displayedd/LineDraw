using LineDraw.External;
using LineDraw.Interfaces;
using LineDraw.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Point = LineDraw.Models.Point;
using Size = LineDraw.Models.Size;

namespace LineDraw.Canvas
{
    /// <summary>
    /// This class implements the viewmodel for <see cref="CanvasView"/>.
    /// </summary>
    public class CanvasViewModel
    {
        private readonly ILineService lineService;
        private ICommand selectPointCommand;

        #region Properties
        /// <summary>
        /// Lines to draw on the canvas.
        /// </summary>
        public ObservableCollection<Point[]> Lines { get; set; }

        /// <summary>
        /// The height of the clickable area.
        /// </summary>
        public int CanvasHeight { get; set; }

        /// <summary>
        /// The width of the clickable area.
        /// </summary>
        public int CanvasWidth { get; set; }

        /// <summary>
        /// Start point for user selected line
        /// </summary>
        public Point? StartPoint { get; set; }

        /// <summary>
        /// End point for user selected line
        /// </summary>
        public Point? EndPoint { get; set; }

        /// <summary>
        /// Command object for selecting line points.
        /// </summary>
        public ICommand SelectPointCommand
        {
            get
            {
                if (selectPointCommand == null) 
                    selectPointCommand = new RelayCommand(param => SelectPoint((MouseEventArgs)param));
                return selectPointCommand;
            }
        }

        /// <summary>
        /// Field for error messages from the ILineService.
        /// </summary>
        public string ErrorMessage { get; set; }

        #endregion

        /// <summary>
        /// Instantiates a new instance of this class.
        /// </summary>
        /// <param name="lineService">Service to use for computing lines.</param>
        public CanvasViewModel(ILineService lineService)
        {
            if (lineService == null)
                new ArgumentNullException("lineService");
            this.lineService = lineService;
            Size canvasSize = this.lineService.GetCanvasSize();
            this.CanvasHeight = canvasSize.Height;
            this.CanvasWidth = canvasSize.Width;
            this.Lines = new ObservableCollection<Point[]>();
        }

        /// <summary>
        /// Event handler for mouse click event.
        /// </summary>
        /// <param name="e"></param>
        private void SelectPoint(MouseEventArgs e)
        {
            // Get the x.y position of the mouse event.
            int mouseX = (int)e.GetPosition((IInputElement)e.Source).X;
            int mouseY = (int)e.GetPosition((IInputElement)e.Source).Y;

            // If start point has not been set the mouse event sets it.
            if (StartPoint == null)
            {
                StartPoint = new Point { X = mouseX, Y = mouseY };
            }
            // Else the mouse event sets the end point.
            else
            {
                EndPoint = new Point { X = mouseX, Y = mouseY };

                // Start and end point has been set so we line service.
                AddLine(StartPoint.Value, EndPoint.Value);

                // Reset start and end point for the next line.
                StartPoint = null;
                EndPoint = null;
            }
        }

        /// <summary>
        /// Calls the ILineService with submitted points.
        /// </summary>
        private void AddLine(Point startPoint, Point endPoint)
        {
            // Clear error message before a new call.
            this.ErrorMessage = string.Empty;

            // Query the ILineService with select start and end points.
            LineQueryResult result = this.lineService.AddLine(startPoint, endPoint);

            // Process the query result.
            if (result.Success)
            {
                this.Lines.Add(result.Result);
            }
            else
            {
                this.ErrorMessage = result.Message;
            }
        }
    }
}
