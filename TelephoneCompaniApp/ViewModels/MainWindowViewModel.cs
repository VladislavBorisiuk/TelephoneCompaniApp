using Rep_interfases;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using TelephoneCompaniApp.Services.Interfaces;
using TelephoneCompaniApp.ViewModels.Base;
using TelephoneCompaniDataBase.Entityes;
using TelephoneCompaniDataBase.Repositories;
using TelephoneCompaniApp.Infrastructure.Extensions;
using System.Windows.Input;
using TelephoneCompaniApp.Infrastructure.Commands;
using TelephoneCompaniApp.Infrastructure.Commands.Base;

namespace TelephoneCompaniApp.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Сервисы/репозитории
        private readonly IUserDialog _UserDialog;
        private readonly IDataService _DataService;

        #endregion

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
        private ObservableCollection<MainDataGridItem> _mainDataGridItems;

        public ObservableCollection<MainDataGridItem> _MainDataGridItems
        {
            get { return _mainDataGridItems; }
            set => Set(ref _mainDataGridItems, value);
        }
        #endregion

        private ICommand _RefreshCommand;

        public ICommand RefreshCommand => _RefreshCommand
            ??= new LambdaCommandAsync(OnRefreshCommandExecuted, CanRefreshCommandExecute);


        private bool CanRefreshCommandExecute() => true;

        private async Task OnRefreshCommandExecuted()
        {
            _MainDataGridItems = await _DataService.GetDataForDataGrid();
        }

        private ICommand _PhoneNumberFiltereCommand;

        public ICommand PhoneNumberFiltereCommand => _PhoneNumberFiltereCommand
            ??= new LambdaCommandAsync(OnPhoneNumberFiltereCommandExecuted, CanPhoneNumberFiltereCommandExecute);


        private bool CanPhoneNumberFiltereCommandExecute() => true;

        private async Task OnPhoneNumberFiltereCommandExecuted()
        {
            var phoneNumber = _UserDialog.FilterPhoneNumber();
            if (phoneNumber != null)
            {
                _MainDataGridItems = await _DataService.FindAbonentsByPhoneNumber(phoneNumber);
                if (_MainDataGridItems.Count == 0)
                {
                    MessageBox.Show("Абонентов с таким номером не найдено");
                    _MainDataGridItems = await _DataService.GetDataForDataGrid();
                }
            }
            else
            {
                return;
            }
        }

        private ICommand _ShowStreetWindowCommand;

        public ICommand ShowStreetWindowCommand => _ShowStreetWindowCommand
            ??= new LambdaCommandAsync(OnShowStreetWindowCommandExecuted, CanShowStreetWindowCommandExecute);


        private bool CanShowStreetWindowCommandExecute() => true;

        private async Task OnShowStreetWindowCommandExecuted()
        {
            _UserDialog.ShowStreetList(await _DataService._StreetRepository.GetAllAsync());
        }

        public MainWindowViewModel(IUserDialog UserDialog,
            IDataService DataService)
        {
            _UserDialog = UserDialog;
            _DataService = DataService;
            SetDataGridInfo();

        }

        private async void SetDataGridInfo()
        {
            _MainDataGridItems = await _DataService.GetDataForDataGrid();
        }


    }
}
