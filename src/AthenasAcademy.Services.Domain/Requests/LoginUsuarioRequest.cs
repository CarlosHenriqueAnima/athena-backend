using System.ComponentModel.DataAnnotations;

namespace AthenasAcademy.Services.Domain.Requests;

public class LoginUsuarioRequest
{
    [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatorio.")]
    [EmailAddress(ErrorMessage = "Email deve seguir as especificações RFC 5322, [usuario]@[dominio].[topo do dominio]")]
    public string Email { get; set; }


    [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatorio.")]
    public string Senha { get; set; }
}