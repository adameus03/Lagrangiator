namespace LagrangiatorData
{
    public abstract class LagrangiatorAbstractDataAPI
    {
        protected DataInputMethod? dataInputMethod;
        protected string? filePath;
        protected int? functionIndex;

        public LagrangiatorAbstractDataAPI() { }
        public static LagrangiatorAbstractDataAPI CreateInstance()
        {
            return new LagrangiatorDataAPI();
        }

        public abstract void SetDataInputMethod(DataInputMethod dataInputMethod);
        public abstract void SetFilePath(string filePath);

        public abstract void SetFunctionIndex(int index);

        public abstract ISamplingManager GetSamplingManager(); //overload it? - instead of declaring the above abstract methods

        public abstract Func<double, double> GetFunction();


    }
}