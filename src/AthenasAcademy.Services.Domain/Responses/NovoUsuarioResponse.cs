using AthenasAcademy.Services.Domain.Configurations.DTO;
using System.Security.Principal;

namespace AthenasAcademy.Services.Domain.Responses;

public class NovoUsuarioResponse
{
    public bool Resultado { get; set; } = true;

    public string Usuario { get; set; }

    public TokenResponse Token { get; set; }
}