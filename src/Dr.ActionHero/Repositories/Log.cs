using Microsoft.Extensions.Logging.Abstractions;

namespace Dr.ActionHero.Repositories;

// TODO: Rename.
public class Log
{
    private readonly ConcurrentBag<LogEntry> _entries = new();

    public void Add(LogEntry entry) =>
        _entries.Add(entry);
}
