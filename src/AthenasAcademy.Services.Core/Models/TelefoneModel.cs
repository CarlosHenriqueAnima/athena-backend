namespace AthenasAcademy.Services.Core.Models;

public class TelefoneModel
{
    public int Id { get; set; }
    public int AlunoId { get; set; }
    public string TelefoneResidencial { get; set; }
    public string TelefoneCelular { get; set; }
    public string TelefoneRecado { get; set; }
}
