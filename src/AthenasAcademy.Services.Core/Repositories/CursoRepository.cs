using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Configurations.Enums;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Intercfaces;
using AthenasAcademy.Services.Core.Repositories.Intercfaces.Base;
using System.Data;

namespace AthenasAcademy.Services.Core.Repositories;

public class CursoRepository : BaseRepository, ICursoRepository
{
    public CursoRepository(IConfiguration configuration) : base(configuration) { }

    #region Curso

    public Task<CursoModel> ObterCurso(int id)
    {
        using (IDbConnection connection = GetConnection(Database.Curso))
        {

        }

        throw new NotImplementedException();
    }

    public Task<IEnumerable<CursoModel>> ObterCursos()
    {
        using (IDbConnection connection = GetConnection(Database.Curso))
        {

        }

        throw new NotImplementedException();
    }

    public Task<CursoModel> CadastrarCurso(CursoArgument argument)
    {
        using (IDbConnection connection = GetConnection(Database.Curso))
        {

        }

        throw new NotImplementedException();
    }

    public Task<CursoModel> AtualizarCurso(CursoArgument argument)
    {
        using (IDbConnection connection = GetConnection(Database.Curso))
        {

        }

        throw new NotImplementedException();
    }

    public Task<bool> DesativarCurso(int id)
    {
        using (IDbConnection connection = GetConnection(Database.Curso))
        {

        }

        throw new NotImplementedException();
    }

    #endregion

    #region Disciplina

    public Task<DisciplinaModel> ObterDisciplina(int id)
    {
        using (IDbConnection connection = GetConnection(Database.Curso))
        {

        }

        throw new NotImplementedException();
    }

    public Task<IEnumerable<DisciplinaModel>> ObterDisciplinas()
    {
        using (IDbConnection connection = GetConnection(Database.Curso))
        {

        }

        throw new NotImplementedException();
    }

    public Task<DisciplinaModel> CadastrarDisciplina(DisciplinaArgument argument)
    {
        using (IDbConnection connection = GetConnection(Database.Curso))
        {

        }

        throw new NotImplementedException();
    }

    public Task<DisciplinaModel> AtualizarDisciplina(DisciplinaArgument argument)
    {
        using (IDbConnection connection = GetConnection(Database.Curso))
        {

        }

        throw new NotImplementedException();
    }

    public Task<bool> DesativarDisciplina(int id)
    {
        using (IDbConnection connection = GetConnection(Database.Curso))
        {

        }

        throw new NotImplementedException();
    }

    #endregion

    #region Área de Conhecimento

    public Task<AreaConhecimentoModel> ObterAreaConhecimento(int id)
    {
        using (IDbConnection connection = GetConnection(Database.Curso))
        {

        }

        throw new NotImplementedException();
    }

    public Task<IEnumerable<AreaConhecimentoModel>> ObterAreasConhecimento()
    {
        using (IDbConnection connection = GetConnection(Database.Curso))
        {

        }

        throw new NotImplementedException();
    }

    public Task<AreaConhecimentoModel> CadastrarAreaConhecimento(AreaConhecimentoArgument argument)
    {
        using (IDbConnection connection = GetConnection(Database.Curso))
        {

        }

        throw new NotImplementedException();
    }

    public Task<AreaConhecimentoModel> AtualizarAreaConhecimento(AreaConhecimentoArgument argument)
    {
        using (IDbConnection connection = GetConnection(Database.Curso))
        {

        }

        throw new NotImplementedException();
    }

    public Task<bool> DesativarAreaConhecimento(int id)
    {
        using (IDbConnection connection = GetConnection(Database.Curso))
        {

        }

        throw new NotImplementedException();
    }

    #endregion
}

