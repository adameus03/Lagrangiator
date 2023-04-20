using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lagrangiator
{
    class MainViewModel : BaseViewModel
    {
        private Command launchLagrangiator = new Command();
        public MainViewModel() : base(){
            this.launchLagrangiator.ExecuteReceived += LaunchLagrangiator_ExecuteReceived;
        }

        private void LaunchLagrangiator_ExecuteReceived(object? sender, EventArgs e)
        {
            //Window lagrangatorWindow = new LagrangiatorWindow();
            //lagrangatorWindow.Show();

            MainViewModel.WindowService.ShowWindow<LagrangiatorViewModel>(new LagrangiatorViewModel());
            //(new LagrangiatorWindow()).Show();
            MainViewModel.WindowService.CloseMainWindow();
        }

        public Command LaunchLagrangiator => this.launchLagrangiator;

    }
}
