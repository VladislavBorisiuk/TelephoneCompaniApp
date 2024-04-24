using Microsoft.Extensions.DependencyInjection;
using Rep_interfases;
using TelephoneCompaniDataBase.Entityes;

namespace TelephoneCompaniDataBase.Repositories
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesInDb(this IServiceCollection services) => services
            .AddTransient<IRepository<Abonent>, AbonentRepository>()
            .AddTransient<IRepository<PhoneNumber>, PhoneNumberRepository>()
            .AddTransient<IRepository<Street>, StreetRepository>()
            .AddTransient<IRepository<Address>, AddressRepository>();

    }
}
