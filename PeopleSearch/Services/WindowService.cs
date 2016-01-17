using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PeopleSearch.Services
{
    class WindowService : IWindowService
    {
        public void showWindow(object viewModel)
        {
            var window = new Window();
            window.WindowState = WindowState.Maximized;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Content = viewModel;
            //window.DataContext = viewModel;
            window.Show();
        }
    }
}
