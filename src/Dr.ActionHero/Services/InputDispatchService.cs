using Microsoft.Extensions.Hosting;

namespace Dr.ActionHero.Services;

public interface IInputReceiver
{
    public void OnInput(ConsoleKeyInfo keyInfo);
}

/// <summary>
/// Dispatches user input to the active controller.
/// </summary>
public class InputDispatchService() : BackgroundService
{
    private readonly Queue<IInputReceiver> _inputReceivers = new();

    public void Register(IInputReceiver receiver) =>
        _inputReceivers.Enqueue(receiver);

    public void Unregister(IInputReceiver receiver)
    {
        if (_inputReceivers.TryPeek(out var currentReceiver))
        {
            if (currentReceiver == receiver)
            {
                _inputReceivers.Dequeue();
                return;
            }

            throw new ArgumentOutOfRangeException(nameof(receiver), "Cannot unregister receiver.  It is not the current receiver.");
        }

        throw new ArgumentOutOfRangeException(nameof(receiver), "Cannot unregister receiver.  It has not been registered.");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await Task.Run(() => ExecuteInner(stoppingToken));
            }
            catch(Exception e)
            {
                // TODO: Debug mode.
                AnsiConsole.WriteException (e);
                // hostApplicationLifetime.StopApplication();
            }
        }
    }

    protected void ExecuteInner(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (!Console.KeyAvailable && _inputReceivers.TryPeek(out var receiver))
            {
                receiver.OnInput(Console.ReadKey(intercept: true));
                continue;
            }

            // TODO: Should be an exception?
            // AnsiConsole.WriteLine("Unable to route input");
        }
    }
}

// Command.Invoke()
// {
//      // OpenWelcomeViewCommand
//      ControllerManager.Register(this);
//      ControllerManager.Unregister(this); =>
//
//      // do
//      var x = ControllerFactory.Create(Controllers.Welcome);
//
//      // undo
//      ControllerManager.Destroy(x);
//
// }


// open welcome command => pushes controller onto stack
// close welcome command => takes from stack
// where stack => owns ids and view renderer,
// where stack => pushes events from ids
// where controller => exposes IView or whatever


// asp => request, response, context, user.
// ControllerManager


// view => controller
// controllerFactory => ???(viewRenderer, ids)
// ids -> close command -> pop current controller & view.
// ControllerViewHost
