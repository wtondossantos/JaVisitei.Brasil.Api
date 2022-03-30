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
    [ControllerName("Arquipélagos Brasilieros")]
    [Route("api/v{version:apiVersion}/arquipelago")]
    public class ArquipelagosController : ControllerBase
    {
        private readonly IArquipelagoService _arquipelago;
        private readonly IIlhaService _ilha;

        public ArquipelagosController(IArquipelagoService arquipelago,
            IIlhaService ilha)
        {
            _arquipelago = arquipelago;
            _ilha = ilha;
        }

        [Authorize]
        [HttpGet(Name = "GetArquipelagos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Arquipelago>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Pesquisar()
        {
            var lista = _arquipelago.Pesquisar();

            if (lista == null)
                return NotFound();

            return Ok(lista);
        }

        [Authorize]
        [HttpGet("{id_arquipelago}", Name = "GetArquipelago")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Arquipelago))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Pesquisar([FromRoute] string id_arquipelago)
        {
            var model = _arquipelago.Pesquisar(x => x.Id == id_arquipelago).ToList();

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [Authorize]
        [HttpGet("{id_arquipelago}/ilha/", Name = "GetArquipelagoIlhas")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Ilha>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult PesquisarIlhas([FromRoute] string id_arquipelago)
        {
            var model = _ilha.Pesquisar(x => x.IdArquipelago == id_arquipelago).ToList();

            if (model == null)
                return NotFound();

            return Ok(model);
        }
    }
}
