using System.Collections.Generic;

namespace LegacyRenewalApp;

public class SegmentDiscountCalculator
{
    private IEnumerable<ISegmentDiscountStrategy> _segmentDiscountStrategies;

    public SegmentDiscountCalculator(IEnumerable<ISegmentDiscountStrategy> segmentDiscountStrategies)
    {
        _segmentDiscountStrategies = segmentDiscountStrategies;
    }

    public DiscountInfo CalculateDiscount(Customer customer, SubscriptionPlan subscriptionPlan, decimal baseAmount)
    {
        foreach (var strategy in _segmentDiscountStrategies)
        {
            if (strategy.RightDiscount(customer, subscriptionPlan))
            {
                return strategy.ApplyDiscount(baseAmount);
            }
        }
        return new DiscountInfo();
    }
    
    
    
}