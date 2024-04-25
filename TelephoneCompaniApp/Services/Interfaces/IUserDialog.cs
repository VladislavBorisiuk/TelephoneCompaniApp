using Rep_interfases;
using System;
using System.Collections.Generic;
using System.Text;
using TelephoneCompaniDataBase.Entityes;

namespace TelephoneCompaniApp.Services.Interfaces
{
    internal interface IUserDialog
    {
        string? FilterPhoneNumber();
        void ShowStreetList(IEnumerable<Street> streets);
    }

}
