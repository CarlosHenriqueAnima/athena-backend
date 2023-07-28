namespace AthenasAcademy.Services.Domain.Responses;

public class DadosUsuarioResponse
{
    public int? IdUsuario { get; set; }
    public string Login { get; set; }
    public DateTime? DataCadastroUsuario { get; set; }
    public string NomeAluno { get; set; }
    public string CPF { get; set; }
    public char Sexo { get; set; }
    public DateTime? DataNascimento { get; set; }
    public int? CodigoInscricao { get; set; }
    public DateTime? DataInscricao { get; set; }
    public bool? BoletoPago { get; set; }
    public string DiretorioBoleto { get; set; }
    public string NomeCurso { get; set; }
    public string DescricaoCurso { get; set; }
    public int? CargaHorariaCurso { get; set; }
    public string AreaDoConhecimento { get; set; }

    public List<DadosDisciplinaUsuarioResponse> Disciplinas { get; set; }

    public int? Matricula { get; set; }
    public bool? Ativa { get; set; }
    public DateTime? DataMatricula { get; set; }
    public int? Contrato { get; set; }
    public bool? Assinado { get; set; }
    public string FormaPagamento { get; set; }
    public decimal? ValorPagamento { get; set; }
    public DateTime? DataAceite { get; set; }
    public string DiretorioContrato { get; set; }
    public decimal? Aproveitamento { get; set; }
    public DateTime? DataConclusao { get; set; }
    public bool? Gerado { get; set; }
    public string DiretorioCertificadoPDF { get; set; }
    public string DiretorioCertificadoPNG { get; set; }
}

public class DadosDisciplinaUsuarioResponse
{
    public string Disciplina { get; set; }
    public string DescricaoDisciplina { get; set; }
    public int? CargaHorariaDisciplina { get; set; }
}