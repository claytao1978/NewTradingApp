using Microsoft.AspNetCore.Mvc;
using RatesService.Services;

namespace RatesService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatesController : ControllerBase
    {
        private readonly ILogger<RateFetcherService> _rateFetcherService;

        public RatesController(ILogger<RateFetcherService> rateFetcherService)
        {
            _rateFetcherService = rateFetcherService;
        }

        [HttpGet("fetch")]
        public async Task<IActionResult> FetchRates()
        {
            RateFetcherService rateFetcherService = new(new HttpClient());
            var rates = await rateFetcherService.FetchRatesAsync();

            return Ok(rates);
            
        }
    }
}
