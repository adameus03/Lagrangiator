using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagrangiatorLogic
{
    internal class LagrangeInterpolator : ConstantIntervalInterpolator
    {
        public LagrangeInterpolator(double? start = null, double? interval = null, int? nodesCount = null) : base(start, interval, nodesCount) { }


        private double Quotient(double t, int j)
        {
            double output = 1;
            for(int i =0; i<j; i++)
            {
                output *= (t - i) / (j - i);
            }
            for (int i = j+1; i < base.nodesCount; i++)
            {
                output *= (t - i) / (j - i);
            }
            return output;
        }

        public override Func<double, double> Interpolate(IEnumerable<double> samples)
        {
            double[] samplesArray = samples.ToArray();
            return (x) =>
            {
                double sum = 0;
                double t = (x - base.start) / base.interval;
                for (int i = 0; i < samplesArray.Length; i++)
                {
                    sum += samplesArray[i] * Quotient(t, i);
                }
                return sum;
            };
            
        }
    }
}
