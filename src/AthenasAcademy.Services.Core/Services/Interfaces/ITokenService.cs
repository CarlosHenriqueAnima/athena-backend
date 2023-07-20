using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Core.Services.Interfaces;

public interface ITokenService
{
    Task<UsuarioTokenModel> GerarTokenAsync(UsuarioModel user);
}