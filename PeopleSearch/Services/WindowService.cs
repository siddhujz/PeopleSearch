using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PeopleSearch.Services
{
    class WindowService : Window, IWindowService
    {
        public void showWindow(object viewModel)
        {
            var window = new Window();
            window.Content = viewModel;
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();
        }
    }
}
