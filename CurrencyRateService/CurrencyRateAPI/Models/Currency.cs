namespace CurrencyRateAPI.Models;

public class Currency
{
    public string Id { get; set; } = String.Empty;
    public string NumCode { get; set; } = String.Empty;
    public string CharCode { get; set; } = String.Empty;
    public int Nominal { get; set; }
    public string Name { get; set; } = String.Empty;
    public double Value { get; set; }
    public double Previous { get; set; }
}