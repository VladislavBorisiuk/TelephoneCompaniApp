using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TelephoneCompaniApp.ViewModels;

namespace TelephoneCompaniApp.Infrastructure.Behaviors
{
    public class DataGridSortingBehavior : Behavior<DataGrid>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Sorting += DataGrid_Sorting;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Sorting -= DataGrid_Sorting;
        }

        private void DataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            var viewModel = AssociatedObject.DataContext as MainWindowViewModel; // Измените тип вашей ViewModel
            if (viewModel != null && viewModel.DataGridSortingCommand != null && viewModel.DataGridSortingCommand.CanExecute(e))
            {
                viewModel.DataGridSortingCommand.Execute(e);
            }
        }
    }
}
