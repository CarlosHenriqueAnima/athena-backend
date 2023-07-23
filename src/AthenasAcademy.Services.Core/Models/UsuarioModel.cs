using AthenasAcademy.Services.Core.Configurations.Enums;

namespace AthenasAcademy.Services.Core.Models;

public class UsuarioModel
{
    public int Id { get; set; }

    public string Usuario { get; set; }

    public string Senha { get; set; }

    public Role Perfil { get; set; } = Role.Usuario;
}    