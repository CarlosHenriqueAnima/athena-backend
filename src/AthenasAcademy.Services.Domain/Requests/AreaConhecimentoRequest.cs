using System.ComponentModel.DataAnnotations;

namespace AthenasAcademy.Services.Domain.Requests;

public class AreaConhecimentoRequest
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo Nome deve ter no máximo 100 caracteres.")]
    public string Nome { get; set; }

    [StringLength(500, ErrorMessage = "O campo Descrição deve ter no máximo 500 caracteres.")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O campo Ativo é obrigatório.")]
    public bool Ativo { get; set; }

    [Required(ErrorMessage = "O campo DataCadastro é obrigatório.")]
    public DateTime DataCadastro { get; set; }

    [Required(ErrorMessage = "O campo DataAtualizacao é obrigatório.")]
    public DateTime DataAtualizacao { get; set; }
}

