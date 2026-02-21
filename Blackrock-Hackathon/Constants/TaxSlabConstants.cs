namespace BlackRock_Hackathon.Constants;

/// <summary>
/// Tax Slabs (Simplified): <br/>
/// • ₹0 to ₹7,00,000: 0% <br/>
/// • ₹7,00,001 to ₹10,00,000: 10% on amount above ₹7L <br/>
/// • ₹10,00,001 to ₹12,00,000: 15% on amount above ₹10L <br/>
/// • ₹12,00,001 to ₹15,00,000: 20% on amount above ₹12L <br/>
/// • Above ₹15,00,000: 30% on amount above ₹15L 
/// </summary>
public static class TaxSlabConstants
{
    public const double SevenLakhsPlusToTenLakhs = 7_00_000;
    public const double SevenLakhPlusToTenLakhsRate = 0.10;

    public const double TenLakhPlusToTwelveLakhs = 10_00_000;
    public const double TenLakhPlusToTwelveLakhsRate = 0.15;

    public const double TwelveLakhPlusToFifteenLakhs = 12_00_000;
    public const double TwelveLakhPlusToFifteenLakhsRate = 0.20;

    public const double FifteenLakhPlus = 15_00_000;
    public const double FifteenLakhPlusRate = 0.30;
}
