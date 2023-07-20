using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Core.Repositories.Intercfaces;

namespace AthenasAcademy.Services.Core.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    public Task<UsuarioModel> BuscarUsuario(UsuarioArgument novoUsuario)
    {
        throw new NotImplementedException();
    }

    public Task<NovoUsuarioModel> CadastrarUsuario(NovoUsuarioArgument novoUsuario)
    {
        throw new NotImplementedException();
    }
}