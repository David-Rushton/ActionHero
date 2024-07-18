
namespace Dr.ActionHero.Controllers;

public abstract class Controller : IInputReceiver
{
    public abstract IView View { get; }

    public abstract void OnInput(ConsoleKeyInfo keyInfo);
}
