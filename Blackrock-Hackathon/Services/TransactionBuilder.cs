using Blackrock_Hackathon.Interfaces;
using BlackRock_Hackathon.Common;
using BlackRock_Hackathon.DTOs;
using BlackRock_Hackathon.Models;

namespace BlackRock_Hackathon.Services;

public sealed class TransactionBuilder : ITransactionBuilder
{
    public IEnumerable<Transaction> BuildFrom(IEnumerable<ExpenseDTO> expenses)
    {
        return expenses.Select(exp => 
        {
            double ceiling = RemanentCalculator.RoundUpExpense(exp.Amount);
            double remanent = RemanentCalculator.CalculateRemanent(exp.Amount);

            return new Transaction(exp.Date, exp.Amount, ceiling, remanent);
        });
    }
}
