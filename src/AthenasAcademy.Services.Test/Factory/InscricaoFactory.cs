using AthenasAcademy.Services.Core.CrossCutting;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;
using Microsoft.AspNetCore.Components.Forms;

namespace AthenasAcademy.Services.Test.Factory
{
    public class InscricaoFactory
    {
        private readonly CursoFactory _cursoFactory;
        private readonly DisciplinaFactory _disciplinaFactory;
        private readonly AlunoFactory _alunoFactory;
        public InscricaoFactory()
        {
            _cursoFactory = new CursoFactory();
            _disciplinaFactory = new DisciplinaFactory();
            _alunoFactory = new AlunoFactory();
        }
        public NovaInscricaoCandidatoRequest ObterNovaInscricaoCandidatoRequestValido()
        {
            var endereco = _alunoFactory.ObterNovoEnderecoRequestValido();

            var telefone = _alunoFactory.ObterNovoTelefoneRequestValido();

            var opcaoCurso = _cursoFactory.ObterOpcaoCursoRequestValido();

            return new NovaInscricaoCandidatoRequest
            {
                NomeCompleto = "João da Silva",
                CPF = "123.456.789-00",
                Sexo = 'M',
                DataNascimento = new DateTime(1990, 05, 15),
                Email = "joao.silva@example.com",
                Endereco = endereco,
                Telefone = telefone,
                Curso = opcaoCurso
            };
        }

        public InscricaoCandidatoModel ObterInscricaoCandidatoModelValido()
        {
            return new InscricaoCandidatoModel
            {
                Id = 1,
                CodigoInscricao = 12345,
                DataInscricao = DateTime.Now,
                Nome = "João da Silva",
                Email = "joao.silva@example.com",
                Telefone = "(11) 99999-9999",
                CodigoCurso = "CSC001",
                NomeCurso = "Ciências da Computação",
                Boleto = "1234567890",
                BoletoPago = false
            };
        }

    }
}
