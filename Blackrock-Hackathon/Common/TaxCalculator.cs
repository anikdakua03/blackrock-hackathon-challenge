using BlackRock_Hackathon.Constants;

namespace BlackRock_Hackathon.Common;

public static class TaxCalculator
{
    public static double CalculateTaxBenefit(double wage, double eligibleInvestment)
    {
        double taxBefore = CalculateTax(wage);

        double taxAfter = CalculateTax(wage - eligibleInvestment);

        return taxBefore - taxAfter;
    }

    public static double Compound(double principal, double rate, int years)
    {
        return principal * Math.Pow(1 + rate, years);
    }

    public static double AdjustForInflation(double amount, double inflation, int years)
    {
        return amount / Math.Pow(1 + inflation / 100.0, years);
    }

    public static double ApplyNPSCap(double invested, double wage)
    {
        return Math.Min(Math.Min(invested, wage * InvestmentConstants.MaxNpsIncomePercentage),
            InvestmentConstants.MaxNpsAnnualCap);
    }

    private static double CalculateTax(double income)
    {
        double tax = 0;

        if (income > TaxSlabConstants.FifteenLakhPlus)
        {
            tax += (income - TaxSlabConstants.FifteenLakhPlus) * TaxSlabConstants.FifteenLakhPlusRate;
            income = TaxSlabConstants.FifteenLakhPlus;
        }

        if (income > TaxSlabConstants.TwelveLakhPlusToFifteenLakhs)
        {
            tax += (income - TaxSlabConstants.TwelveLakhPlusToFifteenLakhs) * TaxSlabConstants.TwelveLakhPlusToFifteenLakhsRate;
            income = TaxSlabConstants.TwelveLakhPlusToFifteenLakhs;
        }

        if (income > TaxSlabConstants.TenLakhPlusToTwelveLakhs)
        {
            tax += (income - TaxSlabConstants.TenLakhPlusToTwelveLakhs) * TaxSlabConstants.TenLakhPlusToTwelveLakhsRate;
            income = TaxSlabConstants.TenLakhPlusToTwelveLakhs;
        }

        if (income > TaxSlabConstants.SevenLakhsPlusToTenLakhs)
        {
            tax += (income - TaxSlabConstants.SevenLakhsPlusToTenLakhs) * TaxSlabConstants.SevenLakhPlusToTenLakhsRate;
        }

        return tax;
    }
}
