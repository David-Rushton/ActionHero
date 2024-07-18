using Spectre.Console.Rendering;

namespace Dr.ActionHero.Views;

public class HomeView : IView
{
    private string _content = string.Empty;

    public string Name => "Home";
    public bool IsDirty { get; set; }

    public void DoStuff(string content) =>
        _content = content;

    public IEnumerable<IRenderable> GetContent()
    {
        var table = new Table()
            .AddColumn("Key")
            .AddColumn("Command")
            .Border(TableBorder.None)
            .AddRow(new Markup[] { new("x"), new("Exit") })
            .AddRow(new Markup[] { new("y"), new(_content) });

        yield return table;
    }
}
