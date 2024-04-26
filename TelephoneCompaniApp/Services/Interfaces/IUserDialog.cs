using Rep_interfases;
using System;
using System.Collections.Generic;
using System.Text;
using TelephoneCompaniDataBase.Entityes;

namespace TelephoneCompaniApp.Services.Interfaces
{
    internal interface IUserDialog
    {
        bool AddAbonent(MainDataGridItem item);
        string? FilterPhoneNumber();
        void ShowStreetList(IEnumerable<Street> streets);
    }

}
