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



        /*private Func<double, double>[] functions =
        {
            new Func<double, double>((double x) => x*(x*(x+1)-2)-1),
            new Func<double, double>((double x) => Math.Cos(x)),
            new Func<double, double>((double x) => Math.Pow(2, x)-3),
            new Func<double, double>((double x) => Math.Cos(Math.Exp(x))),
            new Func<double, double>((double x) => Math.Pow(Math.Cos(2*x*x),2)-Math.Pow(2,Math.Sin(x*x)))
        };*/
    }
}
