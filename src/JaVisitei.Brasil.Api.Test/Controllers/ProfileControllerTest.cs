using JaVisitei.Brasil.Api.Controllers;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.ViewModels.Request.Profile;
using JaVisitei.Brasil.Business.ViewModels.Response.Profile;
using JaVisitei.Brasil.Test.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Net;
using Moq;
using System;

namespace JaVisitei.Brasil.Test.Controllers
{
    [TestClass]
    public class ProfileControllerTest
    {
        private readonly ProfileController _profileController;
        private readonly Mock<IProfileService> _mockProfileService;
        private readonly Mock<IUrlHelper> _mockUrlHelper;

        public ProfileControllerTest()
        {
            _mockProfileService = new Mock<IProfileService>();
            _mockUrlHelper = new Mock<IUrlHelper>();
            _profileController = new ProfileController(_mockProfileService.Object);
        }

        #region Hello world

        [TestMethod("Hello world Correct return")]
        public void Index_ShouldCorrectReturn_Hello()
        {
            var message = "Hello World";

            var result = _profileController.Index() as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(message, result.Value);
        }

        #endregion

        #region Activation account

        [TestMethod("Activation account Correct return")]
        public async Task PostActiveAccountAsync_ShouldCorrectReturn_ActiveAccount()
        {
            var request = ProfileMock.ActiveAccountRequestMock();
            var response = ProfileMock.ProfileActivationResponseMock();

            _ = _mockProfileService
                .Setup(x => x.ActiveAccountAsync(request))
                .ReturnsAsync(response);
            _ = _mockUrlHelper
                .Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>()))
                .Returns("http://location/");

            _profileController.Url = _mockUrlHelper.Object;
            var result = await _profileController.PostActiveAccountAsync(request) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.Accepted, result.StatusCode);
            Assert.IsNotNull(((ProfileValidator<ActivationResponse>)result.Value).Data);
            Assert.IsTrue(((ProfileValidator<ActivationResponse>)result.Value).IsValid);
            Assert.AreEqual(response, result.Value);
            Assert.AreEqual(response.Data.Actived, ((ProfileValidator<ActivationResponse>)result.Value).Data.Actived);
        }

        [TestMethod("Activation account Not found")]
        public async Task PostActiveAccountAsync_ShouldNotFind_Nullable()
        {
            _ = _mockProfileService
                .Setup(x => x.ActiveAccountAsync(It.IsAny<ActiveAccountRequest>()))
                .ReturnsAsync((ProfileValidator<ActivationResponse>)null);

            var result = await _profileController.PostActiveAccountAsync(It.IsAny<ActiveAccountRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNull((ProfileValidator<ActivationResponse>)result.Value);
            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod("Activation account Errors return")]
        public async Task PostActiveAccountAsync_ShouldReturnInvalid_Validation()
        {
            var response = ProfileMock.ProfileValidatorErrorMock<ActivationResponse>();

            _ = _mockProfileService
                .Setup(x => x.ActiveAccountAsync(It.IsAny<ActiveAccountRequest>()))
                .ReturnsAsync(response);

            var result = await _profileController.PostActiveAccountAsync(It.IsAny<ActiveAccountRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.IsFalse(((ProfileValidator<ActivationResponse>)result.Value).IsValid);
            Assert.IsTrue(((ProfileValidator<ActivationResponse>)result.Value).Errors.Count > 0);
        }

        [TestMethod("Activation account Return exception")]
        public async Task PostActiveAccountAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";

            _ = _mockProfileService
                .Setup(x => x.ActiveAccountAsync(It.IsAny<ActiveAccountRequest>()))
                .Throws(new Exception(message));

            var result = await _profileController.PostActiveAccountAsync(It.IsAny<ActiveAccountRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Generate confirmation code

        [TestMethod("Generate confirmation code Correct return")]
        public async Task PostGenerateConfirmationCodeAsync_ShouldCorrectReturn_GenerateConfirmationCode()
        {
            var request = ProfileMock.GenerateConfirmationCodeRequestMock();
            var response = ProfileMock.ProfileGenerateConfirmationCodeResponseMock();

            _ = _mockProfileService
                .Setup(x => x.GenerateConfirmationCodeAsync(request))
                .ReturnsAsync(response);
            _ = _mockUrlHelper
                .Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>()))
                .Returns("http://location/");

            _profileController.Url = _mockUrlHelper.Object;
            var result = await _profileController.PostGenerateConfirmationCodeAsync(request) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.Accepted, result.StatusCode);
            Assert.IsNotNull(((ProfileValidator<GenerateConfirmationCodeResponse>)result.Value).Data);
            Assert.IsTrue(((ProfileValidator<GenerateConfirmationCodeResponse>)result.Value).IsValid);
            Assert.AreEqual(response, result.Value);
            Assert.AreEqual(response.Data.Generated, ((ProfileValidator<GenerateConfirmationCodeResponse>)result.Value).Data.Generated);
        }

        [TestMethod("Generate confirmation code Not found")]
        public async Task PostGenerateConfirmationCodeAsync_ShouldNotFind_Nullable()
        {
            _ = _mockProfileService
                .Setup(x => x.GenerateConfirmationCodeAsync(It.IsAny<GenerateConfirmationCodeRequest>()))
                .ReturnsAsync((ProfileValidator<GenerateConfirmationCodeResponse>)null);

            var result = await _profileController.PostGenerateConfirmationCodeAsync(It.IsAny<GenerateConfirmationCodeRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNull((ProfileValidator<GenerateConfirmationCodeResponse>)result.Value);
            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod("Generate confirmation code Errors return")]
        public async Task PostGenerateConfirmationCodeAsync_ShouldReturnInvalid_Validation()
        {
            var response = ProfileMock.ProfileValidatorErrorMock<GenerateConfirmationCodeResponse>();

            _ = _mockProfileService
                .Setup(x => x.GenerateConfirmationCodeAsync(It.IsAny<GenerateConfirmationCodeRequest>()))
                .ReturnsAsync(response);

            var result = await _profileController.PostGenerateConfirmationCodeAsync(It.IsAny<GenerateConfirmationCodeRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.IsFalse(((ProfileValidator<GenerateConfirmationCodeResponse>)result.Value).IsValid);
            Assert.IsTrue(((ProfileValidator<GenerateConfirmationCodeResponse>)result.Value).Errors.Count > 0);
        }

        [TestMethod("Generate confirmation code Return exception")]
        public async Task PostGenerateConfirmationCodeAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";

            _ = _mockProfileService
                .Setup(x => x.GenerateConfirmationCodeAsync(It.IsAny<GenerateConfirmationCodeRequest>()))
                .Throws(new Exception(message));

            var result = await _profileController.PostGenerateConfirmationCodeAsync(It.IsAny<GenerateConfirmationCodeRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Forgot password

        [TestMethod("Forgot password Correct return")]
        public async Task PostForgotPasswordAsync_ShouldCorrectReturn_ForgotPassword()
        {
            var request = ProfileMock.ForgotPasswordRequestMock();
            var response = ProfileMock.ProfileForgotPasswordResponseMock();
            
            _ = _mockProfileService
                .Setup(x => x.ForgotPasswordAsync(request))
                .ReturnsAsync(response);
            _ = _mockUrlHelper
                .Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>()))
                .Returns("http://location/");

            _profileController.Url = _mockUrlHelper.Object;

            var result = await _profileController.PostForgotPasswordAsync(request) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.Accepted, result.StatusCode);
            Assert.IsNotNull(((ProfileValidator<ForgotPasswordResponse>)result.Value).Data);
            Assert.IsTrue(((ProfileValidator<ForgotPasswordResponse>)result.Value).IsValid);
            Assert.AreEqual(response, result.Value);
            Assert.AreEqual(response.Data.Requested, ((ProfileValidator<ForgotPasswordResponse>)result.Value).Data.Requested);
        }

        [TestMethod("Forgot password Not found")]
        public async Task PostForgotPasswordAsync_ShouldNotFind_Nullable()
        {
            _ = _mockProfileService
                .Setup(x => x.ForgotPasswordAsync(It.IsAny<ForgotPasswordRequest>()))
                .ReturnsAsync((ProfileValidator<ForgotPasswordResponse>)null);

            var result = await _profileController.PostForgotPasswordAsync(It.IsAny<ForgotPasswordRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNull((ProfileValidator<ForgotPasswordResponse>)result.Value);
            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod("Forgot password Errors return")]
        public async Task PostForgotPasswordAsync_ShouldReturnInvalid_Validation()
        {
            var response = ProfileMock.ProfileValidatorErrorMock<ForgotPasswordResponse>();

            _ = _mockProfileService
                .Setup(x => x.ForgotPasswordAsync(It.IsAny<ForgotPasswordRequest>()))
                .ReturnsAsync(response);

            var result = await _profileController.PostForgotPasswordAsync(It.IsAny<ForgotPasswordRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.IsFalse(((ProfileValidator<ForgotPasswordResponse>)result.Value).IsValid);
            Assert.IsTrue(((ProfileValidator<ForgotPasswordResponse>)result.Value).Errors.Count > 0);
        }

        [TestMethod("Forgot password Return exception")]
        public async Task PostForgotPasswordAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";

            _ = _mockProfileService
                .Setup(x => x.ForgotPasswordAsync(It.IsAny<ForgotPasswordRequest>()))
                .Throws(new Exception(message));

            var result = await _profileController.PostForgotPasswordAsync(It.IsAny<ForgotPasswordRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Reset password

        [TestMethod("Reset password Correct return")]
        public async Task PostResetPasswordAsync_ShouldResturnSucess_AlterUser()
        {
            var request = ProfileMock.ResetPasswordRequestMock();
            var response = ProfileMock.ProfileResetPasswordResponseMock();

            _ = _mockProfileService
                .Setup(x => x.ResetPasswordAsync(request))
                .ReturnsAsync(response);
            _ = _mockUrlHelper
                .Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>()))
                .Returns("http://location/");

            _profileController.Url = _mockUrlHelper.Object;
            var result = await _profileController.PostResetPasswordAsync(request) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.Accepted, result.StatusCode);
            Assert.IsNotNull(((ProfileValidator<ResetPasswordResponse>)result.Value).Data);
            Assert.IsTrue(((ProfileValidator<ResetPasswordResponse>)result.Value).IsValid);
            Assert.AreEqual(response, result.Value);
            Assert.AreEqual(response.Data.UserEmail, ((ProfileValidator<ResetPasswordResponse>)result.Value).Data.UserEmail);
        }

        [TestMethod("Reset password Not found")]
        public async Task PostResetPasswordAsync_ShouldNotFind_Nullable()
        {
            _ = _mockProfileService
                .Setup(x => x.ResetPasswordAsync(It.IsAny<ResetPasswordRequest>()))
                .ReturnsAsync((ProfileValidator<ResetPasswordResponse>)null);

            var result = await _profileController.PostResetPasswordAsync(It.IsAny<ResetPasswordRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNull((ProfileValidator<ResetPasswordResponse>)result.Value);
            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod("Reset password Invalid return model state")]
        public async Task PostResetPasswordAsync_ShouldReturnInvalid_ModelState()
        {
            _profileController.ModelState.AddModelError("test", "test");
            var result = await _profileController.PostResetPasswordAsync(It.IsAny<ResetPasswordRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod("Reset password Errors return")]
        public async Task PostResetPasswordAsync_ShouldReturnInvalid_Validation()
        {
            var response = ProfileMock.ProfileValidatorErrorMock<ResetPasswordResponse>();

            _ = _mockProfileService
                .Setup(x => x.ResetPasswordAsync(It.IsAny<ResetPasswordRequest>()))
                .ReturnsAsync(response);

            var result = await _profileController.PostResetPasswordAsync(It.IsAny<ResetPasswordRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.IsFalse(((ProfileValidator<ResetPasswordResponse>)result.Value).IsValid);
            Assert.IsTrue(((ProfileValidator<ResetPasswordResponse>)result.Value).Errors.Count > 0);
        }

        [TestMethod("Reset password Return exception")]
        public async Task PostResetPasswordAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";

            _ = _mockProfileService
                .Setup(x => x.ResetPasswordAsync(It.IsAny<ResetPasswordRequest>()))
                .Throws(new Exception(message));

            var result = await _profileController.PostResetPasswordAsync(It.IsAny<ResetPasswordRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Login

        [TestMethod("Login Correct return")]
        public async Task PostLoginAsync_ShouldResturnSucess_AlterUser()
        {
            var request = ProfileMock.LoginRequestMock();
            var response = ProfileMock.ProfileLoginResponseMock();

            _ = _mockProfileService
                .Setup(x => x.LoginAsync(request))
                .ReturnsAsync(response);
            _ = _mockUrlHelper
                .Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>()))
                .Returns("http://location/");

            _profileController.Url = _mockUrlHelper.Object;
            var result = await _profileController.PostLoginAsync(request) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.Accepted, result.StatusCode);
            Assert.IsNotNull(((ProfileValidator<LoginResponse>)result.Value).Data);
            Assert.IsTrue(((ProfileValidator<LoginResponse>)result.Value).IsValid);
            Assert.AreEqual(response, result.Value);
            Assert.AreEqual(response.Data.Token, ((ProfileValidator<LoginResponse>)result.Value).Data.Token);
        }

        [TestMethod("Login Not found")]
        public async Task PostLoginAsync_ShouldNotFind_Nullable()
        {
            _ = _mockProfileService
                .Setup(x => x.LoginAsync(It.IsAny<LoginRequest>()))
                .ReturnsAsync((ProfileValidator<LoginResponse>)null);

            var result = await _profileController.PostLoginAsync(It.IsAny<LoginRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNull((ProfileValidator<LoginResponse>)result.Value);
            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod("Login Invalid return model state")]
        public async Task PostLoginAsync_ShouldReturnInvalid_ModelState()
        {
            _profileController.ModelState.AddModelError("test", "test");
            var result = await _profileController.PostLoginAsync(It.IsAny<LoginRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod("Login Errors return")]
        public async Task PostLoginAsync_ShouldReturnInvalid_Validation()
        {
            var response = ProfileMock.ProfileValidatorErrorMock<LoginResponse>();

            _ = _mockProfileService
                .Setup(x => x.LoginAsync(It.IsAny<LoginRequest>()))
                .ReturnsAsync(response);

            var result = await _profileController.PostLoginAsync(It.IsAny<LoginRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.IsFalse(((ProfileValidator<LoginResponse>)result.Value).IsValid);
            Assert.IsTrue(((ProfileValidator<LoginResponse>)result.Value).Errors.Count > 0);
        }

        [TestMethod("Login Return exception")]
        public async Task PostLoginAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";

            _ = _mockProfileService
                .Setup(x => x.LoginAsync(It.IsAny<LoginRequest>()))
                .Throws(new Exception(message));

            var result = await _profileController.PostLoginAsync(It.IsAny<LoginRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion
    }
}
