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

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Microregion>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(Name = "GetMicroregions")]
        public async Task<IActionResult> GetMicroregionsAsync()
        {
            var result = await _microregionService.GetAllAsync();

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Microregion))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{microregion_id}", Name = "GetMicroregion")]
        public async Task<IActionResult> GetMicroregionAsync([FromRoute] string microregion_id)
        {
            var result = await _microregionService.GetAsync(x => x.Id == microregion_id);

            if (result == null)
                return NotFound();

            return Ok(result.FirstOrDefault());
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Municipality>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{microregion_id}/municipalities/", Name = "GetMunicipalitiesByMicroregion")]
        public async Task<IActionResult> GetMunicipalitiesByMicroregionAsync([FromRoute] string microregion_id)
        {
            var result = await _municipalityService.GetAsync(x => x.MicroregionId == microregion_id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
