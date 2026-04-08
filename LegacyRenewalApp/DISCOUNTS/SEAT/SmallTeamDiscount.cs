namespace LegacyRenewalApp;

public class SmallTeamDiscount : ISeatDiscountStrategy
{
    public bool RightDiscount(int seatCount)
    {
        return seatCount >= 10 &&  seatCount < 20;
    }

    public DiscountInfo ApplyDiscount(decimal baseAmount)
    {
        return new DiscountInfo
        {
            DiscountAmount = baseAmount * 0.04m,
            Notes = "small team discount; "
        };
    }
}