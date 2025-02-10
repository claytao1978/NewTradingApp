using Microsoft.AspNetCore.Mvc;
using PositionsService.Models;
using PositionsService.Repositories;

namespace PositionsService.Controllers
{
    [ApiController]
    [Route("api/positions")]
    public class PositionsController : ControllerBase
    {
        private readonly PositionRepository _positionRepository;

        public PositionsController(PositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPosition([FromBody] Position position)
        {
            await _positionRepository.AddPositionAsync(position);
            return Ok();
        }

        [HttpPost("close/{instrumentId}")]
        public async Task<IActionResult> ClosePosition(string instrumentId)
        {
            await _positionRepository.ClosePositionAsync(instrumentId);
            return Ok();
        }
    }
}
