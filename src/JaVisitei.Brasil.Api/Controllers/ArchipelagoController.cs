using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1")]
    [ControllerName("Brazilian Archipelago")]
    [Route("api/v{version:apiVersion}/archipelagos")]
    public class ArchipelagoController : ControllerBase
    {
        private readonly IArchipelagoService _archipelagoService;
        private readonly IIslandService _islandService;

        public ArchipelagoController(IArchipelagoService archipelagoService, IIslandService islandService)
        {
            _archipelagoService = archipelagoService;
            _islandService = islandService;
        }

        [Authorize]
        [HttpGet(Name = "GetArchipelagos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Archipelago>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetArchipelagosAsync()
        {
            var result = await _archipelagoService.GetAllAsync();

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{archipelago_id}", Name = "GetArchipelago")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Archipelago))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetArchipelagoAsync([FromRoute] string archipelago_id)
        {
            var result = await _archipelagoService.GetAsync(x => x.Id == archipelago_id);

            if (result == null)
                return NotFound();

            return Ok(result.FirstOrDefault());
        }

        [Authorize]
        [HttpGet("{archipelago_id}/islands/", Name = "GetIslandsByArchipelago")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Island>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetIslandsByArchipelagoAsync([FromRoute] string archipelago_id)
        {
            var result = await _islandService.GetAsync(x => x.ArchipelagoId == archipelago_id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
