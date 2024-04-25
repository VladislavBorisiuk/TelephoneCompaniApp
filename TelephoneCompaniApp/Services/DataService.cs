using Rep_interfases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using TelephoneCompaniApp.Infrastructure.Extensions;
using TelephoneCompaniApp.Services.Interfaces;
using TelephoneCompaniDataBase.Entityes;

namespace TelephoneCompaniApp.Services
{
    internal class DataService : IDataService
    {

        public readonly IRepository<Abonent> _AbonentRepository;
        public readonly IRepository<Street> _StreetRepository;
        public readonly IRepository<Address> _AddressRepository;
        public readonly IRepository<PhoneNumber> _PhoneNumberRepository;
        private enum PhoneType
        {
            home,
            work,
            mobile
        }



        public DataService(IRepository<Abonent> abonentRepository,
            IRepository<Street> streetRepository,
            IRepository<Address> addressRepository,
            IRepository<PhoneNumber> phonenumberRepository)
        {
            _AbonentRepository = abonentRepository;
            _AddressRepository = addressRepository;
            _StreetRepository = streetRepository;
            _PhoneNumberRepository = phonenumberRepository;
        }


        public async Task<ObservableCollection<MainDataGridItem>> GetDataForDataGrid()
        {
            var _abonents = (await _AbonentRepository.GetAllAsync()).ToObservableCollection();
            var _phoneNumbers = (await _PhoneNumberRepository.GetAllAsync()).ToObservableCollection();
            var _addresses = (await _AddressRepository.GetAllAsync()).ToObservableCollection();
            var dataGridItems = new List<MainDataGridItem>();


            foreach (var abonent in _abonents)
            {
                var address = _addresses.FirstOrDefault(a => a.AbonentId == abonent.Id);
                var homePhoneNumber = _phoneNumbers.FirstOrDefault(p => p.AbonentId == abonent.Id && p.Type == PhoneType.home.ToString());
                var workPhoneNumber = _phoneNumbers.FirstOrDefault(p => p.AbonentId == abonent.Id && p.Type == PhoneType.work.ToString());
                var mobilePhoneNumber = _phoneNumbers.FirstOrDefault(p => p.AbonentId == abonent.Id && p.Type == PhoneType.mobile.ToString());

                var dataGridItem = new MainDataGridItem
                {
                    FullName = abonent.FullName,
                    Street = address != null ? (await _StreetRepository.GetByIdAsync(address.StreetId)).StreetName : "",
                    HouseNumber = address != null ? address.HouseNumber : "",
                    HomePhoneNumber = homePhoneNumber != null ? homePhoneNumber.PhoneNumberString : "",
                    WorkPhoneNumber = workPhoneNumber != null ? workPhoneNumber.PhoneNumberString : "",
                    MobilePhoneNumber = mobilePhoneNumber != null ? mobilePhoneNumber.PhoneNumberString : ""
                };

                dataGridItems.Add(dataGridItem);
            }

            return dataGridItems.ToObservableCollection();
        }

        public async Task<ObservableCollection<MainDataGridItem>> FindAbonentsByPhoneNumber(string number)
        {

            var _abonents = (await _AbonentRepository.GetAllAsync()).ToObservableCollection();
            var _phoneNumbers = (await _PhoneNumberRepository.GetAllAsync()).ToObservableCollection();
            var _addresses = (await _AddressRepository.GetAllAsync()).ToObservableCollection();

            var matchingAbonentIds = _phoneNumbers
                .Where(phoneNumber => phoneNumber.PhoneNumberString.StartsWith(number))
                .Select(phoneNumber => phoneNumber.AbonentId)
                .Distinct();

            var matchingAbonents = _abonents
                .Where(abonent => matchingAbonentIds.Contains(abonent.Id))
                .ToObservableCollection();

            var dataGridItems = new List<MainDataGridItem>();


            foreach (var abonent in matchingAbonents)
            {
                var address = _addresses.FirstOrDefault(a => a.AbonentId == abonent.Id);
                var homePhoneNumber = _phoneNumbers.FirstOrDefault(p => p.AbonentId == abonent.Id && p.Type == PhoneType.home.ToString());
                var workPhoneNumber = _phoneNumbers.FirstOrDefault(p => p.AbonentId == abonent.Id && p.Type == PhoneType.work.ToString());
                var mobilePhoneNumber = _phoneNumbers.FirstOrDefault(p => p.AbonentId == abonent.Id && p.Type == PhoneType.mobile.ToString());

                var dataGridItem = new MainDataGridItem
                {
                    FullName = abonent.FullName,
                    Street = address != null ? (await _StreetRepository.GetByIdAsync(address.StreetId)).StreetName : "",
                    HouseNumber = address != null ? address.HouseNumber : "",
                    HomePhoneNumber = homePhoneNumber != null ? homePhoneNumber.PhoneNumberString : "",
                    WorkPhoneNumber = workPhoneNumber != null ? workPhoneNumber.PhoneNumberString : "",
                    MobilePhoneNumber = mobilePhoneNumber != null ? mobilePhoneNumber.PhoneNumberString : ""
                };

                dataGridItems.Add(dataGridItem);
            }

            return dataGridItems.ToObservableCollection();
        }
    }
}
