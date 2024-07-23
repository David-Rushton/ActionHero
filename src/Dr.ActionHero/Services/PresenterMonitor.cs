namespace Dr.ActionHero.Services;

public class PresenterMonitor
{
    private IPresenter? _lastPresenter = null;

    public bool IsDirty(IEnumerable<IPresenter> presenters)
    {
        var active = presenters.LastOrDefault();

        if (active != _lastPresenter)
        {
            _lastPresenter = active;
            return true;
        }

        return active?.View.IsDirty ?? false;
    }
}
