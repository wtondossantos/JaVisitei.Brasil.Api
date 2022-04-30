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
    [ControllerName("Brazilian Municipality")]
    [Route("api/v{version:apiVersion}/municipalities")]
    public class MunicipalityController : ControllerBase
    {
        private readonly IMunicipalityService _municipalityService;

        public MunicipalityController(IMunicipalityService municipalityService)
        {
            _municipalityService = municipalityService;
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Municipality>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(Name = "GetMunicipalities")]
        public async Task<IActionResult> GetMunicipalitiesAsync()
        {
            var result = await _municipalityService.GetAllAsync();

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Municipality))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{municipality_id}", Name = "GetMunicipality")]
        public async Task<IActionResult> GetMunicipalityAsync([FromRoute] string municipality_id)
        {
            var result = await _municipalityService.GetAsync(x => x.Id == municipality_id);

            if (result == null)
                return NotFound();

            return Ok(result.FirstOrDefault());
        }
    }
}
