namespace AthenasAcademy.Services.Core.Models;

public class TelefoneAlunoModel
{
    public int Id { get; set; }
    public int IdAluno { get; set; }
    public string TelefoneResidencial { get; set; }
    public string TelefoneCelular { get; set; }
    public string TelefoneRecado { get; set; }
}