using LagrangiatorData;

namespace LagrangiatorLogic
{
    internal class LagrangiatorLogicAPI : LagrangiatorAbstractLogicAPI
    {
        private new readonly ConstantIntervalInterpolator interpolator;
        public LagrangiatorLogicAPI(LagrangiatorData.LagrangiatorAbstractDataAPI dataAPI) : base(dataAPI) {
            this.interpolator = base.interpolator as ConstantIntervalInterpolator ?? new LagrangeInterpolator();
        }

        public override LagrangeFileInputResult GetLagrangePolynomialForFileSamples(string filePath)
        {
            base.dataAPI.SetDataInputMethod(LagrangiatorData.DataInputMethod.FromFile);
            base.dataAPI.SetFilePath(filePath);
            LagrangiatorData.ISamplingManager sampler = base.dataAPI.GetSamplingManager();

            LagrangeFileInputResult result = new LagrangeFileInputResult
            {
                Start = sampler.GetStart(),
                Interval = sampler.GetInterval(),
                NodesCount = sampler.CountNodes()
            };
            
            // convert sampler.Sample() to interpolation

            if(result.Start == null || result.Interval == null || result.NodesCount == null)
            {
                throw new NullReferenceException();
            }

            //IDataInterpolator<double> interpolator = new LagrangeInterpolator((double)result.Start, (double)result.Interval, (int)result.NodesCount);

            this.ConfigureInterpolator((double)result.Start, (double)result.Interval, (int)result.NodesCount);


            result.Interpolation = this.interpolator.Interpolate(sampler.Sample());

            return result;
            
        }

        public override Func<double, double> GetLagrangePolynomialForFunction(int functionIndex, double start, double interval, int nodesCount)
        {
            base.dataAPI.SetDataInputMethod(LagrangiatorData.DataInputMethod.Function);
            base.dataAPI.SetFunctionIndex(functionIndex);
            LagrangiatorData.ISamplingManager sampler = base.dataAPI.GetSamplingManager();
            sampler.SetStart(start);
            sampler.SetNodesCount(nodesCount);
            sampler.SetInterval(interval);

            // convert sampler.Sample() to interpolation

            this.ConfigureInterpolator(start, interval, nodesCount);

            return this.interpolator.Interpolate(sampler.Sample());

        }

        private void ConfigureInterpolator(double start, double interval, int nodesCount)
        {
            this.interpolator.Start = start;
            this.interpolator.Interval = interval;
            this.interpolator.NodesCount = nodesCount;
        }
    }
}