namespace Dr.ActionHero.Presenters;

public interface IPresenter : IInputReceiver
{
    public IView View { get; }
}
