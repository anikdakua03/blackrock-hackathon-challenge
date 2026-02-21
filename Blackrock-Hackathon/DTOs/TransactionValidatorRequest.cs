using BlackRock_Hackathon.Models;

namespace BlackRock_Hackathon.DTOs;

public record TransactionValidatorRequest(double Wage, List<Transaction> Transactions);