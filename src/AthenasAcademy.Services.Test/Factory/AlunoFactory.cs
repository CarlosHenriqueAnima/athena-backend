using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.CrossCutting;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Domain.Requests;

namespace AthenasAcademy.Services.Test.Factory
{
    public class AlunoFactory
    {
        public readonly CursoFactory _cursoFactory;
        public AlunoFactory()
        {
            _cursoFactory = new CursoFactory();
        }
        public AlunoModel ObterAlunoModelValido()
        {
            return new AlunoModel()
            {
                Nome = "Joao Tenorio",
                Sobrenome = "Tenorio",
                CPF = "12345678910",
                Sexo = 'M',
                DataNascimento = new DateTime(2000, 1, 1, 12, 0, 0),
                Email = "email@dominio.com"
            };
        }

        public NovoAlunoArgument RetornarNovoAlunoArgumentValido()
        {
            return new NovoAlunoArgument()
            {
                Nome = "Joao Tenorio",
                Sobrenome = "Tenorio",
                CPF = "12345678910",
                Sexo = 'M',
                DataNascimento = new DateTime(2000, 1, 1, 12, 0, 0),
                Email = "email@dominio.com"
            };
        }

        public NovoDetalheAlunoArgument RetornarNovoDetalheAlunoArgumentValido()
        {
            return new NovoDetalheAlunoArgument
            {
                IdAluno = 1,
                CodigoUsuario = 1,
                DataUsuario = new DateTime(2023, 7, 26, 10, 30, 0),
                CodigoInscricao = 1,
                DataInscricao = new DateTime(2023, 7, 26),
                CodigoMatricula = 1,
                CodigoContrato = 1
            };
        }

        public DetalheAlunoModel ObterDetalheAlunoModelValido()
        {
            return new DetalheAlunoModel
            {
                IdAluno = 1,
                CodigoUsuario = 1,
                DataUsuario = new DateTime(2023, 7, 26, 10, 30, 0),
                CodigoInscricao = 1,
                DataInscricao = new DateTime(2023, 7, 26),
                CodigoMatricula = 1,
                CodigoContrato = 1
            };
        }

        public DetalheAlunoArgumentoModel RetornarDetalheAlunoArgumentoModelValido()
        {
            return new DetalheAlunoArgumentoModel
            {
                IdAluno = 1,
                CodigoUsuario = 1,
                DataUsuario = new DateTime(2023, 7, 26, 10, 30, 0),
                CodigoInscricao = 1,
                DataInscricao = new DateTime(2023, 7, 26),
                CodigoMatricula = 1,
                CodigoContrato = 1,
                NomeCompleto = "Nome Completo do Aluno"
            };
        }

        public EnderecoAlunoModel ObterEnderecoAlunoModelValido()
        {
            return new EnderecoAlunoModel
            {
                Id = 1,
                IdAluno = 1,
                Logradouro = "Rua x",
                Numero = "1",
                Complemento = "Apto y",
                Bairro = "Bairro k",
                Localidade = "Cidade z",
                UF = "UF",
                CEP = "12345-678",
                Ativo = true,
                DataCadastro = DateTime.Now.AddDays(-7)
            };
        }

        public NovoEnderecoAlunoArgument RetornarNovoEnderecoAlunoArgumentValido()
        {
            return new NovoEnderecoAlunoArgument
            {
                IdAluno = 1,
                Logradouro = "Rua x",
                Numero = "1",
                Complemento = "Apto y",
                Bairro = "Bairro k",
                Localidade = "Cidade z",
                UF = "UF",
                CEP = "12345-678"
            };
        }

        public NovoTelefoneRequest ObterNovoTelefoneRequestValido()
        {
            return new NovoTelefoneRequest
            {
                TelefoneCelular = "99999-9999",
                TelefoneResidencial = "99999-9999",
                TelefoneRecado = "99999-9999"
            };
        }

        public NovoEnderecoRequest ObterNovoEnderecoRequestValido()
        {
            return new NovoEnderecoRequest
            {
                Logradouro = "Rua das Flores",
                Numero = "123",
                Complemento = "Ap. 5",
                Bairro = "Centro",
                Localidade = "São Paulo",
                UF = "SP",
                CEP = "01010-010"
            };
        }

        public NovoTelefoneAlunoArgument RetornarNovoTelefoneAlunoArgumentValido()
        {
            return new NovoTelefoneAlunoArgument
            {
                IdAluno = 1,
                TelefoneCelular = "99999-9999",
                TelefoneResidencial = "99999-9999",
                TelefoneRecado = "99999-9999"
            };
        }

        public TelefoneAlunoModel ObterTelefoneAlunoModelValido()
        {
            return new TelefoneAlunoModel
            {
                IdAluno = 1,
                TelefoneCelular = "99999-9999",
                TelefoneResidencial = "99999-9999",
                TelefoneRecado = "99999-9999"
            };
        }

        public FichaAluno ObterFichaAlunoValida()
        {
            var aluno = ObterAlunoModelValido();
            var endereco = ObterEnderecoAlunoModelValido();
            var telefone = ObterTelefoneAlunoModelValido();
            var detalhesFicha = ObterDetalheAlunoModelValido();
            var OpcaoCurso = _cursoFactory.ObterOpcaoCursoValida();
            var obterContrato = ObterContratoExemplo();
            return new FichaAluno()
            {
                Aluno = aluno,
                Endereco = endereco,
                Telefone = telefone,
                DetalhesFicha = detalhesFicha,
                OpcaoCurso = OpcaoCurso,
                Contrato = obterContrato
            };
        }

        public Contrato ObterContratoExemplo()
        {
            return new Contrato
            {
                NumeroContrato = 123456,
                Matricula = 7890,
                Assinado = true
            };
        }
    }
}
