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

    public async Task<MatriculaModel> GerarPreMatricula(FichaAluno fichaAluno)
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync(Database.Matricula);
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();

            try
            {
                var matriculaAluno = await RegistrarPreMatriculaAluno(transaction, fichaAluno);
                var contratoMatriculaAluno = await RegistrarPreContratoMatriculaAluno(transaction, fichaAluno);

                transaction.Commit();

                return new MatriculaModel
                {
                    MatriculaAlunoId = matriculaAluno.Id,
                    Matricula = matriculaAluno.Matricula,
                    Ativa = matriculaAluno.Ativa,
                    CodigoAluno = matriculaAluno.CodigoAluno,
                    NomeAluno = matriculaAluno.NomeAluno,
                    DataMatricula = matriculaAluno.DataMatricula,
                    CodigoContrato = contratoMatriculaAluno.Contrato,
                    ContratoAlunoId = contratoMatriculaAluno.Id,
                    Assinado = contratoMatriculaAluno.Assinado,
                    FormaPagamento = contratoMatriculaAluno.FormaPagamento,
                    ValorContrato = contratoMatriculaAluno.ValorContrato,
                    DataAceite = contratoMatriculaAluno.DataAceite
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
            }
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public Task<MatriculaModel> AtivarMatricula(FichaAluno fichaAluno)
    {
        throw new NotImplementedException();
    }

    public Task<ContratoMatriculaAlunoModel> AssinarContratoMatricula(FichaAluno fichaAluno)
    {
        throw new NotImplementedException();
    }

    public async Task<MatriculaModel> ObterMatricula(int matricula)
    {
        try
        {
            string query = @"
            SELECT 
                ma.id AS MatriculaAlunoId,
                ma.matricula AS Matricula,
                ma.ativa AS Ativa,
                ma.codigo_aluno AS CodigoAluno,
                ma.nome_aluno AS NomeAluno,
                ma.data_matricula AS DataMatricula,
                cm.id AS ContratoAlunoId,
                cm.contrato AS CodigoContrato,
                cm.assinado AS Assinado,
                cm.forma_pagamento AS FormaPagamento,
                cm.valor_contrato AS ValorContrato,
                cm.data_aceite AS DataAceite
            FROM matricula_aluno ma
            LEFT JOIN contrato_matricula_aluno cm ON ma.id = cm.id_matricula
            WHERE ma.matricula = @Matricula";

            using IDbConnection connection = await GetConnectionAsync(Database.Matricula);
            return await connection.QueryFirstOrDefaultAsync<MatriculaModel>(query, new { Matricula = matricula });
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    private static async Task<MatriculaAlunoModel> RegistrarPreMatriculaAluno(IDbTransaction transaction, FichaAluno fichaAluno)
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

    private static async Task<ContratoMatriculaAlunoModel> RegistrarPreContratoMatriculaAluno(IDbTransaction transaction, FichaAluno fichaAluno)
    {
        try
        {
            string queryContratoMatriculaAluno = @"INSERT INTO contrato_matricula_aluno (id_matricula, contrato, assinado, forma_pagamento, valor_contrato) 
                                               VALUES ((SELECT currval('matricula_seq')), nextval('contrato_seq'), @Assinado, @FormaPagamento, @ValorContrato) 
                                               RETURNING *";

            return await transaction.Connection.QueryFirstAsync<ContratoMatriculaAlunoModel>(queryContratoMatriculaAluno,
                new
                {
                    fichaAluno.Contrato.Assinado,
                    fichaAluno.Contrato.FormaPagamento,
                    fichaAluno.Contrato.ValorContrato,
                });
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

}