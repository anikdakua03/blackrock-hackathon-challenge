using BlackRock_Hackathon.Models;

namespace BlackRock_Hackathon.DTOs;

public record TransactionValidatorResponse(List<Transaction> Valid, List<InvalidTransaction> Invalid);