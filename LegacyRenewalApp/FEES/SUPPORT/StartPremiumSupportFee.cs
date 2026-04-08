namespace LegacyRenewalApp;

public class StartPremiumSupportFee : IPremiumSupportFeeStrategy
{
    public bool RightFee(string planCode)
    {
        return planCode == "START";
    }

    public decimal CalculateFee()
    {
        return 250m;
    }
}