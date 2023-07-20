using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AthenasAcademy.Services.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class UsuarioController : ControllerBase
{
    readonly IAutorizaUsuarioService _autorizaService;

    public UsuarioController(IAutorizaUsuarioService autorizaService)
    {
        _autorizaService = autorizaService;
    }

    /// <summary>
    /// Endpoint para cadastrar um novo usuário.
    /// </summary>
    /// <param name="request">Objeto contendo os dados do novo usuário.</param>
    /// <returns>Resposta com informações do usuário cadastrado.</returns>
    [HttpPost]
    [Route("registrar")]
    [ProducesResponseType(typeof(NovoUsuarioResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CadastrarUsuario([FromBody] NovoUsuarioRequest request)
    {
        return Ok(await _autorizaService.CadastrarUsuario(request));
    }

    /// <summary>
    /// Endpoint para realizar o login de um usuário.
    /// </summary>
    /// <param name="request">Objeto contendo as credenciais do usuário.</param>
    /// <returns>Resposta com informações do usuário logado.</returns>
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(typeof(LoginUsuarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> LoginUsuario([FromBody] LoginUsuarioRequest request)
    {
        return Ok(await _autorizaService.LoginUsuario(request));
    }

}