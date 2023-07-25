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
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoService _alunoService;

        public AlunoController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(AlunoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ObterAlunoPorIdAsync(int id)
        {
            var aluno = await _alunoService.ObterAlunoPorId(id);
            if (aluno == null)
            {
                return NotFound();
            }

            return Ok(aluno);
        }

        [HttpGet("todos")]
        [ProducesResponseType(typeof(IEnumerable<AlunoModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ObterTodosAlunosAsync()

         {

            var alunos = await _alunoService.ObterTodosAlunos();
            return Ok(alunos);
        }

        [HttpPost("registrar")]
        [ProducesResponseType(typeof(AlunoModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CadastrarAluno([FromBody] AlunoModel aluno)
        {
            await _alunoService.AdicionarAluno(aluno);
            return CreatedAtAction(nameof(ObterAlunoPorIdAsync), new { id = aluno.Id }, aluno);
        }

        [HttpPut("atualizar")]
        [ProducesResponseType(typeof(AlunoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AtualizarAluno([FromBody] AlunoModel aluno)
        {
            if (aluno == null)
            {
                return BadRequest();
            }

            await _alunoService.AtualizarAluno(aluno);
            return Ok(aluno);
        }

        [HttpDelete("desativar/{id:int}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DesativarAluno(int id)
        {
            var aluno = await _alunoService.ObterAlunoPorId(id);
            if (aluno == null)
            {
                return NotFound();
            }

            await _alunoService.DesativarAluno(id);
            return NoContent();
        }
    }
}
