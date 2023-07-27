using AthenasAcademy.Services.Core.CrossCutting;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;

namespace AthenasAcademy.Services.Test.Factory
{
    public class DisciplinaFactory
    {
        public DisciplinaRequest ObterDisciplinaRequestValido()
        {
            return new DisciplinaRequest
            {
                Id = 1,
                Nome = "Programação Orientada a Objetos",
                Descricao = "Introdução aos conceitos de programação orientada a objetos.",
                CargaHoraria = 40,
                IdCurso = 1
            };
        }

        public List<DisciplinaRequest> ObterListaDisciplinaRequestValidas()
        {
            return Enumerable.Range(1, 2).Select(index => new DisciplinaRequest
            {
                Id = index,
                Nome = $"Disciplina {index}",
                Descricao = $"Descrição da disciplina {index}",
                CargaHoraria = 40 + index,
                IdCurso = 1
            }).ToList();
        }

        public DisciplinaModel ObterDisciplinaModelValido()
        {
            return new DisciplinaModel
            {
                Id = 1,
                Nome = "Disciplina",
                Descricao = "Descrição da disciplina",
                CargaHoraria = 40,
                IdCurso = 1,
                Ativo = true,
                DataCadastro = DateTime.Now.AddDays(-7),
                DataAlteracao = null
            };
        }

        public List<DisciplinaModel> ObterListaDisciplinaModelValidos()
        {
            return Enumerable.Range(1, 2).Select(index => new DisciplinaModel
            {
                Id = index,
                Nome = $"Disciplina {index}",
                Descricao = $"Descrição da disciplina {index}",
                CargaHoraria = 40 + index,
                IdCurso = 1,
                Ativo = true,
                DataCadastro = DateTime.Now.AddDays(index),
                DataAlteracao = null
            }).ToList();
        }

        public NovaDisciplinaRequest ObterNovaDisciplinaRequestValido()
        {
            return new NovaDisciplinaRequest
            {
                Nome = "Programação Orientada a Objetos",
                Descricao = "Introdução aos conceitos de programação orientada a objetos.",
                CargaHoraria = 40,
                IdCurso = 1
            };
        }

        public List<DisciplinaResponse> ObterListaDisciplinaResponseValidos()
        {
            return Enumerable.Range(1, 2).Select(index => new DisciplinaResponse
            {
                Id = index,
                Nome = $"Disciplina {index}",
                CargaHoraria = 40 + index
            }).ToList();
        }

        public List<Disciplina> ObterListaDisciplinasValidas()
        {
            return Enumerable.Range(1, 2).Select(index => new Disciplina
            {
                Id = index,
                Nome = $"Disciplina {index}",
                Descricao = $"Descrição da disciplina {index}",
                CargaHoraria = 40 + index
            }).ToList();
        }
    }
}
