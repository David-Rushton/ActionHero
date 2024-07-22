

namespace Dr.ActionHero.Presenters;

public class HelpPresenter(HelpView view) : IPresenter
{
    public IView View => view;

    public bool TryProcessInput(ActionHeroHost host, ConsoleKeyInfo keyInfo)
    {
        throw new NotImplementedException();
    }
}
