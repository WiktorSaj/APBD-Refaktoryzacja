namespace LegacyRenewalApp;

public class PolandTaxFee : ITaxStrategy
{
    public bool RightTax(string country)
    {
        return country == "Poland";
    }

    public decimal ApplyTax()
    {
        return 0.23m;
    }
}