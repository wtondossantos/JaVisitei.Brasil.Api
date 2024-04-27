using JaVisitei.Brasil.Business.ViewModels.Request.Visit;
using JaVisitei.Brasil.Business.ViewModels.Response.Visit;
using JaVisitei.Brasil.Business.Service.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Api.Controllers;
using JaVisitei.Brasil.Test.Mocks;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net;
using Moq;
using System;

namespace JaVisitei.Brasil.Test.Controllers
{
    [TestClass]
    public class VisitControllerTest
    {
        private readonly VisitController _visitController;
        private readonly Mock<IVisitService> _mockVisitService;
        private readonly Mock<IUrlHelper> _mockUrlHelper;

        public VisitControllerTest()
        {
            _mockVisitService = new Mock<IVisitService>();
            _mockUrlHelper = new Mock<IUrlHelper>();
            _visitController = new VisitController(_mockVisitService.Object);
        }

        #region Visit creation

        [TestMethod("Visit creation Correct return")]
        public async Task PostVisitAsync_ShouldReturnSuccess_CreateVisit()
        {
            var request = VisitMock.CreateVisitRequestMock();
            var response = VisitMock.CreatedVisitResponseMock();

            _ = _mockVisitService
                .Setup(x => x.InsertAsync(request))
                .ReturnsAsync(response);
            _ = _mockUrlHelper
                .Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>()))
                .Returns("http://location/");

            _visitController.Url = _mockUrlHelper.Object;
            var result = await _visitController.PostVisitAsync(request) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.Accepted, result.StatusCode);

            var visitValidation = (VisitValidator)result.Value;

            Assert.IsNotNull(visitValidation.Data);
            Assert.IsTrue(visitValidation.IsValid);
            Assert.AreEqual(response, result.Value);
            Assert.AreEqual(response.Data.RegionId, visitValidation.Data.RegionId);
            Assert.AreEqual(response.Data.UserId, visitValidation.Data.UserId);
            Assert.AreEqual(response.Data.RegionTypeId, visitValidation.Data.RegionTypeId);
            Assert.AreEqual(response.Data.VisitDate, visitValidation.Data.VisitDate);
            Assert.AreEqual(response.Data.Color, visitValidation.Data.Color);
            Assert.AreEqual(response.Data.RegistryDate, visitValidation.Data.RegistryDate);
        }

        [TestMethod("Visit creation Invalid return model state")]
        public async Task PostVisitAsync_ShouldReturnInvalid_ModelState()
        {
            _visitController.ModelState.AddModelError("test", "test");
            var result = await _visitController.PostVisitAsync(It.IsAny<InsertVisitRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod("Visit creation Not found")]
        public async Task PostVisitAsync_ShouldNotFind_Nullable()
        {
            _ = _mockVisitService
                .Setup(x => x.InsertAsync(It.IsAny<InsertVisitRequest>()))
                .ReturnsAsync((VisitValidator)null);

            var result = await _visitController.PostVisitAsync(It.IsAny<InsertVisitRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNull((VisitValidator)result.Value);
            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod("Visit creation Errors return")]
        public async Task PostVisitAsync_ShouldReturnInvalid_Validation()
        {
            var response = VisitMock.VisitValidatorErrorMock();

            _ = _mockVisitService
                .Setup(x => x.InsertAsync(It.IsAny<InsertVisitRequest>()))
                .ReturnsAsync(response);

            var result = await _visitController.PostVisitAsync(It.IsAny<InsertVisitRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.IsFalse(((VisitValidator)result.Value).IsValid);
            Assert.IsTrue(((VisitValidator)result.Value).Errors.Count > 0);
        }

        [TestMethod("Visit creation Return exception")]
        public async Task PostVisitAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";

            _ = _mockVisitService
                .Setup(x => x.InsertAsync(It.IsAny<InsertVisitRequest>()))
                .Throws(new Exception(message));

            var result = await _visitController.PostVisitAsync(It.IsAny<InsertVisitRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Visits by user, region type and region

        [TestMethod("Visits by user, region type and region Invalid return model state")]
        public async Task GetVisitAsync_ShouldReturnInvalid_ModelState()
        {
            _visitController.ModelState.AddModelError("test", "test");
            var result = await _visitController.GetVisitAsync(It.IsAny<VisitKeyRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod("Visits by user, region type and region Correct return")]
        public async Task GetVisitAsync_ShouldCorrectReturn_Visit()
        {
            var response = VisitMock.ReturnVisitMunicipalityMock();
            var request = VisitMock.VisitKeyRequestMock();

            _ = _mockVisitService
                .Setup(x => x.GetByIdAsync<VisitResponse>(request))
                .ReturnsAsync(response);

            var result = await _visitController.GetVisitAsync(request) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(((VisitResponse)result.Value).UserId, response.UserId);
            Assert.AreEqual(((VisitResponse)result.Value).RegionTypeId, response.RegionTypeId);
            Assert.AreEqual(((VisitResponse)result.Value).RegionId, response.RegionId);
        }

        [TestMethod("Visits by user, region type and region Not found")]
        public async Task GetVisitAsync_ShouldNoContent_Visit()
        {
            _ = _mockVisitService
                .Setup(x => x.GetByIdAsync<VisitResponse>(It.IsAny<VisitKeyRequest>()))
                .ReturnsAsync((VisitResponse)null);

            var result = await _visitController.GetVisitAsync(It.IsAny<VisitKeyRequest>()) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Visits by user, region type and region Problem exception")]
        public async Task GetVisitAsync_ShouldProgrem_Exception()
        {
            var message = "Exception test";
            _ = _mockVisitService
                .Setup(x => x.GetByIdAsync<VisitResponse>(It.IsAny<VisitKeyRequest>()))
                .Throws(new Exception(message));

            var result = await _visitController.GetVisitAsync(It.IsAny<VisitKeyRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Visit deletion

        [TestMethod("Visit deletion Correct return")]
        public async Task DeleteVisitAsync_ShouldResturnSucess_RemoveVisit()
        {
            var request = VisitMock.VisitKeyRequestMock();
            var response = VisitMock.DeletedVisitResponseMock();

            _ = _mockVisitService
                .Setup(x => x.DeleteAsync(request))
                .ReturnsAsync(response);
            _ = _mockUrlHelper
                .Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>()))
                .Returns("http://location/");

            _visitController.Url = _mockUrlHelper.Object;
            var result = await _visitController.DeleteVisitAsync(request) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.Accepted, result.StatusCode);
            Assert.IsNotNull(((VisitValidator)result.Value).Data);
            Assert.IsTrue(((VisitValidator)result.Value).IsValid);
            Assert.AreEqual(response, result.Value);
            Assert.AreEqual(response.Data.UserId, ((VisitValidator)result.Value).Data.UserId);
        }

        [TestMethod("Visit deletion Invalid return model state")]
        public async Task DeleteVisitAsync_ShouldReturnInvalid_ModelState()
        {
            _visitController.ModelState.AddModelError("test", "test");
            var result = await _visitController.DeleteVisitAsync(It.IsAny<VisitKeyRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod("Visit deletion Not found")]
        public async Task DeleteVisitAsync_ShouldNotFind_Nullable()
        {
            _ = _mockVisitService
                .Setup(x => x.DeleteAsync(It.IsAny<VisitKeyRequest>()))
                .ReturnsAsync((VisitValidator)null);

            var result = await _visitController.DeleteVisitAsync(It.IsAny<VisitKeyRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNull((VisitValidator)result.Value);
            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod("Visit deletion Errors return")]
        public async Task DeleteVisitAsync_ShouldReturnInvalid_Validation()
        {
            var response = VisitMock.VisitValidatorErrorMock();

            _ = _mockVisitService
                .Setup(x => x.DeleteAsync(It.IsAny<VisitKeyRequest>()))
                .ReturnsAsync(response);

            var result = await _visitController.DeleteVisitAsync(It.IsAny<VisitKeyRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.IsFalse(((VisitValidator)result.Value).IsValid);
            Assert.IsTrue(((VisitValidator)result.Value).Errors.Count > 0);
        }

        [TestMethod("Visit deletion Return exception")]
        public async Task DeleteVisitAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";

            _ = _mockVisitService
                .Setup(x => x.DeleteAsync(It.IsAny<VisitKeyRequest>()))
                .Throws(new Exception(message));

            var result = await _visitController.DeleteVisitAsync(It.IsAny<VisitKeyRequest>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion
    }
}
