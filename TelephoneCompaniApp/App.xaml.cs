using TelephoneCompaniApp.Services;
using TelephoneCompaniApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using TelephoneCompaniApp;
using TelephoneCompaniApp.Data;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace TelephoneCompaniApp
{
    public partial class App
    {
        public static bool IsDesignMode { get; private set; } = true;
        public static Window ActivedWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);

        public static Window FocusedWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsFocused);

        public static Window CurrentWindow => FocusedWindow ?? ActivedWindow;

        private static IHost __Host;

        public static IHost Host => __Host ?? Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Host.Services;

        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = Host;
            base.OnStartup(e);
            await host.StartAsync();

        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            using var host = Host;
            await host.StopAsync();
        }

        public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
            services.AddTransient<IDbConnection>(provider => new Microsoft.Data.SqlClient.SqlConnection(connectionString));

            DbRegistrator.AddDataBase(services, context.Configuration);
            ViewModelRegistrator.AddViews(services);
            ServiceRegistrator.AddServices(services);
        }



#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
        public static string CurrentDirectory => IsDesignMode ? Path.GetDirectoryName(GetSourceCodePath())
            : Environment.CurrentDirectory;
#pragma warning restore CS8603 // Возможно, возврат ссылки, допускающей значение NULL.

        private static string GetSourceCodePath([CallerFilePath] string Path = null) => Path;
    }
}
