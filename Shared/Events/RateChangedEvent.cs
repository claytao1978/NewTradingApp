
namespace Shared.Events
{
    public class RateChangedEvent
    {
        public string? Symbol { get; set; }
        public decimal NewRate { get; set; }
        public decimal InitialRate { get; set; }
        public decimal PercentageChange { get; set; }
    }
}
