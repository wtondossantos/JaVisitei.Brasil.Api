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
    [ControllerName("Brazilian Archipelago")]
    [Route("api/v{version:apiVersion}/archipelagos")]
    public class ArchipelagoController : ControllerBase
    {
        private readonly IArchipelagoService _archipelagoService;
        private readonly IIslandService _islandService;

        public ArchipelagoController(IArchipelagoService archipelagoService, IIslandService islandService)
        {
            _archipelagoService = archipelagoService;
            _islandService = islandService;
        }

        [Authorize(Roles = "administrator")]
        [HttpGet(Name = "GetArchipelagos")]
        public async Task<IActionResult> GetArchipelagosAsync()
        {
            try
            {
                var result = await _archipelagoService.GetAsync<ArchipelagoResponse>();

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
        [HttpGet("{id}", Name = "GetArchipelago")]
        public async Task<IActionResult> GetArchipelagoAsync([FromRoute] string id)
        {
            try
            {
                var result = await _archipelagoService.GetByIdAsync<ArchipelagoResponse>(id);

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
        [HttpGet("{id}/islands/", Name = "GetIslandsByArchipelago")]
        public async Task<IActionResult> GetIslandsByArchipelagoAsync([FromRoute] string id)
        {
            try
            {
                var result = await _islandService.GetAsync<IslandResponse>(x => x.ArchipelagoId.Equals(id));

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
