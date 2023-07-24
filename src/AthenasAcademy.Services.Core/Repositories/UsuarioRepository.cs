using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Configurations.Enums;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Repositories.Interfaces.Base;

namespace AthenasAcademy.Services.Core.Repositories;

public class UsuarioRepository : BaseRepository, IUsuarioRepository
{
    private IConfiguration _configuration; 
    public UsuarioRepository(IConfiguration configuration) : base(configuration)  
    {
        _configuration = configuration;
    }

    public Task<UsuarioModel> BuscarUsuario(UsuarioArgument novoUsuario)
    {
        return Task.FromResult(
            new UsuarioModel
            {
                Usuario = "rafael.deroncio@example.com",
                Senha = "8bb0cf6eb9b17d0f7d22b456f121257dc1254e1f01665370476383ea776df414",
                Perfil = Role.Administrador
            });
    }

    public Task<NovoUsuarioModel> CadastrarUsuario(NovoUsuarioArgument novoUsuario)
    {
        return Task.FromResult(
            new NovoUsuarioModel
            {
                Usuario = _configuration["AwsAccessKey"],
                Senha = _configuration["AlunoBase"],
                Perfil = Role.Administrador
            });
    }

    public List<string> GetCredentials()
    {
        var lista = new List<string>();
        lista.Add( _configuration["AwsAccessKey"]);
        lista.Add(_configuration["AlunoBase"]);
        lista.Add(_configuration["AwsBucketBase"]);
        return lista;
    }
}