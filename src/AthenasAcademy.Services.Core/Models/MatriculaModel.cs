namespace AthenasAcademy.Services.Core.Models;

public class MatriculaModel
{
    public int MatriculaAlunoId { get; set; }
    public int Matricula { get; set; }
    public bool Ativa { get; set; }
    public int CodigoAluno { get; set; }
    public string NomeAluno { get; set; }
    public DateTime? DataMatricula { get; set; }
    public int CodigoContrato { get; set; }
    public int ContratoAlunoId { get; set; }
    public bool Assinado { get; set; }
    public string FormaPagamento { get; set; }
    public decimal ValorContrato { get; set; }
    public DateTime? DataAceite { get; set; }
}
