using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Configurations.Enums;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Repositories.Interfaces.Base;

namespace AthenasAcademy.Services.Core.Repositories;

public class UsuarioRepository : BaseRepository, IUsuarioRepository
{
    public UsuarioRepository(IConfiguration configuration) : base(configuration)  { }

    public Task<UsuarioModel> BuscarUsuario(UsuarioArgument novoUsuario)
    {
        return Task.FromResult(
            new UsuarioModel
            {
                Usuario = "rafael.deroncio@example.com",
                Senha = "8bb0cf6eb9b17d0f7d22b456f121257dc1254e1f01665370476383ea776df414",
                Perfil = Role.Admin
            });
    }

    public Task<NovoUsuarioModel> CadastrarUsuario(NovoUsuarioArgument novoUsuario)
    {
        return Task.FromResult(
            new NovoUsuarioModel
            {
                Usuario = "rafael.deroncio@example.com",
                Senha = "1234567",
                Perfil = Role.Admin
            });
    }
}