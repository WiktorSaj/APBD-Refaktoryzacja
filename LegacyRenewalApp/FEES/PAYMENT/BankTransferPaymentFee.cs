namespace LegacyRenewalApp;

public class BankTransferPaymentFee : IPaymentFeeStrategy
{
    public bool RightFee(string paymentMethod)
    {
        return paymentMethod == "BANK_TRANSFER";
    }

    public PaymentFeeInfo ApplyFee(decimal totalAfterDiscount, decimal supportFee)
    {
        return new PaymentFeeInfo
        {
            PaymentFee = (totalAfterDiscount + supportFee) * 0.01m,
            Note = "bank transfer fee; "
        };
    }
}