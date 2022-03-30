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
    [ControllerName("Municípios Brasileiros")]
    [Route("api/v{version:apiVersion}/municipio")]
    public class MunicipiosController : ControllerBase
    {
        private readonly IMunicipioService _municipio;

        public MunicipiosController(IMunicipioService municipio)
        {
            _municipio = municipio;
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Municipio>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(Name = "GetMunicipios")]
        public IActionResult Pesquisar()
        {
            var lista = _municipio.Pesquisar();

            if (lista == null)
                return NotFound();

            return Ok(lista);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Municipio))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id_municipio}", Name = "GetMunicipio")]
        public IActionResult Pesquisar([FromRoute] string id_municipio)
        {
            var model = _municipio.Pesquisar(x => x.Id == id_municipio).ToList();

            if (model == null)
                return NotFound();

            return Ok(model);
        }
    }
}
