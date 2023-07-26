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

public class UsuarioRepository : BaseRepository, IUsuarioRepository
{
    public UsuarioRepository(IConfiguration configuration) : base(configuration) { }

    public async Task<UsuarioModel> BuscarUsuario(UsuarioArgument novoUsuario)
    {
        try
        {
            using IDbConnection connection = GetConnection(Database.Usuario);
            string query = @"SELECT 
                                    id,
                                    usuario, 
                                    senha_hash AS Senha, 
                                    id_perfil AS Perfil
                                FROM usuario
                                WHERE usuario = @Usuario OR email = @Usuario";

            return await connection.QueryFirstOrDefaultAsync<UsuarioModel>(query, new { Usuario = novoUsuario.Email });
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task<IEnumerable<UsuarioModel>> BuscarUsuarios()
    {
        try
        {
            using IDbConnection connection = GetConnection(Database.Usuario);
            string query = @"SELECT 
                                    id,
                                    usuario, 
                                    senha_hash AS Senha, 
                                    id_perfil AS Perfil,
                                    ativo,
                                    data_cadastro As DataCadastro
                                FROM usuario";

            return await connection.QueryAsync<UsuarioModel>(query);
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }

    public async Task<UsuarioModel> CadastrarUsuario(NovoUsuarioArgument novoUsuario)
    {
        try
        {
            using IDbConnection connection = GetConnection(Database.Usuario);
            string query = @"INSERT INTO usuario (usuario, email, senha_hash, ativo, data_cadastro, data_alteracao, id_perfil)
                             VALUES (@Usuario, @Email, @Senha, @Ativo, @DataCadastro, @DataAlteracao, 2)
                             RETURNING id, usuario, senha_hash AS Senha, id_perfil AS Perfil";

            return await connection.QueryFirstOrDefaultAsync<UsuarioModel>(query, novoUsuario);
        }
        catch (Exception ex)
        {
            throw new DatabaseCustomException(ex.Message, ExceptionResponseType.Error);
        }
    }
}