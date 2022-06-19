using JaVisitei.Brasil.Business.ViewModels.Response.Macroregion;
using JaVisitei.Brasil.Business.ViewModels.Response.Microregion;
using JaVisitei.Brasil.Business.ViewModels.Response.Archipelago;
using JaVisitei.Brasil.Business.ViewModels.Response.Island;
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
    public class MacroregionControllerTest
    {
        private readonly MacroregionController _macroregionController;
        private readonly Mock<IMacroregionService> _mockMacroregionService;
        private readonly Mock<IMicroregionService> _mockMicroregionService;
        private readonly Mock<IArchipelagoService> _mockArchipelagoService;
        private readonly Mock<IMunicipalityService> _mockMunicipalityService;
        private readonly Mock<IIslandService> _mockIslandService;

        public MacroregionControllerTest()
        {
            _mockMacroregionService = new Mock<IMacroregionService>();
            _mockMicroregionService = new Mock<IMicroregionService>();
            _mockArchipelagoService = new Mock<IArchipelagoService>();
            _mockMunicipalityService = new Mock<IMunicipalityService>();
            _mockIslandService = new Mock<IIslandService>();
            _macroregionController = new MacroregionController(_mockMacroregionService.Object,
                _mockMicroregionService.Object,
                _mockArchipelagoService.Object,
                _mockMunicipalityService.Object,
                _mockIslandService.Object);
        }

        #region Macroregions

        [TestMethod("Macroregions Correct return")]
        public async Task GetMacroregionsAsync_ShouldCorrectReturn_Macroregions()
        {
            var macroregions = MacroregionMock.ReturnMacroregionListMock();

            _ = _mockMacroregionService
                .Setup(x => x.GetAsync<MacroregionResponse>(null, null))
                .ReturnsAsync(macroregions);

            var result = await _macroregionController.GetMacroregionsAsync() as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(macroregions, result.Value);
        }

        [TestMethod("Macroregions No content")]
        public async Task GetMacroregionsAsync_ShouldNoContent_Macroregions()
        {
            _ = _mockMacroregionService
                .Setup(x => x.GetAsync<MacroregionResponse>(null, null))
                .ReturnsAsync(new List<MacroregionResponse>());

            var result = await _macroregionController.GetMacroregionsAsync() as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Macroregions Return exception")]
        public async Task GetMacroregionsAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";

            _ = _mockMacroregionService
                .Setup(x => x.GetAsync<MacroregionResponse>(null, null))
                .Throws(new Exception(message));

            var result = await _macroregionController.GetMacroregionsAsync() as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Macroregion by id

        [TestMethod("Macroregion by id Correct return")]
        public async Task GetMacroregionAsync_ShouldCorrectReturn_Macroregion()
        {
            var macroregion = MacroregionMock.ReturnMacroregion3Mock();
            var macroregionId = "ac_vale_do_acre_macro";

            _ = _mockMacroregionService
                .Setup(x => x.GetByIdAsync<MacroregionResponse>(macroregionId))
                .ReturnsAsync(macroregion);

            var result = await _macroregionController.GetMacroregionAsync(macroregionId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(((MacroregionResponse)result.Value).Id, macroregion.Id);
            Assert.AreEqual(((MacroregionResponse)result.Value).Id, macroregionId);
        }

        [TestMethod("Macroregion by id No content")]
        public async Task GetMacroregionAsync_ShouldNoContent_Macroregion()
        {
            _ = _mockMacroregionService
                .Setup(x => x.GetByIdAsync<MacroregionResponse>(It.IsAny<string>()))
                .ReturnsAsync((MacroregionResponse)null);

            var result = await _macroregionController.GetMacroregionAsync(It.IsAny<string>()) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Macroregion by id Return exception")]
        public async Task GetMacroregionAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";
            var macroregionId = "exception";

            _ = _mockMacroregionService
                .Setup(x => x.GetByIdAsync<MacroregionResponse>(macroregionId))
                .Throws(new Exception(message));

            var result = await _macroregionController.GetMacroregionAsync(macroregionId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Microregions by macroregion id

        [TestMethod("Microregions by macroregion id Correct return")]
        public async Task GetMicroregionsByMacroregionAsync_ShouldCorrectResturn_MicroregionsByMacroregionId()
        {
            var microregions = MicroregionMock.ReturnMicroregionListMock();
            var macroregionId = "ma_leste_maranhense_macro";

            _ = _mockMicroregionService
                .Setup(x => x.GetAsync<MicroregionResponse>(x => x.MacroregionId.Equals(macroregionId), null))
                .ReturnsAsync(microregions);

            var result = await _macroregionController.GetMicroregionsByMacroregionAsync(macroregionId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(microregions, result.Value);
        }

        [TestMethod("Microregions by macroregion id No content")]
        public async Task GetMicroregionsByMacroregionAsync_ShouldNoContent_MicroregionsByMacroregionId()
        {
            var macroregionId = "not_exists";

            _ = _mockMicroregionService
                .Setup(x => x.GetAsync<MicroregionResponse>(x => x.MacroregionId.Equals(macroregionId), null))
                .ReturnsAsync(new List<MicroregionResponse>());

            var result = await _macroregionController.GetMicroregionsByMacroregionAsync(macroregionId) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Microregions by macroregion id Return exception")]
        public async Task GetMicroregionsByMacroregionAsync_ShouldProbrem_Exception()
        {
            var macroregionId = "exception";
            var message = "Exception test";

            _ = _mockMicroregionService
                .Setup(x => x.GetAsync<MicroregionResponse>(x => x.MacroregionId.Equals(macroregionId), null))
                .Throws(new Exception(message));

            var result = await _macroregionController.GetMicroregionsByMacroregionAsync(macroregionId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Archipelagos by macroregion id

        [TestMethod("Archipelagos by macroregion id Correct return")]
        public async Task GetArchipelagosByMacroregionAsync_ShouldCorrectResturn_ArchipelagosByMacroregionId()
        {
            var archipelagos = ArchipelagoMock.ReturnArchipelagoListMock();
            var macroregionId = "ba_metropolitana_de_salvador_macro";

            _ = _mockArchipelagoService
                .Setup(x => x.GetAsync<ArchipelagoResponse>(x => x.MacroregionId.Equals(macroregionId), null))
                .ReturnsAsync(archipelagos);

            var result = await _macroregionController.GetArchipelagosByMacroregionAsync(macroregionId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(archipelagos, result.Value);
        }

        [TestMethod("Archipelagos by macroregion id No content")]
        public async Task GetArchipelagosByMacroregionAsync_ShouldNoContent_ArchipelagosByMacroregionId()
        {
            var macroregionId = "not_exists";

            _ = _mockArchipelagoService
                .Setup(x => x.GetAsync<ArchipelagoResponse>(x => x.MacroregionId.Equals(macroregionId), null))
                .ReturnsAsync(new List<ArchipelagoResponse>());

            var result = await _macroregionController.GetArchipelagosByMacroregionAsync(macroregionId) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Archipelagos by macroregion id Return exception")]
        public async Task GetArchipelagosByMacroregionAsync_ShouldProbrem_Exception()
        {
            var macroregionId = "exception";
            var message = "Exception test";

            _ = _mockArchipelagoService
                .Setup(x => x.GetAsync<ArchipelagoResponse>(x => x.MacroregionId.Equals(macroregionId), null))
                .Throws(new Exception(message));

            var result = await _macroregionController.GetArchipelagosByMacroregionAsync(macroregionId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Municipalities by macroregion id

        [TestMethod("Municipalities by macroregion id Correct return")]
        public async Task GetMunicipalitiesByMacroregionAsync_ShouldCorrectResturn_MunicipalitiesByMacroregionId()
        {
            var municipalities = MunicipalityMock.ReturnMunicipalityListMock();
            var macroregionId = "ba_metropolitana_de_salvador_macro";

            _ = _mockMunicipalityService
                .Setup(x => x.GetByMacroregionAsync<MunicipalityResponse>(macroregionId))
                .ReturnsAsync(municipalities);

            var result = await _macroregionController.GetMunicipalitiesByMacroregionAsync(macroregionId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(municipalities, result.Value);
        }

        [TestMethod("Municipalities by macroregion id No content")]
        public async Task GetMunicipalitiesByMacroregionAsync_ShouldNoContent_MunicipalitiesByMacroregionId()
        {
            var macroregionId = "not_exists";

            _ = _mockMunicipalityService
                .Setup(x => x.GetByMacroregionAsync<MunicipalityResponse>(macroregionId))
                .ReturnsAsync(new List<MunicipalityResponse>());

            var result = await _macroregionController.GetMunicipalitiesByMacroregionAsync(macroregionId) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Municipalities by macroregion id Return exception")]
        public async Task GetMunicipalitiesByMacroregionAsync_ShouldProbrem_Exception()
        {
            var macroregionId = "exception";
            var message = "Exception test";

            _ = _mockMunicipalityService
                .Setup(x => x.GetByMacroregionAsync<MunicipalityResponse>(macroregionId))
                .Throws(new Exception(message));

            var result = await _macroregionController.GetMunicipalitiesByMacroregionAsync(macroregionId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Islands by macroregion id

        [TestMethod("Islands by macroregion id Correct return")]
        public async Task GetIslandsByMacroregionAsync_ShouldCorrectResturn_IslandsByMacroregionId()
        {
            var islands = IslandMock.ReturnIslandListMock();
            var macroregionId = "ba_metropolitana_de_salvador_macro";

            _ = _mockIslandService
                .Setup(x => x.GetByMacroregionAsync<IslandResponse>(macroregionId))
                .ReturnsAsync(islands);

            var result = await _macroregionController.GetIslandsByMacroregionAsync(macroregionId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(islands, result.Value);
        }

        [TestMethod("Islands by macroregion id No content")]
        public async Task GetIslandsByMacroregionAsync_ShouldNoContent_IslandsByMacroregionId()
        {
            var macroregionId = "not_exists";

            _ = _mockIslandService
                .Setup(x => x.GetByMacroregionAsync<IslandResponse>(macroregionId))
                .ReturnsAsync(new List<IslandResponse>());

            var result = await _macroregionController.GetIslandsByMacroregionAsync(macroregionId) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Islands by macroregion id Return exception")]
        public async Task GetIslandsByMacroregionAsync_ShouldProbrem_Exception()
        {
            var macroregionId = "exception";
            var message = "Exception test";

            _ = _mockIslandService
                .Setup(x => x.GetByMacroregionAsync<IslandResponse>(macroregionId))
                .Throws(new Exception(message));

            var result = await _macroregionController.GetIslandsByMacroregionAsync(macroregionId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion
    }
}
