using Rep_interfases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneCompaniApp.Infrastructure.Extensions;
using TelephoneCompaniApp.ViewModels.Base;
using TelephoneCompaniDataBase.Entityes;

namespace TelephoneCompaniApp.ViewModels
{
    internal class StreetsViewModel : ViewModel
    {
        private ObservableCollection<Street> _streets;
        
        public ObservableCollection<Street> _Streets
        {
            get => _streets;
            set => Set(ref  _streets, value);
        }
        public StreetsViewModel(IEnumerable<Street> streets)
        {
            _Streets = streets.ToObservableCollection();
        }
    }
}
