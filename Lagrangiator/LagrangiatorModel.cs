using LagrangiatorLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagrangiator
{
    class LagrangiatorModel
    {

        private readonly LagrangiatorLogic.LagrangiatorAbstractLogicAPI logicAPI;


        public LagrangiatorModel(LagrangiatorLogic.LagrangiatorAbstractLogicAPI? logicAPI = null)
        {
            this.logicAPI = logicAPI ?? LagrangiatorLogic.LagrangiatorAbstractLogicAPI.CreateInstance();
        }


        public Func<double, double> InterpolateUsingFile(string fileName)
        {
            LagrangeFileInputResult result = this.logicAPI.GetLagrangePolynomialForFileSamples(fileName);
            if(result.Start == null || result.Interval==null || result.NodesCount == null)
            {
                throw new NullReferenceException();
            }
            this.LeftInterpolationBound = (double) result.Start;
            this.RightInterpolationBound = (double)(result.Start + (result.NodesCount - 1) * result.Interval);
            this.NumberOfNodes = (int)result.NodesCount;

            return result.Interpolation;
        }

        public Func<double, double> InterpolateUsingFunctionIndex(int functionIndex)
        {
            return this.logicAPI.GetLagrangePolynomialForFunction(functionIndex, LeftInterpolationBound, Interval, NumberOfNodes);
        }

        public (double, double)[] GetNodes(Func<double, double> function)
        {
            (double, double)[] nodes = new (double, double)[NumberOfNodes];
            for(int i=0; i<NumberOfNodes; i++)
            {
                double x = LeftInterpolationBound + i * Interval;
                nodes[i] = (x, function(x));
            }
            return nodes;
        }

        private double Interval => (RightInterpolationBound - LeftInterpolationBound) / (NumberOfNodes - 1);

        public double LeftInterpolationBound { get; set; }
        public double RightInterpolationBound { get; set; }
        public int NumberOfNodes { get; set; }

        public Func<double, double> Function => this.logicAPI.GetFunction();

    }
}
