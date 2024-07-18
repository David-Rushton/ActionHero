
namespace Dr.ActionHero.Services;

/// <summary>
/// Responsible for routing input to the active controller, and registering the active view in the
/// <see cref="ViewRenderingService"/>.
/// </summary>
public class ControllerHost : IInputReceiver
{
    private readonly AppLifetime _appLifetime;
    private readonly InputDispatchService _inputDispatchService;
    private readonly ViewRenderingService _viewRenderingService;
    private readonly Queue<Controller> _controllers = new();
    private Controller _activeController;


    public ControllerHost(
        AppLifetime appLifetime,
        InputDispatchService inputDispatchService,
        ViewRenderingService viewRenderingService,
        HomeController homeController)
    {
        _appLifetime = appLifetime;
        _inputDispatchService = inputDispatchService;
        _inputDispatchService.Register(this);

        _viewRenderingService = viewRenderingService;

        _controllers.Enqueue(homeController);
        _activeController = homeController;

        _viewRenderingService.Open(homeController.View);
    }

    ~ControllerHost()
    {
        _inputDispatchService.Unregister(this);
    }

    public void Registered(Controller controller)
    {
        _controllers.Enqueue(controller);
        _activeController = controller;

        _viewRenderingService.Open(controller.View);
    }

    public void OnInput(ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Key == ConsoleKey.X)
        {
            _appLifetime.Shutdown();
        }

        if (keyInfo.Key == ConsoleKey.Escape)
        {
            if (_controllers.Count > 1)
            {
                var closingController = _controllers.Dequeue();
                _activeController = _controllers.Peek();
                _viewRenderingService.Close(closingController.View);
            }

            return;
        }

        _activeController.OnInput(keyInfo);
    }
}
