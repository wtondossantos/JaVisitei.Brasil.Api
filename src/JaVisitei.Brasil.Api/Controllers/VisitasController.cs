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
    [ControllerName("Já Visitei")]
    [Route("api/v{version:apiVersion}/visita")]
    public class VisitasController : ControllerBase
    {
        private readonly IVisitaService _visita;
        private readonly IUsuarioService _usuario;

        public VisitasController(IVisitaService visita, IUsuarioService usuario)
        {
            _visita = visita;
            _usuario = usuario;
        }

        [HttpGet("{id_usuario}", Name = "GetVisitasUsuario")]
        [ProducesResponseType(statusCode: 200, Type = typeof(List<Visita>))]
        public IActionResult Pesquisar([FromRoute] int id_usuario)
        {
            var lista = _visita.Pesquisar(x => x.IdUsuario == id_usuario).ToList();

            if (lista == null)
                return NotFound();

            return Ok(lista);
        }

        //[HttpPost("{id_usuario}", Name = "PostVisita")]
        //[ProducesResponseType(statusCode: 200, Type = typeof(List<Visita>))]
        //public IActionResult Adicionar([FromRoute] int id_usuario, [FromBody] VisitaAdicionarRequest model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var validacao = new Validations();
        //        var retorno = new ValidacaoResponse();
        //        var mensagens = new List<string>();
        //        var helper = new Helper();

        //        retorno.Sucesso = false;
        //        retorno.Codigo = 0;

        //        try
        //        {
        //            if (_visita.Pesquisar(x => x.IdUsuario == id_usuario && x.IdTipoRegiao == model.IdTipoRegiao && x.IdRegiao == model.IdRegiao).ToList().Count > 0)
        //                mensagens.Add("Visita já registrada.");

        //            else
        //            {
        //                retorno.Mensagem = validacao.ValidaRegistroVisita(model);

        //                if (_usuario.Pesquisar(x => x.Id == id_usuario).ToList().Count <= 0)
        //                    retorno.Mensagem.Add("Usuário não encontrado.");

        //                if (retorno.Mensagem.Count > 0)
        //                    return Ok(retorno);

        //                var visita = new Visita
        //                {
        //                    IdUsuario = id_usuario,
        //                    IdTipoRegiao = model.IdTipoRegiao,
        //                    IdRegiao = model.IdRegiao,
        //                    Cor = model.Cor == null ? helper.RandomHexString() : model.Cor,
        //                    Data = model.Data == null ? DateTime.Now : model.Data.GetValueOrDefault()
        //                };

        //                _visita.Adicionar(visita);

        //                retorno.Sucesso = true;
        //                retorno.Codigo = 1;
        //                retorno.Mensagem.Add("Visita registrada com sucesso.");
        //            }
        //        }
        //        catch
        //        {
        //            retorno.Mensagem.Add("Erro ao registrar visita. Exceção.");
        //            retorno.Codigo = -1;
        //        }

        //        return Ok(retorno);

        //    }
        //    return BadRequest();
        //}
    }
}
