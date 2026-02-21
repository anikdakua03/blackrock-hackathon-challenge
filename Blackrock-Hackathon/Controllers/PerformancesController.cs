using BlackRock_Hackathon.DTOs;
using BlackRock_Hackathon.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlackRock_Hackathon.Controllers;

[ApiController]
[Route("/blackrock/challenge/v1/performance")]
public sealed class PerformancesController : ControllerBase
{
    private readonly ILogger<PerformancesController> _logger;
    private readonly PerformanceTimer _performanceTimer;

    public PerformancesController(ILogger<PerformancesController> logger, PerformanceTimer performanceTimer)
    {
        _logger = logger;
        _performanceTimer = performanceTimer;
    }


    [HttpGet]
    public ActionResult<PerformanceReport> GetPerformaceReport()
    {
        Process process = Process.GetCurrentProcess();

        long executionTime = _performanceTimer.GetExecutionTime();

        double memory = process.WorkingSet64 / 1024.0 / 1024.0; // MB

        PerformanceReport performanceReport = new(
            executionTime,
           $"{memory:F2}",
            process.Threads.Count
            );

        return Ok(performanceReport);
    }
}
