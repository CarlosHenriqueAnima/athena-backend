using AthenasAcademy.Services.Core.Models;
using Moq;

namespace AthenasAcademy.Services.Test.Factory
{
    public class CursoFactory
    {
        public CursoModel ObterCursoModelValido()
        {
            return new CursoModel()
            {
                Id = 1,
                Nome = "Curso de Programação",
                Descricao = "Um curso sobre programação",
                CargaHoraria = 40,
                IdAreaConhecimento = 123,
                Ativo = true,
                DataCadastro = DateTime.Now,
                DataAlteracao = null
            };
        }

        public List<CursoModel> ObterListaDeCursos()
        {
            return Enumerable.Range(1, 2).Select(index => new CursoModel
            {
                Id = index,
                Nome = $"Curso de Programação {index}",
                Descricao = $"Um curso sobre programação {index}",
                CargaHoraria = 40 + index,
                IdAreaConhecimento = 123 + index,
                Ativo = index % 2 == 0,
                DataCadastro = DateTime.Now.AddDays(index),
                DataAlteracao = null
            }).ToList();
        }

    }
}
