using PositionsService.Models;

namespace PositionsService.Repositories
{
    public class PositionRepository
    {
        private readonly PositionsDbContext _context;

        public PositionRepository(PositionsDbContext context)
        {
            _context = context;
        }

        public Task AddPositionAsync(Position position)
        {
            _context.Positions.Add(position);
            return Task.CompletedTask;
        }

        public Task ClosePositionAsync(string instrumentId)
        {
            var position = _context.Positions.FirstOrDefault(p => p.InstrumentId == instrumentId);
            if (position != null)
            {
                _context.Positions.Remove(position);
                
            }
            return Task.CompletedTask;
        }

        public Task<List<Position>> GetPositionsAsync()
        {
            _context.Positions =
        [
            new Position {Id = "BTC", InstrumentId = "BTC/USD", Quantity = 3, InitialRate = 58871.01215m, Pln = 0, Side = "BUY" },
            new Position {Id = "ETH", InstrumentId = "ETH/USD", Quantity = 10, InitialRate = 2682.019189m, Pln = 0, Side = "SELL" },
            new Position {Id = "SOL", InstrumentId = "SOL/USD", Quantity = 20, InitialRate = 138.5050875m, Pln = 0, Side = "BUY" },
            new Position {Id = "BNB", InstrumentId = "BNB/USD", Quantity = 5, InitialRate = 512.9499832m, Pln = 0, Side = "BUY" },
            new Position {Id = "USDT", InstrumentId = "USDT/USD", Quantity = 10000, InitialRate = 1.000134593m,Pln = 0, Side = "BUY" },
            new Position {Id = "ADA", InstrumentId = "ADA/USD", Quantity = 5000, InitialRate = 0.335245269m,Pln = 0, Side = "SELL" },
            new Position {Id = "SHIB", InstrumentId = "SHIB/USD", Quantity = 100000, InitialRate = 0.00001364104071m,Pln = 0, Side = "BUY" },
            new Position {Id = "DOGE", InstrumentId = "DOGE/USD", Quantity = 43000, InitialRate = 0.105241227m,Pln = 0, Side = "BUY" },
            new Position {Id = "XRP", InstrumentId = "XRP/USD", Quantity = 27000, InitialRate = 0.565457483m,Pln = 0, Side = "SELL" },
            new Position {Id = "AVAX", InstrumentId = "AVAX/USD", Quantity = 50, InitialRate = 21.02913658m,Pln = 0, Side = "SELL" },
            new Position {Id = "LTC", InstrumentId = "LTC/USD", Quantity = 10, InitialRate = 61.03340933m,Pln = 0, Side = "BUY" },
            new Position {Id = "CRO", InstrumentId = "CRO/USD", Quantity = 80000, InitialRate = 0.087408805m,Pln = 0, Side = "BUY" },
            new Position {Id = "XLM", InstrumentId = "XLM/USD", Quantity = 63000, InitialRate = 0.09764245m,Pln = 0, Side = "SELL" }
        ];

            return Task.FromResult(_context.Positions);
        }
    }
}
