namespace Dr.ActionHero.Abstractions;

public interface IInputReceiver
{
    public bool TryProcessInput(ActionHeroHost host, ConsoleKeyInfo keyInfo);
}
