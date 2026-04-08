namespace LegacyRenewalApp;

public class SilverDiscount : ISegmentDiscountStrategy
{
    public bool RightDiscount(Customer customer, SubscriptionPlan subscriptionPlan)
    {
        return customer.Segment.Equals("Silver");
    }

    public DiscountInfo ApplyDiscount(decimal baseAmount)
    {
        return new DiscountInfo
        {
            DiscountAmount = baseAmount * 0.05m,
            Notes = "silver discount; "
        };
    }
}