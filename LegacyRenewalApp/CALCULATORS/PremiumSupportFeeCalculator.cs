using System.Collections.Generic;

namespace LegacyRenewalApp;

public class PremiumSupportFeeCalculator
{
    private IEnumerable<IPremiumSupportFeeStrategy> _premiumSupportFeeStrategies;

    public PremiumSupportFeeCalculator(IEnumerable<IPremiumSupportFeeStrategy> premiumSupportFeeStrategies)
    {
        _premiumSupportFeeStrategies = premiumSupportFeeStrategies;
    }

    public decimal Calculate(string planCode, bool includePremiumSupport)
    {
        if (!includePremiumSupport)
        {
            return 0m;
        }

        foreach (var strategy in _premiumSupportFeeStrategies)
        {
            if (strategy.RightFee(planCode))
            {
                return strategy.CalculateFee();
            }
        }
        return 0m;
    }
    
}

