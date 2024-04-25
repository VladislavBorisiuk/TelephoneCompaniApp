using Microsoft.Extensions.DependencyInjection;

namespace TelephoneCompaniApp.ViewModels
{
    internal static class ViewModelRegistrator
    {
        public static IServiceCollection AddViews(this IServiceCollection services) => services
           .AddSingleton<MainWindowViewModel>()
           .AddSingleton<StreetsWindowViewModel>()
        ;
    }
}