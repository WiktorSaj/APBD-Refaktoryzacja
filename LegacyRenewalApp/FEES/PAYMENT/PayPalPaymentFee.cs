namespace LegacyRenewalApp;

public class PayPalPaymentFee : IPaymentFeeStrategy
{
    public bool RightFee(string paymentMethod)
    {
        return paymentMethod == "PAYPAL";
    }

    public PaymentFeeInfo ApplyFee(decimal totalAfterDiscount, decimal supportFee)
    {
        return new PaymentFeeInfo
        {
            PaymentFee = (totalAfterDiscount + supportFee) * 0.035m,
            Note = "paypal fee; "
        };
    }
}