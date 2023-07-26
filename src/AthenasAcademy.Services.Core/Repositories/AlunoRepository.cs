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


public class AlunoRepository : BaseRepository, IAlunoRepository
{
    public AlunoRepository(IConfiguration configuration) : base(configuration) { }

    public async Task<AlunoModel> CadastrarAluno(NovoAlunoArgument aluno)
    {
        try
        {
            using (IDbConnection connection = GetConnection(Database.Aluno))
            {
                string query = @"
                    INSERT INTO aluno (nome, sobrenome, cpf, sexo, data_nascimento, email, ativo, data_cadastro)
                    VALUES (@Nome, @Sobrenome, @CPF, @Sexo, @DataNascimento, @Email, @Ativo, @DataCadastro)
                    RETURNING *;";

                return await connection.QueryFirstAsync<AlunoModel>(query, aluno);
            }
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task<DetalheAlunoModel> CadastrarDetalheAluno(NovoDetalheAlunoArgument detalhe)
    {
        try
        {
            using (IDbConnection connection = GetConnection(Database.Aluno))
            {
                string query = @"
                    INSERT INTO detalhe (id_aluno, codigo_usuario, data_usuario, codigo_inscricao, data_inscricao, codigo_matricula, data_matricula, data_contrato, codigo_contrato)
                    VALUES (@IdAluno, @CodigoUsuario, @DataUsuario, @CodigoInscricao, @DataInscricao, @CodigoMatricula, @DataMatricula, @DataContrato, @CodigoContrato)
                    RETURNING *;";

                return await connection.QueryFirstAsync<DetalheAlunoModel>(query, detalhe);
            }
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task<EnderecoAlunoModel> CadastrarEnderecoAluno(NovoEnderecoAlunoArgument endereco)
    {
        try
        {
            using (IDbConnection connection = GetConnection(Database.Aluno))
            {
                string query = @"
                    INSERT INTO endereco (id_aluno, logradouro, numero, complemento, bairro, localidade, uf, cep, ativo, data_cadastro)
                    VALUES (@IdAluno, @Logradouro, @Numero, @Complemento, @Bairro, @Localidade, @UF, @CEP, @Ativo, @DataCadastro)
                    RETURNING *;";

                return await connection.QueryFirstAsync<EnderecoAlunoModel>(query, endereco);
            }
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task<TelefoneAlunoModel> CadastrarTelefoneAluno(NovoTelefoneAlunoArgument telefone)
    {
        try
        {
            using (IDbConnection connection = GetConnection(Database.Aluno))
            {
                string query = @"
                    INSERT INTO telefone (id_aluno, telefone_residencial, telefone_celular, telefone_recado)
                    VALUES (@IdAluno, @TelefoneResidencial, @TelefoneCelular, @TelefoneRecado)
                    RETURNING *;";

                return await connection.QueryFirstAsync<TelefoneAlunoModel>(query, telefone);
            }
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task<AlunoModel> ObterAluno(int id)
    {
        try
        {
            using (IDbConnection connection = GetConnection(Database.Aluno))
            {
                string query = @"
                    SELECT * FROM aluno WHERE id = @Id;";

                return await connection.QueryFirstAsync<AlunoModel>(query, new { Id = id });
            }
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }
}
