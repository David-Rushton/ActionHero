using Microsoft.Extensions.Logging.Abstractions;

namespace Dr.ActionHero.Repositories;

public class LogRepository
{
    private readonly ConcurrentBag<LogEntry> _entries = new();

    public int Count { get; private set; }

    public void Add(LogEntry entry)
    {
        _entries.Add(entry);
        Count++;
    }

    public IEnumerable<LogEntry> GetEntries() =>
        _entries;
}
