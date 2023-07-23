using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Domain.Responses;
using AutoMapper;

namespace AthenasAcademy.Services.Core.Configurations.Mappers.Profiles;

public class ModelToResponseProfile : Profile
{
    public ModelToResponseProfile()
    {
        CreateMap<CursoModel, CursoResponse>().ReverseMap();
        CreateMap<AreaConhecimentoModel, AreaConhecimentoResponse>().ReverseMap();
        CreateMap<DisciplinaModel, DisciplinaResponse>().ReverseMap();
    }
}