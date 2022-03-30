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
    [ControllerName("Países")]
    [Route("api/v{version:apiVersion}/pais")]
    public class PaisesController : ControllerBase
    {
        private readonly IPaisService _pais;
        private readonly IEstadoService _estado;

        public PaisesController(IPaisService pais, IEstadoService estado)
        {
            _pais = pais;
            _estado = estado;
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Pais>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(Name = "GetPaises")]
        public IActionResult Pesquisar()
        {
            var lista = _pais.Pesquisar();

            if (lista == null)
                return NotFound();

            return Ok(lista);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pais))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id_pais}", Name = "GetPais")]
        public IActionResult Pesquisar([FromRoute] string id_pais)
        {
            var model = _pais.Pesquisar(x => x.Id == id_pais).ToList();

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Estado>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id_pais}/estado/", Name = "GetPaisEstados")]
        public IActionResult PesquisarEstados([FromRoute] string id_pais)
        {
            var model = _estado.Pesquisar(x => x.IdPais == id_pais).ToList();

            if (model == null)
                return NotFound();

            return Ok(model);
        }
    }
}
