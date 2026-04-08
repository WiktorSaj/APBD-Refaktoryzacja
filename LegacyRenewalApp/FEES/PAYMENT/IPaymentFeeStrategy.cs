namespace LegacyRenewalApp;

public interface IPaymentFeeStrategy
{
    bool RightFee(string paymentMethod);
    PaymentFeeInfo ApplyFee(decimal totalAfterDiscount, decimal supportFee);
}