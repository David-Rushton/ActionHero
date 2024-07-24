using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

using Microsoft.Extensions.DependencyInjection;

namespace Dr.ActionHero.Presenters;

public class PresenterService(
    IServiceProvider serviceProvider,
    ILogger<PresenterService> logger,
    HomePresenter homePresenter) : IInputReceiver

{
    public Stack<IPresenter> OpenPresenters { get; } = new();

    public void OpenPresenter<T>() where T : IPresenter
    {
        var instance = serviceProvider.GetRequiredService<T>();
        instance.View.IsDirty = true;

        logger.LogInformation("Opening presenter: {presenterName}.", instance.View.Name);

        OpenPresenters.Push(instance);
    }

    public void OnTick()
    {
        if (OpenPresenters.FirstOrDefault() is ITickReceiver tickReceiver)
            tickReceiver.OnTick();
    }

    public void Render()
    {
        var activePresenter = OpenPresenters.FirstOrDefault();
        homePresenter.Render(activePresenter, OpenPresenters.Reverse());
    }

    public void CloseActivePresenter()
    {
        OpenPresenters.TryPop(out _);

        if (OpenPresenters.Any())
            OpenPresenters.Peek().View.IsDirty = true;
    }

    public bool TryProcessInput(ActionHeroHost host, ConsoleKeyInfo keyInfo)
    {
        foreach (var presenter in GetPresenters())
            if (presenter.TryProcessInput(host, keyInfo))
                return true;

        return false;

        IEnumerable<IInputReceiver> GetPresenters()
        {
            yield return homePresenter;

            if (OpenPresenters.FirstOrDefault() is IInputReceiver inputReceiver)
                yield return inputReceiver;
        }
    }
}
