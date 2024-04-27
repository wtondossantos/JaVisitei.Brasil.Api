using JaVisitei.Brasil.Business.ViewModels.Response.Microregion;
using JaVisitei.Brasil.Business.ViewModels.Response.Municipality;
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
    [ControllerName("Brazilian Microregion")]
    [Route("api/v{version:apiVersion}/microregions")]
    public class MicroregionController : ControllerBase
    {
        private readonly IMicroregionService _microregionService;
        private readonly IMunicipalityService _municipalityService;

        public MicroregionController(IMicroregionService microregionService,
            IMunicipalityService municipalityService)
        {
            _microregionService = microregionService;
            _municipalityService = municipalityService;
        }

        [Authorize(Roles = "administrator")]
        [HttpGet(Name = "GetMicroregions")]
        public async Task<IActionResult> GetMicroregionsAsync()
        {
            try
            {
                var result = await _microregionService.GetAsync<MicroregionResponse>();

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
        [HttpGet("{id}", Name = "GetMicroregion")]
        public async Task<IActionResult> GetMicroregionAsync([FromRoute] string id)
        {
            try
            {
                var result = await _microregionService.GetByIdAsync<MicroregionResponse>(id);

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
        [HttpGet("{id}/municipalities/", Name = "GetMunicipalitiesByMicroregion")]
        public async Task<IActionResult> GetMunicipalitiesByMicroregionAsync([FromRoute] string id)
        {
            try
            {
                var result = await _municipalityService.GetAsync<MunicipalityResponse>(x => x.MicroregionId.Equals(id));

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
