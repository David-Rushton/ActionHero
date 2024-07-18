namespace Dr.ActionHero.Commands;

/// <summary>
/// Executes commands.
/// </summary>
public class CommandProcessor
{
    public async Task Invoke(ICommand command) =>
        await Invoke(command, CancellationToken.None);

    public async Task Invoke(ICommand command, CancellationToken token)
    {
        if (token.IsCancellationRequested)
            return;

        if (command is CommandSync commandSync)
        {
            commandSync.Invoke();
            return;
        }

        if (command is CommandAsync commandAsync)
        {
            await commandAsync.Invoke(token);
            return;
        }

        throw new NotImplementedException();
    }

    public async Task Undo(ICommand command) =>
        await Undo(command, CancellationToken.None);

    public async Task Undo(ICommand command, CancellationToken token)
    {
        if (token.IsCancellationRequested)
            return;

        if (command is CommandSync commandSync)
        {
            commandSync.Undo();
            return;
        }

        if (command is CommandAsync commandAsync)
        {
            await commandAsync.Undo(token);
            return;
        }

        throw new NotImplementedException();
    }
}
