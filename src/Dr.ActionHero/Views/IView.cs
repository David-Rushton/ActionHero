using Spectre.Console.Rendering;

namespace Dr.ActionHero.Views;

public interface IView
{
    public string Name { get; }

    public bool IsDirty { get; set; }

    public IEnumerable<IRenderable> GetContent();
}
