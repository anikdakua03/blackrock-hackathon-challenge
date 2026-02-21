using BlackRock_Hackathon.DTOs;
using BlackRock_Hackathon.Models;

namespace Blackrock_Hackathon.Interfaces;

public interface ITransactionBuilder
{
    IEnumerable<Transaction> BuildFrom(IEnumerable<ExpenseDTO> expenses);
}