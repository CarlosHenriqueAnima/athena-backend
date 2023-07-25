using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Services.Interfaces;

using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;

using Microsoft.AspNetCore.Mvc;

namespace AthenasAcademy.Services.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class MatriculaController : ControllerBase
{

            private readonly IMatriculaService _matriculaService;

            public MatriculaController(IMatriculaService matriculaService)
            {
                _matriculaService = matriculaService;
            }

            [HttpGet("{id:int}")]
            [ProducesResponseType(typeof(MatriculaModel), StatusCodes.Status200OK)]
            [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
            [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
            public async Task<ActionResult> ObterMatriculaPorIdAsync(int id)
            {
                var matricula = await _matriculaService.ObterMatricula(id);
                if (matricula == null)
                {
                    return NotFound();
                }

                return Ok(matricula);
                
            }

            [HttpGet("todas")]
            [ProducesResponseType(typeof(IEnumerable<MatriculaModel>), StatusCodes.Status200OK)]
            [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
            [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
            public async Task<ActionResult> ObterTodasMatriculasAsync()
            {
                var matriculas = await _matriculaService.ObterTodasMatriculas();
                return Ok(matriculas);
            }


            [HttpPost("registrar")]
            [ProducesResponseType(typeof(NovaMatriculaResponse), StatusCodes.Status201Created)]
            [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
            [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
            public async Task<ActionResult> CadastrarMatricula([FromBody] Domain.Requests.NovaMatriculaRequest request)
            {
                var matriculaModel = new MatriculaModel
                {
                    AlunoId = request.AlunoId,
                    DataMatricula = request.DataMatricula,
                    Curso = request.Curso
                };

                var novaMatriculaResponse = await _matriculaService.CadastrarMatricula(matriculaModel);
                return CreatedAtAction(nameof(ObterMatriculaPorIdAsync), new { id = novaMatriculaResponse.Id }, novaMatriculaResponse);
            }

            [HttpPut("atualizar")]
            [ProducesResponseType(typeof(MatriculaModel), StatusCodes.Status200OK)]
            [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
            [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
            public async Task<ActionResult> AtualizarMatricula([FromBody] MatriculaRequest request)
            {
                if (request == null)
                {
                    return BadRequest();
                }

                var matriculaModel = new MatriculaModel
                {
                    Id = request.Id,
                    AlunoId = request.AlunoId,
                    DataMatricula = request.DataMatricula,
                    Curso = request.Curso
                };

                await _matriculaService.AtualizarMatricula(matriculaModel);
                return Ok(matriculaModel);
            }

            [HttpDelete("desativar/{id:int}")]
            [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
            [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
            [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
            public async Task<ActionResult> DesativarMatricula(int id)
            {
                var matricula = await _matriculaService.ObterMatricula(id);
                if (matricula == null)
                {
                    return NotFound();
                }

                await _matriculaService.DesativarMatricula(id);
                return NoContent();
            }

}