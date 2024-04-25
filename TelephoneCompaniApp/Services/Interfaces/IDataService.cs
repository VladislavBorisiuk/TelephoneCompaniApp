
using Rep_interfases;
using System.Collections.ObjectModel;
using TelephoneCompaniDataBase.Entityes;

namespace TelephoneCompaniApp.Services.Interfaces
{
    internal interface IDataService
    {
        Task<ObservableCollection<MainDataGridItem>> FindAbonentsByPhoneNumber(string number);

        public Task<ObservableCollection<MainDataGridItem>> GetDataForDataGrid()
        {
            throw new NotImplementedException();
        }
    }
}
