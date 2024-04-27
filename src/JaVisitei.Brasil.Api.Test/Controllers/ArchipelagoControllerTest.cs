using JaVisitei.Brasil.Api.Controllers;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.ViewModels.Response.Archipelago;
using JaVisitei.Brasil.Business.ViewModels.Response.Island;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using JaVisitei.Brasil.Test.Mocks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Moq;
using System;

namespace JaVisitei.Brasil.Test.Controllers
{
    [TestClass]
    public class ArchipelagoControllerTest
    {
        private readonly ArchipelagoController _archipelagoController;
        private readonly Mock<IArchipelagoService> _mockArchipelagoService;
        private readonly Mock<IIslandService> _mockIslandService;

        public ArchipelagoControllerTest()
        {
            _mockArchipelagoService = new Mock<IArchipelagoService>();
            _mockIslandService = new Mock<IIslandService>();
            _archipelagoController = new ArchipelagoController(_mockArchipelagoService.Object, _mockIslandService.Object);
        }

        #region Archipelagos

        [TestMethod("Archipelagos Correct return")]
        public async Task GetArchipelagosAsync_ShouldCorrectReturn_Archipelagos()
        {
            var archipelagos = ArchipelagoMock.ReturnArchipelagoListMock();

            _ = _mockArchipelagoService
                .Setup(x => x.GetAsync<ArchipelagoResponse>(null, null))
                .ReturnsAsync(archipelagos);

            var result = await _archipelagoController.GetArchipelagosAsync() as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(archipelagos, result.Value);
        }

        [TestMethod("Archipelagos No content")]
        public async Task GetArchipelagoAsync_ShouldNoContent_Archipelagos()
        {
            _ = _mockArchipelagoService
                .Setup(x => x.GetAsync<ArchipelagoResponse>(null, null))
                .ReturnsAsync(new List<ArchipelagoResponse>());

            var result = await _archipelagoController.GetArchipelagosAsync() as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Archipelagos Return exception")]
        public async Task GetArchipelagosAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";

            _ = _mockArchipelagoService
                .Setup(x => x.GetAsync<ArchipelagoResponse>(null, null))
                .Throws(new Exception(message));

            var result = await _archipelagoController.GetArchipelagosAsync() as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Archipelago by id

        [TestMethod("Archipelago by id Correct return")]
        public async Task GetArchipelagoAsync_ShouldCorrectReturn_Archipelago()
        {
            var archipelago = ArchipelagoMock.ReturnArchipelago1Mock();
            var archipelagoId = "ba_arquipelago_de_abrolhos_ilha";

            _ = _mockArchipelagoService
                .Setup(x => x.GetByIdAsync<ArchipelagoResponse>(archipelagoId))
                .ReturnsAsync(archipelago);

            var result = await _archipelagoController.GetArchipelagoAsync(archipelagoId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(((ArchipelagoResponse)result.Value).Id, archipelago.Id);
            Assert.AreEqual(((ArchipelagoResponse)result.Value).Id, archipelagoId);
        }

        [TestMethod("Archipelago by id No content")]
        public async Task GetArchipelagoAsync_ShouldNoContent_Archipelago()
        {
            _ = _mockArchipelagoService
                .Setup(x => x.GetByIdAsync<ArchipelagoResponse>(It.IsAny<string>()))
                .ReturnsAsync((ArchipelagoResponse)null);

            var result = await _archipelagoController.GetArchipelagoAsync(It.IsAny<string>()) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Archipelago by id Return exception")]
        public async Task GetArchipelagoAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";

            _ = _mockArchipelagoService
                .Setup(x => x.GetByIdAsync<ArchipelagoResponse>(It.IsAny<string>()))
                .Throws(new Exception(message));

            var result = await _archipelagoController.GetArchipelagoAsync(It.IsAny<string>()) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Islands by archipelago id

        [TestMethod("Islands by archipelago id Correct return")]
        public async Task GetIslandsByArchipelagoAsync_ShouldCorrectResturn_IslandsByArchipelagoId()
        {
            var islands = IslandMock.ReturnIslandListMock();
            var archipelagoId = "ba_arquipelago_de_abrolhos_ilha";

            _ = _mockIslandService
                .Setup(x => x.GetAsync<IslandResponse>(x => x.ArchipelagoId.Equals(archipelagoId), null))
                .ReturnsAsync(islands);
            
            var result = await _archipelagoController.GetIslandsByArchipelagoAsync(archipelagoId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(islands, result.Value);
        }

        [TestMethod("Islands by archipelago id No content")]
        public async Task GetIslandsByArchipelagoAsync_ShouldNoContent_IslandsByArchipelagoId()
        {
            var archipelagoId = "not_exists";

            _ = _mockIslandService
                .Setup(x => x.GetAsync<IslandResponse>(x => x.ArchipelagoId.Equals(archipelagoId), null))
                .ReturnsAsync(new List<IslandResponse>());

            var result = await _archipelagoController.GetIslandsByArchipelagoAsync(archipelagoId) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Islands by archipelago id Return exception")]
        public async Task GetIslandsByArchipelagoAsync_ShouldProbrem_Exception()
        {
            var archipelagoId = "exception";
            var message = "Exception test";

            _ = _mockIslandService
                .Setup(x => x.GetAsync<IslandResponse>(x => x.ArchipelagoId.Equals(archipelagoId), null))
                .Throws(new Exception(message));

            var result = await _archipelagoController.GetIslandsByArchipelagoAsync(archipelagoId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion
    }
}
