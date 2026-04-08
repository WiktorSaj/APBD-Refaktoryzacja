namespace LegacyRenewalApp;

public class CardPaymentFee : IPaymentFeeStrategy
{
    public bool RightFee(string paymentMethod)
    {
        return paymentMethod == "CARD";
    }

    public PaymentFeeInfo ApplyFee(decimal totalAfterDiscount, decimal supportFee)
    {
        return new PaymentFeeInfo
        {
            PaymentFee = (totalAfterDiscount + supportFee) * 0.02m,
            Note = "card payment fee; "
        };
    }
    
}