using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lagrangiator
{
    class WindowsWindowService : IWindowService
    {
        private static Type GetViewType<TViewModel>()
        {

            return Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(
                t => t.GetCustomAttributes(typeof(VVMAttribute)).Any(
                    a => ((VVMAttribute)a).ViewModelType == typeof(TViewModel)
                )
            ) ?? typeof(Window);


        }

        public void ShowWindow<TViewModel>(TViewModel viewModel)
        {
            Type viewType = WindowsWindowService.GetViewType<TViewModel>();
            Window window = Activator.CreateInstance(viewType) as Window ?? new Window();
            window.DataContext = viewModel;
            window.Show();
        }

        public void CloseWindow<TViewModel>(TViewModel viewModel)
        {
            Window? window = Application.Current.Windows.OfType<Window>().SingleOrDefault((w => w.DataContext.Equals(viewModel)));
            window?.Close();
        }

        public void CloseMainWindow()
        {
            Application.Current.MainWindow.Close();
        }

        
    }
}
