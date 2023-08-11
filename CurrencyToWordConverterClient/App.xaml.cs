using CurrencyToWordConverterClient.Adapter;
using CurrencyToWordConverterClient.Interfaces;
using CurrencyToWordConverterClient.Workflows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace CurrencyToWordConverterClient;

public partial class App : Application {

    public static IHost? AppHost { get; private set; }


    public App() {

        //var builder = Host.CreateApplicationBuilder().Build();
        //builder.Configuration.AddJsonFile("externalServicesSettings.json", optional: false, reloadOnChange: true);

        AppHost = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) => {
            services.AddHttpClient<CurrencyToWordConverterAdapter>();
            services.AddSingleton<CurrencyToWordConverterAdapter>();
            services.AddSingleton<ICurrencyToWordConverter, CurrencyToWordConverter>();
            services.AddSingleton<UserWorkflows>();
            services.AddSingleton<MainWindow>();
            
        }).Build();
    }

    protected override async void OnStartup(StartupEventArgs e) {

        await AppHost!.StartAsync();

        var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
        startupForm.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e) {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }
}
