using CurrencyToWordConverterClient.Adapter.CurrencyToWordApi;
using CurrencyToWordConverterClient.Domain.Interfaces;
using CurrencyToWordConverterClient.Domain.Workflows;
using CurrencyToWordConverterClient.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace CurrencyToWordConverterClient;

/// <summary>
/// Enhanced startup of appllication. Handles dependency injection and configurations.
/// </summary>
public partial class App : Application {

    public static IHost? AppHost { get; private set; }


    public App() {

        var builder = Host.CreateApplicationBuilder();

        builder.Services.Configure<CurrencyToWordConverterApiOptions>(builder.Configuration.GetSection("externalServices").GetSection("currencyToWordConverterApi"));

        builder.Services.AddHttpClient<CurrencyToWordConverterAdapter>();

        builder.Services.AddSingleton<CurrencyToWordConverterAdapter>();
        builder.Services.AddSingleton<ICurrencyToWordConverter, CurrencyToWordConverter>();
        builder.Services.AddSingleton<UserWorkflows>();
        builder.Services.AddSingleton<MainWindow>();
        builder.Services.AddSingleton<CurrencyToWordConverterVm>();
            
        AppHost = builder.Build();
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
