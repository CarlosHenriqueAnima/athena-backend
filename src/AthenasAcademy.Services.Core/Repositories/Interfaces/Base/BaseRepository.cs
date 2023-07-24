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
            _connection = new NpgsqlConnection(_configuration.GetConnectionString(database.ToString()));

        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException("Erro ao tentar se conectar com a base de dados.", ExceptionResponseType.Error, ex);
        }

        _connection.Open();
        return _connection;
    }
}