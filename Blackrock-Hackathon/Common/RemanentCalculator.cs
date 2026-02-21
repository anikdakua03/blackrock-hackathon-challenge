namespace BlackRock_Hackathon.Common;

public static class RemanentCalculator
{
    public static double RoundUpExpense(double expenseAmount)
    {
        return Math.Ceiling(expenseAmount / 100.0) * 100;
    }

    public static double CalculateRemanent(double expenseAmount)
    {
        return RoundUpExpense(expenseAmount) - expenseAmount;
    }
}
