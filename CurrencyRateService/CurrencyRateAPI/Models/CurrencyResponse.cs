namespace CurrencyRateAPI.Models;

public class CurrencyResponse
{
    public int CurrentPage { get; set; }

    public int TotalItems { get; set; }

    public int TotalPages { get; set; }

    public IEnumerable<Currency> Items { get; set; } = null!;
}