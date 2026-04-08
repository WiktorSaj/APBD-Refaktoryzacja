namespace LegacyRenewalApp;

public interface IPremiumSupportFeeStrategy
{
    bool RightFee(string planCode);
    decimal CalculateFee();
}