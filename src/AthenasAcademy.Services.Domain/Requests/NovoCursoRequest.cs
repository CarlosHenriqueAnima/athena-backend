using System.ComponentModel.DataAnnotations;

namespace AthenasAcademy.Services.Domain.Requests;

public class NovoCursoRequest
{
    [Required(ErrorMessage = "O campo 'Nome' é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo Nome deve ter no máximo 100 caracteres.")]
    public string Nome { get; set; }

    [StringLength(500, ErrorMessage = "O campo 'Descricao' deve ter no máximo 500 caracteres.")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O campo 'IdAreaConhecimento' é obrigatório.")]
    public int IdAreaConhecimento { get; set; }

    //[Required(ErrorMessage = "É necessário informar pelo menos uma disciplina.")]
    //public List<DisciplinaRequest> Disciplinas { get; set; }

    //[Required(ErrorMessage = "É necessário informar a Área de Conhecimento.")]
    //public AreaConhecimentoRequest AreaConhecimento { get; set; }
}
