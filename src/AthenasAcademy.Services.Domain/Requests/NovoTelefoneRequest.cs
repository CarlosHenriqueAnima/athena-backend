using System.ComponentModel.DataAnnotations;

namespace AthenasAcademy.Services.Domain.Requests;

public class NovoTelefoneRequest
{
    [RegularExpression(@"^[0-9]{2} [0-9]{4}-[0-9]{4}$", ErrorMessage = "O campo TelefoneResidencial deve estar no formato XX 9 xxxx-xxxx.")]
    public string TelefoneResidencial { get; set; }

    [Required(ErrorMessage = "O campo TelefoneCelular é obrigatório.")]
    [RegularExpression(@"^[0-9]{2} 9 [0-9]{4}-[0-9]{4}$", ErrorMessage = "O campo TelefoneCelular deve estar no formato XX 9 xxxx-xxxx.")]
    public string TelefoneCelular { get; set; }

    [RegularExpression(@"^[0-9]{2} [0-9]{4}-[0-9]{4}$", ErrorMessage = "O campo TelefoneRecado deve estar no formato XX 9 xxxx-xxxx.")]
    public string TelefoneRecado { get; set; }
}