using System.Collections.ObjectModel;
using TelephoneCompaniApp.Services.Interfaces;
using TelephoneCompaniApp.ViewModels.Base;
using System.Windows.Input;
using TelephoneCompaniApp.Infrastructure.Commands;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Controls;
using MessageBox = System.Windows.MessageBox;

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
            set
            {
                _mainDataGridItems = value;
                MainDataGridItemsView.Source = value;
                OnPropertyChanged(nameof(_MainDataGridItems));
                OnPropertyChanged(nameof(MainDataGridItemsView));
            }
        }


        private CollectionViewSource _mainDataGridItemsView;

        public CollectionViewSource MainDataGridItemsView
        {
            get { return _mainDataGridItemsView; }
            set
            {
                _mainDataGridItemsView = value;
                OnPropertyChanged(nameof(MainDataGridItemsView));
            }
        }
        #endregion

        #region Выбранный DGitem

        private MainDataGridItem _currentMainDataGridItem;

        public MainDataGridItem _CurrentMainDataGridItem
        {
            get { return _currentMainDataGridItem; }
            set => Set(ref _currentMainDataGridItem, value);
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
                    System.Windows.MessageBox.Show("Нет абонентов, удовлетворяющих критерии поиска.");
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
            ICollectionView sortedCollection = MainDataGridItemsView.View;
            IEnumerable<MainDataGridItem> sortedItems = sortedCollection.Cast<MainDataGridItem>();

            // Передаем отсортированные данные в ReportCreatorService
            _ReportCreatorService.CreateReport(sortedItems);
        }
        #endregion
        #endregion

        #region Комманды работы с БД
        #region Добавление абонента
        private ICommand _AddAbonentCommand;

        public ICommand AddAbonentCommand => _AddAbonentCommand
            ??= new LambdaCommandAsync(OnAddAbonentCommandExecuted, CanAddAbonentCommandExecute);


        private bool CanAddAbonentCommandExecute() => true;

        private async Task OnAddAbonentCommandExecuted()
        {
            var new_DGitem = new MainDataGridItem()
            {
                PhoneNumbers = new string[3]
            };
            if (!_UserDialog.RedactAbonent(new_DGitem)) return;

            await _DataService.AddAbonent(new_DGitem);
            _MainDataGridItems.Add(new_DGitem);
            OnPropertyChanged(nameof(_MainDataGridItems));
        }
        #endregion
        #region Обновление абонента
        private ICommand _UpdateAbonentCommand;

        public ICommand UpdateAbonentCommand => _UpdateAbonentCommand
            ??= new LambdaCommandAsync(OnUpdateAbonentCommandExecuted, CanUpdateAbonentCommandExecute);


        private bool CanUpdateAbonentCommandExecute() => _CurrentMainDataGridItem != null;

        private async Task OnUpdateAbonentCommandExecuted()
        {

            if (!_UserDialog.RedactAbonent(_CurrentMainDataGridItem)) return;
            await _DataService.UpdateAbonent(_CurrentMainDataGridItem);
            OnPropertyChanged(nameof(_MainDataGridItems));
        }
        #endregion
        #region Удаление абонента
        private ICommand _RemoveAbonentCommand;

        public ICommand RemoveAbonentCommand => _RemoveAbonentCommand
            ??= new LambdaCommandAsync(OnRemoveAbonentCommandExecuted, CanRemoveAbonentCommandExecute);


        private bool CanRemoveAbonentCommandExecute() => _CurrentMainDataGridItem != null;

        private async Task OnRemoveAbonentCommandExecuted()
        {

            if (!_UserDialog.RemoveAbonent()) return;

            await _DataService.RemoveAbonent(_CurrentMainDataGridItem.AbonentId);
            _MainDataGridItems.Remove(_CurrentMainDataGridItem);
            OnPropertyChanged(nameof(_MainDataGridItems));
        }
        #endregion
        #region Сортировка столбцов
        private void OnDataGridSortingCommand(object parameter)
        {
            var DataGridSorting = parameter as DataGridSortingEventArgs;

            if (DataGridSorting != null)
            {


                string sortedPropertyName = DataGridSorting.Column.SortMemberPath;

                ListSortDirection sortDirection = new ListSortDirection();

                if (sortDirection == ListSortDirection.Ascending && DataGridSorting.Column.SortDirection != null)
                {
                    sortDirection = ListSortDirection.Descending;
                }
                else
                {
                    sortDirection = ListSortDirection.Ascending;
                }


                switch (sortedPropertyName)
                {
                    case "FullName":
                        SortByPropertyName(sortedPropertyName, sortDirection);
                        break;
                    case "Street":
                        SortByPropertyName(sortedPropertyName, sortDirection);
                        break;
                    case "HouseNumber":
                        SortByPropertyName(sortedPropertyName, sortDirection);
                        break;
                    case "PhoneNumbers[0]":
                        SortByPropertyName(sortedPropertyName, sortDirection);
                        break;
                    case "PhoneNumbers[1]":
                        SortByPropertyName(sortedPropertyName, sortDirection);
                        break;
                    case "PhoneNumbers[2]":
                        SortByPropertyName(sortedPropertyName, sortDirection);
                        break;
                    default:
                        MessageBox.Show("Свойство сортировки не опознанно");
                        break;
                }
            }
        }
        private void SortByPropertyName(string propertyName, ListSortDirection sortDirection)
        {
            MainDataGridItemsView.SortDescriptions.Clear();
            MainDataGridItemsView.SortDescriptions.Add(new SortDescription(propertyName, sortDirection));
        }

        private ICommand _DataGridSortingCommand;

        public ICommand DataGridSortingCommand => _DataGridSortingCommand
            ??= new LambdaCommand(OnDataGridSortingCommand, CanDataGridSortingExecute);

        private bool CanDataGridSortingExecute(object parameter) => parameter != null;
        #endregion
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
            MainDataGridItemsView = new();
            _MainDataGridItems = await _DataService.GetDataForDataGrid();
            MainDataGridItemsView.Source = _MainDataGridItems;
        }


    }

}
