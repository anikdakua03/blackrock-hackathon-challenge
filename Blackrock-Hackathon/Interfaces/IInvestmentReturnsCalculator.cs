using BlackRock_Hackathon.DTOs;

namespace Blackrock_Hackathon.Interfaces;

public interface IInvestmentReturnsCalculator
{
    InvestmentsReturnResponse Calculate(InvestmentsReturnRequest investmentsReturnRequest);
}