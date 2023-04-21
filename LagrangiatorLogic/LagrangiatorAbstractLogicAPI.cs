using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagrangiatorLogic
{
    public abstract class LagrangiatorAbstractLogicAPI
    {
        protected readonly LagrangiatorData.LagrangiatorAbstractDataAPI dataAPI;

        protected readonly IDataInterpolator<double> interpolator;


        public LagrangiatorAbstractLogicAPI(LagrangiatorData.LagrangiatorAbstractDataAPI dataAPI)
        {
            this.dataAPI = dataAPI;
            this.interpolator = new LagrangeInterpolator();
        }

        public static LagrangiatorAbstractLogicAPI CreateInstance(LagrangiatorData.LagrangiatorAbstractDataAPI? dataAPI = null)
        {
            return new LagrangiatorLogicAPI(dataAPI ?? LagrangiatorData.LagrangiatorAbstractDataAPI.CreateInstance());
        }

        public abstract Func<double, double> GetLagrangePolynomialForFunction(int functionIndex, double start, double interval, int nodesCount);

        public abstract LagrangeFileInputResult GetLagrangePolynomialForFileSamples(string filePath);


        /*public void SetFunctionIndex(int functionIndex)
        {
            this.dataAPI
        }
        public void SetInterpolationNodesNumber { get; set; }
        public void SetLeftInterpolationBound { get; set; }
        public void SetRightInterpolationBound { get; set; }*/

    }
}
