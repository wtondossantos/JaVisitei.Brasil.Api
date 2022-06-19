using JaVisitei.Brasil.Business.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JaVisitei.Brasil.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [ControllerName("User")]
    [Route("api/v{version:apiVersion}/users")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
    }
}
