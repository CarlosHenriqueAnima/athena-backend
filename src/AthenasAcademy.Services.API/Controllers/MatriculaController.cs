using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Responses;
using Microsoft.AspNetCore.Http;
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
    [ProducesResponseType(typeof(MatriculaModel), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CadastrarMatricula([FromBody] MatriculaModel matricula)
    {
        await _matriculaService.CadastrarMatricula(matricula);
        return CreatedAtAction(nameof(ObterMatriculaPorIdAsync), new { id = matricula.Id }, matricula);
    }

    [HttpPut("atualizar")]
    [ProducesResponseType(typeof(MatriculaModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AtualizarMatricula([FromBody] MatriculaModel matricula)
    {
        if (matricula == null)
        {
            return BadRequest();
        }

        await _matriculaService.AtualizarMatricula(matricula);
        return Ok(matricula);
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