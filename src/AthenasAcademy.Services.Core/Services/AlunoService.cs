using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;


namespace AthenasAcademy.Services.Core.Services;

public class AlunoService : IAlunoService
{
    private readonly IAlunoRepository _alunoRepository;

    public AlunoService(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }

    public async Task<AlunoModel> AtualizarAluno(AlunoModel aluno)
    {
        if (aluno == null)
        {
            throw new ArgumentNullException(nameof(aluno), "O aluno não pode ser nulo.");
        }

        // Implemente aqui a validação dos dados do aluno, se necessário.

        await _alunoRepository.AtualizarAlunoAsync(aluno);
        return aluno;
    }

    public async Task<AlunoModel> AdicionarAluno(AlunoModel aluno)
    {
        if (aluno == null)
        {
            throw new ArgumentNullException(nameof(aluno), "O aluno não pode ser nulo.");
        }

        // Implemente aqui a validação dos dados do aluno, se necessário.

        await _alunoRepository.AdicionarAlunoAsync(aluno);
        return aluno;
    }

    public async Task<bool> DesativarAluno(int id)
    {
        // Verifique se o aluno existe antes de desativá-lo
        var alunoExistente = await _alunoRepository.ObterAlunoPorIdAsync(id);
        if (alunoExistente == null)
        {
            return false; // Indica que o aluno não foi encontrado
        }

        await _alunoRepository.RemoverAlunoAsync(id);
        return true; // Indica que o aluno foi desativado com sucesso
    }

    public async Task<AlunoModel> ObterAlunoPorId(int id)
    {
        return await _alunoRepository.ObterAlunoPorIdAsync(id);
    }

    public async Task<IEnumerable<AlunoModel>> ObterTodosAlunos()
    {
        return await _alunoRepository.ObterTodosAlunosAsync();
    }

    Task IAlunoService.ObterAlunoPorIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
