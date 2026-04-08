namespace LegacyRenewalApp;

public class BasicLoyaltyDiscount : ILoyalityDiscountStrategy
{
    public bool RightDiscount(Customer customer)
    {
        return customer.YearsWithCompany >= 2 && customer.YearsWithCompany < 5;
    }

    public DiscountInfo ApplyDiscount(decimal baseAmount)
    {
        return new DiscountInfo
        {
            DiscountAmount = baseAmount * 0.03m,
            Notes = "basic loyalty discount; "
        };
    }
}