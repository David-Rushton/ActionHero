using Microsoft.Extensions.DependencyInjection;
using Dr.ActionHero.Infrastructure;
using Dr.ActionHero;
using Microsoft.Extensions.Hosting;

var services = new ServiceCollection();
services
    .AddSingleton<AppLifetime>()
    .AddSingleton<AppHost>()
    .AddSingleton<InputDispatchService>()
    .AddSingleton<ViewRenderingService>()
    .AddSingleton<ControllerHost>()
    .AddSingleton<HomeView>()
    .AddSingleton<HomeController>()
    .AddHostedService(s => s.GetRequiredService<InputDispatchService>())
    .AddHostedService(s => s.GetRequiredService<ViewRenderingService>());

var provider = services.BuildServiceProvider();
var app = provider.GetRequiredService<AppHost>();

await app.Run();
