using JaVisitei.Brasil.Business.ViewModels.Request.Recaptcha;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using JaVisitei.Brasil.Business.Service.Interfaces;
using System;
using Asp.Versioning;

namespace JaVisitei.Brasil.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [ControllerName("Contact")]
    [Route("api/v{version:apiVersion}/recaptcha")]
    public class RecaptchaController : ControllerBase
    {
        private readonly IRecaptchaService _recaptchaService;

        public RecaptchaController(IRecaptchaService recaptchaService)
        {
            _recaptchaService = recaptchaService;
        }

        [AllowAnonymous]
        [HttpPost("validate", Name = "ValidateAsync")]
        public async Task<IActionResult> ValidateAsync([FromBody] RecaptchaRequest recaptchaRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _recaptchaService.RetrieveAsync(recaptchaRequest);
                
                if (result is null)
                    return NotFound(result);

                if (!result.IsValid)
                    return BadRequest(result);

                return Accepted(Url.Link("ValidateAsync", new { sucess = result?.Data.Success }), result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
