using Asp.Versioning;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.ViewModels.Response.Island;
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
    [ControllerName("Brazilian Islands")]
    [Route("api/v{version:apiVersion}/islands")]
    public class IslandController : ControllerBase
    {
        private readonly IIslandService _islandService;

        public IslandController(IIslandService islandService)
        {
            _islandService = islandService;
        }

        [Authorize(Roles = "administrator")]
        [HttpGet(Name = "GetIslands")]
        public async Task<IActionResult> GetIslandsAsync()
        {
            try
            {
                var result = await _islandService.GetAsync<IslandResponse>();

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
        [HttpGet("{id}", Name = "GetIsland")]
        public async Task<IActionResult> GetIslandAsync([FromRoute] string id)
        {
            try
            {
                var result = await _islandService.GetByIdAsync<IslandResponse>(id);

                if (result is null)
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
