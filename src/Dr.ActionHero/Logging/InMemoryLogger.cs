namespace Dr.ActionHero.Logging;

public class InMemoryLogger(
#pragma warning disable CS9113 // Parameter is unread.
    string name,
    Log log,
    Func<InMemoryLoggerConfiguration> getConfiguration) : ILogger
#pragma warning restore CS9113 // Parameter is unread.
{
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull =>
        default!;

    public bool IsEnabled(LogLevel logLevel) =>
        true;

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
            return;

        // AnsiConsole.WriteLine($"{logLevel} | {formatter(state, exception)}");

        log.Add(new(DateTime.UtcNow, logLevel, formatter(state, exception)));
    }
}
