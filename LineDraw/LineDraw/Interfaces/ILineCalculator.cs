using LineDraw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LineDraw.Interfaces
{
    public interface ILineCalculator
    {
        Point[] CalculateLine(Node[][] graph, Point startPoint, Point endPoint);
    }
}
