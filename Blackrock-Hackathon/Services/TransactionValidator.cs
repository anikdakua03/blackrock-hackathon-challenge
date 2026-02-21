using Blackrock_Hackathon.Interfaces;
using BlackRock_Hackathon.Constants;
using BlackRock_Hackathon.DTOs;
using BlackRock_Hackathon.Models;

namespace BlackRock_Hackathon.Services;

public sealed class TransactionValidator : ITransactionValidator
{
    public (List<Transaction> valids, List<InvalidTransaction> invalids) Validate(TransactionValidatorRequest transactionValidatorRequest)
    {
        if (transactionValidatorRequest.Wage < 0)
        {
            throw new InvalidDataException("Wage cannot be negative.");
        }

        List<Transaction> valids = new List<Transaction>();
        List<InvalidTransaction> invalids = new List<InvalidTransaction>();

        HashSet<DateTime> seenDates = new HashSet<DateTime>();

        double totalRemanent = 0;

        double annualIncome = transactionValidatorRequest.Wage * 12;
        double maxAllowed = Math.Min(annualIncome * InvestmentConstants.MaxNpsIncomePercentage, InvestmentConstants.MaxNpsAnnualCap);

        foreach (Transaction transaction in transactionValidatorRequest.Transactions)
        {
            string? invalidMessage = null;

            if (transaction.Amount < 0)
            {
                invalidMessage = "Negative amounts are not allowed.";
            }
            else if (transaction.Ceiling < transaction.Amount)
            {
                invalidMessage = "Ceiling must be greater than or equal to amount.";
            }
            else if (transaction.Remanent < 0)
            {
                invalidMessage = "Remanent must be positive.";
            }
            else if (seenDates.Contains(transaction.Date))
            {
                invalidMessage = "Duplicate transaction.";
            }

            if (invalidMessage is not null)
            {
                invalids.Add(new InvalidTransaction(transaction, invalidMessage));

                continue;
            }

            // otherwise check remanents
            totalRemanent += transaction.Remanent;

            if (totalRemanent > maxAllowed)
            {
                invalids.Add(new InvalidTransaction(transaction, $"Total remanent amount exceeded the NPS threshold amount of {InvestmentConstants.MaxNpsAnnualCap}"));

                continue;
            }

            // valid
            valids.Add(transaction);
        }

        return (valids, invalids);
    }
}
