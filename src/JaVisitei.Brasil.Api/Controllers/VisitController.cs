using JaVisitei.Brasil.Business.ViewModels.Request.Visit;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1")]
    [ControllerName("Já Visitei")]
    [Route("api/v{version:apiVersion}/visits")]
    public class VisitController : ControllerBase
    {
        private readonly IVisitService _visitService;

        public VisitController(IVisitService visitService)
        {
            _visitService = visitService;
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Visit>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{user_id}", Name = "GetUserVisits")]
        public async Task<IActionResult> GetUserVisitsAsync([FromRoute] int user_id)
        {
            var result = await _visitService.GetAsync(x => x.UserId == user_id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost(Name = "PostVisit")]
        public async Task<IActionResult> PostVisitAsync([FromBody] AddVisitRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _visitService.AddAsync(request);

                if (result == null || result.Validation == null)
                    return BadRequest();

                if (result.Validation.Successfully)
                    return Ok(result);

                return Unauthorized(result);
            }
            return BadRequest();
        }
    }
}
