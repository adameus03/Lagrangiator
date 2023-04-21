using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagrangiatorLogic
{
    public interface IDataInterpolator<T>
    {
        Func<double, double> Interpolate(IEnumerable<T> data);
    }
}
