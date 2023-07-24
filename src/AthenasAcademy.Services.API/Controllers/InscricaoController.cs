using AthenasAcademy.Services.Core.Configurations.Enums;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Services;
using AthenasAcademy.Services.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AthenasAcademy.Services.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class InscricaoController : ControllerBase
{
    private readonly InscricaoService _inscricaoService;

    public InscricaoController(InscricaoService inscricaoService)
    {
        _inscricaoService = inscricaoService;
    }

    /// <summary>
    /// Obtém uma inscrição por ID.
    /// </summary>
    /// <param name="matriculaId">ID da inscrição a ser buscada.</param>
    /// <returns>Objeto contendo informações do candidato.</returns>
    [HttpGet("inscricao/{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(InscricaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CandidatoModel>> ObterInscricaoPorIdAsync(int matriculaId)
    {
        var inscricao = await _inscricaoService.ObterInscricaoPorIdAsync(matriculaId);
        if (inscricao == null)
        {
            return NotFound();
        }

        return inscricao;
    }

    /// <summary>
    /// Obtém todas as inscrições pendentes.
    /// </summary>
    /// <returns>Objeto contendo informações do candidato.</returns>
    [HttpGet("inscricao/pendentes")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(InscricaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<CandidatoModel>>> ObterInscricoesPendentesAsync()
    {
        var inscricoes = await _inscricaoService.ObterInscricoesPendentesAsync();
        return Ok(inscricoes);
    }

    /// <summary>
    /// Cadastra uma nova inscrição.
    /// </summary>
    /// <param name="inscricao">Objeto contendo os dados da nova inscrição.</param>
    /// <returns>Objeto contendo informações da inscrição cadastrada.</returns>
    [HttpPost("inscricao/cadastrar")]
    [Authorize(Roles = nameof(Role.Administrador))]
    [ProducesResponseType(typeof(InscricaoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AdicionarInscricaoAsync([FromBody] CandidatoModel inscricao)
    {
        await _inscricaoService.AdicionarInscricaoAsync(inscricao);
        return CreatedAtAction(nameof(ObterInscricaoPorIdAsync), new { id = inscricao.Id }, inscricao);
    }

    /// <summary>
    /// Atualiza uma inscrição existente.
    /// </summary>
    /// <param name="inscricao">Objeto contendo a inscrição atualizada.</param>
    /// <param name="inscricaoId">Id da inscrição atualizada</param>
    /// <returns>Objeto contendo informações da inscrição atualizada.</returns>
    [HttpPut("inscricao/atualizar")]
    [Authorize(Roles = nameof(Role.Administrador))]
    [ProducesResponseType(typeof(InscricaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AtualizarInscricaoAsync(int inscricaoId, [FromBody] CandidatoModel inscricao)
    {
        if (inscricaoId != inscricao.Id)
        {
            return BadRequest();
        }

        await _inscricaoService.AtualizarInscricaoAsync(inscricao);
        return NoContent();
    }

    /// <summary>
    /// Cancela uma inscricão.
    /// </summary>
    /// <param name="inscricaoId">ID da inscrição a ser cancelada.</param>
    /// <returns>Confirmação de inscrição cancelada.</returns>
    [HttpDelete("inscricao/cancelar/{id:int}")]
    [Authorize(Roles = nameof(Role.Administrador))]
    [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CancelarInscricaoAsync(int inscricaoId)
    {
        var inscricao = await _inscricaoService.ObterInscricaoPorIdAsync(inscricaoId);
        if (inscricao == null)
        {
            return NotFound();
        }

        await _inscricaoService.CancelarInscricaoAsync(inscricaoId);
        return NoContent();
    }
}
