using JaVisitei.Brasil.Business.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using JaVisitei.Brasil.Data.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace JaVisitei.Brasil.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1")]
    [ControllerName("Brazilian Macroregion")]
    [Route("api/v{version:apiVersion}/macroregions")]
    public class MacroregionController : ControllerBase
    {
        private readonly IMacroregionService _macroregionService;
        private readonly IMicroregionService _microregionService;
        private readonly IArchipelagoService _archipelagoService;
        private readonly IMunicipalityService _municipalityService;
        private readonly IIslandService _islandService;

        public MacroregionController(IMacroregionService macroregionService,
            IMicroregionService microregionService,
            IArchipelagoService archipelagoService,
            IMunicipalityService municipalityService,
            IIslandService islandService)
        {
            _macroregionService = macroregionService;
            _microregionService = microregionService;
            _archipelagoService = archipelagoService;
            _municipalityService = municipalityService;
            _islandService = islandService;
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Macroregion>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(Name = "GetMacroregions")]
        public async Task<IActionResult> GetMacroregionsAsync()
        {
            var result = await _macroregionService.GetAllAsync();

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Macroregion))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{macroregion_id}", Name = "GetMacroregion")]
        public async Task<IActionResult> GetMacroregionAsync([FromRoute] string macroregion_id)
        {
            var result = await _macroregionService.GetAsync(x => x.Id == macroregion_id);

            if (result == null)
                return NotFound();

            return Ok(result.FirstOrDefault());
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Microregion>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{macroregion_id}/microregions/", Name = "GetMicroregionsByMacroregion")]
        public async Task<IActionResult> GetMicroregionsByMacroregionAsync([FromRoute] string macroregion_id)
        {
            var result = await _microregionService.GetAsync(x => x.MacroregionId == macroregion_id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Archipelago>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{macroregion_id}/archipelagos/", Name = "GetArchipelagosByMacroregion")]
        public async Task<IActionResult> GetArchipelagosByMacroregionAsync([FromRoute] string macroregion_id)
        {
            var result = await _archipelagoService.GetAsync(x => x.MacroregionId == macroregion_id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Municipality>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{macroregion_id}/municipalities/", Name = "GetMunicipalitiesByMacroregion")]
        public async Task<IActionResult> GetMunicipalitiesByMacroregionAsync([FromRoute] string macroregion_id)
        {
            var result = await _municipalityService.GetByMacroregionAsync(macroregion_id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Island>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{macroregion_id}/islands/", Name = "GetIslandsByMacroregion")]
        public async Task<IActionResult> GetIslandsByMacroregionAsync([FromRoute] string macroregion_id)
        {
            var result = await _islandService.GetByMacroregionAsync(macroregion_id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}

