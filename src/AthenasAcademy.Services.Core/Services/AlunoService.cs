using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Configurations.Enums;

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
}
