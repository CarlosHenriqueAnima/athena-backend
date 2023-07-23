using System.ComponentModel.DataAnnotations;

namespace AthenasAcademy.Services.Domain.Requests;

public class NovaDisciplinaRequest
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo Nome deve ter no máximo 100 caracteres.")]
    public string Nome { get; set; }

    [StringLength(500, ErrorMessage = "O campo Descrição deve ter no máximo 500 caracteres.")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O campo CargaHoraria é obrigatório.")]
    [Range(1, 1000, ErrorMessage = "A Carga Horária deve estar entre 1 e 1000 horas.")]
    public int CargaHoraria { get; set; }

    [Required(ErrorMessage = "O ID do Curso é obrigatório.")]
    public int IdCurso { get; set; }
}
