namespace LegacyRenewalApp;

public class MediumTeamDiscount : ISeatDiscountStrategy
{
    public bool RightDiscount(int seatCount)
    {
        return seatCount >= 20 && seatCount < 50;
    }

    public DiscountInfo ApplyDiscount(decimal baseAmount)
    {
        return new DiscountInfo
        {
            DiscountAmount = baseAmount * 0.18m,
            Notes = "medium team discount; "
        };
    }
}