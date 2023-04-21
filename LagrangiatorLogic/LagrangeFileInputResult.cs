using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagrangiatorLogic
{
    public struct LagrangeFileInputResult
    {
        public double? Start;
        public double? Interval;
        public double? NodesCount;
        public Func<double, double> Interpolation;
    }
}
