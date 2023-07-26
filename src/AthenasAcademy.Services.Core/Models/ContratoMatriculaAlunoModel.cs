namespace AthenasAcademy.Services.Core.Models;

public class ContratoMatriculaAlunoModel
{
    public int Id { get; set; }
    public int IdMatricula { get; set; }
    public int Contrato { get; set; }
    public bool Assinado { get; set; }
    public string FormaPagamento { get; set; }
    public decimal ValorContrato { get; set; }
    public DateTime DataAceite { get; set; }
}