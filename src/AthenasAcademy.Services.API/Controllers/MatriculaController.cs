using AthenasAcademy.Services.Core.Configurations.Enums;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Services;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AthenasAcademy.Services.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class MatriculaController : ControllerBase
{
    private readonly MatriculaService _matriculaService;

    public MatriculaController(MatriculaService matriculaService)
    {
        _matriculaService = matriculaService;
    }

    /// <summary>
    /// Obtém uma matrícula por ID.
    /// </summary>
    /// <param name="matriculaId">ID da matrícula a ser buscada.</param>
    /// <returns>Objeto contendo informações do candidato.</returns>
    [HttpGet("matricula/{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(MatriculaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AlunoDetalhesModel>> ObterMatriculaPorId(int matriculaId)
    {
        var matricula = await _matriculaService.ObterMatriculaPorId(matriculaId);
        if (matricula == null)
        {
            return NotFound();
        }

        return matricula;
    }

    /// <summary>
    /// Obtém todas as matrículas.
    /// </summary>
    /// <returns>Lista de objetos contendo informações das matrículas.</returns>
    [HttpGet("matricula/todas")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(MatriculaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<AlunoDetalhesModel>>> ObterTodasMatriculas()
    {
        var matriculas = await _matriculaService.ObterTodasMatriculas();
        return Ok(matriculas);
    }

    /// <summary>
    /// Cadastra uma matrícula.
    /// </summary>
    /// <param name="matricula">Objeto contendo os dados de matrícula.</param>
    /// <returns>Objeto contendo informações da matrícula cadastrada.</returns>
    [HttpPost("matricula/cadastrar")]
    [Authorize(Roles = nameof(Role.Administrador))]
    [ProducesResponseType(typeof(MatriculaResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AdicionarMatricula([FromBody] AlunoDetalhesModel matricula)
    {
        await _matriculaService.AdicionarMatricula(matricula);
        return CreatedAtAction(nameof(ObterMatriculaPorId), new { id = matricula.Id }, matricula);
    }

    /// <summary>
    /// Atualiza uma inscrição existente.
    /// </summary>
    /// <param name="matricula">Objeto contendo a inscrição atualizada.</param>
    /// <param name="matriculaId">Id da inscrição atualizada</param>
    /// <returns>Objeto contendo informações da inscrição atualizada.</returns>
    [HttpPut("matricula/atualizar")]
    [Authorize(Roles = nameof(Role.Administrador))]
    [ProducesResponseType(typeof(MatriculaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AtualizarMatricula(int matriculaId, [FromBody] AlunoDetalhesModel matricula)
    {
        if (matriculaId != matricula.Id)
        {
            return BadRequest();
        }

        await _matriculaService.AtualizarMatricula(matricula);
        return NoContent();
    }

    /// <summary>
    /// Cancela uma inscricão.
    /// </summary>
    /// <param name="matriculaId">ID da inscrição a ser cancelada.</param>
    /// <returns>Confirmação de inscrição cancelada.</returns>
    [HttpDelete("matricula/cancelar/{id:int}")]
    [Authorize(Roles = nameof(Role.Administrador))]
    [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CancelarMatricula(int matriculaId)
    {
        var matricula = await _matriculaService.ObterMatriculaPorId(matriculaId);
        if (matricula == null)
        {
            return NotFound();
        }

        await _matriculaService.CancelarMatricula(matriculaId);
        return NoContent();
    }
}
