using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TelephoneCompaniDataBase.Repositories;

namespace TelephoneCompaniApp.Data
{
    static class DbRegistrator
    {
        public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration Configuration) => services
            .AddRepositoriesInDb();
    }
}
