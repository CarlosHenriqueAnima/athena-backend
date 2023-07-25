using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Core.Services.Interfaces;

public interface IMatriculaService
{
    Task<AlunoDetalhesModel> ObterMatriculaPorIdAsync(int id);
    Task<IEnumerable<AlunoDetalhesModel>> ObterTodasMatriculasAsync();
    Task AdicionarMatriculaAsync(AlunoDetalhesModel matricula);
    Task AtualizarMatriculaAsync(AlunoDetalhesModel matricula);
    Task CancelarMatriculaAsync(int id);
}