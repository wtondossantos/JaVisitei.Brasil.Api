using JaVisitei.Brasil.Business.Service.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.ViewModels.Request.User;
using JaVisitei.Brasil.Business.ViewModels.Response.User;
using JaVisitei.Brasil.Business.ViewModels.Response.Visit;
using JaVisitei.Brasil.Api.Controllers;
using System.Collections.Generic;
using JaVisitei.Brasil.Test.Mocks;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Moq;
using System;

namespace JaVisitei.Brasil.Test.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        private readonly UserController _userController;
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IVisitService> _mockVisitService;
        private readonly Mock<IUrlHelper> _mockUrlHelper;

        public UserControllerTest()
        {
            _mockUserService = new Mock<IUserService>();
            _mockVisitService = new Mock<IVisitService>();
            _mockUrlHelper = new Mock<IUrlHelper>();
            _userController = new UserController(_mockUserService.Object, _mockVisitService.Object);
        }

        #region User creation

        [TestMethod("User creation Correct return")]
        public async Task PostUserAsync_ShouldResturnSucess_CreateBasicUser()
        {
            var request = UserMock.CreateUserBasicRequestMock();
            var response = UserMock.CreatedUserBasicResponseMock();

            _ = _mockUserService
                .Setup(x => x.InsertAsync(request))
                .ReturnsAsync(response);
            _ = _mockUrlHelper
                .Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>()))
                .Returns("http://location/");

            _userController.Url = _mockUrlHelper.Object;
            var result = await _userController.PostUserAsync(request) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.Accepted, result.StatusCode);
            Assert.IsNotNull(((UserValidator)result.Value).Data);
            Assert.IsTrue(((UserValidator)result.Value).IsValid);
            Assert.AreEqual(response, result.Value);
            Assert.AreEqual(response.Data.Id, ((UserValidator)result.Value).Data.Id);
        }

        [TestMethod("User creation Invalid return model state")]
        public async Task PostUserAsync_ShouldReturnInvalid_ModelState()
        {
            _userController.ModelState.AddModelError("test", "test");
            var result = await _userController.PostUserAsync(It.IsAny<InsertUserRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod("User creation Not found")]
        public async Task PostUserAsync_ShouldNotFind_Nullable()
        {
            _ = _mockUserService
                .Setup(x => x.InsertAsync(It.IsAny<InsertUserRequest>()))
                .ReturnsAsync((UserValidator)null);

            var result = await _userController.PostUserAsync(It.IsAny<InsertUserRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNull((UserValidator)result.Value);
            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod("User creation Errors return")]
        public async Task PostUserAsync_ShouldReturnInvalid_Validation()
        {
            var response = UserMock.UserValidatorErrorMock();

            _ = _mockUserService
                .Setup(x => x.InsertAsync(It.IsAny<InsertUserRequest>()))
                .ReturnsAsync(response);

            var result = await _userController.PostUserAsync(It.IsAny<InsertUserRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.IsFalse(((UserValidator)result.Value).IsValid);
            Assert.IsTrue(((UserValidator)result.Value).Errors.Count > 0);
        }

        [TestMethod("User creation Return exception")]
        public async Task PostUserAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";

            _ = _mockUserService
                .Setup(x => x.InsertAsync(It.IsAny<InsertUserRequest>()))
                .Throws(new Exception(message));

            var result = await _userController.PostUserAsync(It.IsAny<InsertUserRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region User alteration

        [TestMethod("User alteration Correct return")]
        public async Task PutUserAsync_ShouldResturnSucess_AlterUser()
        {
            var request = UserMock.UpdateUserRequestMock();
            var response = UserMock.CreatedUserContributorResponseMock();

            _ = _mockUserService
                .Setup(x => x.UpdateAsync(request))
                .ReturnsAsync(response);
            _ = _mockUrlHelper
                .Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>()))
                .Returns("http://location/");

            _userController.Url = _mockUrlHelper.Object;
            var result = await _userController.PutUserAsync(request) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.Accepted, result.StatusCode);
            Assert.IsNotNull(((UserValidator)result.Value).Data);
            Assert.IsTrue(((UserValidator)result.Value).IsValid);
            Assert.AreEqual(response, result.Value);
            Assert.AreEqual(response.Data.Id, ((UserValidator)result.Value).Data.Id);
        }

        [TestMethod("User alteration Invalid return model state")]
        public async Task PutUserAsync_ShouldReturnInvalid_ModelState()
        {
            _userController.ModelState.AddModelError("test", "test");
            var result = await _userController.PutUserAsync(It.IsAny<UpdateUserRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod("User alteration Not found")]
        public async Task PutUserAsync_ShouldNotFind_Nullable()
        {
            _ = _mockUserService
                .Setup(x => x.UpdateAsync(It.IsAny<UpdateUserRequest>()))
                .ReturnsAsync((UserValidator)null);

            var result = await _userController.PutUserAsync(It.IsAny<UpdateUserRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNull((UserValidator)result.Value);
            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod("User alteration Errors return")]
        public async Task PutUserAsync_ShouldReturnInvalid_Validation()
        {
            var response = UserMock.UserValidatorErrorMock();

            _ = _mockUserService
                .Setup(x => x.UpdateAsync(It.IsAny<UpdateUserRequest>()))
                .ReturnsAsync(response);

            var result = await _userController.PutUserAsync(It.IsAny<UpdateUserRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.IsFalse(((UserValidator)result.Value).IsValid);
            Assert.IsTrue(((UserValidator)result.Value).Errors.Count > 0);
        }

        [TestMethod("User alteration Return exception")]
        public async Task PutUserAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";

            _ = _mockUserService
                .Setup(x => x.UpdateAsync(It.IsAny<UpdateUserRequest>()))
                .Throws(new Exception(message));

            var result = await _userController.PutUserAsync(It.IsAny<UpdateUserRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Users Correct

        [TestMethod("Users Correct return")]
        public async Task GetUserAsync_ShouldResturnSucess_Users()
        {
            var response = UserMock.GetListUserTest();

            _ = _mockUserService
                .Setup(x => x.GetAsync<UserResponse>(null, null))
                .ReturnsAsync(response);

            var result = await _userController.GetUsersAsync() as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(response, result.Value);
        }

        [TestMethod("Users No content")]
        public async Task GetUserAsync_ShouldNoContent_Users()
        {
            _ = _mockUserService
                .Setup(x => x.GetAsync<UserResponse>(null, null))
                .ReturnsAsync(new List<UserResponse>());

            var result = await _userController.GetUsersAsync() as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Users Return exception")]
        public async Task GetUserAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";

            _ = _mockUserService
                .Setup(x => x.GetAsync<UserResponse>(null, null))
                .Throws(new Exception(message));

            var result = await _userController.GetUsersAsync() as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region User by username

        [TestMethod("User by username Correct return")]
        public async Task GetUserByUsernameAsync_ShouldResturnSucess_UserByUsername()
        {
            var username = "wellington";
            var response = UserMock.UserActivedBasicMock();

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync<UserResponse>(x => x.Username.Equals(username), null))
                .ReturnsAsync(response);

            var result = await _userController.GetUserByUsernameAsync(username) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(response, result.Value);
        }

        [TestMethod("User by username No content")]
        public async Task GetUserByUsernameAsync_ShouldNoContent_UserByUsername()
        {
            var username = "not_exists";

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync<UserResponse>(x => x.Username.Equals(username), null))
                .ReturnsAsync((UserResponse)null);

            var result = await _userController.GetUserByUsernameAsync(username) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("User by username Return exception")]
        public async Task GetUserByUsernameAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";
            var username = "exception";

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync<UserResponse>(x => x.Username.Equals(username), null))
                .Throws(new Exception(message));

            var result = await _userController.GetUserByUsernameAsync(username) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region User by id

        [TestMethod("User by id Correct return")]
        public async Task GetUserByIdAsync_ShouldResturnSucess_UserById()
        {
            var id = Guid.NewGuid().ToString();
            var response = UserMock.UserActivedBasicMock();

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync<UserResponse>(x => x.Id.Equals(id), null))
                .ReturnsAsync(response);

            var result = await _userController.GetUserByIdAsync(id) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(response, result.Value);
        }

        [TestMethod("User by id No content")]
        public async Task GetUserByIdAsync_ShouldNoContent_UserById()
        {
            var id = string.Empty;

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync<UserResponse>(x => x.Id.Equals(id), null))
                .ReturnsAsync((UserResponse)null);

            var result = await _userController.GetUserByIdAsync(id) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("User by id Return exception")]
        public async Task GetUserByIdAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";
            var id = string.Empty;

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync<UserResponse>(x => x.Id.Equals(id), null))
                .Throws(new Exception(message));

            var result = await _userController.GetUserByIdAsync(id) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Visits by user id

        [TestMethod("Visits by user id Correct return")]
        public async Task GetVisitsAsync_ShouldCorrectReturn_AllVisitByUserId()
        {
            var userId = Guid.NewGuid().ToString();
            var visits = VisitMock.ReturnVisitListMock();

            _ = _mockVisitService
                .Setup(x => x.GetByUserIdAsync<VisitResponse>(userId))
                .ReturnsAsync(visits);

            var result = await _userController.GetVisitsAsync(userId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(visits, result.Value);
        }

        [TestMethod("Visits by user id No content")]
        public async Task GetVisitsAsync_ShouldNoContent_VisitsByUserId()
        {
            var userId = Guid.NewGuid().ToString();

            _ = _mockVisitService
                .Setup(x => x.GetByUserIdAsync<VisitResponse>(userId))
                .ReturnsAsync(new List<VisitResponse>());

            var result = await _userController.GetVisitsAsync(userId) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Visits by user id Return exception")]
        public async Task GetVisitsAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";
            var userId = Guid.NewGuid().ToString();

            _ = _mockVisitService
                .Setup(x => x.GetByUserIdAsync<VisitResponse>(userId))
                .Throws(new Exception(message));

            var result = await _userController.GetVisitsAsync(userId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion
    }
}
