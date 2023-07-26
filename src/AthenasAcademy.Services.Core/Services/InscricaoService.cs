using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.CrossCutting;
using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Core.Extensions;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;

namespace AthenasAcademy.Services.Core.Services;

public class InscricaoService : IInscricaoService
{
    private readonly IInscricaoRepository _inscricaoRepository;
    private readonly IAutorizaUsuarioService _usuarioService;
    private readonly ICursoService _cursoService;
    private readonly IAlunoService _alunoService;

    public InscricaoService(
        IInscricaoRepository inscricaoRepository,
        IAutorizaUsuarioService usuarioService,
        ICursoService cursoService,
        IAlunoService alunoService)
    {
        _inscricaoRepository = inscricaoRepository;
        _usuarioService = usuarioService;
        _cursoService = cursoService;
        _alunoService = alunoService;

    }

    public async Task<InscricaoCandidatoResponse> CadastrarCandidato(NovaInscricaoCandidatoRequest request)
    {
        UsuarioModel usuario = await ValidarUsuarioExistente(request);

        OpcaoCurso opcaoCurso = await ValidarOpcaoCursoAluno(request.Curso);

        InscricaoCandidatoModel inscricao = await RegistrarNovaInscricaoCandidato(request);

        FichaAluno fichaAluno = await RegistrarAluno(request, usuario, inscricao);

        // await _servicoGeracaoContrato.Gerar(fichaAluno, opcaoCurso);

        // await _servicoGeracaoBoleto.Gerar(fichaAluno, opcaoCurso);

        return new InscricaoCandidatoResponse
        {
            Inscricao = inscricao.CodigoInscricao
        };
    }

    private async Task<OpcaoCurso> ValidarOpcaoCursoAluno(OpcaoCursoRequest curso)
    {
        var opcao = await _cursoService.ObterCurso(curso.CodigoCurso);

        return new OpcaoCurso
        {
            Id = opcao.Id,
            Nome = opcao.Nome,
            Descricao = opcao.Descricao,
            CargaHoraria = opcao.Disciplinas.Sum(disciplina => disciplina.CargaHoraria),
            AreaConhecimento = $"{opcao.AreaConhecimento.Id} - {opcao.AreaConhecimento.Nome}",
            Disciplinas = opcao.Disciplinas.Select(disciplina =>
            {
                return new Disciplina
                {
                    Id = disciplina.Id,
                    Nome = disciplina.Nome.FormatarTextoCamelCase(),
                    Descricao = disciplina.Descricao,
                    CargaHoraria = disciplina.CargaHoraria
                };
            }).ToList(),
        };
    }

    private async Task<FichaAluno> RegistrarAluno(NovaInscricaoCandidatoRequest request, UsuarioModel usuario, InscricaoCandidatoModel inscricao)
    {
        // cadastrar aluno
        NovoAlunoArgument alunoArgument = await MontarNovoRegistroAluno(request);
        AlunoModel aluno = await _alunoService.CadastrarAluno(alunoArgument);

        // cadastrar endereco
        NovoEnderecoAlunoArgument enderecoAlunoArgument = await MontarNovoRegistroEnderecoAluno(aluno.Id, request);
        EnderecoAlunoModel enderecoAluno = await _alunoService.CadastrarEnderecoAluno(enderecoAlunoArgument);

        // cadastrar telefone
        NovoTelefoneAlunoArgument telefoneAlunoArgument = await MontarNovoRegistroTelefoneAluno(aluno.Id, request);
        TelefoneAlunoModel telefoneAluno = await _alunoService.CadastrarTelefoneAluno(telefoneAlunoArgument);

        // cadastrar detalhes
        NovoDetalheAlunoArgument detalheAlunoArgument = await MontarNovoRegistroDetalheAluno(aluno.Id, usuario, inscricao);
        DetalheAlunoModel detalheAluno = await _alunoService.CadastrarDetalheAluno(detalheAlunoArgument);

        return new FichaAluno
        {
            Aluno = aluno,
            Endereco = enderecoAluno,
            Telefone = telefoneAluno,
            DetalhesFicha = detalheAluno
        };
    }

    private static async Task<NovoDetalheAlunoArgument> MontarNovoRegistroDetalheAluno(int id, UsuarioModel usuario, InscricaoCandidatoModel inscricao)
    {
        return await Task.FromResult(new NovoDetalheAlunoArgument()
        {
            IdAluno = id,
            CodigoInscricao = inscricao.CodigoInscricao.ToString(),
            DataInscricao = inscricao.DataInscricao,
            CodigoUsuario = usuario.Id.ToString(),
            DataUsuario = usuario.DataCadastro
        });
    }

    private static async Task<NovoTelefoneAlunoArgument> MontarNovoRegistroTelefoneAluno(int id, NovaInscricaoCandidatoRequest request)
    {
        return await Task.FromResult(new NovoTelefoneAlunoArgument()
        {
            IdAluno = id,
            TelefoneCelular = request.Telefone.TelefoneCelular ?? string.Empty,
            TelefoneRecado = request.Telefone.TelefoneRecado ?? string.Empty,
            TelefoneResidencial = request.Telefone.TelefoneResidencial ?? string.Empty,
        });
    }

    private static async Task<NovoEnderecoAlunoArgument> MontarNovoRegistroEnderecoAluno(int id, NovaInscricaoCandidatoRequest request)
    {
        return await Task.FromResult(new NovoEnderecoAlunoArgument()
        {
            IdAluno = id,
            Logradouro = request.Endereco.Logradouro.ToUpper(),
            Numero = request.Endereco.Numero,
            Complemento = request.Endereco.Complemento.ToUpper(),
            Bairro = request.Endereco.Bairro.ToUpper(),
            Localidade = request.Endereco.Localidade.ToUpper(),
            UF = request.Endereco.UF.ToUpper(),
            CEP = request.Endereco.CEP.ToUpper()
        });
    }

    private static async Task<NovoAlunoArgument> MontarNovoRegistroAluno(NovaInscricaoCandidatoRequest request)
    {
        return await Task.FromResult(new NovoAlunoArgument()
        {
            Nome = request.NomeCompleto.ObterPrimeiroNome(),
            Sobrenome = request.NomeCompleto.ObterSobrenome(),
            CPF = request.CPF.FormatarCPF(),
            Sexo = request.Sexo,
            DataNascimento = request.DataNascimento,
            Email = request.Email.Trim().ToLower(),
        });
    }

    private async Task<InscricaoCandidatoModel> RegistrarNovaInscricaoCandidato(NovaInscricaoCandidatoRequest request)
    {
        InscricaoCandidatoArgument argument = new()
        {
            Nome = request.NomeCompleto.FormatarTextoCamelCase(),
            Email = request.Email.Trim().ToLower(),
            Telefone = request.Telefone.TelefoneCelular ?? request.Telefone.TelefoneResidencial ?? request.Telefone.TelefoneRecado,
            CodigoCurso = request.Curso.CodigoCurso,
            NomeCurso = request.Curso.CodigoCurso.ToString()
        };

        return await _inscricaoRepository.RegistrarNovaInscricao(argument);
    }

    private async Task<UsuarioModel> ValidarUsuarioExistente(NovaInscricaoCandidatoRequest request)
    {
        UsuarioModel usuario = await _usuarioService.ObterUsuario(request.Email.Trim().ToLower());

        if (usuario is null)
            throw new APICustomException(
                message: $"Usuário {usuario.Usuario} não existe.",
                responseType: Domain.Configurations.Enums.ExceptionResponseType.Error,
                statusCode: System.Net.HttpStatusCode.BadRequest);

        if (!usuario.Ativo)
            throw new APICustomException(
                message: $"Usuário {usuario.Usuario} está inativo.",
                responseType: Domain.Configurations.Enums.ExceptionResponseType.Error,
                statusCode: System.Net.HttpStatusCode.BadRequest);

        return usuario;
    }
}
