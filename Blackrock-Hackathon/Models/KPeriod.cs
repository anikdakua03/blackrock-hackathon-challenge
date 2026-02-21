namespace BlackRock_Hackathon.Models;

/// <summary>
/// KPeriod represents a period of time defined by a start and end date. It inherits from the Period class, which provides common functionality for handling time periods. The KPeriod record is used to encapsulate the start and end dates for a specific period, allowing for easy manipulation and comparison of time intervals within the application.
/// </summary>
/// <param name="Start"></param>
/// <param name="End"></param>
public record KPeriod(DateTime Start, DateTime End) : Period(Start, End);