namespace AthenasAcademy.Services.Core.Requests;

public class NovoCertificadoRequest
{
    public string NomeAluno { get; set; }

    public int Matricula { get; set; }

    public int Aproveitamento { get; set; }

    public DateTime DataConclusao { get; set; }

    public string NomeCurso { get; set; }

    public int CodigoCurso { get; set; }

    public int CargaHoraria { get; set; }
}