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
    public class EstudiosController : ControllerBase
    {
        private IEstudioRepository _estudioRepository { get; set; }

        public EstudiosController()
        {
            _estudioRepository = new EstudioRepository();
        }

        /// <summary>
        /// Retorna uma lista de estudios e seus jogos cadastrados
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var estudios = _estudioRepository.Listar();

                return Ok(estudios);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        /// <summary>
        /// Busca pelo nome o estudio
        /// </summary>
        ///<param name="nome">Nome do Estudio que ira achar o estudio(s)</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet ("{nome}")]
        public IActionResult BuscarEstudio(string nome)
        {
            var estudiosAchados = _estudioRepository.BuscarEstudio(nome);

            if(estudiosAchados.Count == 0)
            {
                return NotFound("Não existe estudios com esse nome");
            }
            return Ok(estudiosAchados);
        }

        /// <summary>
        /// Cadastra um novo estudio
        /// </summary>
        /// <remarks>
        /// Sample response:
        /// 
        ///     {
        ///         "NomeEstudio": "Name"
        ///     }
        ///     
        ///</remarks>
        /// <param name="estudio">Objeto Estudio que sera cadastrado</param>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "2")]
        [HttpPost]
        public IActionResult Cadastrar(EstudioDomain estudio)
        {
            try
            {
                if(estudio.NomeEstudio != "")
                {
                    _estudioRepository.Cadastrar(estudio);
                    return StatusCode(201);
                }
                return BadRequest("Preecha todos os campos!");

            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }
    }
}