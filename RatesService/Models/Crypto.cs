namespace RatesService.Models
{
    public class Crypto
    {
        public string? Symbol { get; set; }
        public decimal Price { get; set; }
        public decimal PercentageChanged24h { get; set; }
    }
}
