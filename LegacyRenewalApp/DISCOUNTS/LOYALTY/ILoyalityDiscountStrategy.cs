namespace LegacyRenewalApp;

public interface ILoyalityDiscountStrategy
{
    bool RightDiscount(Customer customer);
    DiscountInfo ApplyDiscount(decimal baseAmount);
}