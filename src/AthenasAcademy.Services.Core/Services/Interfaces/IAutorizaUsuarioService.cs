using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;

namespace AthenasAcademy.Services.Core.Services.Interfaces;

/// <summary>
/// Interface para o serviço de autorização de usuário.
/// </summary>
public interface IAutorizaUsuarioService
{
    /// <summary>
    /// Cadastra um novo usuário no sistema.
    /// </summary>
    /// <param name="novoUsuario">Dados do novo usuário a ser cadastrado.</param>
    /// <returns>Um objeto <see cref="NovoUsuarioResponse"/> que representa a resposta do cadastro.</returns>
    Task<NovoUsuarioResponse> CadastrarUsuario(NovoUsuarioRequest novoUsuario);

    /// <summary>
    /// Realiza o login de um usuário no sistema.
    /// </summary>
    /// <param name="loginUsuario">Dados do usuário para realizar o login.</param>
    /// <returns>Um objeto <see cref="LoginUsuarioResponse"/> que representa a resposta do login.</returns>
    Task<LoginUsuarioResponse> LoginUsuario(LoginUsuarioRequest loginUsuario);

    /// <summary>
    /// Obtém informações de um usuário com base no nome de usuário (usuario).
    /// </summary>
    /// <param name="usuario">O nome de usuário do usuário a ser obtido.</param>
    /// <param name="exception">Indica se deve ser lançada uma exceção se o usuário não for encontrado.</param>
    /// <returns>Um objeto <see cref="UsuarioModel"/> que representa as informações do usuário.</returns>
    Task<UsuarioModel> ObterUsuario(string usuario, bool exception);
}