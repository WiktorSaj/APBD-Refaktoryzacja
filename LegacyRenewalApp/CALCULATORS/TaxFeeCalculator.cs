using System.Collections.Generic;

namespace LegacyRenewalApp;

public class TaxFeeCalculator
{
    private IEnumerable<ITaxStrategy> taxStrategies;

    public TaxFeeCalculator(IEnumerable<ITaxStrategy> taxStrategies)
    {
        this.taxStrategies = taxStrategies;
    }


    public decimal CalculateTax(string country)
    {
        foreach (var strategy in taxStrategies)
        {
            if (strategy.RightTax(country))
            {
                return strategy.ApplyTax();
            }
        }

        return 0.20m;
    }
}