using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace JaVisitei.Brasil.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1")]
    [ControllerName("Brazilian State")]
    [Route("api/v{version:apiVersion}/states")]
    public class StateController : ControllerBase
    {
        private readonly IStateService _stateService;
        private readonly IMacroregionService _macroregionService;
        private readonly IMicroregionService _microregionService;
        private readonly IArchipelagoService _archipelagoService;
        private readonly IMunicipalityService _municipalityService;
        private readonly IIslandService _islandService;

        public StateController(IStateService stateService,
            IMacroregionService macroregionService,
            IMicroregionService microregionService,
            IArchipelagoService archipelagoService,
            IMunicipalityService municipalityService,
            IIslandService islandService)
        {
            _stateService = stateService;
            _macroregionService = macroregionService;
            _microregionService = microregionService;
            _archipelagoService = archipelagoService;
            _municipalityService = municipalityService;
            _islandService = islandService;
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<State>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(Name = "GetStates")]
        public async Task<IActionResult> GetStatesAsync()
        {
            var result = await _stateService.GetAllAsync();

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(State))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{state_id}", Name = "GetState")]
        public async Task<IActionResult> GetStateAsync([FromRoute] string state_id)
        {
            var result = await _stateService.GetAsync(x => x.Id == state_id);

            if (result == null)
                return NotFound();

            return Ok(result.FirstOrDefault());
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Macroregion>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{state_id}/macroregions/", Name = "GetMacroregionsByState")]
        public async Task<IActionResult> GetMacroregionsByStateAsync([FromRoute] string state_id)
        {
            var result = await _macroregionService.GetAsync(x => x.StateId == state_id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Microregion>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{state_id}/microregions/", Name = "GetMicroregionsByState")]
        public async Task<IActionResult> GetMicroregionsByStateAsync([FromRoute] string state_id)
        {
            var result = await _microregionService.GetByStateAsync(state_id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Archipelago>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{state_id}/archipelagos/", Name = "GetArchipelagosByState")]
        public async Task<IActionResult> GetArchipelagosByStateAsync([FromRoute] string state_id)
        {
            var result = await _archipelagoService.GetByStateAsync(state_id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Municipality>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{state_id}/municipalities/", Name = "GetMunicipalitiesByState")]
        public async Task<IActionResult> GetMunicipalitiesByStateAsync([FromRoute] string state_id)
        {
            var result = await _municipalityService.GetByStateAsync(state_id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Island>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{state_id}/islands/", Name = "GetIslandsByState")]
        public async Task<IActionResult> GetIslandsByStateAsync([FromRoute] string state_id)
        {
            var result = await _islandService.GetByStateAsync(state_id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
