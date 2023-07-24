using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Core.Repositories.Interfaces;

public interface IUsuarioRepository
{
    Task<UsuarioModel> BuscarUsuario(UsuarioArgument novoUsuario);

    Task<NovoUsuarioModel> CadastrarUsuario(NovoUsuarioArgument novoUsuario);
    List<string> GetCredentials();
}