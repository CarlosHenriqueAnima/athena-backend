using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Configurations.Mappers;
using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
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
    private readonly HttpContext _httpContext;
    private readonly ITokenService _tokenService;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IObjectConverter _mapper;

    public AutorizaUsuarioService(
        IHttpContextAccessor httpContextAccessor,
        ITokenService tokenService,
        IUsuarioRepository usuarioRepository,
        IObjectConverter objectConverter)
    {
        _httpContext = httpContextAccessor.HttpContext;
        _tokenService = tokenService;
        _usuarioRepository = usuarioRepository;
        _mapper = objectConverter;
    }

    public async Task<NovoUsuarioResponse> CadastrarUsuario(NovoUsuarioRequest novoUsuario)
    {
        try
        {
            await ValidarUsuarioExistente(novoUsuario.Email);

            NovoUsuarioArgument argument = _mapper.Map<NovoUsuarioArgument>(novoUsuario);
            argument.Senha = GerarHashDeSenhaSHA256(novoUsuario.Senha);
            argument.Ativo = true;
            argument.DataCadastro = DateTime.Now;
            argument.DataAlteracao = null;

            UsuarioModel usuario = await _usuarioRepository.CadastrarUsuario(argument);

            TokenModel token = await ObterTokenNovoUsuario(usuario);

            NovoUsuarioResponse response = new()
            {
                Usuario = usuario.Usuario,
                Token = _mapper.Map<TokenResponse>(token)
            };

            return response;
        }
        catch (Exception ex)
        {
            throw new APICustomException(
                message: string.Format("Não foi possível efetivar cadastro do usuário. {0}", ex.Message),
                responseType: ExceptionResponseType.Warning,
                statusCode: HttpStatusCode.InternalServerError);
        }
    }

    public async Task<LoginUsuarioResponse> LoginUsuario(LoginUsuarioRequest loginUsuario)
    {
        UsuarioModel usuario = await _usuarioRepository.BuscarUsuario(new() { Email = loginUsuario.Email.Trim().ToLower() });

        // Validar se usuario-email existe
        if (usuario is null)
        {
            throw new APICustomException(
                message: $"O usuário {loginUsuario.Email} não foi localizado.",
                responseType: ExceptionResponseType.Warning,
                statusCode: HttpStatusCode.Unauthorized);
        }

        // Verifica credenciais
        if (!ValidarHashDeSenhaSHA256(usuario.Senha, loginUsuario.Senha))
        {
            throw new APICustomException(
                message: "Login inválido!",
                responseType: ExceptionResponseType.Warning,
                statusCode: HttpStatusCode.Unauthorized);
        }

        // Retorna sucesso no login
        return await Task.FromResult(new LoginUsuarioResponse
        {
            Resultado = true,
            Token = null
        });
    }

    public Task<IEnumerable<UsuarioResponse>> ObterUsuarios()
    {
        throw new NotImplementedException();
    }

    #region Métodos Privados
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

    private async Task ValidarUsuarioExistente(string usuario)
    {
        var usuarioBanco = await _usuarioRepository.BuscarUsuario(new() { Email = usuario.Trim().ToLower() });

        if (usuarioBanco is not null)
        {
            throw new APICustomException(
                message: string.Format("O e-mail {0} não está disponível para cadastro.", usuario),
                responseType: ExceptionResponseType.Warning,
                statusCode: HttpStatusCode.BadRequest);
        }
    }

    private async Task<TokenModel> ObterTokenNovoUsuario(UsuarioModel usuario)
    {
        UsuarioTokenModel usuarioToken = new()
        {
            Usuario = usuario.Usuario.Trim().ToLower(),
            Perfil = (int)usuario.Perfil
        };

        return await _tokenService.GerarTokenUsuario(usuarioToken);
    }
    #endregion

}