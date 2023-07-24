using AthenasAcademy.Services.Domain.Configurations.DTO;

namespace AthenasAcademy.Services.Core.Models;

public class UsuarioTokenModel
{
    public string Usuario { get; set; }
    public int Perfil { get; set; }
}