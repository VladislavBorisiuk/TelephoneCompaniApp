using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneCompaniDataBase.Entityes.Base;

namespace TelephoneCompaniDataBase.Entityes
{
    internal class PhoneNumbers : Entity
    {
        public string HomePhoneNumber { get; set; }
        public string WorkPhoneNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
    }
}
