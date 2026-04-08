using System.Collections.Generic;

namespace LegacyRenewalApp;

public class SeatDiscountCalculator
{
    private IEnumerable<ISeatDiscountStrategy> _seatDiscountStrategies;

    public SeatDiscountCalculator(IEnumerable<ISeatDiscountStrategy> seatDiscountStrategies)
    {
        _seatDiscountStrategies = seatDiscountStrategies;
    }

    public DiscountInfo CalculateDiscount(int seatCount, decimal baseAmount)
    {
        foreach (var strategy in _seatDiscountStrategies)
        {
            if (strategy.RightDiscount(seatCount))
            {
                return strategy.ApplyDiscount(baseAmount);
            }
        }
        return new DiscountInfo();
    }


}