using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.Validations;
using JaVisitei.Brasil.Business.ViewModels.Request;
using JaVisitei.Brasil.Business.ViewModels.Response;
using JaVisitei.Brasil.Helper;
using JaVisitei.Brasil.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

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

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Visita>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id_usuario}", Name = "GetVisitasUsuario")]
        public IActionResult Pesquisar([FromRoute] int id_usuario)
        {
            var lista = _visita.Pesquisar(x => x.IdUsuario == id_usuario).ToList();

            if (lista == null)
                return NotFound();

            return Ok(lista);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("{id_usuario}", Name = "PostVisita")]
        public IActionResult Adicionar([FromRoute] int id_usuario, [FromBody] VisitaAdicionarRequest model)
        {
            if (ModelState.IsValid)
            {
                var validacao = new VisitaValidation();
                var retorno = new ValidacaoResponse();
                var mensagens = new List<string>();
                

                retorno.Sucesso = false;
                retorno.Codigo = 0;

                try
                {
                    if (_visita.Pesquisar(x => x.IdUsuario == id_usuario && x.IdTipoRegiao == model.IdTipoRegiao && x.IdRegiao == model.IdRegiao).ToList().Count > 0)
                        mensagens.Add("Visita já registrada.");

                    else
                    {
                        retorno.Mensagem = validacao.ValidaRegistroVisita(model);

                        if (_usuario.Pesquisar(x => x.Id == id_usuario).ToList().Count <= 0)
                            retorno.Mensagem.Add("Usuário não encontrado.");

                        if (retorno.Mensagem.Count > 0)
                            return Ok(retorno);

                        var visita = new Visita
                        {
                            IdUsuario = id_usuario,
                            IdTipoRegiao = model.IdTipoRegiao,
                            IdRegiao = model.IdRegiao,
                            Cor = model.Cor == null ? Util.RandomHexString() : model.Cor,
                            Data = model.Data == null ? DateTime.Now : model.Data
                        };

                        _visita.Adicionar(visita);

                        retorno.Sucesso = true;
                        retorno.Codigo = 1;
                        retorno.Mensagem.Add("Visita registrada com sucesso.");
                    }
                }
                catch
                {
                    retorno.Mensagem.Add("Erro ao registrar visita. Exceção.");
                    retorno.Codigo = -1;
                }

                return Ok(retorno);

            }
            return BadRequest();
        }
    }
}
