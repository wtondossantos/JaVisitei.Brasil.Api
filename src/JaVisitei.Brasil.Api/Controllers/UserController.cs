using JaVisitei.Brasil.Business.ViewModels.Request.User;
using JaVisitei.Brasil.Business.ViewModels.Response.User;
using JaVisitei.Brasil.Business.ViewModels.Response.Visit;
using JaVisitei.Brasil.Business.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System;
using Asp.Versioning;

namespace JaVisitei.Brasil.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [ControllerName("User")]
    [Route("api/v{version:apiVersion}/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IVisitService _visitService;

        public UserController(IUserService userService, IVisitService visitService)
        {
            _userService = userService;
            _visitService = visitService;
        }

        [AllowAnonymous]
        [HttpPost(Name = "PostUser")]
        public async Task<IActionResult> PostUserAsync([FromBody] InsertUserRequest request)
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

        [Authorize(Roles = "administrator, basic, contributor")]
        [HttpPut(Name = "PutUser")]
        public async Task<IActionResult> PutUserAsync([FromBody] UpdateUserRequest request)
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

        [Authorize(Roles = "administrator")]
        [HttpGet(Name = "GetUsers")]
        public async Task<IActionResult> GetUsersAsync()
        {
            try
            {
                var result = await _userService.GetAsync<UserResponse>();

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
        [HttpGet("username/{username}", Name = "GetUserByUsername")]
        public async Task<IActionResult> GetUserByUsernameAsync([FromRoute] string username)
        {
            try
            {
                var result = await _userService.GetFirstOrDefaultAsync<UserResponse>(x => x.Username.Equals(username));

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
        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] string id)
        {
            try
            {
                var result = await _userService.GetFirstOrDefaultAsync<UserResponse>(x => x.Id.Equals(id));

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
        [HttpGet("{id}/visits", Name = "GetVisits")]
        public async Task<IActionResult> GetVisitsAsync([FromRoute] string id)
        {
            try
            {
                var result = await _visitService.GetByUserIdAsync<VisitResponse>(id);

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
