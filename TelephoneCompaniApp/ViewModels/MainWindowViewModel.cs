﻿using Rep_interfases;
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
        private readonly IReportCreatorService _ReportCreatorService;

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

        #region Коллекция обхектов DG
        private ObservableCollection<MainDataGridItem> _mainDataGridItems;

        public ObservableCollection<MainDataGridItem> _MainDataGridItems
        {
            get { return _mainDataGridItems; }
            set => Set(ref _mainDataGridItems, value);
        }
        #endregion

        #region Комманды вкладки меню
        #region Сброс фильтра номера
        private ICommand _RefreshCommand;

        public ICommand RefreshCommand => _RefreshCommand
            ??= new LambdaCommandAsync(OnRefreshCommandExecuted, CanRefreshCommandExecute);


        private bool CanRefreshCommandExecute() => true;

        private async Task OnRefreshCommandExecuted()
        {
            _MainDataGridItems = await _DataService.GetDataForDataGrid();
        }

        #endregion

        #region Поиск по номеру
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
                    MessageBox.Show("Нет абонентов, удовлетворяющих критерии поиска.");
                    _MainDataGridItems = await _DataService.GetDataForDataGrid();
                }
            }
            else
            {
                return;
            }
        }
        #endregion

        #region Вывод списка улиц
        private ICommand _ShowStreetWindowCommand;

        public ICommand ShowStreetWindowCommand => _ShowStreetWindowCommand
            ??= new LambdaCommandAsync(OnShowStreetWindowCommandExecuted, CanShowStreetWindowCommandExecute);


        private bool CanShowStreetWindowCommandExecute() => true;

        private async Task OnShowStreetWindowCommandExecuted()
        {
            _UserDialog.ShowStreetList(await _DataService._StreetRepository.GetAllAsync());
        }
        #endregion

        #region Создание отчеты
        private ICommand _CreateReportCommand;

        public ICommand CreateReportCommand => _CreateReportCommand
            ??= new LambdaCommand(OnCreateReportCommandExecuted, CanCreateReportCommandExecute);


        private bool CanCreateReportCommandExecute() => true;

        private void OnCreateReportCommandExecuted()
        {
            _ReportCreatorService.CreateReport(_MainDataGridItems);
        }
        #endregion

        #endregion
        private ICommand _AddAbonentCommand;

        public ICommand AddAbonentCommand => _AddAbonentCommand
            ??= new LambdaCommandAsync(OnAddAbonentCommandExecuted, CanAddAbonentCommandExecute);


        private bool CanAddAbonentCommandExecute() => true;

        private async Task OnAddAbonentCommandExecuted()
        {
            var new_DGitem = new MainDataGridItem();
            if(_UserDialog.AddAbonent(new_DGitem)) return;

            var new_Abonent = new Abonent()
            {
                FullName = new_DGitem.FullName
            };

            var new_Street = new Street()
            {
                StreetName = new_DGitem.FullName
            };

            
        }
        #region Комманды работы с БД

        #endregion
        public MainWindowViewModel(IUserDialog UserDialog,
            IDataService DataService,
            IReportCreatorService ReportCreator)
        {
            _UserDialog = UserDialog;
            _DataService = DataService;
            _ReportCreatorService = ReportCreator;
            SetDataGridInfo();

        }

        private async void SetDataGridInfo()
        {
            _MainDataGridItems = await _DataService.GetDataForDataGrid();
        }


    }
}
