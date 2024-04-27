using Asp.Versioning;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.ViewModels.Request.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [HttpGet(Name = "Hello World")]
        public IActionResult Index() => Ok("Hello World");

        [AllowAnonymous]
        [HttpPost("active_account/{activationCode}", Name = "PostActiveAccount")]
        public async Task<IActionResult> PostActiveAccountAsync([FromRoute] ActiveAccountRequest request)
        {
            try
            {
                var result = await _profileService.ActiveAccountAsync(request);

                if (result is null)
                    return NotFound(result);

                if (!result.IsValid || result.Data is null)
                    return BadRequest(result);

                return Accepted(Url.Link("PostActiveAccount", new LoginRequest { Input = result.Data?.UserEmail, Password = "" }), result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("generate_confirmation_code/{email}", Name = "PostGenerateConfirmationCode")]
        public async Task<IActionResult> PostGenerateConfirmationCodeAsync([FromRoute] GenerateConfirmationCodeRequest request)
        {
            try
            {
                var result = await _profileService.GenerateConfirmationCodeAsync(request);

                if (result is null)
                    return NotFound(result);

                if (!result.IsValid || result.Data is null)
                    return BadRequest(result);

                return Accepted(Url.Link("PostActiveAccount", new { activation_code = "" }), result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("forgot_password/{email}", Name = "PostForgotPassword")]
        public async Task<IActionResult> PostForgotPasswordAsync([FromRoute] ForgotPasswordRequest request)
        {
            try
            {
                var result = await _profileService.ForgotPasswordAsync(request);

                if (result is null)
                    return NotFound(result);

                if (!result.IsValid || result.Data is null)
                    return BadRequest(result);

                return Accepted(Url.Link("PostResetPassword", new ResetPasswordRequest { ResetPasswordCode = "", Email = result.Data?.UserEmail }), result); ;
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("reset_password", Name = "PostResetPassword")]
        public async Task<IActionResult> PostResetPasswordAsync([FromBody] ResetPasswordRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _profileService.ResetPasswordAsync(request);

                if (result is null)
                    return NotFound(result);

                if (!result.IsValid || result.Data is null)
                    return BadRequest(result);

                return Accepted(Url.Link("PostResetPassword", new LoginRequest { Input = result.Data?.UserEmail, Password = "" }), result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("login", Name = "PostLogin")]
        public async Task<IActionResult> PostLoginAsync([FromBody] LoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _profileService.LoginAsync(request);

                if (result is null)
                    return NotFound(result);

                if (!result.IsValid || result.Data is null)
                    return BadRequest(result);

                return Accepted(Url.Link("PostLogin", new { id = result.Data.Id }), result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("refresh_token", Name = "RefreshToken")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _profileService.RefreshTokenAsync(request);

                if (result is null)
                    return NotFound(result);

                if (!result.IsValid || result.Data is null)
                    return BadRequest(result);

                return Accepted(Url.Link("RefreshToken", new { id = result.Data.Id }), result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
