using JaVisitei.Brasil.Business.ViewModels.Request.User;
using JaVisitei.Brasil.Business.Service.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Api.Controllers;
using JaVisitei.Brasil.Test.Mocks;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Moq;
using System.Net;
using System;

namespace JaVisitei.Brasil.Test.Controllers
{
    [TestClass]
    public class AdministratorControllerTest
    {
        private readonly AdministratorController _administratorController;
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IUrlHelper> _mockUrlHelper;

        public AdministratorControllerTest()
        {
            _mockUserService = new Mock<IUserService>();
            _mockUrlHelper = new Mock<IUrlHelper>();
            _administratorController = new AdministratorController(_mockUserService.Object);
        }

        #region Admin user creation

        [TestMethod("Admin user creation Correct return")]
        public async Task PostUserAsync_ShouldReturnSuccess_CreateAdminUser()
        {
            var request = UserMock.CreateUserAdminRequestMock();
            var response = UserMock.CreatedUserAdminResponseMock();

            _ = _mockUserService
                .Setup(x => x.InsertAsync(request))
                .ReturnsAsync(response);
            _ = _mockUrlHelper
                .Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>()))
                .Returns("http://location/");

            _administratorController.Url = _mockUrlHelper.Object;
            var result = await _administratorController.PostUserAsync(request) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.Accepted, result.StatusCode);
            Assert.IsNotNull(((UserValidator)result.Value).Data);
            Assert.IsTrue(((UserValidator)result.Value).IsValid);
            Assert.AreEqual(response, result.Value);
            Assert.AreEqual(response.Data.UserRoleId, ((UserValidator)result.Value).Data.UserRoleId);
        }

        [TestMethod("Admin user creation Invalid return Model State")]
        public async Task PostUserAsync_ShouldReturnInvalid_ModelState()
        {
            _administratorController.ModelState.AddModelError("test", "test");
            var result = await _administratorController.PostUserAsync(It.IsAny<InsertFullUserRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod("Admin user creation Not found")]
        public async Task PostUserAsync_ShouldNotFind_Nullable()
        {
            _ = _mockUserService
                .Setup(x => x.InsertAsync(It.IsAny<InsertFullUserRequest>()))
                .ReturnsAsync((UserValidator)null);

            var result = await _administratorController.PostUserAsync(It.IsAny<InsertFullUserRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNull((UserValidator)result.Value);
            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod("Admin user creation Errors return")]
        public async Task PostUserAsync_ShouldReturnInvalid_Validation()
        {
            var response = UserMock.UserValidatorErrorMock();

            _ = _mockUserService
                .Setup(x => x.InsertAsync(It.IsAny<InsertFullUserRequest>()))
                .ReturnsAsync(response);

            var result = await _administratorController.PostUserAsync(It.IsAny<InsertFullUserRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.IsFalse(((UserValidator)result.Value).IsValid);
            Assert.IsTrue(((UserValidator)result.Value).Errors.Count > 0);
        }

        [TestMethod("Admin user creation Return exception")]
        public async Task PostUserAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";

            _ = _mockUserService
                .Setup(x => x.InsertAsync(It.IsAny<InsertFullUserRequest>()))
                .Throws(new Exception(message));

            var result = await _administratorController.PostUserAsync(It.IsAny<InsertFullUserRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Admin user alteration

        [TestMethod("Admin user alteration Correct return")]
        public async Task PutUserAsync_ShouldResturnSucess_AlterUser()
        {
            var request = UserMock.UpdateFullUserRequestMock();
            var response = UserMock.CreatedUserContributorResponseMock();

            _ = _mockUserService
                .Setup(x => x.UpdateAsync(request))
                .ReturnsAsync(response);
            _ = _mockUrlHelper
                .Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>()))
                .Returns("http://location/");

            _administratorController.Url = _mockUrlHelper.Object;
            var result = await _administratorController.PutUserAsync(request) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.Accepted, result.StatusCode);
            Assert.IsNotNull(((UserValidator)result.Value).Data);
            Assert.IsTrue(((UserValidator)result.Value).IsValid);
            Assert.AreEqual(response, result.Value);
            Assert.AreEqual(response.Data.UserRoleId, ((UserValidator)result.Value).Data.UserRoleId);
        }

        [TestMethod("Admin user alteration Invalid return Model State")]
        public async Task PutUserAsync_ShouldReturnInvalid_ModelState()
        {
            _administratorController.ModelState.AddModelError("test", "test");
            var result = await _administratorController.PutUserAsync(It.IsAny<UpdateFullUserRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod("Admin user alteration Not found")]
        public async Task PutUserAsync_ShouldNotFind_Nullable()
        {
            _ = _mockUserService
                .Setup(x => x.UpdateAsync(It.IsAny<UpdateFullUserRequest>()))
                .ReturnsAsync((UserValidator)null);

            var result = await _administratorController.PutUserAsync(It.IsAny<UpdateFullUserRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNull((UserValidator)result.Value);
            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod("Admin user alteration Errors return")]
        public async Task PutUserAsync_ShouldReturnInvalid_Validation()
        {
            var response = UserMock.UserValidatorErrorMock();

            _ = _mockUserService
                .Setup(x => x.UpdateAsync(It.IsAny<UpdateFullUserRequest>()))
                .ReturnsAsync(response);

            var result = await _administratorController.PutUserAsync(It.IsAny<UpdateFullUserRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.IsFalse(((UserValidator)result.Value).IsValid);
            Assert.IsTrue(((UserValidator)result.Value).Errors.Count > 0);
        }

        [TestMethod("Admin user alteration Return exception")]
        public async Task PutUserAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";

            _ = _mockUserService
                .Setup(x => x.UpdateAsync(It.IsAny<UpdateFullUserRequest>()))
                .Throws(new Exception(message));

            var result = await _administratorController.PutUserAsync(It.IsAny<UpdateFullUserRequest>()) as ObjectResult;
            
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion
    }
}
