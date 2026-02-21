namespace BlackRock_Hackathon.Models;

/// <summary>
/// Expense
/// </summary>
/// <param name="Date"> Expense date</param>
/// <param name="Amount">Expense amount</param>
public record Expense(DateTime Date, double Amount);
