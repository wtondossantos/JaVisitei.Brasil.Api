using JaVisitei.Brasil.Business.ViewModels.Response.Archipelago;
using JaVisitei.Brasil.Business.ViewModels.Response.Microregion;
using JaVisitei.Brasil.Business.ViewModels.Response.Municipality;
using JaVisitei.Brasil.Business.ViewModels.Response.Macroregion;
using JaVisitei.Brasil.Business.ViewModels.Response.State;
using JaVisitei.Brasil.Business.ViewModels.Response.Island;
using JaVisitei.Brasil.Business.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System;
using Asp.Versioning;

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

        [Authorize(Roles = "administrator")]
        [HttpGet(Name = "GetStates")]
        public async Task<IActionResult> GetStatesAsync()
        {
            try
            {
                var result = await _stateService.GetAsync<StateResponse>();

                if (result is null || !result.Any())
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Roles = "administrator")]
        [HttpGet("{id}", Name = "GetState")]
        public async Task<IActionResult> GetStateAsync([FromRoute] string id)
        {
            try
            {
                var result = await _stateService.GetByIdAsync<StateResponse>(id);

                if (result is null)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Roles = "administrator, basic, contributor")]
        [HttpGet("{mapTypeId}/names/", Name = "GetStatesNamesAsync")]
        public async Task<IActionResult> GetStatesNamesAsync([FromRoute] short mapTypeId)
        {
            try
            {
                var result = await _stateService.GetNamesAsync(mapTypeId);

                if (result is null || !result.Any())
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Roles = "administrator")]
        [HttpGet("{id}/macroregions/", Name = "GetMacroregionsByState")]
        public async Task<IActionResult> GetMacroregionsByStateAsync([FromRoute] string id)
        {
            try
            {
                var result = await _macroregionService.GetAsync<MacroregionResponse>(x => x.StateId.Equals(id));

                if (result is null || !result.Any())
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Roles = "administrator")]
        [HttpGet("{id}/microregions/", Name = "GetMicroregionsByState")]
        public async Task<IActionResult> GetMicroregionsByStateAsync([FromRoute] string id)
        {
            try
            {
                var result = await _microregionService.GetByStateAsync<MicroregionResponse>(id);

                if (result is null || !result.Any())
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Roles = "administrator")]
        [HttpGet("{id}/archipelagos/", Name = "GetArchipelagosByState")]
        public async Task<IActionResult> GetArchipelagosByStateAsync([FromRoute] string id)
        {
            try
            {
                var result = await _archipelagoService.GetByStateAsync<ArchipelagoResponse>(id);

                if (result is null || !result.Any())
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Roles = "administrator")]
        [HttpGet("{id}/municipalities/", Name = "GetMunicipalitiesByState")]
        public async Task<IActionResult> GetMunicipalitiesByStateAsync([FromRoute] string id)
        {
            try
            {
                var result = await _municipalityService.GetByStateAsync<MunicipalityResponse>(id);

                if (result is null || !result.Any())
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Roles = "administrator")]
        [HttpGet("{id}/islands/", Name = "GetIslandsByState")]
        public async Task<IActionResult> GetIslandsByStateAsync([FromRoute] string id)
        {
            try
            {
                var result = await _islandService.GetByStateAsync<IslandResponse>(id);

                if (result is null || !result.Any())
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
