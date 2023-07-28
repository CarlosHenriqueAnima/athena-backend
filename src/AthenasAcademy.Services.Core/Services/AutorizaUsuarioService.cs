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
    private readonly ITokenService _tokenService;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IObjectConverter _mapper;

    public AutorizaUsuarioService(
        ITokenService tokenService,
        IUsuarioRepository usuarioRepository,
        IObjectConverter objectConverter)
    {
        _tokenService = tokenService;
        _usuarioRepository = usuarioRepository;
        _mapper = objectConverter;
    }

    #region Métodos Públicos
    public async Task<NovoUsuarioResponse> CadastrarUsuario(NovoUsuarioRequest novoUsuario)
    {
        try
        {
            await ValidarUsuarioExistenteCadastro(novoUsuario.Email, true);

            NovoUsuarioArgument argument = _mapper.Map<NovoUsuarioArgument>(novoUsuario);
            argument.Usuario = novoUsuario.Email.Trim().ToLower();
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
        UsuarioModel usuario = await ValidarUsuarioExistenteLogin(loginUsuario.Usuario);

        ValidarHashDeSenhaSHA256(usuario.Senha, loginUsuario.Senha, true);

        TokenModel token = await ObterTokenNovoUsuario(usuario);

        LoginUsuarioResponse response = new()
        {
            Resultado = true,
            Token = _mapper.Map<TokenResponse>(token),
            DadosUsuario = await ObterDadosUsuarioResponse(loginUsuario.Usuario)
    };

        return response;
    }

    public async Task<UsuarioModel> ObterUsuario(string usuario, bool exception)
    {
        return await ValidarUsuarioExistenteCadastro(usuario, exception);
    }
    #endregion

    #region Métodos Privados
    private static string GerarHashDeSenhaSHA256(string senha)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] passwordBytes = Encoding.UTF8.GetBytes(senha);
        byte[] hashedBytes = sha256.ComputeHash(passwordBytes);

        StringBuilder sb = new();
        foreach (byte b in hashedBytes)
        {
            sb.Append(b.ToString("x2"));
        }

        return sb.ToString();
    }

    private static bool ValidarHashDeSenhaSHA256(string senhaHashBanco, string senhaHashLogin, bool lancarException)
    {
        string senhaHashLoginCalculado = GerarHashDeSenhaSHA256(senhaHashLogin);
        bool senhaConfere = senhaHashBanco == senhaHashLoginCalculado;

        if (!senhaConfere && lancarException)
            throw new APICustomException(
                message: "Login inválido!",
                responseType: ExceptionResponseType.Warning,
                statusCode: HttpStatusCode.Unauthorized);

        return senhaConfere;
    }

    private async Task<UsuarioModel> ValidarUsuarioExistenteCadastro(string usuario, bool exception)
    {
        UsuarioModel usuarioBanco = await _usuarioRepository.BuscarUsuario(new() { Email = usuario.Trim().ToLower() });

        if (usuarioBanco is not null && exception)
            throw new APICustomException(
                message: string.Format("O e-mail {0} não está disponível para cadastro.", usuario),
                responseType: ExceptionResponseType.Warning,
                statusCode: HttpStatusCode.BadRequest);

        return usuarioBanco;
    }

    private async Task<UsuarioModel> ValidarUsuarioExistenteLogin(string usuario)
    {
        UsuarioModel usuarioBanco = await _usuarioRepository.BuscarUsuario(new() { Email = usuario.Trim().ToLower() });

        return usuarioBanco is null
            ? throw new APICustomException(
                message: string.Format("O usuário '{0}' não foi localizado.", usuario),
                responseType: ExceptionResponseType.Warning,
                statusCode: HttpStatusCode.Unauthorized)
            : usuarioBanco;
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

    public async Task<DadosUsuarioResponse> ObterDadosUsuarioResponse(string usuario)
    {
        IEnumerable<DadosUsuarioModel> dadosUsuarios = await _usuarioRepository.ObterDadosCompletosUsuario(usuario);

        if (dadosUsuarios == null || !dadosUsuarios.Any())
            return null;


        DadosUsuarioModel primeiroUsuario = dadosUsuarios.FirstOrDefault();

        return new DadosUsuarioResponse
        {
            IdUsuario = primeiroUsuario.IdUsuario,
            Login = primeiroUsuario.Login,
            DataCadastroUsuario = primeiroUsuario.DataCadastroUsuario,
            NomeAluno = primeiroUsuario.NomeAluno,
            CPF = primeiroUsuario.CPF,
            Sexo = primeiroUsuario.Sexo,
            DataNascimento = primeiroUsuario.DataNascimento,
            CodigoInscricao = primeiroUsuario.CodigoInscricao,
            DataInscricao = primeiroUsuario.DataInscricao,
            BoletoPago = primeiroUsuario.BoletoPago,
            DiretorioBoleto = primeiroUsuario.DiretorioBoleto,
            NomeCurso = primeiroUsuario.NomeCurso,
            DescricaoCurso = primeiroUsuario.DescricaoCurso,
            CargaHorariaCurso = primeiroUsuario.CargaHorariaCurso,
            AreaDoConhecimento = primeiroUsuario.AreaDoConhecimento,
            Matricula = primeiroUsuario.Matricula,
            Ativa = primeiroUsuario.Ativa,
            DataMatricula = primeiroUsuario.DataMatricula,
            Contrato = primeiroUsuario.Contrato,
            Assinado = primeiroUsuario.Assinado,
            FormaPagamento = primeiroUsuario.FormaPagamento,
            ValorPagamento = primeiroUsuario.ValorPagamento,
            DataAceite = primeiroUsuario.DataAceite,
            DiretorioContrato = primeiroUsuario.DiretorioContrato,
            Aproveitamento = primeiroUsuario.Aproveitamento,
            DataConclusao = primeiroUsuario.DataConclusao,
            Gerado = primeiroUsuario.Gerado,
            DiretorioCertificadoPDF = primeiroUsuario.DiretorioCertificadoPDF,
            DiretorioCertificadoPNG = primeiroUsuario.DiretorioCertificadoPNG,
            Disciplinas = dadosUsuarios.Select(d => new DadosDisciplinaUsuarioResponse
            {
                Disciplina = d.Disciplina,
                DescricaoDisciplina = d.DescricaoDisciplina,
                CargaHorariaDisciplina = d.CargaHorariaDisciplina
            }).ToList()
        };
    }
    #endregion

}