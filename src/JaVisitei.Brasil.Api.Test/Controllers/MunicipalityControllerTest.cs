using JaVisitei.Brasil.Api.Controllers;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.ViewModels.Response.Municipality;
using JaVisitei.Brasil.Test.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Moq;
using System;

namespace JaVisitei.Brasil.Test.Controllers
{
    [TestClass]
    public class MunicipalityControllerTest
    {
        private readonly MunicipalityController _municipalityController;
        private readonly Mock<IMunicipalityService> _mockMunicipalityService;

        public MunicipalityControllerTest()
        {
            _mockMunicipalityService = new Mock<IMunicipalityService>();
            _municipalityController = new MunicipalityController(_mockMunicipalityService.Object);
        }

        #region Municipalities

        [TestMethod("Municipalities Correct return")]
        public async Task GetMunicipalitiesAsync_ShouldCorrectReturn_Municipalities()
        {
            var Municipalitys = MunicipalityMock.ReturnMunicipalityListMock();

            _ = _mockMunicipalityService
                .Setup(x => x.GetAsync<MunicipalityResponse>(null, null))
                .ReturnsAsync(Municipalitys);

            var result = await _municipalityController.GetMunicipalitiesAsync() as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(Municipalitys, result.Value);
        }

        [TestMethod("Municipalities No content")]
        public async Task GetMunicipalitiesAsync_ShouldNoContent_Municipalities()
        {
            _ = _mockMunicipalityService
                .Setup(x => x.GetAsync<MunicipalityResponse>(null, null))
                .ReturnsAsync(new List<MunicipalityResponse>());

            var result = await _municipalityController.GetMunicipalitiesAsync() as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Municipalities Return exception")]
        public async Task GetMunicipalitiesAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";

            _ = _mockMunicipalityService
                .Setup(x => x.GetAsync<MunicipalityResponse>(null, null))
                .Throws(new Exception(message));

            var result = await _municipalityController.GetMunicipalitiesAsync() as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Municipality by id

        [TestMethod("Municipality by id Correct return")]
        public async Task GetMunicipalityAsync_ShouldCorrectReturn_Municipality()
        {
            var Municipality = MunicipalityMock.ReturnMunicipality1Mock();
            var MunicipalityId = "ba_camacari";

            _ = _mockMunicipalityService
                .Setup(x => x.GetByIdAsync<MunicipalityResponse>(MunicipalityId))
                .ReturnsAsync(Municipality);

            var result = await _municipalityController.GetMunicipalityAsync(MunicipalityId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(((MunicipalityResponse)result.Value).Id, Municipality.Id);
            Assert.AreEqual(((MunicipalityResponse)result.Value).Id, MunicipalityId);
        }

        [TestMethod("Municipality by id No content")]
        public async Task GetMunicipalityAsync_ShouldNoContent_Municipality()
        {
            _ = _mockMunicipalityService
                .Setup(x => x.GetByIdAsync<MunicipalityResponse>(It.IsAny<string>()))
                .ReturnsAsync((MunicipalityResponse)null);

            var result = await _municipalityController.GetMunicipalityAsync(It.IsAny<string>()) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Municipality by id Return exception")]
        public async Task GetMunicipalityAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";
            var MunicipalityId = "exception";

            _ = _mockMunicipalityService
                .Setup(x => x.GetByIdAsync<MunicipalityResponse>(MunicipalityId))
                .Throws(new Exception(message));

            var result = await _municipalityController.GetMunicipalityAsync(MunicipalityId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion
    }
}
