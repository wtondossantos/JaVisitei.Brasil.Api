using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1")]
    [ControllerName("Regiões")]
    [Route("api/v{version:apiVersion}/regiao")]
    public class TipoRegioesController : ControllerBase
    {
        private readonly ITipoRegiaoService _tiporegiao;

        public TipoRegioesController(ITipoRegiaoService tiporegiao)
        {
            _tiporegiao = tiporegiao;
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TipoRegiao>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("tipo", Name = "GetTipoRegioes")]
        public IActionResult Pesquisar()
        {
            var lista = _tiporegiao.Pesquisar();

            if (lista == null)
                return NotFound();

            return Ok(lista);
        }
    }
}
