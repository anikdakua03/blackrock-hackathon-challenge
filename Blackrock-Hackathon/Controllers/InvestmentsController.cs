using Blackrock_Hackathon.Interfaces;
using BlackRock_Hackathon.DTOs;
using BlackRock_Hackathon.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BlackRock_Hackathon.Controllers;

[ApiController]
[Route("/blackrock/challenge/v1/returns")]
public sealed class InvestmentsController : ControllerBase
{
    private readonly ILogger<InvestmentsController> _logger;
    private readonly IInvestmentReturnsCalculator _npsInvestmentReturnsCalculator;
    private readonly IInvestmentReturnsCalculator _indexInvestmentReturnsCalculator;

    public InvestmentsController(ILogger<InvestmentsController> logger, [FromKeyedServices(InvestmentInstrument.NPS)] IInvestmentReturnsCalculator npsInvestmentReturnsCalculator, [FromKeyedServices(InvestmentInstrument.Index)] IInvestmentReturnsCalculator indexInvestmentReturnsCalculator)
    {
        _logger = logger;
        _npsInvestmentReturnsCalculator = npsInvestmentReturnsCalculator;
        _indexInvestmentReturnsCalculator = indexInvestmentReturnsCalculator;
    }


    [HttpPost("nps")]
    public ActionResult<InvestmentsReturnResponse> CalculateNPSReturns([FromBody] InvestmentsReturnRequest investmentsReturnRequest)
    {
        InvestmentsReturnResponse response = _npsInvestmentReturnsCalculator.Calculate(investmentsReturnRequest);

        return Ok(response);
    }

    [HttpPost("index")]
    public ActionResult<InvestmentsReturnResponse> CalculateIndexReturns([FromBody] InvestmentsReturnRequest investmentsReturnRequest)
    {
        InvestmentsReturnResponse response = _indexInvestmentReturnsCalculator.Calculate(investmentsReturnRequest);

        return Ok(response);
    }
}
