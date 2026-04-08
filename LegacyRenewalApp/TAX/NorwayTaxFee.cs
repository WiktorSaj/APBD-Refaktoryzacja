namespace LegacyRenewalApp;

public class NorwayTaxFee : ITaxStrategy
{
    public bool RightTax(string country)
    {
        return country == "Norway";
    }

    public decimal ApplyTax()
    {
        return 0.25m;
    }
}