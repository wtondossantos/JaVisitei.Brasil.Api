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
    [ControllerName("Brazilian Islands")]
    [Route("api/v{version:apiVersion}/islands")]
    public class IslandController : ControllerBase
    {
        private readonly IIslandService _islandService;

        public IslandController(IIslandService islandService)
        {
            _islandService = islandService;
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Island>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(Name = "GetIslands")]
        public async Task<IActionResult> GetIslandsAsync()
        {
            var result = await _islandService.GetAllAsync();

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Island))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{island_id}", Name = "GetIsland")]
        public async Task<IActionResult> GetIslandAsync([FromRoute] string island_id)
        {
            var result = await _islandService.GetAsync(x => x.Id == island_id);

            if (result == null)
                return NotFound();

            return Ok(result.FirstOrDefault());
        }
    }
}
