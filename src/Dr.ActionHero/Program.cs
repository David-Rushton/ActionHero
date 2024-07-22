using Microsoft.Extensions.DependencyInjection;

Host.CreateDefaultBuilder(args)
    .AddInMemoryLogger(config => { })
    .ConfigureServices(services =>
    {
        services
            .AddSingleton<Log>()
            .AddSingleton<ConsoleMonitor>()
            .AddSingleton<PresenterManager>()
            .AddSingleton<HomePresenter>()
            .AddSingleton<HelpPresenter>()
            .AddSingleton<HomeView>()
            .AddSingleton<HelpView>()
            .AddHostedService<ActionHeroHost>();
    })
    .Build()
    .Run();
