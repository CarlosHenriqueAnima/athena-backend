using AthenasAcademy.Services.Core.Configurations.Enums;
using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Repositories.Interfaces.Base;
using AthenasAcademy.Services.Domain.Configurations.Enums;
using Dapper;
using System.Data;

namespace AthenasAcademy.Services.Core.Repositories;

public class MatriculaRepository : BaseRepository, IMatriculaRepository
{
    public MatriculaRepository(IConfiguration configuration) : base(configuration)
    {

    }

    public async Task<AlunoDetalhesModel> ObterMatriculaPorIdAsync(int matriculaId)
    {
        try
        {
            using (IDbConnection connection = GetConnection(Database.Matricula))
            {
                string query = @"SELECT
                                    id,
                                    aluno_id AlunoId,
                                    codigo_contrato CodigoContrato,
                                    codigo_inscricao CodigoInscricao,
                                    data_matricula DataMatricula,
                                    codigo_matricula CodigoMatricula,
                                    codigo_usuario CodigoUsuario
                                FROM aluno_detalhes
                                WHERE id = @matriculaId
                                AND codigo_matricula is not null
                                AND codigo_usuario is not null";

                return await connection.QueryFirstOrDefaultAsync<AlunoDetalhesModel>(query, new { Id = matriculaId });
            }
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task<IEnumerable<AlunoDetalhesModel>> ObterTodasMatriculasAsync()
    {
        try
        {
            using (IDbConnection connection = GetConnection(Database.Matricula))
            {
                string query = @"SELECT
                                    id,
                                    aluno_id AlunoId,
                                    codigo_contrato CodigoContrato,
                                    codigo_inscricao CodigoInscricao,
                                    data_matricula DataMatricula,
                                    codigo_matricula CodigoMatricula,
                                    codigo_usuario CodigoUsuario
                                FROM aluno_detalhes
                                AND codigo_contrato is not null
                                AND data_matricula is not null
                                AND codigo_matricula is not null
                                AND codigo_usuario is not null";

                return await connection.QueryAsync<AlunoDetalhesModel>(query);
            }
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task AdicionarMatriculaAsync(AlunoDetalhesModel matricula)
    {
        try
        {
            using (IDbConnection connection = GetConnection(Database.Matricula))
            {
                matricula.DataMatricula = DateTime.Now;

                string query = @"INSERT INTO aluno_detalhes
                                 (aluno_id, codigo_contrato, codigo_inscricao, data_matricula, codigo_matricula, codigo_usuario)
                                 VALUES
                                 (@AlunoId, @CodigoContrato, @CodigoInscricao, @DataMatricula, @CodigoMatricula, @CodigoUsuario)";

                await connection.ExecuteAsync(query, matricula);
            }
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task AtualizarMatriculaAsync(AlunoDetalhesModel matricula)
    {
        try
        {
            using (IDbConnection connection = GetConnection(Database.Matricula))
            {
                matricula.DataMatricula ??= DateTime.Now;

                string query = @"UPDATE aluno_detalhes
                                SET codigo_contrato = @CodigoContrato,
                                    codigo_inscricao = @CodigoInscricao,
                                    data_matricula = @DataMatricula,
                                    codigo_matricula = @CodigoMatricula,
                                    codigo_usuario = @CodigoUsuario
                                WHERE id = @Id";

                await connection.ExecuteAsync(query, matricula);
            }
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task CancelarMatriculaAsync(int matriculaId)
    {
        try
        {
            using (IDbConnection connection = GetConnection(Database.Matricula))
            {
                string query = @"UPDATE aluno_detalhes
                                SET status = 3
                                WHERE id = @matriculaId";
                await connection.ExecuteAsync(query, new { Id = matriculaId });
            }
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }
}