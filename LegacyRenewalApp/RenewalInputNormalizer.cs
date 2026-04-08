namespace LegacyRenewalApp;

public class RenewalInputNormalizer
{
    public RenewalInput Normalize(string planCode, string paymentMethod)
    {
        return new RenewalInput
        {
            PlanCode = planCode.Trim().ToUpperInvariant(),
            PaymentMethod = paymentMethod.Trim().ToUpperInvariant()
        };
    }
}