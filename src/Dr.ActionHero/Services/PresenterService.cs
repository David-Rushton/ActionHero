using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

using Microsoft.Extensions.DependencyInjection;

namespace Dr.ActionHero.Presenters;

public class PresenterService(
    IServiceProvider serviceProvider,
    ILogger<PresenterService> logger,
    HomePresenter homePresenter) : IInputReceiver

{
    public Queue<IPresenter> OpenPresenters { get; } = new();

    public void OpenPresenter<T>() where T : IPresenter
    {
        var instance = serviceProvider.GetRequiredService<T>();
        instance.View.IsDirty = true;

        logger.LogInformation("Opening presenter: {presenterName}.", instance.View.Name);

        OpenPresenters.Enqueue(instance);
    }

    public void Render()
    {
        var activePresenter = OpenPresenters.LastOrDefault();
        homePresenter.Render(activePresenter, OpenPresenters);
    }

    public void CloseActivePresenter()
    {
        OpenPresenters.TryDequeue(out _);

        if (OpenPresenters.Any())
            OpenPresenters.Peek().View.IsDirty = true;
    }

    public bool TryProcessInput(ActionHeroHost host, ConsoleKeyInfo keyInfo)
    {
        foreach (var presenter in GetPresenters())
            if (presenter.TryProcessInput(host, keyInfo))
                return true;

        return false;

        IEnumerable<IPresenter> GetPresenters()
        {
            yield return homePresenter;

            if (OpenPresenters.Any())
                yield return OpenPresenters.Last();
        }
    }
}
