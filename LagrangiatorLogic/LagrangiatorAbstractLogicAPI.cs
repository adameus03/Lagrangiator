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

        public LagrangiatorAbstractLogicAPI(LagrangiatorData.LagrangiatorAbstractDataAPI dataAPI)
        {
            this.dataAPI = dataAPI;
        }

        public static LagrangiatorAbstractLogicAPI CreateInstance(LagrangiatorData.LagrangiatorAbstractDataAPI? dataAPI = null)
        {
            return new LagrangiatorLogicAPI(dataAPI ?? LagrangiatorData.LagrangiatorAbstractDataAPI.CreateInstance());
        }
    }
}
