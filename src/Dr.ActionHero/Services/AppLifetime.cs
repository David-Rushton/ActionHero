namespace Dr.ActionHero.Services;

public class AppLifetime
{
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public CancellationToken CancellationToken => _cancellationTokenSource.Token;

    public void Shutdown()
    {
        _cancellationTokenSource.Cancel();
    }
}
