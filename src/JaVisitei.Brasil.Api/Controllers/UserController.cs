using JaVisitei.Brasil.Business.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using JaVisitei.Brasil.Data.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using JaVisitei.Brasil.Business.ViewModels.Request.User;

namespace JaVisitei.Brasil.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [ControllerName("User")]
    [Route("api/v{version:apiVersion}/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost(Name = "PostUser")]
        public async Task<IActionResult> PostUserAsync([FromBody] AddUserRequest request)
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

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<User>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(Name = "GetUsers")]
        public async Task<IActionResult> GetUsersAsync()
        {
            var result = await _userService.GetAllAsync();

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{username}", Name = "GetUserByUsername")]
        public async Task<IActionResult> GetUserByUsernameAsync(string username)
        {
            var result = await _userService.GetAsync(x => x.Username == username);

            if (result == null)
                return NotFound();

            return Ok(result.FirstOrDefault());
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("{user_id}", Name = "PostUserId")]
        public async Task<IActionResult> PostUserIdAsync([FromRoute] int user_id, [FromBody] EditUserRequest request)
        {
            if (ModelState.IsValid)
            {
                request.Id = user_id;
                var result = await _userService.EditAsync(request);

                if (result.IsValid)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest();
        }
    }
}
