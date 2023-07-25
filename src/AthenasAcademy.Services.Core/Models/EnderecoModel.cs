namespace AthenasAcademy.Services.Core.Models;

public class EnderecoModel
{
    public int Id { get; set; }
    public int AlunoId { get; set; }
    public string RGNumero { get; set; }
    public DateTime? RGDataEmissao { get; set; }
    public string RGOrgExpeditor { get; set; }
}
