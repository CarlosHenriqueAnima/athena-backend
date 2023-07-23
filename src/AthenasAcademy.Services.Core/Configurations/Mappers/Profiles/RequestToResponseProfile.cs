using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;
using AutoMapper;

namespace AthenasAcademy.Services.Core.Configurations.Mappers.Profiles;

public class RequestToResponseProfile : Profile
{
    public RequestToResponseProfile()
    {
        CreateMap<NovoCursoRequest, CursoResponse>().ReverseMap();
        CreateMap<NovaDisciplinaRequest, DisciplinaResponse>().ReverseMap();
        CreateMap<NovaAreaConhecimentoRequest, AreaConhecimentoResponse>().ReverseMap();

        CreateMap<CursoRequest, CursoResponse>().ReverseMap();
        CreateMap<DisciplinaRequest, DisciplinaResponse>().ReverseMap();
        CreateMap<AreaConhecimentoRequest, AreaConhecimentoResponse>().ReverseMap();
    }
}