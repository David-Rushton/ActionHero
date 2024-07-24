using Spectre.Console.Rendering;

namespace Dr.ActionHero.Views;

public class LogView : IView
{
    public string Name => "Logs";

    public bool IsDirty { get ; set ; }

    public IEnumerable<LogEntry> LogEntries { get; set; } = Array.Empty<LogEntry>();

    public IEnumerable<IRenderable> GetContent()
    {
        var table = new Table()
            .Border(TableBorder.None)
            .AddColumn("time")
            .AddColumn("level")
            .AddColumn("entry");

        foreach (var entry in LogEntries.OrderByDescending(l => l.DateTime))
            table.AddRow(GetRow(entry));

        yield return table;
        yield break;

        IEnumerable<IRenderable> GetRow(LogEntry logEntry)
        {
            var colour = GetColour(logEntry.logLevel).ToMarkup();

            yield return new Markup($"[{colour}]{logEntry.DateTime:HH:mm:ss}[/]");
            yield return new Markup($"[{colour}]{logEntry.logLevel}[/]");
            yield return new Markup($"[{colour}]{logEntry.Message.EscapeMarkup()}[/]");
        }

        Color GetColour(LogLevel level) =>
            level switch
            {
                LogLevel.Trace => Color.Green,
                LogLevel.Debug => Color.Green,
                LogLevel.Information => Color.Blue,
                LogLevel.Warning => Color.Yellow,
                LogLevel.Error => Color.Red,
                LogLevel.Critical => Color.Red,
                _ => Color.White
            };
    }
}
