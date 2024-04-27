using Rep_interfases;
using System.Collections.Generic;
using System.Windows;
using TelephoneCompaniApp.Services.Interfaces;
using TelephoneCompaniApp.ViewModels;
using TelephoneCompaniApp.Views.Windows;
using TelephoneCompaniDataBase.Entityes;

namespace TelephoneCompaniApp.Services
{
    internal class UserDialog : IUserDialog
    {
        public string? FilterPhoneNumber()
        {
            var filter_numbers_model = new PhoneFilterViewModel();

            var filter_numbers_window = new PhoneFilterWindow
            {
                DataContext = filter_numbers_model,
            };

            if (filter_numbers_window.ShowDialog() == false) return null;

            string PhoneNumber = filter_numbers_model.PhoneNumber;

            return PhoneNumber;
        }

        public void ShowStreetList(IEnumerable<Street> streets)
        {
            var streets_view_model = new StreetsViewModel(streets);

            var streets_window = new StreetsWindow()
            {
                DataContext = streets_view_model,
            };

            if (streets_window.ShowDialog() == false) return;

            return;
        }

        public bool RedactAbonent(MainDataGridItem item)
        {
            var add_abonent_model = new AddAbonentViewModel(item);

            var add_abonent_window = new AddAbonentWindow
            {
                DataContext = add_abonent_model,
            };

            if (add_abonent_window.ShowDialog() == false) return false;

            return true;
        }

        public bool RemoveAbonent()
        {
            var result = MessageBox.Show("Вы действительно хотите удалить объек?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
