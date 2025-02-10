using PositionsService.Models;

namespace PositionsService.Repositories
{
    public class PositionsDbContext
    {
        public List<Position> Positions { get; set; } = [];
    }
}
