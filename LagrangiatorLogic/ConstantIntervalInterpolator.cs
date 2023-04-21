using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagrangiatorLogic
{
    abstract class ConstantIntervalInterpolator : IDataInterpolator<double>
    {
        protected double start;
        protected double interval;
        protected int nodesCount;

        public ConstantIntervalInterpolator(double? start = null, double? interval = null, int? nodesCount = null)
        {
            this.start = start ?? 0;
            this.interval = interval ?? 0;
            this.nodesCount = nodesCount ?? 0;
        }

        public abstract Func<double, double> Interpolate(IEnumerable<double> samples);

        public double Start
        {
            get => this.start;
            set => this.start = value;
        }
        public double Interval
        {
            get => this.interval;
            set => this.interval = value;
        }
        public int NodesCount
        {
            get => this.nodesCount;
            set => this.nodesCount = value;
        }
    }
}
