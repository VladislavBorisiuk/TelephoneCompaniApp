
using Rep_interfases;
using System.Collections.ObjectModel;
using TelephoneCompaniDataBase.Entityes;

namespace TelephoneCompaniApp.Services.Interfaces
{
    internal interface IDataService
    {
        public IRepository<Abonent> _AbonentRepository
        {  get; }
        public IRepository<Street> _StreetRepository
         { get; }
        public IRepository<Address> _AddressRepository
         { get; }
        public IRepository<PhoneNumber> _PhoneNumberRepository
         { get; }
        Task<ObservableCollection<MainDataGridItem>> FindAbonentsByPhoneNumber(string number);

        public Task<ObservableCollection<MainDataGridItem>> GetDataForDataGrid()
        {
            throw new NotImplementedException();
        }
    }
}
