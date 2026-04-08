namespace LegacyRenewalApp;

public class LargeTeamDiscount : ISeatDiscountStrategy
{
    public bool RightDiscount(int seatCount)
    {
        return seatCount >= 50;
    }

    public DiscountInfo ApplyDiscount(decimal baseAmount)
    {
        return new DiscountInfo
        {
            DiscountAmount = baseAmount * 0.12m,
            Notes = "large team discount; "
        };
    }
}