using Asp.Versioning;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.ViewModels.Response.RegionType;
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
    [ControllerName("Region Type")]
    [Route("api/v{version:apiVersion}/region_types")]
    public class RegionTypeController : ControllerBase
    {
        private readonly IRegionTypeService _regionTypeService;

        public RegionTypeController(IRegionTypeService regionTypeService)
        {
            _regionTypeService = regionTypeService;
        }

        [Authorize(Roles = "administrator, basic, contributor")]
        [HttpGet(Name = "GetRegionTypes")]
        public async Task<IActionResult> GetRegionTypesAsync()
        {
            try
            {
                var result = await _regionTypeService.GetAsync<RegionTypeResponse>();

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
