using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1")]
    [ControllerName("Region Type")]
    [Route("api/v{version:apiVersion}/region_types")]
    public class RegionTypeController : ControllerBase
    {
        private readonly IRegionTypeService _regionTypeService;

        public RegionTypeController(IRegionTypeService regionTypeService)
        {
            _regionTypeService = regionTypeService;
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RegionType>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(Name = "GetRegionTypes")]
        public async Task<IActionResult> GetRegionTypesAsync()
        {
            var result = await _regionTypeService.GetAllAsync();

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
