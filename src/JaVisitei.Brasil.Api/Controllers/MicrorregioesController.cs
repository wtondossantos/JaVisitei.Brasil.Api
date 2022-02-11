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
    [ControllerName("Microrregiões Brasileiras")]
    [Route("api/v{version:apiVersion}/microrregiao")]
    public class MicrorregioesController : ControllerBase
    {
        private readonly IMicrorregiaoService _microrreigao;
        private readonly IMunicipioService _municipio;

        public MicrorregioesController(IMicrorregiaoService microrreigao,
            IMunicipioService municipio)
        {
            _microrreigao = microrreigao;
            _municipio = municipio;
        }

        [HttpGet(Name = "GetMicrorregioes")]
        [ProducesResponseType(statusCode: 200, Type = typeof(List<Microrregiao>))]
        public IActionResult Pesquisar()
        {
            var lista = _microrreigao.Pesquisar();

            if (lista == null)
                return NotFound();

            return Ok(lista);
        }

        [HttpGet("{id_microrregiao}", Name = "GetMicrorregiao")]
        public IActionResult Pesquisar([FromRoute] string id_microrregiao)
        {
            var model = _microrreigao.Pesquisar(x => x.Id == id_microrregiao).ToList();

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpGet("{id_microrregiao}/municipio/", Name = "GetMicrorregiaoMunicipios")]
        public IActionResult PesquisarMunicipios([FromRoute] string id_microrregiao)
        {
            var model = _municipio.Pesquisar(x => x.IdMicrorregiao == id_microrregiao).ToList();

            if (model == null)
                return NotFound();

            return Ok(model);
        }
    }
}
