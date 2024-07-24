using Microsoft.Extensions.DependencyInjection;

await Host.CreateDefaultBuilder(args)
    .AddInMemoryLogger(config => { })
    .ConfigureServices(services =>
    {
        services
            .AddSingleton<LogRepository>()
            .AddSingleton<PresenterService>()
            .AddSingleton<HomePresenter>()
            .AddSingleton<HelpPresenter>()
            .AddSingleton<LogPresenter>()
            .AddSingleton<HomeView>()
            .AddSingleton<HelpView>()
            .AddSingleton<LogView>()
            .AddTransient<ConsoleMonitor>()
            .AddTransient<LogMonitor>()
            .AddTransient<PresenterMonitor>()
            .AddHostedService<ActionHeroHost>();
    })
    .UseConsoleLifetime()
    .Build()
    .RunAsync();
