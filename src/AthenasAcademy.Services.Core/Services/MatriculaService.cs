using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Intercfaces;
using AthenasAcademy.Services.Core.Services.Interfaces;

namespace AthenasAcademy.Services.Core.Services;

public class MatriculaService : IMatriculaService
{
    private readonly IMatriculaRepository _matriculaRepository;

    public MatriculaService(IMatriculaRepository matriculaRepository)
    {
        _matriculaRepository = matriculaRepository;
    }

    public async Task<AlunoDetalhesModel> ObterMatriculaPorId(int id)
    {
        return await _matriculaRepository.ObterMatriculaPorId(id);
    }

    public async Task<IEnumerable<AlunoDetalhesModel>> ObterTodasMatriculas()
    {
        return await _matriculaRepository.ObterTodasMatriculas();
    }

    public async Task AdicionarMatricula(AlunoDetalhesModel matricula)
    {
        if (matricula == null)
        {
            throw new ArgumentNullException(nameof(matricula), "A matrícula não pode ser nula.");
        }

        await _matriculaRepository.AdicionarMatricula(matricula);
    }

    public async Task AtualizarMatricula(AlunoDetalhesModel matricula)
    {
        if (matricula == null)
        {
            throw new ArgumentNullException(nameof(matricula), "A matrícula não pode ser nula.");
        }

        await _matriculaRepository.AtualizarMatricula(matricula);
    }

    public async Task CancelarMatricula(int id)
    {
        await _matriculaRepository.CancelarMatricula(id);
    }
}
