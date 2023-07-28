using AthenasAcademy.Services.Core.CrossCutting;
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
    /// <returns>Um objeto que contém o status da matrícula do aluno.</returns>
    Task<MatriculaStatusResponse> MatricularAluno(int inscricao);

    /// <summary>
    /// Registra a pré-matrícula de um aluno, fornecendo a ficha do aluno com os dados necessários.
    /// </summary>
    /// <param name="fichaAluno">A ficha do aluno contendo os dados para a pré-matrícula.</param>
    /// <returns>Pré-matrícula do aluno com número de contrato e número de inscrição./returns>
    Task<(int, int)> RegistrarPreContratoMatricula(FichaAluno fichaAluno);
}
