using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Configurations.Enums;
using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Repositories.Interfaces.Base;
using AthenasAcademy.Services.Domain.Configurations.Enums;
using Dapper;
using System.Data;

namespace AthenasAcademy.Services.Core.Repositories;

public class CursoRepository : BaseRepository, ICursoRepository
{
    public CursoRepository(IConfiguration configuration) : base(configuration) { }

    #region Curso

    public async Task<CursoModel> ObterCurso(int id)
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync(Database.Curso);
            string query = @"SELECT 
                                    id, 
                                    nome, 
                                    descricao, 
                                    carga_horaria CargaHoraria, 
                                    id_area_conhecimento IdAreaConhecimento, 
                                    ativo, 
                                    data_cadastro DataCadastro, 
                                    data_alteracao DataAlteracao
                                    FROM curso
                                    WHERE id = @Id";

            return await connection.QueryFirstAsync<CursoModel>(query, new { Id = id });
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<IEnumerable<CursoModel>> ObterCursos()
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync(Database.Curso);
            string query = @"SELECT 
                                    id, 
                                    nome, 
                                    descricao, 
                                    carga_horaria CargaHoraria, 
                                    id_area_conhecimento IdAreaConhecimento, 
                                    ativo, 
                                    data_cadastro DataCadastro, 
                                    data_alteracao DataAlteracao
                                    FROM curso
                                    WHERE ativo";

            return await connection.QueryAsync<CursoModel>(query);
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task<CursoModel> CadastrarCurso(CursoArgument argument)
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync(Database.Curso);
            string query = @"
                INSERT INTO curso (nome, descricao, carga_horaria, id_area_conhecimento, ativo, data_cadastro, data_alteracao)
                VALUES (@Nome, @Descricao, @CargaHoraria, @IdAreaConhecimento, @Ativo, @DataCadastro, @DataAlteracao)
                RETURNING id, nome, descricao, carga_horaria AS CargaHoraria, id_area_conhecimento AS IdAreaConhecimento, ativo, data_cadastro AS DataCadastro, data_alteracao AS DataAlteracao
            ";

            return await connection.QueryFirstOrDefaultAsync<CursoModel>(query, argument);
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task<CursoModel> AtualizarCurso(CursoArgument argument)
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync(Database.Curso);
            string query = @"UPDATE curso
                                 SET nome = @Nome,
                                     descricao = @Descricao,
                                     carga_horaria = @CargaHoraria,
                                     id_area_conhecimento = @IdAreaConhecimento,
                                     ativo = @Ativo,
                                     data_alteracao = @DataAlteracao
                                 WHERE id = @Id";

            await connection.ExecuteAsync(query, argument);

            return await ObterCurso(argument.Id);
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task<bool> DesativarCurso(int id)
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync(Database.Curso);
            string query = @"UPDATE curso
                                 SET ativo = false
                                 WHERE id = @Id";

            int rowsAffected = await connection.ExecuteAsync(query, new { Id = id });

            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    #endregion

    #region Disciplina
    public async Task<DisciplinaModel> ObterDisciplina(int id)
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync(Database.Curso);
            string query = @"SELECT 
                                    id, 
                                    nome, 
                                    descricao, 
                                    carga_horaria CargaHoraria, 
                                    id_curso IdCurso, 
                                    ativo, 
                                    data_cadastro DataCadastro, 
                                    data_alteracao DataAlteracao
                                FROM disciplina
                                WHERE id = @Id";

            return await connection.QueryFirstAsync<DisciplinaModel>(query, new { Id = id });
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<IEnumerable<DisciplinaModel>> ObterDisciplinas()
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync(Database.Curso);
            string query = @"SELECT 
                                    id, 
                                    nome, 
                                    descricao, 
                                    carga_horaria CargaHoraria, 
                                    id_curso IdCurso, 
                                    ativo, 
                                    data_cadastro DataCadastro, 
                                    data_alteracao DataAlteracao
                                FROM disciplina";

            return await connection.QueryAsync<DisciplinaModel>(query);
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task<IEnumerable<DisciplinaModel>> ObterDisciplinasDoCurso(int idCurso)
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync(Database.Curso);
            string query = @"SELECT 
                                dis.id, 
                                dis.nome, 
                                dis.descricao, 
                                dis.carga_horaria CargaHoraria, 
                                dis.id_curso IdCurso, 
                                dis.ativo, 
                                dis.data_cadastro DataCadastro, 
                                dis.data_alteracao DataAlteracao
                            FROM disciplina dis
                            WHERE id_curso = @IdCurso";

            return await connection.QueryAsync<DisciplinaModel>(query, new { IdCurso = idCurso });
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task<DisciplinaModel> CadastrarDisciplina(DisciplinaArgument argument)
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync(Database.Curso);
            string query = @"INSERT INTO disciplina (nome, descricao, carga_horaria, id_curso, ativo, data_cadastro, data_alteracao)
                                 VALUES (@Nome, @Descricao, @CargaHoraria, @IdCurso, @Ativo, @DataCadastro, @DataAlteracao)
                                 RETURNING id, nome, descricao, carga_horaria AS CargaHoraria, id_curso AS IdCurso, ativo, data_cadastro AS DataCadastro, data_alteracao AS DataAlteracao";

            int id = await connection.QuerySingleOrDefaultAsync<int>(query, argument);

            return await ObterDisciplina(id);
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task<DisciplinaModel> AtualizarDisciplina(DisciplinaArgument argument)
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync(Database.Curso);
            string query = @"UPDATE disciplina
                                 SET nome = @Nome,
                                     descricao = @Descricao,
                                     carga_horaria = @CargaHoraria,
                                     id_curso = @IdCurso,
                                     ativo = @Ativo,
                                     data_alteracao = @DataAlteracao
                                 WHERE id = @Id
                                 RETURNING id, nome, descricao, carga_horaria AS CargaHoraria, id_curso AS IdCurso, ativo, data_cadastro AS DataCadastro, data_alteracao AS DataAlteracao";

            return await connection.QueryFirstOrDefaultAsync<DisciplinaModel>(query, argument);
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task<bool> DesativarDisciplina(int id)
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync(Database.Curso);
            string query = @"UPDATE disciplina
                                 SET ativo = false
                                 WHERE id = @Id";

            int rowsAffected = await connection.ExecuteAsync(query, new { Id = id });

            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    #endregion

    #region Área de Conhecimento
    public async Task<AreaConhecimentoModel> ObterAreaConhecimento(int id)
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync(Database.Curso);
            string query = @"SELECT 
                                    id, 
                                    nome, 
                                    descricao, 
                                    ativo, 
                                    data_cadastro DataCadastro, 
                                    data_alteracao DataAlteracao
                                FROM area_conhecimento
                                WHERE id = @Id";

            return await connection.QueryFirstAsync<AreaConhecimentoModel>(query, new { Id = id });
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<IEnumerable<AreaConhecimentoModel>> ObterAreasConhecimento()
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync(Database.Curso);
            string query = @"SELECT 
                                    id, 
                                    nome, 
                                    descricao, 
                                    ativo, 
                                    data_cadastro DataCadastro, 
                                    data_alteracao DataAlteracao
                                FROM area_conhecimento";

            return await connection.QueryAsync<AreaConhecimentoModel>(query);
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task<AreaConhecimentoModel> CadastrarAreaConhecimento(AreaConhecimentoArgument argument)
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync(Database.Curso);
            string query = @"
                INSERT INTO area_conhecimento (nome, descricao, ativo, data_cadastro, data_alteracao)
                VALUES (@Nome, @Descricao, @Ativo, @DataCadastro, @DataAlteracao)
                RETURNING id, nome, descricao, ativo, data_cadastro AS DataCadastro, data_alteracao AS DataAlteracao";

            return await connection.QueryFirstOrDefaultAsync<AreaConhecimentoModel>(query, argument);
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task<AreaConhecimentoModel> AtualizarAreaConhecimento(AreaConhecimentoArgument argument)
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync(Database.Curso);
            string query = @"
                UPDATE area_conhecimento
                SET nome = @Nome,
                    descricao = @Descricao,
                    ativo = @Ativo,
                    data_alteracao = @DataAlteracao
                WHERE id = @Id
                RETURNING id, nome, descricao, ativo, data_cadastro AS DataCadastro, data_alteracao AS DataAlteracao
            ";

            return await connection.QueryFirstOrDefaultAsync<AreaConhecimentoModel>(query, argument);
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task<bool> DesativarAreaConhecimento(int id)
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync(Database.Curso);
            string query = @"
                UPDATE area_conhecimento
                SET ativo = false,
                    data_alteracao = @DataAlteracao
                WHERE id = @Id
            ";

            int rowsAffected = await connection.ExecuteAsync(query, new { Id = id, DataAlteracao = DateTime.Now });

            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }
    #endregion
}