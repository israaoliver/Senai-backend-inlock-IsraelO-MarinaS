using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.InLock.WebApi.Domains;
using Senai.InLock.WebApi.Interfaces;
using Senai.InLock.WebApi.Repositories;

namespace Senai.InLock.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private IUsuarioRepository _usuarioRepository { get; set; }

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        /// <summary>
        /// Lista todos os Usuarios
        /// </summary>
        /// <returns>Retorna uma Lista de Usuarios</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "2")]
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var usuarios = _usuarioRepository.Listar();

                return Ok(usuarios);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }


        /// <summary>
        /// Busca u usuario pelo Id
        /// </summary>
        /// <returns>Retorna um objeto usuario </returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "2")]
        [HttpGet ("{id}")]
        public IActionResult BuscarId(int id)
        {
            var user = _usuarioRepository.BuscarId(id);
            if (user != null)
            {
                return Ok(user);
            }

            return NotFound("Nunhum usuario encontrado com esse id");
        }


        /// <summary>
        /// Cadastra um novo usuario
        /// </summary>
        ///<remarks>
        /// Sample response:
        /// 
        ///      {
        ///        "Email": "Name",
        ///        "Senha" : "password",
        ///        "IdTipoUsuario" : 0
        ///       }
        ///     
        ///</remarks>
        /// <param name="user">Objeto Usuario que vai ser cadastrado</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public IActionResult Cadastrar(UsuarioDomain user)
        {
            try
            {
                if ((user.Email == "") || (user.Senha == "") || (user.IdTipoUsuario == 0))
                {
                    return BadRequest("Preecha todos os campos!");
                }

                _usuarioRepository.Cadastrar(user);
                return StatusCode(201);

            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        /// <summary>
        /// Remove um Usuario
        /// </summary>
        /// <param name="id">paramentro para encontrar o usuario a ser removido</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "2")]
        [HttpDelete ("{id}")]
        public IActionResult Remover(int id)
        {
            var userBuscado = _usuarioRepository.BuscarId(id);

            if (userBuscado == null)
            {
                return NotFound("Usuario não encontrado! E nada acontece camarada");
            }

            _usuarioRepository.Remover(id);
            return Ok("Usuario Removido");
        }
    }
}