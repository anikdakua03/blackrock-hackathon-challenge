namespace BlackRock_Hackathon.Models;

public record QPeriod(double Fixed, DateTime Start, DateTime End) : Period(Start, End);