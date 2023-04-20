using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagrangiator
{
    interface IWindowService
    {
        public void ShowWindow<TViewModel>(TViewModel viewModel);
        public void CloseWindow<TViewModel>(TViewModel viewModel);

        public void CloseMainWindow();
        
    }
}
