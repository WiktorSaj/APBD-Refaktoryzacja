using System.Dynamic;

namespace LegacyRenewalApp;

public class RenewalInput
{
    public string PlanCode { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
}