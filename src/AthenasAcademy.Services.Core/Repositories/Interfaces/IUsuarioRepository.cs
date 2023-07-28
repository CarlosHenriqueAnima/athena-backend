using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Core.Repositories.Interfaces;

/// <summary>
/// Interface responsável por definir os métodos para o repositório de usuários.
/// </summary>
public interface IUsuarioRepository
{
    /// <summary>
    /// Busca um usuário com base nos dados fornecidos no argumento.
    /// </summary>
    /// <param name="novoUsuario">Os dados do usuário a serem buscados.</param>
    /// <returns>Uma tarefa que representa a busca do usuário.</returns>
    Task<UsuarioModel> BuscarUsuario(UsuarioArgument novoUsuario);

    /// <summary>
    /// Realiza o cadastro de um novo usuário com base nos dados fornecidos no argumento.
    /// </summary>
    /// <param name="novoUsuario">Os dados do novo usuário a ser cadastrado.</param>
    /// <returns>Uma tarefa que representa o cadastro do novo usuário.</returns>
    Task<UsuarioModel> CadastrarUsuario(NovoUsuarioArgument novoUsuario);

    /// <summary>
    /// Obtém os dados completos de um usuário com base no seu nome de usuário (usuário/email).
    /// </summary>
    /// <param name="usuario">O nome de usuário (usuário/email) do usuário a ser buscado.</param>
    /// <returns>Uma tarefa que representa a obtenção dos dados completos do usuário.</returns>
    Task<IEnumerable<DadosUsuarioModel>> ObterDadosCompletosUsuario(string usuario);
}
