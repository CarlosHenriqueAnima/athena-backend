using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Configurations.Enums;
using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Domain.Configurations.Enums;
using Npgsql;
using System.Data;
using System.Data.Common;

namespace AthenasAcademy.Services.Core.Repositories.Interfaces.Base;

public class BaseRepository
{
    private readonly IConfiguration _configuration;
    private static NpgsqlConnection _connection;

    protected BaseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected async Task<IDbConnection> GetConnectionAsync(Database database)
    {
        try
        {
            NpgsqlConnectionStringBuilder builder = new(GetConnectionString(Database.Athenas));

            builder.MaxPoolSize = 100;
            builder.MinPoolSize = 1;
            builder.ConnectionIdleLifetime = 500;
            builder.Pooling = true;

            _connection = new NpgsqlConnection(builder.ConnectionString);

            await _connection.OpenAsync();
            return _connection;
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException($"Erro ao tentar se conectar com a base de dados. {ex.Message}", ExceptionResponseType.Error, ex);
        }
    }

    private string GetConnectionString(Database database) 
    {
        return database switch
        {
            Database.Athenas => _configuration["AthenasBase"],
            Database.Usuario => _configuration["UsuarioBase"],
            Database.Inscricao => _configuration["InscricaoBase"],
            Database.Aluno => _configuration["AlunoBase"],
            Database.Matricula => _configuration["MatriculaBase"],
            Database.Pagamento => _configuration["PagamentoBase"],
            Database.Curso => _configuration["CursoBase"],
            Database.Certificado => _configuration["CertificadoBase"],
            _ => throw new NotImplementedException($"Database {database} não implementado."),
        };
    }
}