namespace AthenasAcademy.Services.Core.CrossCutting;

public class OpcaoCurso
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public string Descricao { get; set; }

    public int CargaHoraria { get; set; }

    public string AreaConhecimento  { get; set;}

    public List<Disciplina> Disciplinas { get; set; }
}

public class Disciplina
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public string Descricao { get; set; }

    public int CargaHoraria { get; set; }
}