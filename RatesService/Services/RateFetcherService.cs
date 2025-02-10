using System.Globalization;
using System.Text.Json.Nodes;
using PositionsService.EventBus;
using PositionsService.Models;
using PositionsService.Services;
using RatesService.Models;
using Shared.Events;

namespace RatesService.Services
{
    public class RateFetcherService
    {
        private readonly HttpClient _httpClient;

        public RateFetcherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Position>> FetchRatesAsync()
        {
            List<Position> currentPositions;
            List<Crypto> marketRate = [];
            PositionService positionService = new(new PositionsService.Repositories.PositionsDbContext());

            //Get current positions
            currentPositions = await positionService.GetPositions();

            // Get the latest rates (Hardcoded for simplicity)
            var request = new HttpRequestMessage(HttpMethod.Get, "https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest?convert=USD");
            request.Headers.Add("X-CMC_PRO_API_KEY", "b431a203-8590-413e-8db9-1d4f6c9b4317");

            try
            {
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var json = JsonNode.Parse(content);

                if (json == null || json["data"] == null)
                    throw new Exception("Failed to fetch rates");

                var result = json["data"]!.AsArray();

                foreach (var item in result)
                {
                    var crypto = new Crypto
                    {
                        Symbol = item["symbol"]?.ToString() ?? string.Empty,
                        Price = decimal.TryParse(item["quote"]?["USD"]?["price"]?.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out var price) ? price : 0,
                        PercentageChanged24h = decimal.TryParse(item["quote"]?["USD"]?["percent_change_24h"]?.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out var percentageChange) ? percentageChange : 0
                    };
                    marketRate.Add(crypto);
                }

                // Compare the rates and update positions
                currentPositions = CompareHasChange(currentPositions, marketRate);

                return currentPositions;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error at FetchRatesAsync: {ex.Message}");
            }
        }

        private static List<Position> CompareHasChange(List<Position> currentlistcryptos, List<Crypto> servicelistcryptos)
        {
            RateChangedConsumer rateChangedConsumer = new RateChangedConsumer(new PositionCalculatorService());
            currentlistcryptos.ForEach(crypto =>
             {
                 var newcrypto = servicelistcryptos.Find(c => c.Symbol == crypto.Id);
                 if (newcrypto != null)
                 {
                     if (crypto.InitialRate != newcrypto.Price && (newcrypto.PercentageChanged24h < -5 || newcrypto.PercentageChanged24h > 5))
                     {
                         crypto.Pln = rateChangedConsumer.GetPln(new RateChangedEvent { Symbol = crypto.Id, NewRate = newcrypto.Price, InitialRate = crypto.InitialRate }, crypto);
                         crypto.InitialRate = newcrypto.Price;
                     }
                 }
             });

            return currentlistcryptos;
        }
    }
}
