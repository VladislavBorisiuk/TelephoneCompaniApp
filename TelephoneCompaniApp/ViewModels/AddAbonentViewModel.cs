using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneCompaniApp.ViewModels.Base;

namespace TelephoneCompaniApp.ViewModels
{
    internal class AddAbonentViewModel : ViewModel
    {
        private MainDataGridItem _newMainDataGridItem;

        public MainDataGridItem NewMainDataGridItem
        {
            get => _newMainDataGridItem; 

            set => Set(ref _newMainDataGridItem,value);
        }

        public AddAbonentViewModel(MainDataGridItem item) 
        {
            NewMainDataGridItem = item;
        }

    }
}
