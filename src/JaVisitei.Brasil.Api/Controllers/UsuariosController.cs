using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("{username}", Name = "GetUsuarioUsername")]
        [ProducesResponseType(statusCode: 200, Type = typeof(List<Usuario>))]
        [ProducesResponseType(statusCode: 404)]
        [ProducesResponseType(statusCode: 500)]
        public IActionResult PesquisarUsuarioUsername(string username)
        {
            var model = _usuario.Pesquisar(x => x.NomeUsuario == username).ToList();

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        //[AllowAnonymous]
        //[HttpPost(Name = "PostUsuario")]
        //[ProducesResponseType(statusCode: 201)]
        //[ProducesResponseType(statusCode: 404)]
        //[ProducesResponseType(statusCode: 500)]
        //public IActionResult AdicionarUsuario([FromBody] UsuarioAdicionarRequest model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var validacao = new Validations();
        //        var retorno = new ValidacaoResponse();
        //        var mensagens = new List<string>();
        //        retorno.Sucesso = false;
        //        retorno.Codigo = 0;

        //        try
        //        {
        //            if (_usuario.Pesquisar(x => x.Email == model.Email).ToList().Count > 0)
        //                retorno.Mensagem.Add("Já existe usuário com este e-mail.");

        //            else
        //            {
        //                mensagens = validacao.ValidaRegistroUsuario(model);

        //                if (retorno.Mensagem.Count > 0)
        //                    return Ok(retorno);

        //                var usuario = new Usuario()
        //                {
        //                    Nome = model.Nome,
        //                    Sobrenome = model.Sobrenome,
        //                    NomeUsuario = model.NomeUsuario,
        //                    Email = model.Email,
        //                    Senha = LoginHash.Sha256encrypt(model.Senha)
        //                };

        //                _usuario.Adicionar(usuario);

        //                retorno.Codigo = 1;
        //                retorno.Sucesso = true;
        //                retorno.Mensagem.Add($"Usuário {model.NomeUsuario}, {model.Email} registrado com sucesso.");
        //            }
        //        }
        //        catch
        //        {
        //            retorno.Codigo = -1;
        //            retorno.Mensagem.Add("Erro ao registrar usuário.");
        //        }

        //        return Ok(retorno);
        //    }
        //    return BadRequest();
        //}

        //[AllowAnonymous]
        //[HttpPost("{id_usuario}", Name = "PostUsuarioId")]
        //[ProducesResponseType(statusCode: 201)]
        //[ProducesResponseType(statusCode: 404)]
        //[ProducesResponseType(statusCode: 500)]
        //public IActionResult AlterarUsuario([FromRoute] int id_usuario, [FromBody] UsuarioAlterarRequest model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var validacao = new Validations();
        //        var retorno = new ValidacaoResponse();
        //        var mensagens = new List<string>();
        //        retorno.Sucesso = false;
        //        retorno.Codigo = 0;

        //        try
        //        {
        //            var usuario = _usuario.Pesquisar(x => x.Id == id_usuario).FirstOrDefault();

        //            if (usuario == null)
        //            {
        //                retorno.Mensagem.Add("Usuário não encontrado.");
        //                return Ok(retorno);
        //            }

        //            var resultado = _usuario.Autenticacao(new Usuario()
        //            {
        //                Email = usuario.Email,
        //                Senha = LoginHash.Sha256encrypt(model.SenhaAntiga)
        //            });

        //            if (resultado == null && String.IsNullOrEmpty(resultado.Senha))
        //            {
        //                retorno.Mensagem.Add("Senha antiga incorreta.");
        //                return Ok(retorno);
        //            }

        //            mensagens = validacao.ValidaAlteracaoUsuario(model, usuario.Email);
        //            retorno.Mensagem = mensagens;

        //            if (retorno.Mensagem.Count > 0)
        //                return Ok(retorno);

        //            usuario.Nome = model.Nome;
        //            usuario.Sobrenome = model.Sobrenome;

        //            if (_usuario.Pesquisar(x => x.Id != id_usuario && x.NomeUsuario == model.NomeUsuario).ToList().Count > 0)
        //            {
        //                retorno.Mensagem.Add("Já existe usuário cadastrado com esse nome de usuário.");
        //                return Ok(retorno);
        //            }
        //            else
        //                usuario.NomeUsuario = model.NomeUsuario;

        //            if (_usuario.Pesquisar(x => x.Id != id_usuario && x.Email == model.Email).ToList().Count > 0)
        //            {
        //                retorno.Mensagem.Add("Já existe usuário cadastrado com esse e-mail.");
        //                return Ok(retorno);
        //            }
        //            else
        //                usuario.Email = model.Email;

        //            if (!string.IsNullOrEmpty(model.Senha))
        //                usuario.Senha = LoginHash.Sha256encrypt(model.Senha);

        //            _usuario.Alterar(usuario);

        //            retorno.Codigo = 1;
        //            retorno.Sucesso = true;
        //            retorno.Mensagem.Add($"Usuário {model.Email} atualizado com sucesso.");
        //        }
        //        catch (Exception)
        //        {
        //            retorno.Mensagem.Add("Erro ao alterar usuário.");
        //            retorno.Codigo = -1;
        //        }

        //        return Ok(retorno);
        //    }
        //    return BadRequest();
        //}
    }
}
