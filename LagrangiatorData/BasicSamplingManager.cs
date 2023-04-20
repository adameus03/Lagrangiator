using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagrangiatorData
{
    internal class BasicSamplingManager : ISamplingManager
    {
        private double? start;
        private int? nodesCount;
        private double? interval;
        private readonly Func<double, double>? f;
        private double[]? samples;
        public BasicSamplingManager(Func<double, double> function)
        {
            /*this.start = start;
            this.nodesCount = nodesCount;
            this.interval = interval;*/
            this.f = function;
        }
        public BasicSamplingManager(double start, int nodesCount, double interval, IEnumerable<double> samples)
        {
            this.start = start;
            this.nodesCount = nodesCount;
            this.interval = interval;
            this.samples = (double[])samples;
        }

        public IEnumerable<double> Sample()
        {
            if (this.samples != null) return this.samples;

            if (this.start != null && this.nodesCount != null && this.interval != null)
            {
                double[] samples = new double[(int)this.nodesCount];
                if (this.f != null)
                {
                    samples = new double[(int)this.nodesCount];
                    double x = (double)this.start;
                    for (int i = 0; i < this.nodesCount; i++)
                    {
                        samples[i] = this.f(x);
                        x += (double)this.interval;
                    }
                }
                return samples;
            }

            return Array.Empty<double>();
            
            
        }

        public int? CountNodes()
        {
            return this.nodesCount;
        }

        public double? GetInterval()
        {
            return this.interval;
        }


        public double? GetStart()
        {
            return this.start;
        }

        public void SetStart(double start)
        {
            this.start = start;
        }

        public void SetNodesCount(int nodesCount)
        {
            this.nodesCount = nodesCount;
        }

        public void SetInterval(double interval)
        {
            this.interval = interval;
        }
    }
}
