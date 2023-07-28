namespace AthenasAcademy.Services.Domain.Responses;

public class LoginUsuarioResponse
{
    public bool Resultado { get; set; }

    public TokenResponse Token { get; set; }

    public FichaAlunoRequest FichaAluno { get; set; }
}

public class FichaAlunoRequest
{
    public Aluno AlunoDetalhes { get; set; }

    public Curso CursoDetalhes { get; set; }

    public Inscricao InscricaoDetalhes { get; set; }

    public Matricula MatriculaDetalhes { get; set; }

    public Certificado CertificadoDetalhes { get; set; }
}

public class Aluno
{
    public string Nome { get; set; }

    public string CPF { get; set; }

}

public class Curso
{
    public int Id { get; set; }
    
    public string Nome { get; set; }

    public string Descricao { get; set; }

    public string AreaConhecimento { get; set; }

    public int CargaHoraria { get; set; }

    public List<Disciplinas> Disciplinas { get; set; }
}

public class Disciplinas
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public string Descricao { get; set; }

    public int CargaHoraria { get; set; }
}

public class Inscricao
{
    public int CodigoInscicao { get; set; }

    public DateTime DataInscricao { get; set; }

    public string Boleto { get; set; }

    public DateTime DataPagamento { get; set; }
}

public class Matricula
{
    public int CodigoMatricula { get; set; }

    public DateTime DataMatricula { get; set; }

    public int CodigoContrato { get; set; }

    public bool Assinado { get; set; }

    public string Contrato { get; set; }
}

public class Certificado
{
    public string CertificadoPDF { get; set; }

    public int Aproveitamento { get; set; }
}