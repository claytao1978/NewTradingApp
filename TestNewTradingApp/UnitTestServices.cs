using PositionsService.Repositories;
using PositionsService.Services;
using RatesService.Services;

namespace TestNewTradingApp
{
    public class Tests
    {
        private PositionService _positionsService;
        private RateFetcherService _ratesService;
        private readonly PositionsDbContext _context = new();

        [SetUp]
        public void Setup()
        {
            _positionsService = new PositionService(_context);
            _ratesService = new RateFetcherService(new HttpClient());
        }

        [Test]
        public void GetPositions()
        {
            var positions = _positionsService.GetPositions();
            Assert.That(positions, Is.Not.Null);
        }

        [Test]
        public void FetchPositions()
        {
            Task<List<PositionsService.Models.Position>> positions = _ratesService.FetchRatesAsync();
            Assert.That(positions, Is.Not.Null);
        }
    }
}