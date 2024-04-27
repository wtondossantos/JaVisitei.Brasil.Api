using JaVisitei.Brasil.Business.ViewModels.Request.Visit;
using JaVisitei.Brasil.Business.ViewModels.Response.Visit;
using JaVisitei.Brasil.Business.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Asp.Versioning;

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

        [Authorize(Roles = "administrator, basic, contributor")]
        [HttpPost(Name = "PostVisit")]
        public async Task<IActionResult> PostVisitAsync([FromBody] InsertVisitRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _visitService.InsertAsync(request);

                if (result is null)
                    return NotFound(result);

                if (!result.IsValid)
                    return BadRequest(result);

                return Accepted(Url.Link("GetVisitsByUserId", new { user_id = result.Data?.UserId }), result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Roles = "administrator, basic, contributor")]
        [HttpGet(Name = "GetVisit")]
        public async Task<IActionResult> GetVisitAsync([FromBody] VisitKeyRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _visitService.GetByIdAsync<VisitResponse>(request);

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
        [HttpDelete(Name = "DeleteVisit")]
        public async Task<IActionResult> DeleteVisitAsync([FromBody] VisitKeyRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _visitService.DeleteAsync(request);

                if (result is null)
                    return NotFound(result);

                if (!result.IsValid)
                    return BadRequest(result);

                return Accepted(Url.Link("GetUserById", new { user_id = result.Data?.UserId }), result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Roles = "administrator, basic, contributor")]
        [HttpPut(Name = "PutVisit")]
        public async Task<IActionResult> PutVisitAsync([FromBody] UpdateVisitRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _visitService.UpdateAsync(request);

                if (result is null)
                    return NotFound(result);

                if (!result.IsValid)
                    return BadRequest(result);

                return Accepted(Url.Link("GetUserById", new { user_id = result.Data?.UserId }), result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
