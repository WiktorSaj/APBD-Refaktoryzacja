using System;
using System.Collections.Generic;

namespace LegacyRenewalApp;

public class PaymentFeeCalculator
{
    private IEnumerable<IPaymentFeeStrategy> _paymentFeeStrategies;

    public PaymentFeeCalculator(IEnumerable<IPaymentFeeStrategy> paymentFeeStrategies)
    {
        _paymentFeeStrategies = paymentFeeStrategies;
    }

    public PaymentFeeInfo CalculateFee(string paymentMethod, decimal totalAfterDiscount, decimal supportFee)
    {
        foreach (var strategy in _paymentFeeStrategies)
        {
            if (strategy.RightFee(paymentMethod))
            {
                return strategy.ApplyFee(totalAfterDiscount, supportFee);
            }
        }
        throw new ArgumentException("Unsupported payment method");
    }
    
}