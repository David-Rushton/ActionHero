using System.ComponentModel.DataAnnotations;

namespace Dr.ActionHero.Commands;

public interface ICommand
{

}

public abstract class CommandSync : ICommand
{
    public void Invoke()
    {
        throw new NotImplementedException();
    }

    public void Undo()
    {
        throw new NotImplementedException();
    }
}

public abstract class CommandAsync : ICommand
{
    public async Task Invoke() =>
        await Invoke(CancellationToken.None);

    public async Task Invoke(CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task Undo() =>
        await Undo(CancellationToken.None);

    public async Task Undo(CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
