using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagrangiatorData
{
    internal class LagrangiatorDataAPI : LagrangiatorAbstractDataAPI
    {

        /*
         * 
         * f0(x)=3x-5
         * f1(x)=|x|
         * f2(x)=x^3+x^2-2x-1
         * f3(x)=\cos x
         * f4(x)=\cos^2(2x^2)-|\sin x^2|
         * f5(x)=\sin(|x+1|)+|x|^3-|x|
         * 
         */

        private Func<double, double>[] functions =
        {
            new Func<double, double>((double x) => 3*x-5),
            new Func<double, double>((double x) => Math.Abs(x)),
            new Func<double, double>((double x) => x*(x*(x+1)-2)-1),
            new Func<double, double>((double x) => Math.Cos(x)),
            new Func<double, double>((double x) => Math.Cos(2*x*x)*Math.Cos(2*x*x)-Math.Abs(Math.Sin(x*x))),
            new Func<double, double>((double x) => Math.Sin(Math.Abs(x+1))+Math.Abs(x)*(Math.Abs(x)*Math.Abs(x)-1))
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

        public override Func<double, double> GetFunction()
        {
            if(this.functionIndex == null)
            {
                throw new NullReferenceException();
            }
            return this.functions[(int)this.functionIndex];
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
