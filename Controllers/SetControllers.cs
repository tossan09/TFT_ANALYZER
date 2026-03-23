using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using TFTDataTrackerApi.Models;
using TFTDataTrackerApi.Repository;

namespace TFTDataTrackerApi.Controllers
{
    [ApiController]
    [Route("api/set")]
    public class SetControllers(SetRepository setRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sets = await setRepository.ListarSets();
            return Ok(sets);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Sets sets)
        {
            var ok = await setRepository.AdicionarSet(sets);
            if (!ok) return BadRequest("Erro ao criar set");
            return Ok(sets);

        }
    }
}
