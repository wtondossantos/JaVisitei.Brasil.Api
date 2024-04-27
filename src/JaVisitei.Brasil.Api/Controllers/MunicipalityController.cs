using Asp.Versioning;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.ViewModels.Response.Municipality;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1")]
    [ControllerName("Brazilian Municipality")]
    [Route("api/v{version:apiVersion}/municipalities")]
    public class MunicipalityController : ControllerBase
    {
        private readonly IMunicipalityService _municipalityService;

        public MunicipalityController(IMunicipalityService municipalityService)
        {
            _municipalityService = municipalityService;
        }

        [Authorize(Roles = "administrator")]
        [HttpGet(Name = "GetMunicipalities")]
        public async Task<IActionResult> GetMunicipalitiesAsync()
        {
            try
            {
                var result = await _municipalityService.GetAsync<MunicipalityResponse>();

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
        [HttpGet("{id}", Name = "GetMunicipality")]
        public async Task<IActionResult> GetMunicipalityAsync([FromRoute] string id)
        {
            try
            {
                var result = await _municipalityService.GetByIdAsync<MunicipalityResponse>(id);

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
        [HttpGet("search/{countryId}", Name = "GetNamesByCountryAsync")]
        public async Task<IActionResult> GetNamesByCountryAsync([FromRoute] string countryId)
        {
            try
            {
                var result = await _municipalityService.GetNamesByCountryAsync(countryId);

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
