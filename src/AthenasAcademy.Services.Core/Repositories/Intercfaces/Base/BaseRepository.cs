using AthenasAcademy.Services.Core.Configurations.Enums;
using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Domain.Configurations.Enums;
using Npgsql;
using System.Data;

namespace AthenasAcademy.Services.Core.Repositories.Intercfaces.Base;

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
        if (_connection is null)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString(database.ToString());
                _connection = new NpgsqlConnection(connectionString);
                _connection.Open();
            }
            catch (Exception ex)
            {
                throw new DatabaseCustomException("Erro ao tentar se conectar com a base de dados.", ExceptionResponseType.Error, ex);
            }
        }

        return _connection;
    }
}