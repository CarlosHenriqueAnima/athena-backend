using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;

namespace AthenasAcademy.Services.Core.Services;

public class UsuarioService : IUsuarioService
{
    public Task<NovoUsuarioResponse> CadastrarUsuario(NovoUsuarioRequest novoUsuario)
    {
        throw new NotImplementedException();
    }

    public Task<LoginUsuarioResponse> LoginUsuario(LoginUsuarioRequest loginUsuario)
    {
        throw new NotImplementedException();
    }
}