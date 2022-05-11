using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.ViewModels.Request.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [ControllerName("Profile")]
    [Route("api/v{version:apiVersion}/profiles")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index() => Ok("Hello");

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("active_account/{activation_code}", Name = "GetActiveAccount")]
        public async Task<IActionResult> GetActiveAccount([FromRoute] string activation_code)
        {
            if (ModelState.IsValid)
            {
                var result = await _profileService.ActiveAccountAsync(activation_code);

                if (result.IsValid)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("forgot_password/{email}", Name = "GetForgotPassword")]
        public async Task<IActionResult> GetForgotPassword([FromRoute] string email)
        {
            if (ModelState.IsValid)
            {
                var result = await _profileService.ForgotPasswordAsync(email);

                if (result.IsValid)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("reset_password", Name = "PostResetPassword")]
        public async Task<IActionResult> PostResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _profileService.ResetPasswordAsync(request);

                if (result.IsValid)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("login", Name = "PostLogin")]
        public async Task<IActionResult> PostLoginAsync([FromBody] LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _profileService.LoginAsync(request);

                if (result.IsValid)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest();
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("logout", Name = "PostLogout")]
        public async Task<IActionResult> PostLogoutAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _profileService.LoginAsync(null);

                if (result.IsValid)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest();
        }
    }
}
