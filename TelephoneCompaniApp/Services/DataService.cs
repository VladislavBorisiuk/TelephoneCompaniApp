using Rep_interfases;
using System.Collections.ObjectModel;
using TelephoneCompaniApp.Infrastructure.Extensions;
using TelephoneCompaniApp.Services.Interfaces;
using TelephoneCompaniDataBase.Entityes;
using TelephoneCompaniApp.Infrastructure.Extensions;
using System.Windows;

namespace TelephoneCompaniApp.Services
{
    internal class DataService : IDataService
    {
        #region Репозитории
        public INamedRepository<Abonent> _AbonentRepository
        { get; }
        public INamedRepository<Street> _StreetRepository
        { get; }
        public IRepository<Address> _AddressRepository
        { get; }
        public IRepository<PhoneNumber> _PhoneNumberRepository
        { get; }
        private readonly string[] PhoneType;
        #endregion

        public DataService(INamedRepository<Abonent> abonentRepository,
            INamedRepository<Street> streetRepository,
            IRepository<Address> addressRepository,
            IRepository<PhoneNumber> phonenumberRepository)
        {
            _AbonentRepository = abonentRepository;
            _AddressRepository = addressRepository;
            _StreetRepository = streetRepository;
            _PhoneNumberRepository = phonenumberRepository;

            PhoneType = new string[3] { "home", "work", "mobile" };
        }

        #region Получание всего списка из БД
        public async Task<ObservableCollection<MainDataGridItem>> GetDataForDataGrid()
        {
            var _abonents = (await _AbonentRepository.GetAllAsync()).ToList();
            return (await CreateDataGridItemsAsync(_abonents)).ToObservableCollection();
        }
        #endregion
        #region Поиск по номеру
        public async Task<ObservableCollection<MainDataGridItem>> FindAbonentsByPhoneNumber(string number)
        {
            var _abonents = (await _AbonentRepository.GetAllAsync()).ToObservableCollection();
            var _phoneNumbers = (await _PhoneNumberRepository.GetAllAsync()).ToObservableCollection();

            var matchingAbonentIds = _phoneNumbers
                .Where(phoneNumber => phoneNumber.PhoneNumberString.StartsWith(number))
                .Select(phoneNumber => phoneNumber.AbonentId)
                .Distinct();

            var matchingAbonents = _abonents
                .Where(abonent => matchingAbonentIds.Contains(abonent.Id)).ToList();

            
            return (await CreateDataGridItemsAsync(matchingAbonents)).ToObservableCollection();
        }
        #endregion

        #region Добавление абонента
        public async Task AddAbonent(MainDataGridItem new_DGitem) 
        {
            if(CanSave(new_DGitem))
            {
                MessageBox.Show("Абонент не добавден, должны быть заполненны все поля");
                return;
            }
            var new_Abonent = new Abonent()
            {
                FullName = new_DGitem.FullName
            };

            var abonentId = (await _AbonentRepository.AddAsync(new_Abonent)).Id;

            var new_Street = new Street()
            {
                StreetName = new_DGitem.Street,
                NumberOfSubscribers = 1
            };


            var streetId = (await _StreetRepository.AddAsync(new_Street)).Id;

            var new_Address = new Address()
            {
                HouseNumber = new_DGitem.HouseNumber,
                StreetId = streetId,
                AbonentId = abonentId,
            };
            await _AddressRepository.AddAsync(new_Address);

            for(int i =0; i < new_DGitem.PhoneNumbers.Length; i++)
            {
                var phoneNumber = new PhoneNumber()
                {
                    PhoneNumberString = new_DGitem.PhoneNumbers[i],
                    Type = PhoneType[i],
                    AbonentId = abonentId
                };

                await _PhoneNumberRepository.AddAsync(phoneNumber);
            }
        }
        #endregion

        #region Удаление абонента
        public async Task RemoveAbonent(int id)
        {
            var address = (await _AddressRepository.GetAllAsync()).FirstOrDefault(p => p.AbonentId == id);
            await _AddressRepository.DeleteAsync(address.Id);
            await _StreetRepository.DeleteAsync(address.StreetId);
            await _PhoneNumberRepository.DeleteAsync(id);
            await _AbonentRepository.DeleteAsync(id);
        }
        #endregion

        #region Обновление абонента
        public async Task UpdateAbonent(MainDataGridItem new_DGitem)
        {
            if (CanSave(new_DGitem))
            {
                MessageBox.Show("Абонент не обновлён, должны быть заполненны все поля");
                return;
            }
            var new_Abonent = new Abonent()
            {
                Id = new_DGitem.AbonentId,
                FullName = new_DGitem.FullName
            };

            await _AbonentRepository.UpdateAsync(new_Abonent);

            var address = (await _AddressRepository.GetAllAsync()).FirstOrDefault(p => p.AbonentId == new_Abonent.Id);

            address.HouseNumber = new_DGitem.HouseNumber;

            await _AddressRepository.UpdateAsync(address);
            
            var street = await _StreetRepository.GetByIdAsync(address.StreetId);

            street.StreetName = new_DGitem.HouseNumber;

            await _StreetRepository.UpdateAsync(street);

            var phoneNumbers = (await _PhoneNumberRepository.GetAllAsync()).Where(p => p.AbonentId == new_Abonent.Id).ToList();

            for (int i = 0; i < phoneNumbers.Count; i++) 
            {
                phoneNumbers[i].PhoneNumberString = new_DGitem.PhoneNumbers[i];
                await _PhoneNumberRepository.UpdateAsync(phoneNumbers[i]);
            }
        }
        #endregion

        #region Проверка на пустые поля
        private bool CanSave(MainDataGridItem item)
        {
            return !string.IsNullOrEmpty(item.FullName) &&
                   !string.IsNullOrEmpty(item.Street) &&
                   !string.IsNullOrEmpty(item.HouseNumber) &&
                   !string.IsNullOrEmpty(item.PhoneNumbers[0]) &&
                   !string.IsNullOrEmpty(item.PhoneNumbers[1]) &&
                   !string.IsNullOrEmpty(item.PhoneNumbers[2]);
        }
        #endregion

        #region Создание DataGridItems
        private async Task<List<MainDataGridItem>> CreateDataGridItemsAsync(List<Abonent> items)
        {
            var _phoneNumbers = (await _PhoneNumberRepository.GetAllAsync()).ToObservableCollection();
            var _addresses = (await _AddressRepository.GetAllAsync()).ToObservableCollection();
            var dataGridItems = new List<MainDataGridItem>();


            foreach (var abonent in items)
            {
                var address = _addresses.FirstOrDefault(a => a.AbonentId == abonent.Id);
                var homePhoneNumber = _phoneNumbers.FirstOrDefault(p => p.AbonentId == abonent.Id && p.Type == PhoneType[0]);
                var workPhoneNumber = _phoneNumbers.FirstOrDefault(p => p.AbonentId == abonent.Id && p.Type == PhoneType[1]);
                var mobilePhoneNumber = _phoneNumbers.FirstOrDefault(p => p.AbonentId == abonent.Id && p.Type == PhoneType[2]);
                string[] PhoneArr = [homePhoneNumber.PhoneNumberString, workPhoneNumber.PhoneNumberString, mobilePhoneNumber.PhoneNumberString];
                var dataGridItem = new MainDataGridItem
                {
                    AbonentId = abonent.Id,
                    FullName = abonent.FullName,
                    Street = address != null ? (await _StreetRepository.GetByIdAsync(address.StreetId)).StreetName : "",
                    HouseNumber = address != null ? address.HouseNumber : "",
                    PhoneNumbers = PhoneArr ?? (["", "", ""]),
                };

                dataGridItems.Add(dataGridItem);
            }
            return dataGridItems;
        }

        #endregion
        
    }
}
