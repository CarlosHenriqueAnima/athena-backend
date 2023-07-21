using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Core.Repositories.Interfaces;

public interface IAlunoRepository
{
    Task AdicionarAlunoAsync(AlunoModel aluno);
    Task AtualizarAlunoAsync(AlunoModel aluno);
    Task<AlunoModel> ObterAlunoPorIdAsync(int id);
    Task<IEnumerable<AlunoModel>> ObterTodosAlunosAsync();
    Task RemoverAlunoAsync(int id);
}