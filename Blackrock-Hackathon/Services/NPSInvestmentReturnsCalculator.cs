using Blackrock_Hackathon.Interfaces;
using BlackRock_Hackathon.Common;
using BlackRock_Hackathon.Constants;
using BlackRock_Hackathon.DTOs;
using BlackRock_Hackathon.Models;

namespace BlackRock_Hackathon.Services;

public sealed class NPSInvestmentReturnsCalculator : IInvestmentReturnsCalculator
{
    public InvestmentsReturnResponse Calculate(InvestmentsReturnRequest investmentsReturnRequest)
    {
        int years = AgeCalculator.CalculateYears(investmentsReturnRequest.Age);

        double totalAmount = investmentsReturnRequest.Transactions.Sum(t => t.Amount);
        double totalCeiling = investmentsReturnRequest.Transactions.Sum(t => t.Ceiling);

        List<Savings> savings = new List<Savings>();

        foreach (KPeriod period in investmentsReturnRequest.K)
        {
            double invested = investmentsReturnRequest.Transactions
                .Where(t => t.Date >= period.Start && t.Date <= period.End)
                .Sum(t => t.Amount > 0 ? t.Amount : 0);

            if (invested <= 0)
            {
                savings.Add(new Savings(period.Start, period.End, 0, 0, 0));
                continue;
            }

            // Apply NPS cap
            double eligible = TaxCalculator.ApplyNPSCap(invested, investmentsReturnRequest.Wage);

            // Future value
            double futureValue = TaxCalculator.Compound(eligible, InvestmentConstants.NpsRate, years);

            // Inflation adjusted
            double inflationAdjusted = TaxCalculator.AdjustForInflation(futureValue, investmentsReturnRequest.Inflation, years);

            double profit = inflationAdjusted - eligible;

            double taxBenefit = TaxCalculator.CalculateTaxBenefit(investmentsReturnRequest.Wage, eligible);

            savings.Add(new Savings(period.Start, period.End, Math.Round(eligible, 2), Math.Round(profit, 2), Math.Round(taxBenefit, 2)));
        }

        return new InvestmentsReturnResponse(Math.Round(totalAmount, 2), Math.Round(totalCeiling, 2), savings);
    }
}