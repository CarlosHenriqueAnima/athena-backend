using AthenasAcademy.Services.Core.Arguments;
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


public class AlunoRepository : BaseRepository, IAlunoRepository
{
    public AlunoRepository(IConfiguration configuration) : base(configuration) { }

    public async Task<AlunoModel> CadastrarAluno(NovoAlunoArgument aluno)
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync(Database.Aluno);
            string query = @"
                    INSERT INTO aluno (nome, sobrenome, cpf, sexo, data_nascimento, email, ativo, data_cadastro)
                    VALUES (@Nome, @Sobrenome, @CPF, @Sexo, @DataNascimento, @Email, @Ativo, @DataCadastro)
                    RETURNING *;";

            return await connection.QueryFirstAsync<AlunoModel>(query, aluno);
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
            using IDbConnection connection = await GetConnectionAsync(Database.Aluno);
            string query = @"
                    INSERT INTO detalhe (id_aluno, codigo_usuario, data_usuario, codigo_inscricao, data_inscricao, codigo_matricula, data_matricula, data_contrato, codigo_contrato)
                    VALUES (@IdAluno, @CodigoUsuario, @DataUsuario, @CodigoInscricao, @DataInscricao, @CodigoMatricula, @DataMatricula, @DataContrato, @CodigoContrato)
                    RETURNING *;";

            return await connection.QueryFirstAsync<DetalheAlunoModel>(query, detalhe);
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
            using IDbConnection connection = await GetConnectionAsync(Database.Aluno);
            string query = @"
                    INSERT INTO endereco (id_aluno, logradouro, numero, complemento, bairro, localidade, uf, cep, ativo, data_cadastro)
                    VALUES (@IdAluno, @Logradouro, @Numero, @Complemento, @Bairro, @Localidade, @UF, @CEP, @Ativo, @DataCadastro)
                    RETURNING *;";

            return await connection.QueryFirstAsync<EnderecoAlunoModel>(query, endereco);
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
            using IDbConnection connection = await GetConnectionAsync(Database.Aluno);
            string query = @"
                    INSERT INTO telefone (id_aluno, telefone_residencial, telefone_celular, telefone_recado)
                    VALUES (@IdAluno, @TelefoneResidencial, @TelefoneCelular, @TelefoneRecado)
                    RETURNING *;";

            return await connection.QueryFirstAsync<TelefoneAlunoModel>(query, telefone);
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
            using IDbConnection connection = await GetConnectionAsync(Database.Aluno);
            string query = @"
                    SELECT * FROM aluno WHERE id = @Id;";

            return await connection.QueryFirstAsync<AlunoModel>(query, new { Id = id });
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task<DetalheAlunoArgumentoModel> ObterDetalheAluno(string argumento, int valor)
    {
        try
        {
            SqlBuilder builder = new();

            var query = builder.AddTemplate(@"
                SELECT 
                    d.id,
                    d.id_aluno AS IdAluno,
                    d.codigo_usuario AS CodigoUsuario,
                    d.data_usuario AS DataUsuario,
                    d.codigo_inscricao AS CodigoInscricao,
                    d.data_inscricao AS DataInscricao,
                    d.codigo_curso AS CodigoCurso,
                    d.codigo_matricula AS CodigoMatricula,
                    d.data_matricula AS DataMatricula,
                    d.codigo_contrato AS CodigoContrato,
                    d.data_contrato AS DataContrato,
                    CONCAT(a.nome, ' ', a.sobrenome) AS NomeCompleto
                FROM detalhe d
                JOIN aluno a ON d.id_aluno = a.id
                /**where**/
                ");

            builder.Where(argumento + " = @Valor", new { Valor = valor });

            using IDbConnection connection = await GetConnectionAsync(Database.Aluno);
            return await connection.QueryFirstAsync<DetalheAlunoArgumentoModel>(query.RawSql, query.Parameters);
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task<FichaAluno> ObterFichaAluno(int inscricao)
    {
        try
        {
            using IDbConnection connection = await GetConnectionAsync(Database.Aluno);

            string queryAluno = "SELECT * FROM aluno WHERE id = (SELECT id_aluno FROM detalhe WHERE codigo_inscricao = @Inscricao)";

            string queryEndereco = "SELECT * FROM endereco WHERE id_aluno = (SELECT id_aluno FROM detalhe WHERE codigo_inscricao = @Inscricao)";

            string queryTelefone = "SELECT * FROM telefone WHERE id_aluno = (SELECT id_aluno FROM detalhe WHERE codigo_inscricao = @Inscricao)";

            string queryDetalhe = "SELECT * FROM detalhe WHERE codigo_inscricao = @Inscricao";

            var aluno = await connection.QueryFirstOrDefaultAsync<AlunoModel>(queryAluno, new { Inscricao = inscricao });
            var endereco = await connection.QueryFirstOrDefaultAsync<EnderecoAlunoModel>(queryEndereco, new { Inscricao = inscricao });
            var telefone = await connection.QueryFirstOrDefaultAsync<TelefoneAlunoModel>(queryTelefone, new { Inscricao = inscricao });
            var detalhe = await connection.QueryFirstOrDefaultAsync<DetalheAlunoModel>(queryDetalhe, new { Inscricao = inscricao });

            return new()
            {
                Aluno = aluno,
                Endereco = endereco,
                Telefone = telefone,
                DetalhesFicha = detalhe
            };
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

}
