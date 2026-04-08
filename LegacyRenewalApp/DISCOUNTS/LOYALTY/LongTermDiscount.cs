namespace LegacyRenewalApp;

public class LongTermDiscount : ILoyalityDiscountStrategy
{
    public bool RightDiscount(Customer customer)
    {
        return customer.YearsWithCompany >= 5;
    }

    public DiscountInfo ApplyDiscount(decimal baseAmount)
    {
        return new DiscountInfo
        {
            DiscountAmount = baseAmount * 0.07m,
            Notes = "long-term loyalty discount; "
        };
    }
}