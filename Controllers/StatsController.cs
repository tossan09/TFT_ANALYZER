using Microsoft.AspNetCore.Mvc;
using TFTDataTrackerApi.Repository;

namespace TFTDataTrackerApi.Controllers
{
    [ApiController]
    [Route("api/stats")]
    public class StatsController : ControllerBase
    {
        private readonly StatsRepository _statsRepository;

        public StatsController(StatsRepository statsRepository)
        {
            _statsRepository = statsRepository;
        }

        [HttpGet("patch")]
        public async Task<IActionResult> GetStatsPorPatch()
        {
            var stats = await _statsRepository.GetStatsPorPatch();
            return Ok(stats);
        }

        [HttpGet("comp/{compName}/patch/{patchNumber}")]
        public async Task<IActionResult> GetStatsPorComp(string compName, string patchNumber)
        {
            var stats = await _statsRepository.GetStatsPorComp(compName , patchNumber);

            if (stats == null || !stats.Any())
            {
                return NotFound(new { message = $"Nenhuma estatística encontrada para a comp '{compName}'." });
            }

            return Ok(stats);
        }

    }
}
