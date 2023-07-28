using AthenasAcademy.Services.Core.CrossCutting;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;

namespace AthenasAcademy.Services.Test.Factory
{
    public class CursoFactory
    {
        private readonly AreaConhecimentoFactory _areaConhecimentoFactory;
        private readonly DisciplinaFactory _disciplinaFactory;
        public CursoFactory()
        {
            _areaConhecimentoFactory = new AreaConhecimentoFactory();
            _disciplinaFactory = new DisciplinaFactory();
        }
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

        public NovoCursoRequest ObterNovoCursoRequestValido()
        {
            return new NovoCursoRequest()
            {
                Nome = "Anima Upskilling",
                Descricao = "Aceleração de carreira Backend",
                IdAreaConhecimento = 1
            };
        }

        public CursoRequest ObterCursoRequestValido(List<DisciplinaRequest> disciplinas, AreaConhecimentoRequest areaConhecimento)
        {
            return new CursoRequest()
            {
                Id = 1,
                Nome = "Curso de Programação",
                Descricao = "Um curso sobre programação",
                CargaHoraria = 40,
                Disciplinas = disciplinas,
                AreaConhecimento = areaConhecimento
            };
        }

        public OpcaoCursoRequest ObterOpcaoCursoRequestValido()
        {
            return new OpcaoCursoRequest
            {
                CodigoCurso = 1,
                NomeCurso = "Ciências da Computação"
            };
        }

        public OpcaoCurso ObterOpcaoCursoValida()
        {
            var disciplinas = _disciplinaFactory.ObterListaDisciplinasValidas();

            return new OpcaoCurso
            {
                Id = 1,
                Nome = "Ciências da Computação",
                Descricao = "Curso de Ciências da Computação com foco em desenvolvimento de software.",
                CargaHoraria = 200,
                AreaConhecimento = "Tecnologia da Informação",
                Disciplinas = disciplinas
            };
        }

        public CursoResponse ObterCursoResponseValido()
        {
            var disciplinas = _disciplinaFactory.ObterListaDisciplinaResponseValidos();

            var areaConhecimento = _areaConhecimentoFactory.ObterAreaConhecimentoResponseValida();

            return new CursoResponse
            {
                Id = 1,
                Nome = "Ciências da Computação",
                Descricao = "Curso de Ciências da Computação com foco em desenvolvimento de software.",
                CargaHoraria = 200,
                Disciplinas = disciplinas,
                AreaConhecimento = areaConhecimento,
                Ativo = true,
                DataCadastro = DateTime.Now.AddDays(-30),
                DataAtualizacao = DateTime.Now
            };
        }
    }
}
