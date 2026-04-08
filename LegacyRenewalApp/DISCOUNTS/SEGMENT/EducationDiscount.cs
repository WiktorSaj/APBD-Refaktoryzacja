namespace LegacyRenewalApp;

public class EducationDiscount : ISegmentDiscountStrategy
{
    public bool RightDiscount(Customer customer, SubscriptionPlan subscriptionPlan)
    {
        return customer.Segment.Equals("Education") && subscriptionPlan.IsEducationEligible;
    }

    public DiscountInfo ApplyDiscount(decimal baseAmount)
    {
        return new DiscountInfo
        {
            DiscountAmount = baseAmount * 0.20m,
            Notes = "education discount; "
        };
    }
}