using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AthenasAcademy.Services.API.Controllers;

/// <summary>
/// Controlador responsável por gerenciar as inscrições dos candidatos.
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class InscricaoController : ControllerBase
{
    private readonly IInscricaoService _inscricaoService;

    /// <summary>
    /// Controlador responsável por gerenciar as inscrições dos candidatos.
    /// </summary>
    public InscricaoController(IInscricaoService inscricaoService)
    {
        _inscricaoService = inscricaoService;
    }

    /// <summary>
    /// Cadastra um novo candidato.
    /// </summary>
    /// <param name="request">Os dados do candidato a serem cadastrados.</param>
    /// <returns>Os dados do candidato cadastrado.</returns>
    [HttpPost("cadastrar-candidato")]
    [Authorize]
    [AllowAnonymous]
    [ProducesResponseType(typeof(InscricaoCandidatoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CadastrarCandidato([FromBody] NovaInscricaoCandidatoRequest request)
    {
        return Ok(await _inscricaoService.CadastrarCandidato(request));
    }
}