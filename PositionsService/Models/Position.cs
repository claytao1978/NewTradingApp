namespace PositionsService.Models
{
    public class Position
    {
        public string? Id { get; set; }
        public string? InstrumentId { get; set; }
        public decimal Quantity { get; set; }
        public decimal InitialRate { get; set; }

        public int SideId => Side switch
        {
            "BUY" => 1,
            "SELL" => -1,
            _ => 0
        };
        public string Side { get; set; } = "";

        public decimal Pln { get; set; }

        public void UpdatePnL(decimal pnl)
        {
            Pln = pnl;
        }
    }
}
