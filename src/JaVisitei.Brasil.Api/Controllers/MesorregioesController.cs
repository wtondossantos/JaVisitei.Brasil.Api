using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace JaVisitei.Brasil.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1")]
    [ControllerName("Mesorregiões Brasileiras")]
    [Route("api/v{version:apiVersion}/mesorregiao")]
    public class MesorregioesController : ControllerBase
    {
        private readonly IMesorregiaoService _mesorreigao;
        private readonly IMicrorregiaoService _microrreigao;
        private readonly IArquipelagoService _arquipelago;
        private readonly IMunicipioService _municipio;
        private readonly IIlhaService _ilha;

        public MesorregioesController(IMesorregiaoService mesorreigao,
            IMicrorregiaoService microrreigao,
            IArquipelagoService arquipelago,
            IMunicipioService municipio,
            IIlhaService ilha)
        {
            _mesorreigao = mesorreigao;
            _microrreigao = microrreigao;
            _arquipelago = arquipelago;
            _municipio = municipio;
            _ilha = ilha;
        }

        [HttpGet(Name = "GetMesorregioes")]
        [ProducesResponseType(statusCode: 200, Type = typeof(List<Mesorregiao>))]
        public IActionResult Pesquisar()
        {
            var lista = _mesorreigao.Pesquisar();

            if (lista == null)
                return NotFound();

            return Ok(lista);
        }

        [HttpGet("{id_mesorregiao}", Name = "GetMesorregiao")]
        public IActionResult Pesquisar([FromRoute] string id_mesorregiao)
        {
            var model = _mesorreigao.Pesquisar(x => x.Id == id_mesorregiao).ToList();

            if (model == null)
                return NotFound();

            return Ok(model);
        }
        [HttpGet("{id_mesorregiao}/microrregiao/", Name = "GetMesorregiaoMicrorregioes")]
        public IActionResult PesquisarMicrorregioes([FromRoute] string id_mesorregiao)
        {
            var model = _microrreigao.Pesquisar(x => x.IdMesorregiao == id_mesorregiao).ToList();

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpGet("{id_mesorregiao}/arquipelago/", Name = "GetMesorregiaoArquipelagos")]
        public IActionResult PesquisarArquipelagos([FromRoute] string id_mesorregiao)
        {
            var model = _arquipelago.Pesquisar(x => x.IdMesorregiao == id_mesorregiao).ToList();

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpGet("{id_mesorregiao}/municipio/", Name = "GetMesorregiaoMunicipios")]
        public IActionResult PesquisarMunicipios([FromRoute] string id_mesorregiao)
        {
            var model = _municipio.PesquisarPorMesorregiao(id_mesorregiao).ToList();

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpGet("{id_mesorregiao}/ilha/", Name = "GetMesorregiaoIlhas")]
        public IActionResult PesquisarIlhas([FromRoute] string id_mesorregiao)
        {
            var model = _ilha.PesquisarPorMesorregiao(id_mesorregiao).ToList();

            if (model == null)
                return NotFound();

            return Ok(model);
        }
    }
}

