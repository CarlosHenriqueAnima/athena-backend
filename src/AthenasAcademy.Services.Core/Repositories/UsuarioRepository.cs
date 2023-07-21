using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Interfaces;

namespace AthenasAcademy.Services.Core.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    public Task<UsuarioModel> BuscarUsuario(UsuarioArgument novoUsuario)
    {
        return Task.FromResult(
            new UsuarioModel
            {
                Usuario = "rafael.deroncio@example.com",
                //Senha = "asdasd"
                Senha = "8bb0cf6eb9b17d0f7d22b456f121257dc1254e1f01665370476383ea776df414"
            });
    }

    public Task<NovoUsuarioModel> CadastrarUsuario(NovoUsuarioArgument novoUsuario)
    {
        return Task.FromResult(
            new NovoUsuarioModel
            {
                Usuario = "rafael.deroncio@example.com",
                Senha = "1234567"
            });
    }
}