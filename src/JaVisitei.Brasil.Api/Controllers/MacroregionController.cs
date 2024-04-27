using JaVisitei.Brasil.Business.ViewModels.Response.Municipality;
using JaVisitei.Brasil.Business.ViewModels.Response.Macroregion;
using JaVisitei.Brasil.Business.ViewModels.Response.Microregion;
using JaVisitei.Brasil.Business.ViewModels.Response.Archipelago;
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

        [Authorize(Roles = "administrator")]
        [HttpGet(Name = "GetMacroregions")]
        public async Task<IActionResult> GetMacroregionsAsync()
        {
            try
            {
                var result = await _macroregionService.GetAsync<MacroregionResponse>();

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
        [HttpGet("{id}", Name = "GetMacroregion")]
        public async Task<IActionResult> GetMacroregionAsync([FromRoute] string id)
        {
            try
            {
                var result = await _macroregionService.GetByIdAsync<MacroregionResponse>(id);

                if (result is null)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Roles = "administrator")]
        [HttpGet("{id}/microregions/", Name = "GetMicroregionsByMacroregion")]
        public async Task<IActionResult> GetMicroregionsByMacroregionAsync([FromRoute] string id)
        {
            try
            {
                var result = await _microregionService.GetAsync<MicroregionResponse>(x => x.MacroregionId.Equals(id));

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
        [HttpGet("{id}/archipelagos/", Name = "GetArchipelagosByMacroregion")]
        public async Task<IActionResult> GetArchipelagosByMacroregionAsync([FromRoute] string id)
        {
            try
            {
                var result = await _archipelagoService.GetAsync<ArchipelagoResponse>(x => x.MacroregionId.Equals(id));

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
        [HttpGet("{id}/municipalities/", Name = "GetMunicipalitiesByMacroregion")]
        public async Task<IActionResult> GetMunicipalitiesByMacroregionAsync([FromRoute] string id)
        {
            try
            {
                var result = await _municipalityService.GetByMacroregionAsync<MunicipalityResponse>(id);

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
        [HttpGet("{id}/islands/", Name = "GetIslandsByMacroregion")]
        public async Task<IActionResult> GetIslandsByMacroregionAsync([FromRoute] string id)
        {
            try
            {
                var result = await _islandService.GetByMacroregionAsync<IslandResponse>(id);

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

