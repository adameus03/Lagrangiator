using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagrangiatorData
{
    public interface ISamplingManager
    {
        double? GetStart();
        int? CountNodes();
        double? GetInterval();

        void SetStart(double start);
        void SetNodesCount(int nodesCount);
        void SetInterval(double interval);

        IEnumerable<double> Sample();
    }
}
