using PositionsService.Models;
using PositionsService.Repositories;

namespace PositionsService.Services
{
    public class PositionService
    {
        private readonly PositionsDbContext _context;
        private readonly PositionRepository _positionRepository;

        public PositionService(PositionsDbContext context)
        {
            _context = context;
            _positionRepository = new PositionRepository(_context);
        }

        public async Task<List<Position>> GetPositions()
        {
            var positions = await _positionRepository.GetPositionsAsync();
            return positions;
        }
    }
}
