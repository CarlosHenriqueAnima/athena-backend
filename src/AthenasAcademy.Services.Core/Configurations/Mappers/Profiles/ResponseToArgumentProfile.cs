using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Domain.Responses;
using AutoMapper;

namespace AthenasAcademy.Services.Core.Configurations.Mappers.Profiles;

public class ResponseToArgumentProfile : Profile
{
    public ResponseToArgumentProfile()
    {
        CreateMap<CursoModel, CursoResponse>().ReverseMap();
        CreateMap<AreaConhecimentoModel, AreaConhecimentoResponse>().ReverseMap();
        CreateMap<DisciplinaModel, DisciplinaResponse>().ReverseMap();

        CreateMap<CursoModel, NovoCursoResponse>().ReverseMap();
        CreateMap<AreaConhecimentoModel, NovaAreaConhecimentoResponse>().ReverseMap();
        CreateMap<DisciplinaModel, NovaDisciplinaResponse>().ReverseMap();
    }
}