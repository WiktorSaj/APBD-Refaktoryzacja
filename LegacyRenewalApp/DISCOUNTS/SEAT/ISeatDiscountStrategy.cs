namespace LegacyRenewalApp;

public interface ISeatDiscountStrategy
{
    bool RightDiscount(int seatCount);
    DiscountInfo ApplyDiscount(decimal baseAmount);
}