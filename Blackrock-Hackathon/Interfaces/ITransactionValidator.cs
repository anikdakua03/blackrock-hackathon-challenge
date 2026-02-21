using BlackRock_Hackathon.DTOs;
using BlackRock_Hackathon.Models;

namespace Blackrock_Hackathon.Interfaces;

public interface ITransactionValidator
{
    (List<Transaction> valids, List<InvalidTransaction> invalids) Validate(TransactionValidatorRequest transactionValidatorRequest);
}