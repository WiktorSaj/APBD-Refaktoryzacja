namespace LegacyRenewalApp;

public interface ISegmentDiscountStrategy
{
    bool RightDiscount(Customer customer, SubscriptionPlan subscriptionPlan);
    DiscountInfo ApplyDiscount(decimal baseAmount);
}