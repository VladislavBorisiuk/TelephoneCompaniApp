using Microsoft.Extensions.DependencyInjection;

namespace TelephoneCompaniApp.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}
