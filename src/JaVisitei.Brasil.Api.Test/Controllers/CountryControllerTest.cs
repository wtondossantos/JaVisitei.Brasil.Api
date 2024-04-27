using JaVisitei.Brasil.Api.Controllers;
using JaVisitei.Brasil.Business.ViewModels.Response.Country;
using JaVisitei.Brasil.Business.ViewModels.Response.State;
using JaVisitei.Brasil.Business.Service.Interfaces;
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
    public class CountryControllerTest
    {
        private readonly CountryController _countryController;
        private readonly Mock<ICountryService> _mockCountryService;
        private readonly Mock<IStateService> _mockStateService;

        public CountryControllerTest()
        {
            _mockCountryService = new Mock<ICountryService>();
            _mockStateService = new Mock<IStateService>();
            _countryController = new CountryController(_mockCountryService.Object, _mockStateService.Object);
        }

        #region Countries

        [TestMethod("Countries Correct return")]
        public async Task GetCountriesAsync_ShouldCorrectReturn_Countries()
        {
            var countries = CountryMock.ReturnCountryListMock();

            _ = _mockCountryService
                .Setup(x => x.GetAsync<CountryResponse>(null, null))
                .ReturnsAsync(countries);

            var result = await _countryController.GetCountriesAsync() as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(countries, result.Value);
        }

        [TestMethod("Countries No content")]
        public async Task GetCountriesAsync_ShouldNoContent_Countries()
        {
            _ = _mockCountryService
                .Setup(x => x.GetAsync<CountryResponse>(null, null))
                .ReturnsAsync(new List<CountryResponse>());

            var result = await _countryController.GetCountriesAsync() as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Countries Return exception")]
        public async Task GetCountriesAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";

            _ = _mockCountryService
                .Setup(x => x.GetAsync<CountryResponse>(null, null))
                .Throws(new Exception(message));

            var result = await _countryController.GetCountriesAsync() as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region Country by id

        [TestMethod("Country by id Correct return")]
        public async Task GetCountryAsync_ShouldCorrectReturn_Country()
        {
            var country = CountryMock.ReturnCountry1Mock();
            var countryId = "bra_brasil";

            _ = _mockCountryService
                .Setup(x => x.GetByIdAsync<CountryResponse>(countryId))
                .ReturnsAsync(country);

            var result = await _countryController.GetCountryAsync(countryId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(((CountryResponse)result.Value).Id, country.Id);
            Assert.AreEqual(((CountryResponse)result.Value).Id, countryId);
        }

        [TestMethod("Country by id No content")]
        public async Task GetCountryAsync_ShouldNoContent_Country()
        {
            _ = _mockCountryService
                .Setup(x => x.GetByIdAsync<CountryResponse>(It.IsAny<string>()))
                .ReturnsAsync((CountryResponse)null);

            var result = await _countryController.GetCountryAsync(It.IsAny<string>()) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("Country by id Return exception")]
        public async Task GetCountryAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";
            var countryId = "exception";

            _ = _mockCountryService
                .Setup(x => x.GetByIdAsync<CountryResponse>(countryId))
                .Throws(new Exception(message));

            var result = await _countryController.GetCountryAsync(countryId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion

        #region States by country id

        [TestMethod("States by country id Correct return")]
        public async Task GetStatesByCountryAsync_ShouldCorrectResturn_StatesByCountryId()
        {
            var states = StateMock.ReturnStateListMock();
            var countryId = "bra_brasil";

            _ = _mockStateService
                .Setup(x => x.GetAsync<StateResponse>(x => x.CountryId.Equals(countryId), null))
                .ReturnsAsync(states);

            var result = await _countryController.GetStatesByCountryAsync(countryId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(states, result.Value);
        }

        [TestMethod("States by country id No content")]
        public async Task GetStatesByCountryAsync_ShouldNoContent_StatesByCountryId()
        {
            var countryId = "not_exists";

            _ = _mockStateService
                .Setup(x => x.GetAsync<StateResponse>(x => x.CountryId.Equals(countryId), null))
                .ReturnsAsync(new List<StateResponse>());

            var result = await _countryController.GetStatesByCountryAsync(countryId) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod("States by country id Return exception")]
        public async Task GetStatesByCountryAsync_ShouldProbrem_Exception()
        {
            var message = "Exception test";
            var countryId = "exception";

            _ = _mockStateService
                .Setup(x => x.GetAsync<StateResponse>(x => x.CountryId.Equals(countryId), null))
                .Throws(new Exception(message));

            var result = await _countryController.GetStatesByCountryAsync(countryId) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(((ProblemDetails)result.Value).Detail, message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }

        #endregion
    }
}
