

namespace Dr.ActionHero.Presenters;

public class HomePresenter(
    ILogger<HomePresenter> logger,
    HomeView view,
    ConsoleMonitor consoleMonitor,
    PresenterMonitor presenterMonitor) : IPresenter
{
    public IView View => view;



    public void Render(IPresenter? activePresenter, IEnumerable<IPresenter> openPresenters)
    {
        var consoleHasResized = consoleMonitor.HasResized();
        var isDirty = presenterMonitor.IsDirty(openPresenters);

        if (consoleHasResized)
            view.RenderTitle();

        if (consoleHasResized || isDirty)
        {
            logger.LogInformation($"Rendering b: {openPresenters.Count()}");
            view.RenderPresenter(
                breadcrumbViews: openPresenters.Select(p => p.View.Name),
                activeView: activePresenter?.View);
        }
    }

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
