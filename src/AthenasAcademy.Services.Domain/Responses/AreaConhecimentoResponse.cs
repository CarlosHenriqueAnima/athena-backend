namespace AthenasAcademy.Services.Domain.Responses;

public class AreaConhecimentoResponse
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public string Descricao { get; set; }

    public bool Ativo { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime DataAtualizacao { get; set; }
}