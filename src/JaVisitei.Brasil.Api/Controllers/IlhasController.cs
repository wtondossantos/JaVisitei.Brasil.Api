using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace JaVisitei.Brasil.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1")]
    [ControllerName("Ilhas Brasilieiras")]
    [Route("api/v{version:apiVersion}/ilha")]
    public class IlhasController : ControllerBase
    {
        private readonly IIlhaService _ilha;

        public IlhasController(IIlhaService ilha)
        {
            _ilha = ilha;
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Ilha>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(Name = "GetIlhas")]
        public IActionResult Pesquisar()
        {
            var lista = _ilha.Pesquisar();

            if (lista == null)
                return NotFound();

            return Ok(lista);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Ilha))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id_ilha}", Name = "GetIlha")]
        public IActionResult Pesquisar([FromRoute] string id_ilha)
        {
            var model = _ilha.Pesquisar(x => x.Id == id_ilha).ToList();

            if (model == null)
                return NotFound();

            return Ok(model);
        }
    }
}
