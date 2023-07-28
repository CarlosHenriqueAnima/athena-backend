using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.CrossCutting;
using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Domain.Configurations.Enums;
using AthenasAcademy.Services.Domain.Responses;

namespace AthenasAcademy.Services.Core.Services;

public class AlunoService : IAlunoService
{
    private readonly IAlunoRepository _alunoRepository;

    public AlunoService(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }

    public async Task<AlunoModel> CadastrarAluno(NovoAlunoArgument aluno)
    {
        try
        {
            return await _alunoRepository.CadastrarAluno(aluno);
        }
        catch (Exception ex)
        {
            throw new APICustomException(
                message: $"Erro ao realizar o cadastro do aluno. {ex.Message}",
                responseType: ExceptionResponseType.Error,
                statusCode: System.Net.HttpStatusCode.InternalServerError);
        }
    }

    public async Task<DetalheAlunoModel> CadastrarDetalheAluno(NovoDetalheAlunoArgument detalhe)
    {
        try
        {
            return await _alunoRepository.CadastrarDetalheAluno(detalhe);
        }
        catch (Exception ex)
        {
            throw new APICustomException(
                message: $"Erro ao realizar o cadastro dos detalhes do aluno. {ex.Message}",
                responseType: ExceptionResponseType.Error,
                statusCode: System.Net.HttpStatusCode.InternalServerError);
        }
    }

    public async Task<EnderecoAlunoModel> CadastrarEnderecoAluno(NovoEnderecoAlunoArgument endereco)
    {
        try
        {
            return await _alunoRepository.CadastrarEnderecoAluno(endereco);
        }
        catch (Exception ex)
        {
            throw new APICustomException(
                message: $"Erro ao realizar o cadastro do endereço do aluno. {ex.Message}",
                responseType: ExceptionResponseType.Error,
                statusCode: System.Net.HttpStatusCode.InternalServerError);
        }
    }

    public async Task<TelefoneAlunoModel> CadastrarTelefoneAluno(NovoTelefoneAlunoArgument telefone)
    {
        try
        {
            return await _alunoRepository.CadastrarTelefoneAluno(telefone);
        }
        catch (Exception ex)
        {
            throw new APICustomException(
                message: $"Erro ao realizar o cadastro do telefone do aluno. {ex.Message}",
                responseType: ExceptionResponseType.Error,
                statusCode: System.Net.HttpStatusCode.InternalServerError);
        }
    }

    public async Task<AlunoModel> ObterAluno(int id)
    {
        try
        {
            return await _alunoRepository.ObterAluno(id);
        }
        catch (Exception ex)
        {
            throw new APICustomException(
                message: $"Erro ao obter o aluno. {ex.Message}",
                responseType: ExceptionResponseType.Error,
                statusCode: System.Net.HttpStatusCode.InternalServerError);
        }
    }

    public async Task<DetalheAlunoArgumentoModel> ObterDetalheAluno(int? aluno = null, int? inscricao = null, int? matricula = null)
    {
        try
        {
            string argumento = aluno.HasValue ? "id_aluno = " + aluno :
                               inscricao.HasValue ? "codigo_inscricao" :
                               matricula.HasValue ? "codigo_matricula" :
                               throw new ArgumentException("É necessário fornecer apenas um dos critérios de busca: ID, inscrição ou matrícula.");

            int id = aluno ?? (inscricao ?? matricula ?? throw new ArgumentException("É necessário fornecer apenas um dos critérios de busca: ID, inscrição ou matrícula."));


            return await _alunoRepository.ObterDetalheAluno(argumento, id);
        }
        catch (Exception ex)
        {
            throw new APICustomException(
                message: $"Erro ao obter detalhes do aluno. {ex.Message}",
                responseType: ExceptionResponseType.Error,
                statusCode: System.Net.HttpStatusCode.InternalServerError);
        }
    }

    public async Task<FichaAluno> ObterFichaAluno(int inscricao)
    {
        try
        {
            return await _alunoRepository.ObterFichaAluno(inscricao);
        }
        catch (Exception ex)
        {
            throw new APICustomException(
                message: $"Erro ao obter a ficha do aluno. {ex.Message}",
                responseType: ExceptionResponseType.Error,
                statusCode: System.Net.HttpStatusCode.InternalServerError);
        }
    }

    public async Task AtualizarMatriculaContratoAluno(int matricula, int contrato, int inscricao)
    {
        try
        {
            await _alunoRepository.AtualizarMatriculaContratoAluno(matricula, contrato, inscricao);
        }
        catch (Exception ex)
        {
            throw new APICustomException(
                message: $"Erro ao atualizar cadastro do aluno. {ex.Message}",
                responseType: ExceptionResponseType.Error,
                statusCode: System.Net.HttpStatusCode.InternalServerError);
        }
    }
}
