using Microsoft.Extensions.DependencyInjection;

await Host.CreateDefaultBuilder(args)
    .AddInMemoryLogger(config => { })
    .ConfigureServices(services =>
    {
        services
            .AddSingleton<Log>()
            .AddSingleton<PresenterService>()
            .AddSingleton<HomePresenter>()
            .AddSingleton<HelpPresenter>()
            .AddSingleton<HomeView>()
            .AddSingleton<HelpView>()
            .AddTransient<ConsoleMonitor>()
            .AddTransient<PresenterMonitor>()
            .AddHostedService<ActionHeroHost>();
    })
    .UseConsoleLifetime()
    .Build()
    .RunAsync();
