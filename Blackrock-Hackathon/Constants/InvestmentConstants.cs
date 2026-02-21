namespace BlackRock_Hackathon.Constants;

/// <summary>
/// NPS (National Pension Scheme) at an interest rate of 7.11% compounded annually. Assuming a monthly income equal to the salary in Indian Rupees, for the NPS (National Pension Scheme), you can only invest up to 10% of the annual income or max of 2L annually to take advantage of the tax incentive. 
/// </summary>
public static class InvestmentConstants
{
    /// <summary>
    /// Maximum percentage of annual income eligible for NPS investment
    /// </summary>
    public const double MaxNpsIncomePercentage = 0.10;

    /// <summary>
    /// Maximum NPS deduction allowed per year (₹2,00,000)
    /// </summary>
    public const double MaxNpsAnnualCap = 200_000;

    /// <summary>
    /// NPS (National Pension Scheme) at an interest rate of 7.11%
    /// </summary>
    public const double NpsRate = 0.0711;

    /// <summary>
    /// Index Fund (e.g., NIFTY 50) at an interest rate of 14.49% compounded annually. For this fund, there are no restrictions or tax rebates. 
    /// </summary>
    public const double IndexRate = 0.1449;

    /// <summary>
    /// To calculate the final value of the investment after the remaining years until the age of 60, the compound interest formula is applied. 
    /// </summary>
    public const int RetirementAge = 60;

    /// <summary>
    /// The number of years (the difference between 60 years and your age, assuming it is less than 60, otherwise 5
    /// </summary>
    public const int MinInvestmentYears = 5;
}
