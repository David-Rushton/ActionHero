

namespace Dr.ActionHero.Presenters;

public class LogPresenter(
    LogView view,
    LogMonitor logMonitor,
    LogRepository logRepository) : IPresenter, ITickReceiver
{
    public IView View => view;

    public void OnTick()
    {
        if (logMonitor.HasNewEntries(logRepository))
        {
            view.LogEntries = logRepository.GetEntries();
            view.IsDirty = true;
        }
    }

    public bool TryProcessInput(ActionHeroHost host, ConsoleKeyInfo keyInfo)
    {
        return false;
    }
}
