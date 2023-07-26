using AthenasAcademy.Services.Core.Configurations.Enums;
<<<<<<< HEAD
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Repositories.Interfaces.Base;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;
using System.Data;

namespace AthenasAcademy.Services.Core.Repositories
{
    public class InscricaoRepository : BaseRepository, IInscricaoRepository
    {
        public InscricaoRepository(IConfiguration configuration) : base(configuration) { }

        #region Inscricao

        Task<InscricaoResponse> IInscricaoRepository.ObterInscricaoPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<InscricaoResponse>> IInscricaoRepository.ObterTodasInscricoesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<InscricaoResponse> CadastrarInscricaoAsync(NovaInscricaoRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<InscricaoResponse> AtualizarInscricaoAsync(AtualizarInscricaoRequest request)
        {
            throw new NotImplementedException();
        }

        Task<bool> IInscricaoRepository.RemoverInscricaoAsync(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
=======
using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Repositories.Interfaces.Base;
using AthenasAcademy.Services.Domain.Configurations.Enums;
using Dapper;
using System.Data;

namespace AthenasAcademy.Services.Core.Repositories;

public class InscricaoRepository : BaseRepository, IInscricaoRepository
{
    public InscricaoRepository(IConfiguration configuration) : base(configuration)
    {

    }

    public async Task<CandidatoModel> ObterInscricaoPorIdAsync(int inscricaoId)
    {
        try
        {
            using (IDbConnection connection = GetConnection(Database.Inscricao))
            {
                string query = @"SELECT 
                                    id,
                                    nome Nome,
                                    email Email,
                                    codigo_inscricao CodigoInscricao,
                                    curso_interesse CursoInteresse,
                                    data_inscricao DataInscricao,
                                    curso Curso,
                                    boleto Boleto
                                FROM candidato
                                WHERE id = @inscricaoId
                                AND data_inscricao is not null";

                return await connection.QueryFirstOrDefaultAsync<CandidatoModel>(query, new { Id = inscricaoId });
            }
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task<IEnumerable<CandidatoModel>> ObterInscricoesPendentesAsync()
    {
        try
        {
            using (IDbConnection connection = GetConnection(Database.Inscricao))
            {
                string query = @"SELECT 
                                    id,
                                    nome Nome,
                                    email Email,
                                    codigo_inscricao CodigoInscricao,
                                    curso_interesse CursoInteresse,
                                    data_inscricao DataInscricao,
                                    curso Curso,
                                    boleto Boleto
                                FROM candidato
                                AND data_inscricao is not null";

                return await connection.QueryAsync<CandidatoModel>(query);
            }
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task AdicionarInscricaoAsync(CandidatoModel inscricao)
    {
        try
        {
            using (IDbConnection connection = GetConnection(Database.Inscricao))
            {
                inscricao.DataInscricao = DateTime.Now;

                string query = @"INSERT INTO candidato
                                 (nome, email, codigo_inscricao, curso_interesse, data_inscricao, curso, boleto)
                                 VALUES
                                 (@Nome, @Email, @CodigoInscricao, @CursoInteresse, @DataInscricao, @Curso, @Boleto)";

                await connection.ExecuteAsync(query, inscricao);
            }
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task AtualizarInscricaoAsync(CandidatoModel inscricao)
    {
        try
        {
            using (IDbConnection connection = GetConnection(Database.Inscricao))
            {
                string query = @"UPDATE candidato
                                SET nome = @Nome,
                                    email = @Email,
                                    codigo_inscricao = @CodigoInscricao,
                                    curso = @Curso
                                WHERE id = @Id";

                await connection.ExecuteAsync(query, inscricao);
            }
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task CancelarInscricaoAsync(int inscricaoId)
    {
        try
        {
            using (IDbConnection connection = GetConnection(Database.Inscricao))
            {
                string query = @"UPDATE candidato
                                SET status = 3
                                WHERE id = @inscricaoId";
                await connection.ExecuteAsync(query, new { Id = inscricaoId });
            }
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
>>>>>>> dev
    }
}
