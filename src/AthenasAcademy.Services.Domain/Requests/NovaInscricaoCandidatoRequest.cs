using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AthenasAcademy.Services.Domain.Requests;

public class NovaInscricaoCandidatoRequest
{
    [Required(ErrorMessage = "O campo NomeCompleto é obrigatório.")]
    public string NomeCompleto { get; set; }

    [Required(ErrorMessage = "O campo CPF é obrigatório.")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve conter exatamente 11 dígitos.")]
    public string CPF { get; set; }

    [Required(ErrorMessage = "O campo Sexo é obrigatório.")]
    [RegularExpression("^(M|F|O)$", ErrorMessage = "O campo Sexo deve ser 'M', 'F' ou 'O'.")]
    public char Sexo { get; set; }

    [Required(ErrorMessage = "O campo DataNascimento é obrigatório.")]
    [DataType(DataType.Date, ErrorMessage = "O campo DataNascimento deve estar no formato de data válido.")]
    public DateTime DataNascimento { get; set; }

    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
    public string Email { get; set; }

    [JsonIgnore]
    public DateTime DataInscricao => DateTime.Now;

    [JsonIgnore]
    public bool Ativo => true;

    [Required(ErrorMessage = "O campo Endereco é obrigatório.")]
    public NovoEnderecoRequest Endereco { get; set; }

    [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
    public NovoTelefoneRequest Telefone { get; set; }

    [Required(ErrorMessage = "O campo OpcaoCurso é obrigatório.")]
    public OpcaoCursoRequest Curso { get; set; }
}

public class OpcaoCursoRequest
{
    [Required(ErrorMessage = "O campo CodigoCurso é obrigatório.")]
    public int CodigoCurso { get; set; }

    [Required(ErrorMessage = "O campo NomeCurso é obrigatório.")]
    public string NomeCurso { get; set; }
}