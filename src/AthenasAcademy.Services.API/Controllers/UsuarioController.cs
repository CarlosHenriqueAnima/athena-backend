using AthenasAcademy.Services.Core.Configurations.Enums;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AthenasAcademy.Services.API.Controllers;

/// <summary>
/// Controlador responsável por gerenciar as operações relacionadas a usuários.
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IAutorizaUsuarioService _autorizaService;

    /// <summary>
    /// Controlador responsável por gerenciar as operações relacionadas a usuários.
    /// </summary>
    public UsuarioController(IAutorizaUsuarioService autorizaService)
    {
        _autorizaService = autorizaService;
    }

    /// <summary>
    /// Registra um novo usuário.
    /// </summary>
    /// <param name="request">Objeto contendo os dados do novo usuário.</param>
    /// <returns>Objeto contendo informações do usuário registrado.</returns>
    [HttpPost]
    [Route("registrar")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(NovoUsuarioResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CadastrarUsuario([FromBody] NovoUsuarioRequest request)
    {
        return Ok(await _autorizaService.CadastrarUsuario(request));
    }

    /// <summary>
    /// Realiza o login de um usuário.
    /// </summary>
    /// <param name="request">Objeto contendo as credenciais do usuário.</param>
    /// <returns>Objeto contendo informações do usuário logado.</returns>
    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginUsuarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> LoginUsuario([FromBody] LoginUsuarioRequest request)
    {
        return Ok(await _autorizaService.LoginUsuario(request));
    }
}