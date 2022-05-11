using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.ViewModels.Request.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("user", Name = "PostUserAdmin")]
        public async Task<IActionResult> PostUserAsync([FromBody] AddFullUserRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.AddAsync(request);

                if (result.IsValid)
                    return Ok(result);

                return BadRequest(result);
            }
            return BadRequest();
        }
    }
}
