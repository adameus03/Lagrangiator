using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagrangiatorData
{
    internal class LagrangiatorDataAPI : LagrangiatorAbstractDataAPI
    {
        private Func<double, double>[] functions =
        {
            new Func<double, double>((double x) => x*(x*(x+1)-2)-1),
            new Func<double, double>((double x) => Math.Cos(x)),
            new Func<double, double>((double x) => Math.Pow(2, x)-3),
            new Func<double, double>((double x) => Math.Cos(Math.Exp(x))),
            new Func<double, double>((double x) => Math.Pow(Math.Cos(2*x*x),2)-Math.Pow(2,Math.Sin(x*x)))
        };


        public LagrangiatorDataAPI() : base() { }

        public override ISamplingManager GetSamplingManager()
        {
            if(base.dataInputMethod == null) throw new NullReferenceException();
            if (base.dataInputMethod == DataInputMethod.Function)
            {
                if (base.functionIndex == null) throw new NullReferenceException();
                return new BasicSamplingManager(this.functions[(int)base.functionIndex]);
                
            }
            if(base.filePath == null) throw new NullReferenceException();
            string[] lines = File.ReadAllLines(base.filePath);
            string[] header = lines[0].Split(' ');
            double start = Convert.ToDouble(header[0]);
            double interval = Convert.ToDouble(header[1]);
            double[] samples = new double[lines.Length - 1];
            for(int i=0; i<samples.Length; i++)
            {
                samples[i] = Convert.ToDouble(lines[i+1]);
            }
            return new BasicSamplingManager(start, samples.Length, interval, samples);

        }

        public override void SetDataInputMethod(DataInputMethod dataInputMethod)
        {
            base.dataInputMethod = dataInputMethod;
        }

        public override void SetFilePath(string filePath)
        {
            base.filePath = filePath;
        }

        public override void SetFunctionIndex(int index)
        {
            base.functionIndex = index;
        }
    }
}
