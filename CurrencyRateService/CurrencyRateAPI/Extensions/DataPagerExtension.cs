using CurrencyRateAPI.Models;

namespace CurrencyRateAPI.Extensions;

public static class DataPagerExtension
{
    public static CurrencyResponse Paginate(this List<Currency> currencies, int page, int pageSize)
    {
        page = (page <= 0) ? 1 : page;

        var startPage = (page - 1) * pageSize;
        var totalItems = currencies.Count;
        
        var items = currencies
            .Skip(startPage)
            .Take(pageSize);
        
        return new CurrencyResponse
        {
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling((double)totalItems / pageSize),
            TotalItems = totalItems,
            Items = items
        };
    }
}