using BlackRock_Hackathon.DTOs;
using BlackRock_Hackathon.Models;

namespace Blackrock_Hackathon.Interfaces;

public interface ITemporalConstraintsValidator
{
    (List<Transaction> valids, List<InvalidTransaction> invalids) ValidateWithFilter(TransactionValidatorFilterRequest transactionValidatorFilterRequest);
}