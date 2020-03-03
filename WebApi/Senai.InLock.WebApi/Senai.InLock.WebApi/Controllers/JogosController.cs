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
    [Authorize]
    public class JogosController : ControllerBase
    {

        private IJogosRepository _jogosRepository { get; set; }

        public JogosController()
        {
            _jogosRepository = new JogosRepository();
        }

        /// <summary>
        /// Lista todo os Jogos do Banco de dados
        /// </summary>
        /// <returns>Uma lista com os jogos</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public IActionResult ListarTodos()
        {
            
                var jogos = _jogosRepository.ListaTodos();

                return Ok(jogos);
            
            
        }

        /// <summary>
        /// Busca o jogo pelo id informado
        /// </summary>
        /// <param name="id">Id do jogo que sera buscado</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public IActionResult BuscarId(int id)
        {
            var jogo = _jogosRepository.BuscarId(id);
            if (jogo != null)
            {
                return Ok(jogo);
            }

            return NotFound("Nunhum Jogo encontrado com esse id");
        }

        /// <summary>
        /// Cadastra um novo jogo
        /// </summary>
        /// <remarks>
        /// Sample response:
        /// 
        ///      {
        ///        "NomeJogo": "Name",
        ///        "Descricao" : "description",
        ///        "Datalancamento" : "2015-08-27T15:00:00.000+00:00",
        ///        "Valor" : 0,
        ///        "IdEstudio": 0
        ///        }
        ///     
        ///</remarks>
        /// <param name="jogo">Objeto que sera cadastrado</param>
        /// <returns>o proprio objeto</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Roles = "2")]
        [HttpPost]
        public IActionResult Post(JogoDomain jogo)
        {
            _jogosRepository.CadastrarJogo(jogo);

            return StatusCode(201);
        }

        /// <summary>
        /// Deleta um jogo pelo id informado
        /// </summary>
        /// <param name="id">Id que sera usado para encontrar o jogo certo a ser deletado</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "2")]
        [HttpDelete ("{id}")]
        public IActionResult Delete(int id)
        {
            var jogoBuscado = _jogosRepository.BuscarId(id);

            if(jogoBuscado == null)
            {
                return NotFound("jogo não encontrado! E nada acontece camarada");
            }

            _jogosRepository.Deletar(id);
            return Ok("Jogo deletado");
        }

        
        
    }
}