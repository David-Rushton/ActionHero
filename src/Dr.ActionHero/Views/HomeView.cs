
using Dr.ActionHero.Views.Extensions;

using Spectre.Console.Rendering;

namespace Dr.ActionHero.Views;

public class HomeView : IView
{
    private int _contentTop = 0;

    public string Name => "Home";
    public bool IsDirty { get; set ; }

    public void RenderTitle()
    {
        Console.Clear();
        AnsiConsole.Write(new FigletText("Action Hero")
            .Color(Color.CadetBlue)
            .LeftJustified());

        (_, _contentTop) = Console.GetCursorPosition();
        _contentTop++;
    }

    public void RenderPresenter(IEnumerable<string> breadcrumbViews, IView? activeView)
    {
        var content = activeView?.GetContent()
            ?? Array.Empty<IRenderable>();

        var height = Console.WindowHeight - _contentTop - 1;
        if (height > 5)
        {
            Console.SetCursorPosition(left: 0, _contentTop);

            var table = new Table()
                .AddColumn(column: string.Empty)
                .Border(TableBorder.None);

            foreach (var row in content)
                table.AddRow(row);

            AnsiConsole.Write(new Panel(table)
                .Header(string.Join(" > ", breadcrumbViews))
                .Border(BoxBorder.Rounded)
                .Expand()
                .Height(height));
        }
    }

    public IEnumerable<IRenderable> GetContent()
    {
        throw new NotImplementedException();
    }
}
