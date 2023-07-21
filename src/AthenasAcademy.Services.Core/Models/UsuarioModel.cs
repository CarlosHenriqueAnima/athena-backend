using AthenasAcademy.Services.Core.Configurations.Enums;

namespace AthenasAcademy.Services.Core.Models;

public class UsuarioModel
{
    public string Usuario { get; set; }

    public string Senha { get; set; }

    public Role Perfil { get; set; }
}    