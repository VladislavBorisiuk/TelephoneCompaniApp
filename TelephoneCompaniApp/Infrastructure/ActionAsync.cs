using System.Threading.Tasks;

namespace TelephoneCompaniApp.Infrastructure
{
    internal delegate Task ActionAsync();

    internal delegate Task ActionAsync<in T>(T parameter);
}
