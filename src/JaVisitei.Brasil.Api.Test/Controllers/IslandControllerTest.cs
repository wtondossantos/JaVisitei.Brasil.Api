using JaVisitei.Brasil.Api.Controllers;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.ViewModels.Response.Island;
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
    public class IslandControllerTest
    {
        private readonly IslandController _islandController;
        private readonly Mock<IIslandService> _mockIslandService;

        public IslandControllerTest()
        {
            _mockIslandService = new Mock<IIslandService>();
            _islandController = new IslandController(_mockIslandService.Object);
        }

        #region Islands

        [TestMethod("Islands Correct return")]
        public async Task GetIslandsAsync_ShouldCorrectReturn_Islands()
        {
            var islands = IslandMock.ReturnIslandListMock();

            _ = _mockIslandService
                .Setup(x => x.GetAsync<IslandResponse>(null, null))
                .ReturnsAsync(islands);

            var result = await _islandController.GetIslandsAsync() as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(islands, result.Value);
        }

        [TestMethod("Islands No content")]
        public async Task GetIslandsAsync_ShouldNoContent_Islands()
        {
            _ = _mockIslandService
                .Setup(x => x.GetAsync<IslandResponse>(null, null))
                .ReturnsAsync(new List<IslandResponse>());

            var result = await _islandController.GetIslandsAsync() as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Islands Return exception")]
        public async Task GetIslandsAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";

            _ = _mockIslandService
                .Setup(x => x.GetAsync<IslandResponse>(null, null))
                .Throws(new Exception(message));

            var result = await _islandController.GetIslandsAsync() as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Island by id

        [TestMethod("Island by id Correct return")]
        public async Task GetIslandAsync_ShouldCorrectReturn_Island()
        {
            var island = IslandMock.ReturnIsland1Mock();
            var islandId = "ba_ilha_redonda";

            _ = _mockIslandService
                .Setup(x => x.GetByIdAsync<IslandResponse>(islandId))
                .ReturnsAsync(island);

            var result = await _islandController.GetIslandAsync(islandId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(((IslandResponse)result.Value).Id, island.Id);
            Assert.AreEqual(((IslandResponse)result.Value).Id, islandId);
        }

        [TestMethod("Island by id No content")]
        public async Task GetIslandAsync_ShouldNoContent_Island()
        {
            _ = _mockIslandService
                .Setup(x => x.GetByIdAsync<IslandResponse>(It.IsAny<string>()))
                .ReturnsAsync((IslandResponse)null);

            var result = await _islandController.GetIslandAsync(It.IsAny<string>()) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Island by id Return exception")]
        public async Task GetIslandAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";
            var islandId = "exception";

            _ = _mockIslandService
                .Setup(x => x.GetByIdAsync<IslandResponse>(It.IsAny<string>()))
                .Throws(new Exception(message));

            var result = await _islandController.GetIslandAsync(islandId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion
    }
}
