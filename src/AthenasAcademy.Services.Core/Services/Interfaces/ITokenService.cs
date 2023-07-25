using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Core.Services.Interfaces;

public interface ITokenService
{
    Task<TokenModel> GerarTokenUsuario(UsuarioTokenModel usuario);
    Task<string> GerarTokenRequestClient();
}