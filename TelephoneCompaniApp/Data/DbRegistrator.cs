using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using TelephoneCompaniDataBase.Repositories;

namespace TelephoneCompaniApp.Data
{
    static class DbRegistrator
    {
        public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration Configuration) => services
            .AddTransient<IDbConnection>(provider => new Microsoft.Data.SqlClient.SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            .AddRepositoriesInDb();
    }
}
