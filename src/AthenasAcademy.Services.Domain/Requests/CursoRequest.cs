using System.ComponentModel.DataAnnotations;

namespace AthenasAcademy.Services.Domain.Requests;

public class CursoRequest
{
    [Required(ErrorMessage = "O campo Id é obrigatório.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo Nome deve ter no máximo 100 caracteres.")]
    public string Nome { get; set; }

    [StringLength(500, ErrorMessage = "O campo Descrição deve ter no máximo 500 caracteres.")]
    public string Descricao { get; set; }

    public int CargaHoraria { get; set; }

    public List<DisciplinaRequest> Disciplinas { get; set; }

    public AreaConhecimentoRequest AreaConhecimento { get; set; }
}


