using JaVisitei.Brasil.Api.Controllers;
using JaVisitei.Brasil.Business.Service.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace JaVisitei.Brasil.Test.Controllers
{
    [TestClass]
    public class EmailControllerTest
    {
        private readonly EmailController _emailController;
        private readonly Mock<IEmailService> _mockEmailService;

        public EmailControllerTest()
        {
            _mockEmailService = new Mock<IEmailService>();
            _emailController = new EmailController(_mockEmailService.Object);
        }
    }
}
