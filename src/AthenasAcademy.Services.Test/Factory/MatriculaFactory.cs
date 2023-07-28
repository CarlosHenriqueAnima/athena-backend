using AthenasAcademy.Services.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthenasAcademy.Services.Test.Factory
{
    public class MatriculaFactory
    {
        public MatriculaModel ObterMatriculaModelValida()
        {
            return new MatriculaModel
            {
                MatriculaAlunoId = 1,
                Matricula = 12345,
                Ativa = true,
                CodigoAluno = 7890,
                NomeAluno = "Maria da Silva",
                DataMatricula = DateTime.Now.AddDays(-15),
                CodigoContrato = 9876,
                ContratoAlunoId = 2,
                Assinado = true,
                FormaPagamento = "Boleto",
                ValorContrato = 150.00m,
                DataAceite = DateTime.Now.AddDays(-10)
            };
        }

        public MatriculaModel ObterMatriculaModelInativa()
        {
            return new MatriculaModel
            {
                MatriculaAlunoId = 1,
                Matricula = 12345,
                Ativa = true,
                CodigoAluno = 7890,
                NomeAluno = "Maria da Silva",
                DataMatricula = DateTime.Now.AddDays(-15),
                CodigoContrato = 9876,
                ContratoAlunoId = 2,
                Assinado = true,
                FormaPagamento = "Boleto",
                ValorContrato = 150.00m,
                DataAceite = DateTime.Now.AddDays(-10)
            };
        }
    }
}
