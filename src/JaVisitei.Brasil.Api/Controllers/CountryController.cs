using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1")]
    [ControllerName("Country")]
    [Route("api/v{version:apiVersion}/countries")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly IStateService _stateService;

        public CountryController(ICountryService countryService, IStateService stateService)
        {
            _countryService = countryService;
            _stateService = stateService;
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Country>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(Name = "GetCountries")]
        public async Task<IActionResult> GetCountriesAsync()
        {
            var result = await _countryService.GetAllAsync();

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Country))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{country_id}", Name = "GetCountry")]
        public async Task<IActionResult> GetCountryAsync([FromRoute] string country_id)
        {
            var result = await _countryService.GetAsync(x => x.Id == country_id);

            if (result == null)
                return NotFound();

            return Ok(result.FirstOrDefault());
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<State>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{country_id}/states/", Name = "GetStatesByCountry")]
        public async Task<IActionResult> GetStatesByCountryAsync([FromRoute] string country_id)
        {
            var result = await _stateService.GetAsync(x => x.CountryId == country_id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
