using AthenasAcademy.Services.Core.Configurations.Enums;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AthenasAcademy.Services.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[Authorize]
public class CursoController : ControllerBase
{
    private readonly ICursoService _cursoService;

    public CursoController(ICursoService cursoService)
    {
        _cursoService = cursoService;
    }

    #region Curso
    /// <summary>
    /// Obtém um curso pelo ID.
    /// </summary>
    /// <param name="id">ID do curso.</param>
    /// <returns>Objeto contendo informações do curso.</returns>
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CursoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ObterCurso(int id)
    {
        return Ok(await _cursoService.ObterCurso(id));
    }

    /// <summary>
    /// Obtém todos os cursos cadastrados.
    /// </summary>
    /// <returns>Lista de cursos cadastrados.</returns>
    [HttpGet("todos")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<CursoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ObterCursos()
    {
        return Ok(await _cursoService.ObterCursos());
    }

    /// <summary>
    /// Cadastra um novo curso.
    /// </summary>
    /// <param name="request">Objeto contendo os dados do novo curso.</param>
    /// <returns>Objeto contendo informações do curso cadastrado.</returns>
    [HttpPost("registrar")]
    [Authorize(Roles = nameof(Role.Administrador))]
    [ProducesResponseType(typeof(NovoCursoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CadastrarCurso([FromBody] NovoCursoRequest request)
    {
        return Ok(await _cursoService.CadastrarCurso(request));
    }

    /// <summary>
    /// Atualiza os dados de um curso existente.
    /// </summary>
    /// <param name="request">Objeto contendo os novos dados do curso.</param>
    /// <returns>Objeto contendo informações do curso atualizado.</returns>
    [HttpPut("atualizar/")]
    [Authorize(Roles = nameof(Role.Administrador))]
    [ProducesResponseType(typeof(CursoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AtualizarCurso([FromBody] CursoRequest request)
    {
        return Ok(await _cursoService.AtualizarCurso(request));
    }

    /// <summary>
    /// Desativa um curso existente.
    /// </summary>
    /// <param name="request">Objeto contendo informações do curso a ser desativado.</param>
    /// <returns>Indicação de sucesso na desativação do curso.</returns>
    [HttpDelete("desativar/{id:int}")]
    [Authorize(Roles = nameof(Role.Administrador))]
    [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DesativarCurso([FromBody] int id)
    {
        return Ok(await _cursoService.DesativarCurso(id));
    }
    #endregion

    #region Curso Disciplina
    /// <summary>
    /// Obtém uma disciplina pelo ID.
    /// </summary>
    /// <param name="id">ID da disciplina.</param>
    /// <returns>Objeto contendo informações da disciplina.</returns>
    [HttpGet("disciplina/{id:int}")]
    [ProducesResponseType(typeof(DisciplinaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ObterDisciplina(int id)
    {
        return Ok(await _cursoService.ObterDisciplina(id));
    }

    /// <summary>
    /// Obtém todas as disciplinas cadastradas.
    /// </summary>
    /// <returns>Lista de disciplinas cadastradas.</returns>
    [HttpGet("disciplina/todos")]
    [ProducesResponseType(typeof(IEnumerable<DisciplinaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ObterDisciplinas()
    {
        return Ok(await _cursoService.ObterDisciplinas());
    }

    /// <summary>
    /// Cadastra uma nova disciplina.
    /// </summary>
    /// <param name="request">Objeto contendo os dados da nova disciplina.</param>
    /// <returns>Objeto contendo informações da disciplina cadastrada.</returns>
    [HttpPost("disciplina/registrar")]
    [Authorize(Roles = nameof(Role.Administrador))]
    [ProducesResponseType(typeof(NovaDisciplinaResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CadastrarDisciplina([FromBody] NovaDisciplinaRequest request)
    {
        return Ok(await _cursoService.CadastrarDisciplina(request));
    }

    /// <summary>
    /// Atualiza os dados de uma disciplina existente.
    /// </summary>
    /// <param name="request">Objeto contendo os novos dados da disciplina.</param>
    /// <returns>Objeto contendo informações da disciplina atualizada.</returns>
    [HttpPut("disciplina/atualizar")]
    [Authorize(Roles = nameof(Role.Administrador))]
    [ProducesResponseType(typeof(DisciplinaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AtualizarDisciplina([FromBody] DisciplinaRequest request)
    {
        return Ok(await _cursoService.AtualizarDisciplina(request));
    }

    /// <summary>
    /// Desativa uma disciplina existente.
    /// </summary>
    /// <param name="request">Objeto contendo informações da disciplina a ser desativada.</param>
    /// <returns>Indicação de sucesso na desativação da disciplina.</returns>
    [HttpDelete("disciplina/{id:int}")]
    [Authorize(Roles = nameof(Role.Administrador))]
    [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DesativarDisciplina(int id)
    {
        return Ok(await _cursoService.DesativarDisciplina(id));
    }
    #endregion

    #region Curso Área Conhecimento
    /// <summary>
    /// Obtém uma área de conhecimento pelo ID.
    /// </summary>
    /// <param name="id">ID da área de conhecimento.</param>
    /// <returns>Objeto contendo informações da área de conhecimento.</returns>
    [HttpGet("area-conhecimento/{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AreaConhecimentoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ObterAreaConhecimento(int id)
    {
        return Ok(await _cursoService.ObterAreaConhecimento(id));
    }

    /// <summary>
    /// Obtém todas as áreas de conhecimento cadastradas.
    /// </summary>
    /// <returns>Lista de áreas de conhecimento cadastradas.</returns>
    [HttpGet("area-conhecimento/todos")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<AreaConhecimentoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ObterAreasConhecimento()
    {
        return Ok(await _cursoService.ObterAreasConhecimento());
    }

    /// <summary>
    /// Cadastra uma nova área de conhecimento.
    /// </summary>
    /// <param name="request">Objeto contendo os dados da nova área de conhecimento.</param>
    /// <returns>Objeto contendo informações da área de conhecimento cadastrada.</returns>
    [HttpPost("area-conhecimento/registrar")]
    [Authorize(Roles = nameof(Role.Administrador))]
    [ProducesResponseType(typeof(NovaAreaConhecimentoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CadastrarAreaConhecimento([FromBody] NovaAreaConhecimentoRequest request)
    {
        return Ok(await _cursoService.CadastrarAreaConhecimento(request));
    }

    /// <summary>
    /// Atualiza os dados de uma área de conhecimento existente.
    /// </summary>
    /// <param name="request">Objeto contendo os novos dados da área de conhecimento.</param>
    /// <returns>Objeto contendo informações da área de conhecimento atualizada.</returns>
    [HttpPut("area-conhecimento/atualizar")]
    [Authorize(Roles = nameof(Role.Administrador))]
    [ProducesResponseType(typeof(AreaConhecimentoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AtualizarAreaConhecimento([FromBody] AreaConhecimentoRequest request)
    {
        return Ok(await _cursoService.AtualizarAreaConhecimento(request));
    }

    /// <summary>
    /// Desativa uma área de conhecimento existente.
    /// </summary>
    /// <param name="request">Objeto contendo informações da área de conhecimento a ser desativada.</param>
    /// <returns>Indicação de sucesso na desativação da área de conhecimento.</returns>
    [HttpDelete("area-conhecimento/desativar/{id:int}")]
    [Authorize(Roles = nameof(Role.Administrador))]
    [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DesativarAreaConhecimento(int id)
    {
        return Ok(await _cursoService.DesativarAreaConhecimento(id));
    }
    #endregion
}