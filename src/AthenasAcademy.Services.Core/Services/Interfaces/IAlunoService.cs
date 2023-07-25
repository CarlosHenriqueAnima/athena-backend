using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;

namespace AthenasAcademy.Services.Core.Services.Interfaces;

public interface IAlunoService
{
    // Métodos relacionados a alunos

    /// <summary>
    /// Obtém um aluno pelo ID.
    /// </summary>
    /// <param name="id">ID do aluno a ser obtido.</param>
    /// <returns>Objeto contendo informações do aluno.</returns>
    Task<AlunoModel> ObterAlunoPorId(int id);

    /// <summary>
    /// Obtém todos os alunos cadastrados.
    /// </summary>
    /// <returns>Lista de alunos cadastrados.</returns>
    Task<IEnumerable<AlunoModel>> ObterTodosAlunos();

    /// <summary>
    /// Cadastra um novo aluno.
    /// </summary>
    /// <param name="aluno">Objeto contendo os dados do novo aluno.</param>
    /// <returns>Objeto contendo informações do aluno cadastrado.</returns>
    Task<AlunoModel> AdicionarAluno(AlunoModel aluno);

    /// <summary>
    /// Atualiza os dados de um aluno existente.
    /// </summary>
    /// <param name="aluno">Objeto contendo os novos dados do aluno.</param>
    /// <returns>Objeto contendo informações do aluno atualizado.</returns>
    Task<AlunoModel> AtualizarAluno(AlunoModel aluno);

    /// <summary>
    /// Desativa um aluno existente.
    /// </summary>
    /// <param name="id">ID do aluno a ser desativado.</param>
    /// <returns>Indicação de sucesso na desativação do aluno.</returns>
    Task<bool> DesativarAluno(int id);
<<<<<<< HEAD
    Task<AlunoModel> ObterAlunoPorIdAsync(int id);
=======
    Task ObterAlunoPorIdAsync(int id);
>>>>>>> 7f24942145c284dece18f132624604ac986811b1
}