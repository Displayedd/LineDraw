using LineDraw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineDraw.Interfaces
{
    public interface ILineCalculatorFactory
    {
        ILineCalculator Create(PathAlgorithm algorithm);
    }
}
