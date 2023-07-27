using AthenasAcademy.Services.Core.CrossCutting;
using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Core.Repositories.Interfaces;

/// <summary>
/// Interface responsável por definir o contrato para o repositório de matrículas de alunos.
/// </summary>
public interface IMatriculaRepository
{
    /// <summary>
    /// Gera uma pré-matrícula de um aluno com base nas informações fornecidas na ficha do aluno.
    /// </summary>
    /// <param name="fichaAluno">A ficha do aluno contendo as informações necessárias para gerar a pré-matrícula.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona de geração da pré-matrícula, que retorna um objeto do tipo <see cref="MatriculaModel"/>.</returns>
    Task<MatriculaModel> GerarPreMatricula(FichaAluno fichaAluno);

    /// <summary>
    /// Ativa a matrícula de um aluno com base nas informações fornecidas na ficha do aluno.
    /// </summary>
    /// <param name="fichaAluno">A ficha do aluno contendo as informações necessárias para ativar a matrícula.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona de ativação da matrícula, que retorna um objeto do tipo <see cref="MatriculaModel"/>.</returns>
    Task<MatriculaModel> AtivarMatricula(FichaAluno fichaAluno);

    /// <summary>
    /// Obtém informações sobre uma matrícula de aluno com base no número de matrícula fornecido.
    /// </summary>
    /// <param name="matricula">O número da matrícula do aluno a ser consultada.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona de obtenção das informações da matrícula, que retorna um objeto do tipo <see cref="MatriculaModel"/>.</returns>
    Task<MatriculaModel> ObterMatricula(int matricula);

    /// <summary>
    /// Realiza a assinatura do contrato de matrícula de um aluno com base nas informações fornecidas na ficha do aluno.
    /// </summary>
    /// <param name="fichaAluno">A ficha do aluno contendo as informações necessárias para assinar o contrato de matrícula.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona de assinatura do contrato de matrícula, que retorna um objeto do tipo <see cref="ContratoMatriculaAlunoModel"/>.</returns>
    Task<ContratoMatriculaAlunoModel> AssinarContratoMatricula(FichaAluno fichaAluno);
}