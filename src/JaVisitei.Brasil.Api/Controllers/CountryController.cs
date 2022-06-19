using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.ViewModels.Response.Country;
using JaVisitei.Brasil.Business.ViewModels.Response.State;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [Authorize(Roles = "administrator, basic, contributor")]
        [HttpGet(Name = "GetCountries")]
        public async Task<IActionResult> GetCountriesAsync()
        {
            try
            {
                var result = await _countryService.GetAsync<CountryResponse>();

                if (result is null || !result.Any())
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Roles = "administrator, basic, contributor")]
        [HttpGet("{id}", Name = "GetCountry")]
        public async Task<IActionResult> GetCountryAsync([FromRoute] string id)
        {
            try
            {
                var result = await _countryService.GetByIdAsync<CountryResponse>(id);

                if (result is null)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Roles = "administrator, basic, contributor")]
        [HttpGet("{id}/states/", Name = "GetStatesByCountry")]
        public async Task<IActionResult> GetStatesByCountryAsync([FromRoute] string id)
        {
            try
            {
                var result = await _stateService.GetAsync<StateResponse>(x => x.CountryId.Equals(id));

                if (result is null || !result.Any())
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
