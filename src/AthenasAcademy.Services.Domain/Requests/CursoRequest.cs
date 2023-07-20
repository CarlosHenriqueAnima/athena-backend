namespace AthenasAcademy.Services.Domain.Requests;

public class CursoRequest
{
    public string Nome { get; set; }
    
    public string Descricao { get; set; }

    public int CargaHoraria { get; set; }

    public List<DisciplinaRequest> Disciplinas { get; set; }

    public AreaConhecimentoRequest AreaConhecimento { get; set; }
}
