using AthenasAcademy.Services.Core.Configurations.Enums;
using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Domain.Configurations.Enums;
using Npgsql;
using System.Data;

namespace AthenasAcademy.Services.Core.Repositories.Interfaces.Base;

public class BaseRepository
{
    private readonly IConfiguration _configuration;
    private static NpgsqlConnection _connection;

    protected BaseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected IDbConnection GetConnection(Database database)
    {
        try
        {
            //if (_connection is null)
            //    _connection = new NpgsqlConnection(_configuration.GetConnectionString(database.ToString()));
<<<<<<< HEAD
            _connection = new NpgsqlConnection(_configuration.GetConnectionString(database.ToString()));
=======
            _connection = new NpgsqlConnection(GetConnectionString(database));
>>>>>>> dev

        }
        catch (Exception ex)
        {
<<<<<<< HEAD
            throw new DatabaseCustomException("Erro ao tentar se conectar com a base de dados.", ExceptionResponseType.Error, ex);
=======
            throw new DatabaseCustomException($"Erro ao tentar se conectar com a base de dados. {ex.Message}", ExceptionResponseType.Error, ex);
>>>>>>> dev
        }

        _connection.Open();
        return _connection;
    }

    private string GetConnectionString(Database database) 
    {
        switch (database)
        {
            case Database.Usuario:
                return _configuration["UsuarioBase"];

            case Database.Inscricao:
                return _configuration["InscricaoBase"];

            case Database.Aluno:
                return _configuration["AlunoBase"];

            case Database.Matricula:
                return _configuration["MatriculaBase"];

            case Database.Pagamento:
                return _configuration["PagamentoBase"];

            case Database.Curso:
                return _configuration["CursoBase"];

            case Database.Certificado:
                return _configuration["CertificadoBase"];

            default:
                throw new NotImplementedException($"Database {database} não implementado.");
        }
    }
}