namespace Dr.ActionHero.Services.Helpers;

public class ConsoleMonitor
{
    private int _width = 0;
    private int _height = 0;

    public bool HasResized()
    {
        if (Console.WindowWidth != _width || Console.WindowHeight != _height)
        {
            _width = Console.WindowWidth;
            _height = Console.WindowHeight;
            return true;
        }

        return false;
    }
}
