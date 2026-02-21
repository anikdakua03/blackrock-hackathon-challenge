using BlackRock_Hackathon.Constants;

namespace BlackRock_Hackathon.Common;

public static class AgeCalculator
{
    /// <summary>
    /// Calculates the number of years (the difference between 60 years and your age, assuming it is less than 60, otherwise 5). 
    /// </summary>
    /// <param name="age"></param>
    /// <returns></returns>
    public static int CalculateYears(int age)
    {
        if (age >= InvestmentConstants.RetirementAge)
        {
            return InvestmentConstants.MinInvestmentYears;
        }

        return InvestmentConstants.RetirementAge - age;
    }
}
