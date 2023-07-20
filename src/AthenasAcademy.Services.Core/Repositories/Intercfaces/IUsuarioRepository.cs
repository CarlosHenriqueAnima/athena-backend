using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Core.Repositories.Intercfaces;

public interface IUsuarioRepository
{
    Task<UsuarioModel> BuscarUsuario(UsuarioArgument novoUsuario);

    Task<NovoUsuarioModel> CadastrarUsuario(NovoUsuarioArgument novoUsuario);
}