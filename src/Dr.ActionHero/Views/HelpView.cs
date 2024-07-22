
using Spectre.Console.Rendering;

namespace Dr.ActionHero.Views;

public class HelpView : IView
{
    public string Name => "Help";

    public bool IsDirty { get; set ; }

    public IEnumerable<IRenderable> GetContent()
    {
        yield return new Markup("Help here");
    }
}
