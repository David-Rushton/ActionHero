using Microsoft.Extensions.DependencyInjection;

namespace Dr.ActionHero.Services;

/// <summary>
/// Hosts our app
/// </summary>
public class ActionHeroHost(
    ILogger<ActionHeroHost> logger,
    IHostApplicationLifetime applicationLifetime,
    PresenterService presenterService) : BackgroundService
{
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            logger.LogInformation("Application started");

            ConfigureConsole();
            await MainLoop();
            ResetConsole();
        }
        catch(Exception e)
        {
            ResetConsole();
            logger.LogCritical(e, "The application failed.  This was unexpected and unrecoverable.");
            AnsiConsole.WriteException(e);
        }

        Shutdown();
        return;

        async Task MainLoop()
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (Console.KeyAvailable)
                    presenterService.TryProcessInput(
                        this,
                        Console.ReadKey(intercept: true));

                presenterService.Render();

                // TODO: This should probably go.
                await Task.Yield();
            }
        }

        void ConfigureConsole()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.CancelKeyPress += (_, _) => Shutdown();
        }

        static void ResetConsole()
        {
            Console.Clear();
            Console.CursorVisible = true;
        }
    }

    public void Shutdown() =>
        applicationLifetime.StopApplication();

    public void OpenPresenter<T>() where T : IPresenter =>
        presenterService.OpenPresenter<T>();

    public void CloseActivePresenter() =>
        presenterService.CloseActivePresenter();
}
