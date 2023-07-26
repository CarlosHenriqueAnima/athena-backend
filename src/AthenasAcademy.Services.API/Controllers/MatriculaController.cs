using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AthenasAcademy.Services.API.Controllers;


/// <summary>
/// Controller responsável pelas operações relacionadas à matrícula de alunos.
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class MatriculaController : ControllerBase
{
    private readonly IMatriculaService _matriculaService;

    /// <summary>
    /// Cria uma nova instância do controlador de matrícula de alunos.
    /// </summary>
    /// <param name="matriculaService">O serviço responsável pelas operações de matrícula.</param>
    public MatriculaController(IMatriculaService matriculaService)
    {
        _matriculaService = matriculaService;
    }

    /// <summary>
    /// Realiza a matrícula de um novo aluno.
    /// </summary>
    /// <param name="inscricao">Os dados da matrícula do aluno.</param>
    /// <returns>Um objeto contendo os detalhes da matrícula do aluno.</returns>
    [HttpPut("matricular-aluno/{inscricao:int}")]
    [Authorize(Roles = "Usuario, Admnistrador")]
    [ProducesResponseType(typeof(MatriculaStatusResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MatricularAluno(int inscricao)
    {
        return Ok(await _matriculaService.MatricularAluno(inscricao));
    }
}