
using Rep_interfases;
using System.Collections.ObjectModel;
using TelephoneCompaniDataBase.Entityes;

namespace TelephoneCompaniApp.Services.Interfaces
{
    internal interface IDataService
    {
        public INamedRepository<Abonent> _AbonentRepository
        {  get; }
        public INamedRepository<Street> _StreetRepository
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

        public Task AddAbonent(MainDataGridItem New_DGitem) 
        { 
            throw new NotImplementedException(); 
        }

        public Task RemoveAbonent(int id)
        {
            throw new NotImplementedException();
        }

        Task UpdateAbonent(MainDataGridItem new_DGitem);
    }
}
