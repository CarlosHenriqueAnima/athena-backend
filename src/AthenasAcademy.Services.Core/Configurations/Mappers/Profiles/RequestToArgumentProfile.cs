using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Domain.Requests;
using AutoMapper;

namespace AthenasAcademy.Services.Core.Configurations.Mappers.Profiles;

public class RequestToArgumentProfile : Profile
{
    public RequestToArgumentProfile()
    {
        CreateMap<NovoCursoRequest, CursoArgument>().ReverseMap();
        CreateMap<NovaDisciplinaRequest, DisciplinaArgument>().ReverseMap();
        CreateMap<NovaAreaConhecimentoRequest, AreaConhecimentoArgument>().ReverseMap();

        CreateMap<CursoRequest, CursoArgument>().ReverseMap();
        CreateMap<DisciplinaRequest, DisciplinaArgument>().ReverseMap();
        CreateMap<AreaConhecimentoRequest, AreaConhecimentoArgument>().ReverseMap();
    }
}