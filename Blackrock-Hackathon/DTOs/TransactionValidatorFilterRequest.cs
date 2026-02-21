using BlackRock_Hackathon.Models;

namespace BlackRock_Hackathon.DTOs;

public record TransactionValidatorFilterRequest(List<QPeriod> Q, List<PPeriod> P, List<KPeriod> K, double Wage, List<Transaction> Transactions) : TransactionValidatorRequest(Wage, Transactions);