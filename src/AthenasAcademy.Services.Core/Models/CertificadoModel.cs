namespace AthenasAcademy.Services.Core.Models;

public class CertificadoModel
{
    public int Id { get; set; }

    public string NomeAluno { get; set; }

    public string Matricula { get; set; }

    public string NomeCurso { get; set; }

    public string CodigoCurso { get; set; }

    public int CargaHorariaCurso { get; set; }

    public decimal Aproveitamento { get; set; }

    public DateTime DataConclusao { get; set; }

    public bool Gerado { get; set; }

    public string CaminhoCertificadoPdf { get; set; }

    public string CaminhoCertificadoPng { get; set; }
}