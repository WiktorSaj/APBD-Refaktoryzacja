using System.Collections.Generic;

namespace LegacyRenewalApp;

public class LoyalityDiscountCalculator
{
    private IEnumerable<ILoyalityDiscountStrategy> _loyalityDiscountStrategies;

    public LoyalityDiscountCalculator(IEnumerable<ILoyalityDiscountStrategy> loyalityDiscountStrategies)
    {
        _loyalityDiscountStrategies = loyalityDiscountStrategies;
    }

    public DiscountInfo CalculateDiscount(Customer customer, decimal baseAmount)
    {
        foreach (var strategy in _loyalityDiscountStrategies)
        {
            if (strategy.RightDiscount(customer))
            {
                return strategy.ApplyDiscount(baseAmount);
            }
        }
        return new DiscountInfo();
    }


    
    
}