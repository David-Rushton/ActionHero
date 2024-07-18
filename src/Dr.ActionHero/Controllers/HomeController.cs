
using System.Text;

namespace Dr.ActionHero.Controllers;

/// <summary>
/// The default controller, which is active on start up.
/// Unlike other controllers, this one is never unloaded.
/// </summary>
public class HomeController(HomeView view): Controller
{
    private readonly StringBuilder buffer = new();

    public override IView View => view;

    public override void OnInput(ConsoleKeyInfo keyInfo)
    {
        buffer.Append(keyInfo.KeyChar);

        view.DoStuff(buffer.ToString());
        view.IsDirty = true;
    }
}
