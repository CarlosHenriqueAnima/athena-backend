using System.ComponentModel.DataAnnotations;

namespace AthenasAcademy.Services.Domain.Requests;

public class NovoUsuarioRequest
{
    [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatorio.")]
    [EmailAddress(ErrorMessage = "Email deve seguir as especificações RFC 5322, [usuario]@[dominio].[topo do dominio]")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatorio.")]
    [EmailAddress(ErrorMessage = "Email deve seguir as especificações RFC 5322, [usuario]@[dominio].[topo do dominio]")]
    [Compare(nameof(Email), ErrorMessage = "As informações de e-mail não conferem. Por favor, verifique se os endereços de e-mail digitados são os mesmos")]
    public string ConfirmEmail { get; set; }

    [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatorio.")]
    [MinLength(6, ErrorMessage = "A senha deve ter no mínimo {1} caracteres.")]
    [MaxLength(20, ErrorMessage = "A senha deve ter no máximo {1} caracteres.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,20}$", ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula, um número, um caractere especial e ter entre 6 e 20 caracteres.")]
    public string Senha { get; set; }
}