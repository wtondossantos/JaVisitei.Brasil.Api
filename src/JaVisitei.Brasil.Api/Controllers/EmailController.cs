using Asp.Versioning;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.ViewModels.Request.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [ControllerName("Contact")]
    [Route("api/v{version:apiVersion}/contact")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [AllowAnonymous]
        [HttpPost(Name = "PostContact")]
        public async Task<IActionResult> PostContactAsync([FromBody] SendEmailContactRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _emailService.SendEmailContactAsync(request);

                if (result is null)
                    return NotFound(result);

                if (!result.IsValid)
                    return BadRequest(result);

                return Accepted(Url.Link("PostContact", new { id = result.Data?.Id }), result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
