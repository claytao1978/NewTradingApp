using Shared.Events;
using PositionsService.Services;
using PositionsService.Models;

namespace PositionsService.EventBus
{
    public class RateChangedConsumer : RateChangedEvent
    {
        private readonly PositionCalculatorService _calculator;

        public RateChangedConsumer(PositionCalculatorService calculator)
        {
            _calculator = calculator;
        }

        public Decimal GetPln(RateChangedEvent context, Position position)
        {
            // Logic to update positions based on new rate
            var newRate = context.NewRate;
                      
            var pnl = _calculator.CalculatePnL(position.Quantity, position.InitialRate, newRate, position.SideId);
            position.UpdatePnL(pnl);
           
            return pnl;
        }
    }
}
