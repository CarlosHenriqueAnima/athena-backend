using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Intercfaces;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Configurations.Enums;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace AthenasAcademy.Services.Core.Services;

public class AutorizaUsuarioService : IAutorizaUsuarioService
{
    readonly HttpContext _httpContext;
    readonly ITokenService _tokenService;
    readonly IUsuarioRepository _usuarioRepository;

    public AutorizaUsuarioService(IHttpContextAccessor httpContextAccessor, ITokenService tokenService, IUsuarioRepository usuarioRepository)
    {
        _httpContext = httpContextAccessor.HttpContext;
        _tokenService = tokenService;
        _usuarioRepository = usuarioRepository;
        
    }

    public async Task<NovoUsuarioResponse> CadastrarUsuario(NovoUsuarioRequest novoUsuario)
    {
        // Validar se usuario-email já existe
        if (_usuarioRepository.BuscarUsuario(
            new() { Email = novoUsuario.Email.Trim().ToLower() }) is null)
        {
            throw new CustomAPIException(
                message: $"O e-mail {novoUsuario.Email} já está sendo utilizado por outro usuário.",
                responseType: ExceptionResponseType.Warning,
                statusCode: HttpStatusCode.BadRequest);
        }

        // Cria um novo usuário
        NovoUsuarioArgument usuarioArgument = new()
        {
            Usuario = novoUsuario.Email.Trim().ToLower(),
            Email = novoUsuario.Email.Trim().ToLower(),
            Senha = GerarHashDeSenhaSHA256(novoUsuario.Senha),

            Ativo = true,
            DataCadastro = DateTime.Now,
            DataAlteracao = DateTime.Now
        };
        bool result = await _usuarioRepository.CadastrarUsuario(usuarioArgument) != null;

        // Verifica se a criação do usuário foi bem-sucedida
        if (!result)
        {
            throw new CustomAPIException(
                message: "Não foi possível efetivar cadastro do usuário.",
                responseType: ExceptionResponseType.Warning,
                statusCode: HttpStatusCode.InternalServerError);
        }
        else
        {
            _httpContext.Response.StatusCode = (int)HttpStatusCode.Created;
            return new NovoUsuarioResponse()
            {
                Resultado = result,
                Token = await _tokenService.GerarTokenAsync(
                    new() { Usuario = novoUsuario.Email.Trim().ToLower() })
            };
        }
    }

    public async Task<LoginUsuarioResponse> LoginUsuario(LoginUsuarioRequest loginUsuario)
    {
        UsuarioModel usuario = await _usuarioRepository.BuscarUsuario(new() { Email = loginUsuario.Email.Trim().ToLower() });

        // Validar se usuario-email existe
        if (usuario is null)
        {
            throw new CustomAPIException(
                message: $"O usuário {loginUsuario.Email} não foi localizado.",
                responseType: ExceptionResponseType.Warning,
                statusCode: HttpStatusCode.Unauthorized);
        }

        // Verifica credenciais
        if (!ValidarHashDeSenhaSHA256(usuario.Senha, loginUsuario.Senha))
        {
            throw new CustomAPIException(
                message: "Login inválido!",
                responseType: ExceptionResponseType.Warning,
                statusCode: HttpStatusCode.Unauthorized);
        }

        // Retorna sucesso no login
        return await Task.FromResult(new LoginUsuarioResponse
        {
            Resultado = true,
            Token = await _tokenService.GerarTokenAsync(
                user: new UsuarioModel { Usuario = loginUsuario.Email })
        });
    }

    private string GerarHashDeSenhaSHA256(string senha)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(senha);
            byte[] hashedBytes = sha256.ComputeHash(passwordBytes);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashedBytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }

    private bool ValidarHashDeSenhaSHA256(string senhaHashBanco, string senhaHashLogin)
    {
        string senhaHashLoginCalculado = GerarHashDeSenhaSHA256(senhaHashLogin);
        return senhaHashBanco == senhaHashLoginCalculado;
    }
}