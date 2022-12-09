using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private static readonly string[] Companies = new[]
        {
            "Microsoft", "Apple", "Google", "Amazon", "Facebook"
        };

        private readonly ILogger<StockController> _logger;

        public StockController(ILogger<StockController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetStockPrices")]
        public IEnumerable<StockPrice> Get()
        {
            return Companies.Select(company => new StockPrice
            {
                Company = company,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Price = Random.Shared.Next(100, 1000)
            })
            .ToArray();
        }
    }
}
