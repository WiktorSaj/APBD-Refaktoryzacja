namespace LegacyRenewalApp;

public class EnterprisePremiumSupportFee : IPremiumSupportFeeStrategy
{
    public bool RightFee(string planCode)
    {
        return planCode == "ENTERPRISE";
    }

    public decimal CalculateFee()
    {
        return 700m;
    }
}