namespace LegacyRenewalApp;

public class CzechRepublicTaxFee : ITaxStrategy
{
    public bool RightTax(string country)
    {
        return country == "Czech Republic";
    }

    public decimal ApplyTax()
    {
        return 0.21m;
    }
}