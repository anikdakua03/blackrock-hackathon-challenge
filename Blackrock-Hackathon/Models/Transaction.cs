namespace BlackRock_Hackathon.Models;

public record Transaction(DateTime Date, double Amount, double Ceiling, double Remanent) : Expense(Date, Amount)
{
    public bool InKPeriod { get; set; } = false;
};