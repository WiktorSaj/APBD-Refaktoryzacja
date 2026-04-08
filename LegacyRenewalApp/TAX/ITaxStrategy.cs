namespace LegacyRenewalApp;

public interface ITaxStrategy
{
    bool RightTax(string country);
    decimal ApplyTax();
}