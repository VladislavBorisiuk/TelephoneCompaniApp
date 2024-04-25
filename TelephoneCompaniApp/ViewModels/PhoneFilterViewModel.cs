using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneCompaniApp.ViewModels.Base;

namespace TelephoneCompaniApp.ViewModels
{
    internal class PhoneFilterViewModel : ViewModel
    {
        private string phoneNumber;

        public string PhoneNumber
        {
            get => phoneNumber;

            set => Set(ref phoneNumber,value);
        }

        public PhoneFilterViewModel() 
        {
        
        }
    }
}
