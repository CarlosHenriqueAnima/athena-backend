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

public class InscricaoRepository : BaseRepository, IInscricaoRepository
{
    public InscricaoRepository(IConfiguration configuration) : base(configuration) { }

    public async Task<InscricaoCandidatoModel> RegistrarNovaInscricao(InscricaoCandidatoArgument argument)
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync(Database.Inscricao);
            string query = @"
                INSERT INTO inscricao_candidato 
                (data_inscricao, nome, email, telefone, codigo_curso, nome_curso, boleto_pago) 
                VALUES (@DataInscricao, @Nome, @Email, @Telefone, @CodigoCurso, @NomeCurso, @BoletoPago)
                RETURNING id,codigo_inscricao AS CodigoInscricao, data_inscricao AS DataInscricao, nome, email, telefone, codigo_curso AS CodigoCurso, nome_curso AS NomeCurso, boleto, boleto_pago AS BoletoPago;";

            return await connection.QueryFirstOrDefaultAsync<InscricaoCandidatoModel>(query, argument);
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task<InscricaoCandidatoModel> ObterInscricao(int inscricao)
    {
        try
        {
            string query = "SELECT * FROM inscricao_candidato WHERE codigo_inscricao = @CodigoInscricao";

            using IDbConnection connection = await GetConnectionAsync(Database.Inscricao);
            var inscricaoCandidato = await connection.QueryFirstOrDefaultAsync<InscricaoCandidatoModel>(query, new { CodigoInscricao = inscricao });

            return inscricaoCandidato;
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }
}
