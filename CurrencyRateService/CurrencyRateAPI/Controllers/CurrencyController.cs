using CurrencyRateAPI.Extensions;
using CurrencyRateAPI.Models;
using CurrencyRateAPI.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CurrencyRateAPI.Controllers;

[ApiController]
[Route("currencies")]
public class CurrencyController : ControllerBase
{
    private const string CacheName = "CurrencyData";
    private readonly IMemoryCache _memoryCache;

    public CurrencyController(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    
    [HttpGet]
    public ActionResult<CurrencyResponse> Get([FromQuery] PaginationFilter filter)
    {
        if (_memoryCache.TryGetValue(CacheName, out List<Currency> currencies) == false)
        {
            return NotFound();
        }

        var results =  currencies.Paginate(filter.PageNumber, filter.PageSize);

        return Ok(results);
    }
    
    [HttpGet("{id}")]
    public ActionResult<Currency> Get(string id)
    {
        if (_memoryCache.TryGetValue(CacheName, out List<Currency> currencies) == false)
        {
            return NotFound();
        }

        var currency = currencies.Find(c => c.Id == id);

        if (currency == null)
        {
            return NotFound("Валюта с указанным идентификатором не найдена");
        }
        
        return Ok(currency);
    }
}