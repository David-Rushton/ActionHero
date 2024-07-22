using System.Security.Cryptography.X509Certificates;

using Dr.ActionHero.Services.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Dr.ActionHero.Services;


/// <summary>
/// Hosts our app
/// </summary>
public class ActionHeroHost : BackgroundService
{
    private readonly ILogger<ActionHeroHost> _logger;
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly CancellationToken _cancellationToken;
    private readonly IServiceProvider _serviceProvider;
    private readonly PresenterManager _presenterManager;
    private readonly ConsoleMonitor _consoleMonitor;

    public ActionHeroHost(
        ILogger<ActionHeroHost> logger,
        IServiceProvider serviceProvider,
        PresenterManager presenterManager,
        ConsoleMonitor consoleMonitor)
    {
        _cancellationToken = _cancellationTokenSource.Token;
        _consoleMonitor = consoleMonitor;
        _logger = logger;
        _serviceProvider = serviceProvider;
        _presenterManager = presenterManager;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            _logger.LogInformation("Application started");

            ConfigureConsole();
            await MainLoop();
            ResetConsole();
        }
        catch(Exception e)
        {
            ResetConsole();
            _logger.LogCritical(e, "The application failed.  This was unexpected and unrecoverable."));
            AnsiConsole.WriteException(e);
        }

        return;

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
        _cancellationTokenSource.Cancel();

    public void OpenPresenter<T>() where T : IPresenter =>
        _presenterManager.OpenPresenter(_serviceProvider.GetRequiredService<T>());

    public void CloseActivePresenter() =>
        _presenterManager.CloseActivePresenter();

    private async Task MainLoop()
    {
        IPresenter? lastPresenter = null;

        while (!_cancellationToken.IsCancellationRequested)
        {
            ProcessUserInput();
            RenderTitleWhenRequired();
            RenderPresenterWhenRequired();

            lastPresenter = _presenterManager.ActivePresenter;

            // TODO: This should probably go.
            await Task.Yield();
        }

        return;

        void ProcessUserInput()
        {
            if (Console.KeyAvailable)
            {
                var keyInfo = Console.ReadKey(intercept: true);

                foreach (var presenter in GetPresenters())
                    if (presenter.TryProcessInput(this, keyInfo))
                        return;
            }

            IEnumerable<IPresenter> GetPresenters()
            {
                yield return _presenterManager.HomePresenter;

                if (_presenterManager.ActivePresenter is not null)
                    yield return _presenterManager.ActivePresenter;
            }
        }

        void RenderTitleWhenRequired()
        {
            var shouldRender = _consoleMonitor.HasResized()
                || _presenterManager.ActivePresenter != lastPresenter;

            if (shouldRender)
                _presenterManager.HomePresenter.RenderTitle();
        }

        void RenderPresenterWhenRequired()
        {
            var shouldRender = (_presenterManager.ActivePresenter != lastPresenter
                && _presenterManager.ActivePresenter is not null)
                || (_presenterManager.ActivePresenter?.View?.IsDirty ?? false);

            if (shouldRender)
                _presenterManager.HomePresenter.RenderPresenter(
                    _presenterManager.ActivePresenter!,
                    _presenterManager.OpenPresenters);
        }
    }
}
