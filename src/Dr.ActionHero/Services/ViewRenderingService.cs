namespace Dr.ActionHero.Services;

/// <summary>
/// Thrown if a requested view cannot be closed.
/// </summary>
public class CannotCloseViewException(string message) : Exception(message)
{ }

/// <summary>
/// Writes view to the console.
/// </summary>
public class ViewRenderingService : BackgroundService
{
    private readonly Queue<IView> _registeredViews = new();
    private IView? _currentView = null;


    public void Open(IView view)
    {
        _registeredViews.Enqueue(view);
        _currentView = view;
    }

    public void Close(IView view)
    {
        if (!_registeredViews.Any())
            throw new CannotCloseViewException($"Cannot close view {view.Name}.  There are no open views.");

        if (_registeredViews.Peek() != view)
            throw new CannotCloseViewException($"Cannot close view {view.Name}.  It is not the active view.");

        _registeredViews.Dequeue();
        _currentView = _registeredViews.Any()
            ? _registeredViews.Peek()
            : null;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ExecuteInner(stoppingToken);
            }
            catch(Exception e)
            {
                AnsiConsole.WriteException (e);
            }
        }
    }

    protected async Task ExecuteInner(CancellationToken stoppingToken)
    {
        var width = 0;
        var height = 0;
        IView? lastView = null;

        while (!stoppingToken.IsCancellationRequested)
        {
            var hasConsoleResized = width != Console.WindowWidth || height != Console.WindowHeight;
            if (hasConsoleResized)
            {
                width = Console.WindowWidth;
                height = Console.WindowHeight;

                Console.Clear();
                WriteTitle();
            }

            var hasViewChanged = lastView != _currentView || (_currentView?.IsDirty ?? false);
            if (hasViewChanged || hasConsoleResized)
            {
                WriteContent();

                if (_currentView is not null)
                    _currentView.IsDirty = false;
            }

            await Task.Yield();
        }

        void WriteTitle()
        {
            Console.SetCursorPosition(left: 0,  top: 0);
            AnsiConsole.Write(new FigletText("Action Hero")
                .LeftJustified()
                .Color(Color.White));
        }

        void WriteContent()
        {
            var table = new Table()
                .Border(TableBorder.None)
                .AddColumn(string.Empty);

            if (_currentView is not null)
                foreach (var content in _currentView.GetContent())
                    table.AddRow(content);

            Console.SetCursorPosition(left: 0, top: 6);
            AnsiConsole.Write(new Panel(table)
                .Header(GetBreadcrumbs())
                .HeaderAlignment(Justify.Left)
                .Padding(horizontal: 1, vertical: 1)
                .Expand()
                .Height(Console.WindowHeight - 5)
                .Border(BoxBorder.Rounded));
        }

        string GetBreadcrumbs() =>
            string.Join(" > ", _registeredViews.Select(v => v.Name));
    }
}


public static class PanelExtensions
{
    public static Panel Height(this Panel panel, int height)
    {
        panel.Height = height;
        return panel;
    }
}
