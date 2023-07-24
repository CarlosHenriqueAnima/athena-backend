namespace AthenasAcademy.Services.Core.Models;

public class DisciplinaModel
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public string Descricao { get; set; }

    public int CargaHoraria { get; set; }

    public int IdCurso { get; set; }

    public bool Ativo { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }
}