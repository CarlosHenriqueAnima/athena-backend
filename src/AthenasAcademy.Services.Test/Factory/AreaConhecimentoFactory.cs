using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;

namespace AthenasAcademy.Services.Test.Factory
{
    public class AreaConhecimentoFactory
    {
        public AreaConhecimentoModel ObterAreaConhecimentoValida()
        {
            return new AreaConhecimentoModel
            {
                Id = 1,
                Nome = "Ciências da Computação",
                Descricao = "Área que estuda os fundamentos teóricos da informação e da computação.",
                Ativo = true,
                DataCadastro = DateTime.Now.AddDays(-30),
                DataAlteracao = null
            };
        }

        public List<AreaConhecimentoModel> ObterListaAreaConhecimentoModelValidos()
        {
            return Enumerable.Range(1, 2).Select(index => new AreaConhecimentoModel
            {
                Id = index,
                Nome = $"Área de Conhecimento {index}",
                Descricao = $"Descrição da Área de Conhecimento {index}",
                Ativo = true,
                DataCadastro = DateTime.Now.AddDays(-30).AddDays(index),
                DataAlteracao = null
            }).ToList();
        }

        public AreaConhecimentoRequest ObterAreaConhecimentoRequestValida()
        {
            return new AreaConhecimentoRequest
            {
                Id = 1,
                Nome = "Ciências da Computação",
                Descricao = "Área que estuda os fundamentos teóricos da informação e da computação.",
                Ativo = true,
                DataCadastro = DateTime.Now.AddDays(-30),
                DataAtualizacao = DateTime.Now
            };
        }

        public NovaAreaConhecimentoRequest ObterNovaAreaConhecimentoRequest()
        {
            return new NovaAreaConhecimentoRequest()
            {
                Nome = "Ciências da Computação",
                Descricao = "Área que estuda os fundamentos teóricos da informação e da computação."
            };
        }

        public AreaConhecimentoResponse ObterAreaConhecimentoResponseValida()
        {
            return new AreaConhecimentoResponse
            {
                Id = 1,
                Nome = "Tecnologia da Informação",
                Descricao = "Área que estuda a utilização de tecnologias para solucionar problemas de informação.",
                Ativo = true,
                DataCadastro = DateTime.Now.AddDays(-30),
                DataAtualizacao = DateTime.Now
            };
        }
    }
}
