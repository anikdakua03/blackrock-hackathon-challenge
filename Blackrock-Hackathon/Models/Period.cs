namespace BlackRock_Hackathon.Models;

/// <summary>
/// Period represents a period of time defined by a start and end date. It serves as a base class for specific types of periods, such as KPeriod and QPeriod, which may include additional properties or functionality. The Period record provides a common structure for handling time intervals within the application, allowing for easy manipulation and comparison of different periods of time.
/// </summary>
/// <param name="Start"></param>
/// <param name="End"></param>
public record Period(DateTime Start, DateTime End);