using JaVisitei.Brasil.Business.ViewModels.Response.Macroregion;
using JaVisitei.Brasil.Business.ViewModels.Response.Microregion;
using JaVisitei.Brasil.Business.ViewModels.Response.Archipelago;
using JaVisitei.Brasil.Business.ViewModels.Response.Island;
using JaVisitei.Brasil.Business.ViewModels.Response.Municipality;
using JaVisitei.Brasil.Business.ViewModels.Response.State;
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
    public class StateControllerTest
    {
        private readonly StateController _stateController;
        private readonly Mock<IStateService> _mockStateService;
        private readonly Mock<IMacroregionService> _mockMacroregionService;
        private readonly Mock<IMicroregionService> _mockMicroregionService;
        private readonly Mock<IArchipelagoService> _mockArchipelagoService;
        private readonly Mock<IMunicipalityService> _mockMunicipalityService;
        private readonly Mock<IIslandService> _mockIslandService;

        public StateControllerTest()
        {
            _mockStateService = new Mock<IStateService>();
            _mockMacroregionService = new Mock<IMacroregionService>();
            _mockMicroregionService = new Mock<IMicroregionService>();
            _mockArchipelagoService = new Mock<IArchipelagoService>();
            _mockMunicipalityService = new Mock<IMunicipalityService>();
            _mockIslandService = new Mock<IIslandService>();
            _stateController = new StateController(_mockStateService.Object,
                _mockMacroregionService.Object,
                _mockMicroregionService.Object,
                _mockArchipelagoService.Object,
                _mockMunicipalityService.Object,
                _mockIslandService.Object);
        }

        #region States

        [TestMethod("States Correct return")]
        public async Task GetStatesAsync_ShouldCorrectReturn_States()
        {
            var states = StateMock.ReturnStateListMock();

            _ = _mockStateService
                .Setup(x => x.GetAsync<StateResponse>(null, null))
                .ReturnsAsync(states);

            var result = await _stateController.GetStatesAsync() as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(states, result.Value);
        }

        [TestMethod("States No content")]
        public async Task GetStatesAsync_ShouldNoContent_States()
        {
            _ = _mockStateService
                .Setup(x => x.GetAsync<StateResponse>(null, null))
                .ReturnsAsync(new List<StateResponse>());

            var result = await _stateController.GetStatesAsync() as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("States Return exception")]
        public async Task GetStatesAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";

            _ = _mockStateService
                .Setup(x => x.GetAsync<StateResponse>(null, null))
                .Throws(new Exception(message));

            var result = await _stateController.GetStatesAsync() as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region State by id

        [TestMethod("State by id Correct return")]
        public async Task GetStateAsync_ShouldCorrectReturn_State()
        {
            var state = StateMock.ReturnState2Mock();
            var stateId = "pe_pernambuco_estado";

            _ = _mockStateService
                .Setup(x => x.GetByIdAsync<StateResponse>(stateId))
                .ReturnsAsync(state);

            var result = await _stateController.GetStateAsync(stateId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(((StateResponse)result.Value).Id, state.Id);
            Assert.AreEqual(((StateResponse)result.Value).Id, stateId);
        }

        [TestMethod("State by id No content")]
        public async Task GetStateAsync_ShouldNoContent_State()
        {
            _ = _mockStateService
                .Setup(x => x.GetByIdAsync<StateResponse>(It.IsAny<string>()))
                .ReturnsAsync((StateResponse)null);

            var result = await _stateController.GetStateAsync(It.IsAny<string>()) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("States Return exception")]
        public async Task GetStateAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";
            var stateId = "exception";

            _ = _mockStateService
                .Setup(x => x.GetByIdAsync<StateResponse>(stateId))
                .Throws(new Exception(message));

            var result = await _stateController.GetStateAsync(stateId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Macroregions by state id

        [TestMethod("Macroregions by state id Correct return")]
        public async Task GetMacroregionsByStateAsync_ShouldCorrectResturn_MacroregionsByStateId()
        {
            var macroregions = MacroregionMock.ReturnMacroregionListMock();
            var stateId = "ma_maranhao_estado";

            _ = _mockMacroregionService
                .Setup(x => x.GetAsync<MacroregionResponse>(x => x.StateId.Equals(stateId), null))
                .ReturnsAsync(macroregions);

            var result = await _stateController.GetMacroregionsByStateAsync(stateId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(macroregions, result.Value);
        }

        [TestMethod("Macroregions by state id No content")]
        public async Task GetMacroregionsByStateAsync_ShouldNoContent_MacroregionsByStateId()
        {
            var stateId = "not_exists";

            _ = _mockMacroregionService
                .Setup(x => x.GetAsync<MacroregionResponse>(x => x.StateId.Equals(stateId), null))
                .ReturnsAsync(new List<MacroregionResponse>());

            var result = await _stateController.GetMacroregionsByStateAsync(stateId) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Macroregions by state id Return exception")]
        public async Task GetMacroregionsByStateAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";
            var stateId = "exception";

            _ = _mockMacroregionService
                .Setup(x => x.GetAsync<MacroregionResponse>(x => x.StateId.Equals(stateId), null))
                .Throws(new Exception(message));

            var result = await _stateController.GetMacroregionsByStateAsync(stateId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Microregions by state id

        [TestMethod("Microregions by state id Correct return")]
        public async Task GetMicroregionsByStateAsync_ShouldCorrectResturn_MicroregionsByStateId()
        {
            var microregions = MicroregionMock.ReturnMicroregionListMock();
            var stateId = "ac_acre_estado";

            _ = _mockMicroregionService
                .Setup(x => x.GetByStateAsync<MicroregionResponse>(stateId))
                .ReturnsAsync(microregions);

            var result = await _stateController.GetMicroregionsByStateAsync(stateId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(microregions, result.Value);
        }

        [TestMethod("Microregions by state id No content")]
        public async Task GetMicroregionsByStateAsync_ShouldNoContent_MicroregionsByStateId()
        {
            var stateId = "not_exists";

            _ = _mockMicroregionService
                .Setup(x => x.GetByStateAsync<MicroregionResponse>(stateId))
                .ReturnsAsync(new List<MicroregionResponse>());

            var result = await _stateController.GetMicroregionsByStateAsync(stateId) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Microregions by state id Return exception")]
        public async Task GetMicroregionsByStateAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";
            var stateId = "exception";

            _ = _mockMicroregionService
                .Setup(x => x.GetByStateAsync<MicroregionResponse>(stateId))
                .Throws(new Exception(message));

            var result = await _stateController.GetMicroregionsByStateAsync(stateId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Archipelagos by state id

        [TestMethod("Archipelagos by state id Correct return")]
        public async Task GetArchipelagosByStateAsync_ShouldCorrectResturn_ArchipelagosByStateId()
        {
            var archipelagos = ArchipelagoMock.ReturnArchipelagoListMock();
            var stateId = "ac_acre_estado";

            _ = _mockArchipelagoService
                .Setup(x => x.GetByStateAsync<ArchipelagoResponse>(stateId))
                .ReturnsAsync(archipelagos);

            var result = await _stateController.GetArchipelagosByStateAsync(stateId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(archipelagos, result.Value);
        }

        [TestMethod("Archipelagos by state id No content")]
        public async Task GetArchipelagosByStateAsync_ShouldNoContent_ArchipelagosByStateId()
        {
            var stateId = "not_exists";

            _ = _mockArchipelagoService
                .Setup(x => x.GetByStateAsync<ArchipelagoResponse>(stateId))
                .ReturnsAsync(new List<ArchipelagoResponse>());

            var result = await _stateController.GetArchipelagosByStateAsync(stateId) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Archipelagos by state id Return exception")]
        public async Task GetArchipelagosByStateAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";
            var stateId = "exception";

            _ = _mockArchipelagoService
                .Setup(x => x.GetByStateAsync<ArchipelagoResponse>(stateId))
                .Throws(new Exception(message));

            var result = await _stateController.GetArchipelagosByStateAsync(stateId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Municipalities by state id

        [TestMethod("Municipalities by state id Correct return")]
        public async Task GetMunicipalitiesByStateAsync_ShouldCorrectResturn_MunicipalitiesByStateId()
        {
            var municipalities = MunicipalityMock.ReturnMunicipalityListMock();
            var stateId = "ac_acre_estado";

            _ = _mockMunicipalityService
                .Setup(x => x.GetByStateAsync<MunicipalityResponse>(stateId))
                .ReturnsAsync(municipalities);

            var result = await _stateController.GetMunicipalitiesByStateAsync(stateId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(municipalities, result.Value);
        }

        [TestMethod("Municipalities by state id No content")]
        public async Task GetMunicipalitiesByStateAsync_ShouldNoContent_MunicipalitiesByStateId()
        {
            var stateId = "not_exists";

            _ = _mockMunicipalityService
                .Setup(x => x.GetByStateAsync<MunicipalityResponse>(stateId))
                .ReturnsAsync(new List<MunicipalityResponse>());

            var result = await _stateController.GetMunicipalitiesByStateAsync(stateId) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Municipalities by state id Return exception")]
        public async Task GetMunicipalitiesByStateAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";
            var stateId = "exception";

            _ = _mockMunicipalityService
                .Setup(x => x.GetByStateAsync<MunicipalityResponse>(stateId))
                .Throws(new Exception(message));

            var result = await _stateController.GetMunicipalitiesByStateAsync(stateId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Islands by state id

        [TestMethod("Islands by state id Correct return")]
        public async Task GetIslandsByStateAsync_ShouldCorrectResturn_IslandsByStateId()
        {
            var islands = IslandMock.ReturnIslandListMock();
            var stateId = "ac_acre_estado";

            _ = _mockIslandService
                .Setup(x => x.GetByStateAsync<IslandResponse>(stateId))
                .ReturnsAsync(islands);

            var result = await _stateController.GetIslandsByStateAsync(stateId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(islands, result.Value);
        }

        [TestMethod("Islands by state id No content")]
        public async Task GetIslandsByStateAsync_ShouldNoContentIslandsByStateId()
        {
            var stateId = "not_exists";

            _ = _mockIslandService
                .Setup(x => x.GetByStateAsync<IslandResponse>(stateId))
                .ReturnsAsync(new List<IslandResponse>());

            var result = await _stateController.GetIslandsByStateAsync(stateId) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Islands by state id Return exception")]
        public async Task GetIslandsByStateAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";
            var stateId = "exception";

            _ = _mockIslandService
                .Setup(x => x.GetByStateAsync<IslandResponse>(stateId))
                .Throws(new Exception(message));

            var result = await _stateController.GetIslandsByStateAsync(stateId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion
    }
}
