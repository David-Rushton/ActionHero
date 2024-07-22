

namespace Dr.ActionHero.Presenters;

public class HomePresenter(HomeView view) : IPresenter
{
    public IView View => view;

    public void RenderTitle() =>
        view.RenderTitle();

    public void RenderPresenter(IPresenter activePresenter, IEnumerable<IPresenter> openPresenters) =>
        view.RenderPresenter(
            breadcrumbViews: openPresenters.Select(p => p.View.Name),
            content: activePresenter.View.GetContent());

    public bool TryProcessInput(ActionHeroHost host, ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Key == ConsoleKey.X)
        {
            host.Shutdown();
            return true;
        }

        if (keyInfo.Key == ConsoleKey.Escape)
        {
            host.CloseActivePresenter();
            return true;
        }

        if (keyInfo.KeyChar == '?')
        {
            host.OpenPresenter<HelpPresenter>();
            return true;
        }

        return false;
    }
}
