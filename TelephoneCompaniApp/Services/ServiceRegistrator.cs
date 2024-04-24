using Microsoft.Extensions.DependencyInjection;
using TelephoneCompaniApp.Services.Interfaces;

namespace TelephoneCompaniApp.Services
{
    internal static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
           .AddTransient<IDataService, DataService>()
           .AddTransient<IUserDialog, UserDialog>()
        ;
    }
}
