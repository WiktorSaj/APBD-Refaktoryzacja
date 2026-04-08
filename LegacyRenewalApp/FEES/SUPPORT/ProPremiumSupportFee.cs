namespace LegacyRenewalApp;

public class ProPremiumSupportFee : IPremiumSupportFeeStrategy
{
    public bool RightFee(string planCode)
    {
        return planCode == "PRO";
    }

    public decimal CalculateFee()
    {
        return 400m;
    }
}