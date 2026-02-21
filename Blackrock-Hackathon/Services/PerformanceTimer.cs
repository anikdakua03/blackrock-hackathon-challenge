namespace BlackRock_Hackathon.Services;

public class PerformanceTimer
{
    private long _lastExecutionTimeMs;

    public void SetExecutionTime(long ms)
    {
        Interlocked.Exchange(ref _lastExecutionTimeMs, ms);
    }

    public long GetExecutionTime()
    {
        return Interlocked.Read(ref _lastExecutionTimeMs);
    }
}
