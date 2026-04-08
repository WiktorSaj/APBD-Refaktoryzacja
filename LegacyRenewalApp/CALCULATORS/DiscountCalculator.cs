namespace LegacyRenewalApp;

public class DiscountCalculator
{
    private SegmentDiscountCalculator _segmentDiscountCalculator;
    private LoyalityDiscountCalculator _loyalityDiscountCalculator;
    private SeatDiscountCalculator _seatDiscountCalculator;


    public DiscountCalculator(
        SegmentDiscountCalculator segmentDiscountCalculator,
        LoyalityDiscountCalculator loyalityDiscountCalculator,
        SeatDiscountCalculator seatDiscountCalculator)
    {
        _segmentDiscountCalculator = segmentDiscountCalculator;
        _loyalityDiscountCalculator = loyalityDiscountCalculator;
        _seatDiscountCalculator = seatDiscountCalculator;
    }

    public DiscountInfo Calculate(
        Customer customer,
        SubscriptionPlan plan,
        int seatCount,
        decimal baseAmount,
        bool useLoyaltyPoints)
    {
        decimal totalDiscount = 0m;
        string notes = string.Empty;


        var segmentDiscount = _segmentDiscountCalculator.CalculateDiscount(customer, plan, baseAmount);
        totalDiscount += segmentDiscount.DiscountAmount;
        notes += segmentDiscount.Notes;
        
        var loyalityDiscount = _loyalityDiscountCalculator.CalculateDiscount(customer, baseAmount);
        totalDiscount += loyalityDiscount.DiscountAmount;
        notes += loyalityDiscount.Notes;
        
        var seatDiscount = _seatDiscountCalculator.CalculateDiscount(seatCount, baseAmount);
        totalDiscount += seatDiscount.DiscountAmount;
        notes += seatDiscount.Notes;
        
        if (useLoyaltyPoints && customer.LoyaltyPoints > 0)
        {
            int pointsToUse = customer.LoyaltyPoints > 200 ? 200 : customer.LoyaltyPoints;
            totalDiscount += pointsToUse;
            notes += $"loyalty points used: {pointsToUse}; ";
        }
        
        decimal subtotalAfterDiscount = baseAmount - totalDiscount;
        if (subtotalAfterDiscount < 300m)
        {
            subtotalAfterDiscount = 300m;
            notes += "minimum discounted subtotal applied; ";
        }

        return new DiscountInfo
        {
            DiscountAmount = totalDiscount,
            TotalAfterDiscount = subtotalAfterDiscount,
            Notes = notes
        };
    }
    
}