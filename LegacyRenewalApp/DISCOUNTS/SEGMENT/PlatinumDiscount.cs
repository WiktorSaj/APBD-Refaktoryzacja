namespace LegacyRenewalApp;

public class PlatinumDiscount : ISegmentDiscountStrategy
{
    public bool RightDiscount(Customer customer, SubscriptionPlan subscriptionPlan)
    {
        return customer.Segment.Equals("Platinum");
    }

    public DiscountInfo ApplyDiscount(decimal baseAmount)
    {
        return new DiscountInfo
        {
            DiscountAmount = baseAmount * 0.15m,
            Notes = "platinum discount; "
        };
    }
}