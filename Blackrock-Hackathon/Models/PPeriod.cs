namespace BlackRock_Hackathon.Models;

public record PPeriod(double Extra, DateTime Start, DateTime End) : Period(Start, End);
