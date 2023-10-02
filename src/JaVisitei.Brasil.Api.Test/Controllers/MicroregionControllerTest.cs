using JaVisitei.Brasil.Business.ViewModels.Response.Microregion;
using JaVisitei.Brasil.Business.ViewModels.Response.Municipality;
using JaVisitei.Brasil.Business.Service.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JaVisitei.Brasil.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using JaVisitei.Brasil.Test.Mocks;
using System.Net;
using Moq;
using System;

namespace JaVisitei.Brasil.Test.Controllers
{
    [TestClass]
    public class MicroregionControllerTest
    {
        private readonly MicroregionController _microregionController;
        private readonly Mock<IMicroregionService> _mockMicroregionService;
        private readonly Mock<IMunicipalityService> _mockMunicipalityService;

        public MicroregionControllerTest()
        {
            _mockMicroregionService = new Mock<IMicroregionService>();
            _mockMunicipalityService = new Mock<IMunicipalityService>();
            _microregionController = new MicroregionController(_mockMicroregionService.Object,
                _mockMunicipalityService.Object);
        }

        #region Microregions

        [TestMethod("Microregions Correct return")]
        public async Task GetMicroregionsAsync_ShouldCorrectReturn_Microregions()
        {
            var microregions = MicroregionMock.ReturnMicroregionListMock();

            _ = _mockMicroregionService
                .Setup(x => x.GetAsync<MicroregionResponse>(null, null))
                .ReturnsAsync(microregions);

            var result = await _microregionController.GetMicroregionsAsync() as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(microregions, result.Value);
        }

        [TestMethod("Microregions No content")]
        public async Task GetMicroregionsAsync_ShouldNoContent_Microregions()
        {
            _ = _mockMicroregionService
                .Setup(x => x.GetAsync<MicroregionResponse>(null, null))
                .ReturnsAsync(new List<MicroregionResponse>());

            var result = await _microregionController.GetMicroregionsAsync() as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Microregions Return exception")]
        public async Task GetMicroregionsAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";

            _ = _mockMicroregionService
                .Setup(x => x.GetAsync<MicroregionResponse>(null, null))
                .Throws(new Exception(message));

            var result = await _microregionController.GetMicroregionsAsync() as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Microregion by id

        [TestMethod("Microregion by id Correct return")]
        public async Task GetMicroregionAsync_ShouldCorrectReturn_Microregion()
        {
            var microregion = MicroregionMock.ReturnMicroregion2Mock();
            var microregionId = "al_litoral_norte_alagoano_micro";

            _ = _mockMicroregionService
                .Setup(x => x.GetByIdAsync<MicroregionResponse>(microregionId))
                .ReturnsAsync(microregion);

            var result = await _microregionController.GetMicroregionAsync(microregionId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(((MicroregionResponse)result.Value).Id, microregion.Id);
            Assert.AreEqual(((MicroregionResponse)result.Value).Id, microregionId);
        }

        [TestMethod("Microregion by id No content")]
        public async Task GetMicroregionAsync_ShouldNoContent_Microregion()
        {
            _ = _mockMicroregionService
                .Setup(x => x.GetByIdAsync<MicroregionResponse>(It.IsAny<string>()))
                .ReturnsAsync((MicroregionResponse)null);

            var result = await _microregionController.GetMicroregionAsync(It.IsAny<string>()) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Microregion Return exception")]
        public async Task GetMicroregionAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";
            var microregionId = "exception";

            _ = _mockMicroregionService
                .Setup(x => x.GetByIdAsync<MicroregionResponse>(It.IsAny<string>()))
                .Throws(new Exception(message));

            var result = await _microregionController.GetMicroregionAsync(microregionId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Municipalities by microregion id

        [TestMethod("Municipalities by microregion id Correct return")]
        public async Task GetMunicipalitiesByMicroregionAsync_ShouldCorrectResturn_MunicipalitiesByMicroregionId()
        {
            var municipalities = MunicipalityMock.ReturnMunicipalityListMock();
            var microregionId = "ce_fortaleza_micro";

            _ = _mockMunicipalityService
                .Setup(x => x.GetAsync<MunicipalityResponse>(x => x.MicroregionId.Equals(microregionId), null))
                .ReturnsAsync(municipalities);

            var result = await _microregionController.GetMunicipalitiesByMicroregionAsync(microregionId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(municipalities, result.Value);
        }

        [TestMethod("Municipalities by microregion id No content")]
        public async Task GetMunicipalitiesByMicroregionAsync_ShouldNoContent_MunicipalitiesByMicroregionId()
        {
            var microregionId = "not_exists";

            _ = _mockMunicipalityService
                .Setup(x => x.GetAsync<MunicipalityResponse>(x => x.MicroregionId.Equals(microregionId), null))
                .ReturnsAsync(new List<MunicipalityResponse>());

            var result = await _microregionController.GetMunicipalitiesByMicroregionAsync(microregionId) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Municipalities by microregion id Return exception")]
        public async Task GetMunicipalitiesByMicroregionAsync_ShouldProbrem_Exception()
        {
            var microregionId = "exception";
            var message = "Exception test";

            _ = _mockMunicipalityService
                .Setup(x => x.GetAsync<MunicipalityResponse>(x => x.MicroregionId.Equals(microregionId), null))
                .Throws(new Exception(message));

            var result = await _microregionController.GetMunicipalitiesByMicroregionAsync(microregionId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion
    }
}
