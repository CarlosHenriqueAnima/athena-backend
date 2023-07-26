using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AthenasAcademy.Services.Domain.Requests;
public class NovoEnderecoRequest
{
    [Required(ErrorMessage = "O campo Logradouro é obrigatório.")]
    public string Logradouro { get; set; }

    [Required(ErrorMessage = "O campo Numero é obrigatório.")]
    public string Numero { get; set; }

    public string Complemento { get; set; }

    [Required(ErrorMessage = "O campo Bairro é obrigatório.")]
    public string Bairro { get; set; }

    [Required(ErrorMessage = "O campo Localidade é obrigatório.")]
    public string Localidade { get; set; }

    [Required(ErrorMessage = "O campo UF é obrigatório.")]
    [StringLength(2, MinimumLength = 2, ErrorMessage = "O UF deve conter exatamente 2 dígitos.")]
    public string UF { get; set; }

    [Required(ErrorMessage = "O campo CEP é obrigatório.")]
    [StringLength(8, MinimumLength = 8, ErrorMessage = "O CEP deve conter exatamente 8 dígitos.")]
    public string CEP { get; set; }

    [JsonIgnore]
    public bool Ativo => true;
}