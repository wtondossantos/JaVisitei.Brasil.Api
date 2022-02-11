using JaVisitei.Brasil.Model.Models;
using JaVisitei.Brasil.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet(Name = "GetMunicipios")]
        [ProducesResponseType(statusCode: 200, Type = typeof(List<Municipio>))]
        public IActionResult Pesquisar()
        {
            var lista = _municipio.Pesquisar();

            if (lista == null)
                return NotFound();

            return Ok(lista);
        }

        [HttpGet("{id_municipio}", Name = "GetMunicipio")]
        [ProducesResponseType(statusCode: 200, Type = typeof(Municipio))]
        public IActionResult Pesquisar([FromRoute] string id_municipio)
        {
            var model = _municipio.Pesquisar(x => x.Id == id_municipio).ToList();

            if (model == null)
                return NotFound();

            return Ok(model);
        }
    }
}
