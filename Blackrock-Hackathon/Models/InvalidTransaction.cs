namespace BlackRock_Hackathon.Models;

/// <summary>
/// A invalid transaction inherits transaction with an additional field
/// </summary>
/// <param name="Date">Transaction data</param>
/// <param name="Amount">Transaction amount</param>
/// <param name="Ceiling">Transaction data</param>
/// <param name="Remanent">Transaction data</param>
/// <param name="Message">Invalid transaction message</param>
public record InvalidTransaction(DateTime Date, double Amount, double Ceiling, double Remanent, string Message) : Transaction(Date, Amount, Ceiling, Remanent) 
{
    public InvalidTransaction(Transaction transaction, string message) : this(transaction.Date, transaction.Amount, transaction.Ceiling, transaction.Remanent, message)
    {

    }
}
