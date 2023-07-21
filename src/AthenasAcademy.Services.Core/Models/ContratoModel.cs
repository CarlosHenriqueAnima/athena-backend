namespace AthenasAcademy.Services.Core.Models;

public class ContratoModel
{
    public int Id { get; set; }
    public string Contrato { get; set; }
    public int DetalheContratoId { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAlteracao { get; set; }

    public DetalheContratoModel DetalheContrato { get; set; }
}