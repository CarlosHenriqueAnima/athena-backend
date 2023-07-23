using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Domain.Responses;
using AutoMapper;

namespace AthenasAcademy.Services.Core.Configurations.Mappers.Profiles;

public class ResponseToArgumentProfile : Profile
{
    public ResponseToArgumentProfile()
    {
        CreateMap<CursoResponse, CursoArgument>().ReverseMap();
        CreateMap<AreaConhecimentoResponse, AreaConhecimentoArgument>().ReverseMap();
        CreateMap<DisciplinaResponse, DisciplinaArgument>().ReverseMap();

        CreateMap<NovoCursoResponse, CursoArgument>().ReverseMap();
        CreateMap<NovaAreaConhecimentoResponse, AreaConhecimentoArgument>().ReverseMap();
        CreateMap<NovaDisciplinaResponse, DisciplinaArgument>().ReverseMap();
    }
}