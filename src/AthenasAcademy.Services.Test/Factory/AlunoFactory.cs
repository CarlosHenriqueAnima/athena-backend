using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Models;

namespace AthenasAcademy.Services.Test.Factory
{
    public class AlunoFactory
    {
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
                CodigoUsuario = "COD001",
                DataUsuario = new DateTime(2023, 7, 26, 10, 30, 0),
                CodigoInscricao = "INSC001",
                DataInscricao = new DateTime(2023, 7, 26),
                CodigoMatricula = "MAT001",
                CodigoContrato = "CON001"
            };
        }

        public DetalheAlunoModel ObterDetalheAlunoModelValido()
        {
            return new DetalheAlunoModel
            {
                IdAluno = 1,
                CodigoUsuario = "COD001",
                DataUsuario = new DateTime(2023, 7, 26, 10, 30, 0),
                CodigoInscricao = "INSC001",
                DataInscricao = new DateTime(2023, 7, 26),
                CodigoMatricula = "MAT001",
                CodigoContrato = "CON001"
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
    }
}
