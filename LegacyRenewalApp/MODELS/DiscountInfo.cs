namespace LegacyRenewalApp;

public class DiscountInfo
{
    public decimal DiscountAmount { get; set; }
    public decimal TotalAfterDiscount { get; set; }
    public string Notes { get; set; } = string.Empty;
}