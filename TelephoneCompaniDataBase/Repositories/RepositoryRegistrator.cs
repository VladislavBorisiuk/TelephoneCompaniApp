using Microsoft.Extensions.DependencyInjection;
using Rep_interfases;
using TelephoneCompaniDataBase.Entityes;

namespace TelephoneCompaniDataBase.Repositories
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesInDb(this IServiceCollection services) => services
            .AddTransient<INamedRepository<Abonent>, AbonentRepository>()
            .AddTransient<IRepository<PhoneNumber>, PhoneNumberRepository>()
            .AddTransient<INamedRepository<Street>, StreetRepository>()
            .AddTransient<IRepository<Address>, AddressRepository>();

    }
}
