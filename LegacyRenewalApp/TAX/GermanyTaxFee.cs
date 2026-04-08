namespace LegacyRenewalApp;

public class GermanyTaxFee : ITaxStrategy
{
    public bool RightTax(string country)
    {
        return country == "Germany";
    }

    public decimal ApplyTax()
    {
        return 0.19m;
    }
}