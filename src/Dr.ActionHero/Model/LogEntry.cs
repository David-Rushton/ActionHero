namespace Dr.ActionHero.Model;

public readonly record struct LogEntry(
        DateTime DateTime,
        LogLevel logLevel,
        string Message);
