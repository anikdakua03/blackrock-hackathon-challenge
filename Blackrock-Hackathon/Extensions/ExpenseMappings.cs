using BlackRock_Hackathon.DTOs;
using BlackRock_Hackathon.Models;

namespace Blackrock_Hackathon.Extensions;

public static class ExpenseMappings
{
    public static Expense ToExpense(this ExpenseDTO expenseDTO)
    {
        ArgumentNullException.ThrowIfNull(expenseDTO);

        return new(expenseDTO.Date, expenseDTO.Amount);
    }

    public static ExpenseDTO ToExpenseDTO(this Expense expense)
    {
        ArgumentNullException.ThrowIfNull(expense);

        return new(expense.Date, expense.Amount);
    }
}
