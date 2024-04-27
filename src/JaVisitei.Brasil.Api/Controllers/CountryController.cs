using Asp.Versioning;
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

        [Authorize(Roles = "administrator")]
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
        
        [Authorize(Roles = "administrator")]
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

        [Authorize(Roles = "administrator, basic, contributor")]
        [HttpGet("{id}/full", Name = "GetMapCountryFullByIdAsync")]
        public async Task<IActionResult> GetMapCountryFullByIdAsync([FromRoute] string id)
        {
            try
            {
                var result = await _countryService.GetFullByIdAsync(id);

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
        [HttpGet("{id}/user/{userId}/full", Name = "GetMapCountryFullByIdAndUserIdAsync")]
        public async Task<IActionResult> GetMapCountryFullByIdAndUserIdAsync([FromRoute] string id, string userId)
        {
            try
            {
                var result = await _countryService.GetFullByIdAndUserIdAsync(id, userId);

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
        [HttpGet("{mapTypeId}/mundi", Name = "GetByMapTypeIdAsync")]
        public async Task<IActionResult> GetByMapTypeIdAsync([FromRoute] short mapTypeId)
        {
            try
            {
                var result = await _countryService.GetByMapTypeIdAsync(mapTypeId);

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
        [HttpGet("{mapTypeId}/mundifull", Name = "GetFullByMapTypeAsync")]
        public async Task<IActionResult> GetFullByMapTypeAsync([FromRoute] short mapTypeId)
        {
            try
            {
                var result = await _countryService.GetFullByMapTypeIdAsync(mapTypeId);

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
        [HttpGet("{mapTypeId}/user/{userId}/mundi", Name = "GetByMapTypeIdAndUserIdAsync")]
        public async Task<IActionResult> GetByMapTypeIdAndUserIdAsync([FromRoute] short mapTypeId, string userId)
        {
            try
            {
                var result = await _countryService.GetByMapTypeIdAndUserIdAsync(mapTypeId, userId);

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
        [HttpGet("{mapTypeId}/user/{userId}/mundifull", Name = "GetFullByMapTypeIdAndUserIdAsync")]
        public async Task<IActionResult> GetFullByMapTypeIdAndUserIdAsync([FromRoute] short mapTypeId, string userId)
        {
            try
            {
                var result = await _countryService.GetFullByMapTypeIdAndUserIdAsync(mapTypeId, userId);

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
        [HttpGet("{mapTypeId}/names/mundifull", Name = "GetCountriesNamesAsync")]
        public async Task<IActionResult> GetCountriesNamesAsync([FromRoute] short mapTypeId)
        {
            try
            {
                var result = await _countryService.GetNamesAsync(mapTypeId);

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
