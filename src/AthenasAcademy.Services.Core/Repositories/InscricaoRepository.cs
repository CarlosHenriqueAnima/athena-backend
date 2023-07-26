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
            using IDbConnection connection = GetConnection(Database.Inscricao);
            string query = @"
                INSERT INTO inscricao_candidato 
                (nome, email, telefone, codigo_curso, nome_curso, boleto_pago, data_inscricao) 
                VALUES (@Nome, @Email, @Telefone, @CodigoCurso, @NomeCurso, @BoletoPago, @DataInscricao)
                RETURNING *";

            return await connection.QueryFirstOrDefaultAsync<InscricaoCandidatoModel>(query, argument);
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }
}
