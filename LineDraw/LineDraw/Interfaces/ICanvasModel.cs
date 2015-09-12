using LineDraw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineDraw.Interfaces
{
    public interface ICanvasModel
    {
        void ClearLines();
        Point[] AddLine(Point startPoint, Point endPoint);
        int Height { get; }
        int Width { get; }
    }
}
