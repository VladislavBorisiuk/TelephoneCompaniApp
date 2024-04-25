using TelephoneCompaniApp.Services.Interfaces;
using TelephoneCompaniApp.ViewModels;
using TelephoneCompaniApp.Views.Windows;

namespace TelephoneCompaniApp.Services
{
    internal class UserDialog : IUserDialog
    {
        public string? FilterPhoneNumber()
        {
            var filter_numbers_model = new StreetsWindowViewModel();

            var filter_numbers_window = new StreetsViewWindow
            {
                DataContext = filter_numbers_model,
            };

            if (filter_numbers_window.ShowDialog() == false) return null;

            string PhoneNumber = filter_numbers_model.PhoneNumber;

            return PhoneNumber;
        }
    }
}
