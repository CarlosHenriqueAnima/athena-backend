using AthenasAcademy.Services.Core.Configurations.Enums;
using AthenasAcademy.Services.Core.CrossCutting;
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
    public MatriculaRepository(IConfiguration configuration) : base(configuration) { }

    public async Task<MatriculaModel> AtivarMatricula(FichaAluno fichaAluno)
    {
        throw new NotImplementedException();
    }

    public Task<ContratoMatriculaAlunoModel> GerarContratoMatricula(FichaAluno fichaAluno)
    {
        throw new NotImplementedException();
    }

    public async Task<MatriculaModel> GerarPreMatricula(FichaAluno fichaAluno)
    {
        try
        {
            using (IDbConnection connection = GetConnection(Database.Matricula))
            {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction();

                try
                {
                    var matriculaAluno = await RegistrarPreMatriculaAluno(transaction, fichaAluno);
                    var contratoMatriculaAluno = await RegistrarContratoMatriculaAluno(transaction, fichaAluno);

                    transaction.Commit();

                    return new MatriculaModel
                    {
                    };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
                }
            }
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    private async Task<MatriculaAlunoModel> RegistrarPreMatriculaAluno(IDbTransaction transaction, FichaAluno fichaAluno)
    {
        try
        {
            string queryMatriculaAluno = @"INSERT INTO matricula_aluno (matricula, ativa, codigo_aluno, nome_aluno, codigo_contrato) 
                                       VALUES (nextval('matricula_seq'), @Ativa, @CodigoAluno, @NomeAluno, @CodigoContrato) 
                                       RETURNING *";

            return await transaction.Connection.QueryFirstAsync<MatriculaAlunoModel>(queryMatriculaAluno,
                new
                {
                    Ativa = false,
                    CodigoAluno = fichaAluno.Aluno.Id,
                    NomeAluno = fichaAluno.Aluno.Nome + fichaAluno.Aluno.Sobrenome,

                });
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    private async Task<ContratoMatriculaAlunoModel> RegistrarContratoMatriculaAluno(IDbTransaction transaction, FichaAluno fichaAluno)
    {
        try
        {
            string queryContratoMatriculaAluno = @"INSERT INTO contrato_matricula_aluno (id_matricula, contrato, assinado, forma_pagamento, valor_contrato, data_aceite) 
                                               VALUES ((SELECT currval('matricula_seq')), nextval('contrato_seq'), @Assinado, @FormaPagamento, @ValorContrato, @DataAceite) 
                                               RETURNING *";

            return await transaction.Connection.QueryFirstAsync<ContratoMatriculaAlunoModel>(queryContratoMatriculaAluno,
                new
                {

                });
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

}