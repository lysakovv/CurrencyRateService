using Newtonsoft.Json.Linq;
using CurrencyRateAPI.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CurrencyRateAPI.Services;

/// <summary>
/// Service for getting json file from Central Bank
/// every day at 11:40 Moscow time (UTC + 00:03:00).
/// First data is received right after application starts.
/// Currency rates are kept in MemoryCache.
/// </summary>
public class CurrencyService : BackgroundService
{
    private const string CacheName = "CurrencyData";
    private const string CurrencyUrl = "CurrencyUrl";
    private const string ValuteTokenName = "Valute";
    
    private readonly IMemoryCache _memoryCache;
    private readonly IConfiguration _config;

    public CurrencyService(IMemoryCache memoryCache, IConfiguration config)
    {
        _memoryCache = memoryCache;
        _config = config;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        TimeSpan interval = TimeSpan.FromHours(24);
        var nextRunTime = DateTime.Today.AddDays(1).AddHours(11).AddMinutes(40);
        // Subtracting Moscow time
        var firstInterval = nextRunTime.Subtract(DateTime.UtcNow + TimeSpan.FromHours(3));
        
        await UpdateCurrencyRate();
        await Task.Delay(firstInterval);

        while (stoppingToken.IsCancellationRequested == false)
        {
            await UpdateCurrencyRate();

            await Task.Delay(interval, stoppingToken);
        }
    }
    
    private async Task UpdateCurrencyRate()
    {
        List<Currency> currencies = new List<Currency>();
        using HttpClient client = new HttpClient();
        
        var jsonData = await client.GetStringAsync(_config[CurrencyUrl]);

        if (jsonData == string.Empty)
            return;
        
        var valutes = JObject.Parse(jsonData).SelectTokens(ValuteTokenName);

        if (valutes.Any() == false)
            return;
        
        foreach (var item in valutes.Values().Values())
        {
            currencies.Add(item.ToObject<Currency>()!);
        }
        
        _memoryCache.Set(CacheName, currencies);
    }
}