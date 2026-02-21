using BlackRock_Hackathon.Services;
using System.Diagnostics;

namespace BlackRock_Hackathon.Middlewares;

public class PerformanceMiddleware : IMiddleware
{
    private readonly PerformanceTimer _performanceTimer;

    public PerformanceMiddleware(PerformanceTimer performanceTimer)
    {
        _performanceTimer = performanceTimer;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        Stopwatch sw = new ();

        sw.Start ();

        await next(context);

        sw.Stop ();

        _performanceTimer.SetExecutionTime(sw.ElapsedMilliseconds);
    }
}
