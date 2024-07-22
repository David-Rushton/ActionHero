using System.Diagnostics.CodeAnalysis;
using System.Xml.XPath;

namespace Dr.ActionHero.Presenters;

public class  PresenterManager(HomePresenter homePresenter)
{
    private IPresenter? _activePresenter;
    private IPresenter? _lastPresenter;

    public HomePresenter HomePresenter => homePresenter;
    public IPresenter? ActivePresenter => _activePresenter;
    public Queue<IPresenter> OpenPresenters => new([homePresenter]);

    public void OpenPresenter(IPresenter presenter)
    {
        OpenPresenters.Enqueue(presenter);
        _activePresenter = presenter;
    }

    public bool TryGetActivePresenter([NotNullWhen(returnValue: true)]out IPresenter? presenter)
    {
        presenter = _activePresenter;
        return _activePresenter is not null;
    }

    public bool HasPresenterChanged()
    {
        var result = _lastPresenter != _activePresenter;
        _lastPresenter = _activePresenter;

        return result;
    }

    public void CloseActivePresenter() =>
        OpenPresenters.TryDequeue(out _activePresenter);

}
