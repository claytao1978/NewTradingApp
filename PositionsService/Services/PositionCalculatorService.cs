using PositionsService.Models;

namespace PositionsService.Services
{
    public class PositionCalculatorService
    {
        public decimal CalculatePnL(decimal quantity, decimal initialRate, decimal newRate, int side)
        {
            return quantity * (newRate - initialRate) * side;
        }
    }
}
