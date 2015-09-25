using LineDraw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineDraw.Interfaces
{
    public interface ILineService
    {
        void ClearLines();
        LineQueryResult AddLine(Point startPoint, Point endPoint, PathAlgorithm algorithm = PathAlgorithm.BFS);
        PointQueryResult SelectPoint(Point point);
        Size GetCanvasSize();
    }
}
