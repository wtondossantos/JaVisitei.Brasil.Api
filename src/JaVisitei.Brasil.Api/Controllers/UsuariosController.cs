using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.Validations;
using JaVisitei.Brasil.Business.ViewModels.Request;
using JaVisitei.Brasil.Business.ViewModels.Response;
using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JaVisitei.Brasil.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [ControllerName("Usuários")]
    [Route("api/v{version:apiVersion}/usuario")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuario;

        public UsuariosController(IUsuarioService usuario)
        {
            _usuario = usuario;
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Usuario>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(Name = "GetUsuarios")]
        public IActionResult Pesquisar()
        {
            var lista = _usuario.Pesquisar();

            if (lista == null)
                return NotFound();

            return Ok(lista);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Usuario))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{username}", Name = "GetUsuarioUsername")]
        public IActionResult PesquisarUsuarioUsername(string username)
        {
            var model = _usuario.Pesquisar(x => x.NomeUsuario == username).ToList();

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost(Name = "PostUsuario")]
        public IActionResult AdicionarUsuario([FromBody] UsuarioAdicionarRequest model)
        {
            if (ModelState.IsValid)
            {
                var validacao = new UsuarioValidation();
                var retorno = new ValidacaoResponse();
                retorno.Sucesso = false;
                retorno.Codigo = 0;
                retorno.Mensagem = new List<string>();

                try
                {
                    if (_usuario.Pesquisar(x => x.Email == model.Email || x.NomeUsuario == model.NomeUsuario).ToList().Count > 0)
                    {
                        retorno.Mensagem.Add("Já existe usuário com este e-mail e/ou usuário.");
                        return Ok(retorno);
                    }

                    else
                    {
                        retorno.Mensagem = validacao.ValidaRegistroUsuario(model);
                        if (retorno.Mensagem != null && retorno.Mensagem.Count > 0)
                            return Ok(retorno);

                        var usuario = new Usuario()
                        {
                            Nome = model.Nome,
                            Sobrenome = model.Sobrenome,
                            NomeUsuario = model.NomeUsuario,
                            Email = model.Email,
                            Senha = Encriptar.Sha256encrypt(model.Senha)
                        };

                        _usuario.Adicionar(usuario);

                        retorno.Codigo = 1;
                        retorno.Sucesso = true;
                        retorno.Mensagem.Add($"Usuário {model.NomeUsuario}, {model.Email} registrado com sucesso.");
                    }
                }
                catch
                {
                    retorno.Codigo = -1;
                    retorno.Mensagem.Add("Erro ao registrar usuário.");
                }

                return Ok(retorno);
            }
            return BadRequest();
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("{id_usuario}", Name = "PostUsuarioId")]
        public IActionResult AlterarUsuario([FromRoute] int id_usuario, [FromBody] UsuarioAlterarRequest model)
        {
            if (ModelState.IsValid)
            {
                var validacao = new UsuarioValidation();
                var retorno = new ValidacaoResponse();
                retorno.Sucesso = false;
                retorno.Codigo = 0;
                retorno.Mensagem = new List<string>();

                try
                {
                    var usuario = _usuario.Pesquisar(x => x.Id == id_usuario).FirstOrDefault();

                    if (usuario == null)
                    {
                        retorno.Mensagem.Add("Usuário não encontrado.");
                        return Ok(retorno);
                    }

                    retorno.Mensagem = validacao.ValidaAlteracaoUsuario(model);

                    if (retorno.Mensagem != null && retorno.Mensagem.Count > 0)
                        return Ok(retorno);

                    var resultado = _usuario.Autenticacao(new Usuario()
                    {
                        Email = usuario.Email,
                        Senha = model.SenhaAntiga
                    });

                    if (resultado == null)
                    {
                        retorno.Mensagem.Add("E-mail ou Senha antiga incorreto.");
                        return Ok(retorno);
                    }
                    else if (String.IsNullOrEmpty(resultado.Senha) || resultado.Senha != usuario.Senha)
                    {
                        retorno.Mensagem.Add("Senha antiga incorreta.");
                        return Ok(retorno);
                    }

                    usuario.Nome = model.Nome;
                    usuario.Sobrenome = model.Sobrenome;

                    if (_usuario.Pesquisar(x => x.Id != id_usuario && x.NomeUsuario == model.NomeUsuario).ToList().Count > 0)
                    {
                        retorno.Mensagem.Add("Já existe usuário cadastrado com esse nome de usuário.");
                        return Ok(retorno);
                    }
                    else
                        usuario.NomeUsuario = model.NomeUsuario;

                    if (_usuario.Pesquisar(x => x.Id != id_usuario && x.Email == model.Email).ToList().Count > 0)
                    {
                        retorno.Mensagem.Add("Já existe usuário cadastrado com esse e-mail.");
                        return Ok(retorno);
                    }
                    else
                        usuario.Email = model.Email;

                    if (!string.IsNullOrEmpty(model.Senha))
                        usuario.Senha = Encriptar.Sha256encrypt(model.Senha);

                    _usuario.Alterar(usuario);

                    retorno.Codigo = 1;
                    retorno.Sucesso = true;
                    retorno.Mensagem.Add($"Usuário {model.Email} atualizado com sucesso.");
                }
                catch (Exception)
                {
                    retorno.Mensagem.Add("Erro ao alterar usuário.");
                    retorno.Codigo = -1;
                }

                return Ok(retorno);
            }
            return BadRequest();
        }
    }
}
