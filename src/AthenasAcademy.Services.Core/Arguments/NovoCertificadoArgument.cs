namespace AthenasAcademy.Services.Core.Arguments;

public class NovoCertificadoArgument
{
    public int Id { get; set; }

    public string NomeAluno { get; set; }

    public int Matricula { get; set; }

    public string NomeCurso { get; set; }

    public string CodigoCurso { get; set; }

    public int CargaHorariaCurso { get; set; }

    public decimal Aproveitamento { get; set; }

    public DateTime DataConclusao { get; set; }

    public bool Gerado { get; set; }

    public string CaminhoCertificadoPdf { get; set; }

    public string CaminhoCertificadoPng { get; set; }
}