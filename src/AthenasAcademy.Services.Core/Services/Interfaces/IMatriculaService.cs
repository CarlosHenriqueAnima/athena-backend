using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Domain.Responses;

namespace AthenasAcademy.Services.Core.Services.Interfaces;

/// <summary>
/// Interface responsável por definir o contrato para o serviço de matrícula de alunos.
/// </summary>
public interface IMatriculaService
{
    /// <summary>
    /// Realiza a matrícula de um aluno com base no número de inscrição.
    /// </summary>
    /// <param name="inscricao">O número de inscrição do aluno a ser matriculado.</param>
    /// <returns>Um objeto contendo o status da matrícula do aluno.</returns>
    Task<MatriculaStatusResponse> MatricularAluno(int inscricao);

    Task<DetalheMatriculaAlunoModel> ObterDetalhesMatricula(int matricula);
}