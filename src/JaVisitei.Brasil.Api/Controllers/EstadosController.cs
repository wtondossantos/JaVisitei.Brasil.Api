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
    [ControllerName("Estados Brasilieiros")]
    [Route("api/v{version:apiVersion}/estado")]
    public class EstadosController : ControllerBase
    {
        private readonly IEstadoService _estado;
        private readonly IMesorregiaoService _mesorreigao;
        private readonly IMicrorregiaoService _microrreigao;
        private readonly IArquipelagoService _arquipelago;
        private readonly IMunicipioService _municipio;
        private readonly IIlhaService _ilha;

        public EstadosController(IEstadoService estado,
            IMesorregiaoService mesorreigao,
            IMicrorregiaoService microrreigao,
            IArquipelagoService arquipelago,
            IMunicipioService municipio,
            IIlhaService ilha)
        {
            _estado = estado;
            _mesorreigao = mesorreigao;
            _microrreigao = microrreigao;
            _arquipelago = arquipelago;
            _municipio = municipio;
            _ilha = ilha;
        }

        [HttpGet(Name = "GetEstados")]
        [ProducesResponseType(statusCode: 200, Type = typeof(List<Estado>))]
        public IActionResult Pesquisar()
        {
            var lista = _estado.Pesquisar();

            if (lista == null)
                return NotFound();

            return Ok(lista);
        }

        [HttpGet("{id_estado}", Name = "GetEstado")]
        public IActionResult Pesquisar([FromRoute] string id_estado)
        {
            var model = _estado.Pesquisar(x => x.Id == id_estado).ToList();

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpGet("{id_estado}/mesorregiao/", Name = "GetEstadoMesorregioes")]
        public IActionResult PesquisarMesorregioes([FromRoute] string id_estado)
        {
            var model = _mesorreigao.Pesquisar(x => x.IdEstado == id_estado).ToList();

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpGet("{id_estado}/microrregiao/", Name = "GetEstadoMicrorregioes")]
        public IActionResult PesquisarMicrorregioes([FromRoute] string id_estado)
        {
            var model = _microrreigao.PesquisarPorEstado(id_estado).ToList();

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpGet("{id_estado}/arquipelago/", Name = "GetEstadoArquipelagos")]
        public IActionResult PesquisarArquipelagos([FromRoute] string id_estado)
        {
            var model = _arquipelago.PesquisarPorEstado(id_estado).ToList();

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpGet("{id_estado}/municipio/", Name = "GetEstadoMunicipios")]
        public IActionResult PesquisarMunicipios([FromRoute] string id_estado)
        {
            var model = _municipio.PesquisarPorEstado(id_estado).ToList();

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpGet("{id_estado}/ilha/", Name = "GetEstadoIlhas")]
        public IActionResult PesquisarIlhas([FromRoute] string id_estado)
        {
            var model = _ilha.PesquisarPorEstado(id_estado).ToList();

            if (model == null)
                return NotFound();

            return Ok(model);
        }
    }
}
