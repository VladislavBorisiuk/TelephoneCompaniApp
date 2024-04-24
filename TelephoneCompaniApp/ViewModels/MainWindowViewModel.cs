using System.Collections.ObjectModel;
using System.Net;
using TelephoneCompaniApp.Services.Interfaces;
using TelephoneCompaniApp.ViewModels.Base;
using TelephoneCompaniDataBase.Entityes;

namespace TelephoneCompaniApp.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly IUserDialog _UserDialog;
        private readonly IDataService _DataService;

        #region Title : string - Заголовок окна

        /// <summary>Заголовок окна</summary>
        private string _Title = "Главное окно";

        /// <summary>Заголовок окна</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion

        #region Status : string - Статус

        /// <summary>Статус</summary>
        private string _Status = "Готов!";

        /// <summary>Статус</summary>
        public string Status { get => _Status; set => Set(ref _Status, value); }

        #endregion

        #region Коллекции сущностей БД
        private ObservableCollection<Abonent> _abonents;
        private ObservableCollection<PhoneNumber> _phoneNumbers;
        private ObservableCollection<Address> _addresses;

        public ObservableCollection<Abonent> Abonents
        {
            get { return _abonents; }
            set => Set(ref _abonents, value);
        }

        public ObservableCollection<PhoneNumber> PhoneNumbers
        {
            get { return _phoneNumbers; }
            set => Set(ref _phoneNumbers, value);
        }

        public ObservableCollection<Address> Addresses
        {
            get { return _addresses; }
            set => Set(ref _addresses, value);
        }
        #endregion
        public MainWindowViewModel(IUserDialog UserDialog, IDataService DataService)
        {
            _UserDialog = UserDialog;
            _DataService = DataService;
        }
    }
}
