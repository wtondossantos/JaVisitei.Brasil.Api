using JaVisitei.Brasil.Business.ViewModels.Request.User;
using JaVisitei.Brasil.Business.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Asp.Versioning;

namespace JaVisitei.Brasil.Api.Controllers
{
    [Authorize(Roles = "administrator")]
    [ApiController]
    [ApiVersion("1")]
    [ControllerName("Administration")]
    [Route("api/v{version:apiVersion}/administration")]
    public class AdministratorController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdministratorController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "administrator")]
        [HttpPost("user", Name = "PostUserAdmin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostUserAsync([FromBody] InsertFullUserRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _userService.InsertAsync(request);

                if (result is null)
                    return NotFound(result);

                if (!result.IsValid)
                    return BadRequest(result);

                return Accepted(Url.Link("GetUserById", new { id = result.Data?.Id }), result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Roles = "administrator")]
        [HttpPut("user", Name = "PutUserAdmin")]
        public async Task<IActionResult> PutUserAsync([FromBody] UpdateFullUserRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _userService.UpdateAsync(request);

                if (result is null)
                    return NotFound(result);

                if (!result.IsValid)
                    return BadRequest(result);

                return Accepted(Url.Link("GetUserById", new { id = result.Data?.Id }), result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
