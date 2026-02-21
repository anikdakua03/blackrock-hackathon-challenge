using BlackRock_Hackathon.Models;

namespace BlackRock_Hackathon.DTOs;

public record InvestmentsReturnRequest(int Age, double Inflation, List<QPeriod> Q, List<PPeriod> P, List<KPeriod> K, double Wage, List<Transaction> Transactions) : TransactionValidatorFilterRequest(Q, P, K, Wage, Transactions);