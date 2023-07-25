using AutoMapper;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Domain.Responses;

namespace AthenasAcademy.Services.Core.Services
{
    public class AutoMapperConfig
    {
        public static IMapper Configure()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AlunoModel, AlunoResponse>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        private static int CalcularIdade(DateTime dataNascimento)
        {
            // Implemente aqui o cálculo da idade com base na data de nascimento
            // Exemplo simples para ilustração:
            var idade = DateTime.Now.Year - dataNascimento.Year;
            if (DateTime.Now < dataNascimento.AddYears(idade)) idade--;
            return idade;
        }
    }
}
