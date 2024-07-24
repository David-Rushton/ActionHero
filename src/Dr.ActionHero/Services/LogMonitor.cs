namespace Dr.ActionHero.Services;

public class LogMonitor()
{
    private int _lastCount = 0;

    public bool HasNewEntries(LogRepository logRepository)
    {
        if (_lastCount != logRepository.Count)
        {
            _lastCount = logRepository.Count;
            return true;
        }

        return false;
    }
}
