using BlackRock_Hackathon.Models;

namespace BlackRock_Hackathon.DTOs;

public record InvestmentsReturnResponse(double TransactionsTotalAmount, double TotalCeiling, List<Savings> SavingsByDates);