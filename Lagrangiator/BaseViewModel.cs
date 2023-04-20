using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagrangiator
{
    abstract class BaseViewModel
    {
        private static IWindowService windowService = new WindowsWindowService();

        public BaseViewModel() { }

        protected static IWindowService WindowService => BaseViewModel.windowService;
    }
}
