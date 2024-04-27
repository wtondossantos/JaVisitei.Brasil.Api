using JaVisitei.Brasil.Business.ViewModels.Response.RegionType;
using JaVisitei.Brasil.Api.Controllers;
using JaVisitei.Brasil.Business.Service.Interfaces;
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
    public class RegionTypeControllerTest
    {
        private readonly RegionTypeController _regionTypeController;
        private readonly Mock<IRegionTypeService> _mockRegionTypeService;

        public RegionTypeControllerTest()
        {
            _mockRegionTypeService = new Mock<IRegionTypeService>();
            _regionTypeController = new RegionTypeController(_mockRegionTypeService.Object);
        }

        #region Region types

        [TestMethod("Region types Correct return")]
        public async Task GetRegionTypesAsync_ShouldCorrectReturn_RegionTypes()
        {
            var Municipalitys = RegionTypeMock.ReturnRegionTypeListMock();

            _ = _mockRegionTypeService
                .Setup(x => x.GetAsync<RegionTypeResponse>(null, null))
                .ReturnsAsync(Municipalitys);

            var result = await _regionTypeController.GetRegionTypesAsync() as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(Municipalitys, result.Value);
        }

        [TestMethod("Region types No content")]
        public async Task GetRegionTypesAsync_ShouldNoContent_RegionTypes()
        {
            _ = _mockRegionTypeService
                .Setup(x => x.GetAsync<RegionTypeResponse>(null, null))
                .ReturnsAsync(new List<RegionTypeResponse>());

            var result = await _regionTypeController.GetRegionTypesAsync() as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Region types Return exception")]
        public async Task GetRegionTypesAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";

            _ = _mockRegionTypeService
                .Setup(x => x.GetAsync<RegionTypeResponse>(null, null))
                .Throws(new Exception(message));

            var result = await _regionTypeController.GetRegionTypesAsync() as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion
    }
}
