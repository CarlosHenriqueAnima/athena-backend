using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthenasAcademy.Services.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    //[Authorize]
    public class InscricaoController : ControllerBase
    {
        private readonly IInscricaoService _inscricaoService;

        public InscricaoController(IInscricaoService inscricaoService)
        {
            _inscricaoService = inscricaoService;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(InscricaoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ObterInscricaoPorId(int id)
        {
            var inscricao = await _inscricaoService.ObterInscricao(id);
            if (inscricao == null)
            {
                return NotFound();
            }

            return Ok(inscricao);
        }

        [HttpGet("todos")]
        [ProducesResponseType(typeof(IEnumerable<InscricaoModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ObterTodasInscricoes()
        {
            var inscricoes = await _inscricaoService.ObterTodasInscricoes();
            return Ok(inscricoes);
        }

        [HttpPost("registrar")]
        [ProducesResponseType(typeof(InscricaoModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CadastrarInscricao([FromBody] InscricaoModel inscricao)
        {
            await _inscricaoService.CadastrarInscricao(inscricao);
            return CreatedAtAction(nameof(ObterInscricaoPorId), new { id = inscricao.Id }, inscricao);
        }

        [HttpPut("atualizar")]
        [ProducesResponseType(typeof(InscricaoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AtualizarInscricao([FromBody] InscricaoModel inscricao)
        {
            if (inscricao == null)
            {
                return BadRequest();
            }

            await _inscricaoService.AtualizarInscricao(inscricao);
            return Ok(inscricao);
        }

        [HttpDelete("desativar/{id:int}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DesativarInscricao(int id)
        {
            var inscricao = await _inscricaoService.ObterInscricao(id);
            if (inscricao == null)
            {
                return NotFound();
            }

            await _inscricaoService.DesativarInscricao(id);
            return NoContent();
        }
    }
}
