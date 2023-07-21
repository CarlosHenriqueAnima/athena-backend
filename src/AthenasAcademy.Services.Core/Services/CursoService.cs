using AthenasAcademy.Services.Core.Repositories.Intercfaces;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;

namespace AthenasAcademy.Services.Core.Services;

public class CursoService : ICursoService
{
    private readonly ICursoRepository _cursoRepository;

    public CursoService(ICursoRepository cursoRepository)
    {
        _cursoRepository = cursoRepository;
    }

    #region Curso
    public Task<CursoResponse> AtualizarCurso(NovoCursoRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<NovoCursoResponse> CadastrarCurso(NovoCursoRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DesativarCurso(int id)
    {
        throw new NotImplementedException();
    }

    public Task<CursoResponse> ObterCurso(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CursoResponse>> ObterCursos()
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Disciplina
    public Task<bool> DesativarDisciplina(int id)
    {
        throw new NotImplementedException();
    }

    public Task<DisciplinaResponse> AtualizarDisciplina(DisciplinaRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<NovaDisciplinaResponse> CadastrarDisciplina(NovaDisciplinaRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<DisciplinaResponse> ObterDisciplina(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<DisciplinaResponse>> ObterDisciplinas()
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Área Conhecimento
    public Task<AreaConhecimentoResponse> AtualizarAreaConhecimento(AreaConhecimentoRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<NovaAreaConhecimentoResponse> CadastrarAreaConhecimento(NovaAreaConhecimentoRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DesativarAreaConhecimento(int id)
    {
        throw new NotImplementedException();
    }

    public Task<AreaConhecimentoResponse> ObterAreaConhecimento(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AreaConhecimentoResponse>> ObterAreasConhecimento()
    {
        throw new NotImplementedException();
    }
    #endregion
}