namespace LegacyRenewalApp;

public class InvoicePaymentFee : IPaymentFeeStrategy
{
    public bool RightFee(string paymentMethod)
    {
        return paymentMethod == "INVOICE";
    }

    public PaymentFeeInfo ApplyFee(decimal totalAfterDiscount, decimal supportFee)
    {
        return new PaymentFeeInfo
        {
            PaymentFee = 0m,
            Note = "invoice payment; "
        };
    }
}