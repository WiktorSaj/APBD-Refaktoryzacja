namespace LegacyRenewalApp;

public class GoldDiscount : ISegmentDiscountStrategy
{
    public bool RightDiscount(Customer customer, SubscriptionPlan subscriptionPlan)
    {
        return customer.Segment.Equals("Gold");
    }

    public DiscountInfo ApplyDiscount (decimal baseAmount)
    {
        return new DiscountInfo
        {
            DiscountAmount = baseAmount * 0.10m,
            Notes = "gold discount; "
        };
    }
}