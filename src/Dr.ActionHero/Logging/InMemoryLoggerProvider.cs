using Microsoft.Extensions.Options;

namespace Dr.ActionHero.Logging;

public class InMemoryLoggerProvider : ILoggerProvider
{
    private readonly Log _log;
    private readonly IDisposable? _onChangeToken;
    private readonly ConcurrentDictionary<string, InMemoryLogger> _loggers = new(StringComparer.Ordinal);
    private InMemoryLoggerConfiguration _configuration;

    public InMemoryLoggerProvider(Log log, IOptionsMonitor<InMemoryLoggerConfiguration> configuration)
    {
        _configuration = configuration.CurrentValue;
        _onChangeToken = configuration.OnChange(updatedConfig => _configuration = updatedConfig);
        _log = log;
    }

    public ILogger CreateLogger(string categoryName) =>
        _loggers.GetOrAdd(categoryName, name => new InMemoryLogger(name, _log, () => _configuration));

    public void Dispose()
    {
        _loggers.Clear();
        _onChangeToken?.Dispose();
    }
}
