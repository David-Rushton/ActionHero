
namespace Dr.ActionHero.Services;

public class AppHost(
    AppLifetime appLifetime,
    ControllerHost controllerHost,
    InputDispatchService inputDispatchService,
    ViewRenderingService viewRenderingService)
{
    public async Task Run()
    {
        var token = appLifetime.CancellationToken;

        Console.Clear();
        Console.CursorVisible = false;

        await inputDispatchService.StartAsync(token);
        await viewRenderingService.StartAsync(token);

        Console.CancelKeyPress += async (_, _) =>
        {
            await inputDispatchService.StopAsync(token);
            await viewRenderingService.StopAsync(token);
            appLifetime.Shutdown();
        };

        while (!token.IsCancellationRequested)
        {
            await Task.Yield();
        }

        Console.Clear();
        Console.CursorVisible = true;
    }
}
