using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Domain.Requests;
using AutoMapper;

namespace AthenasAcademy.Services.Core.Configurations.Mappers.Profiles;

public class RequestToArgumentProfile : Profile
{
    public RequestToArgumentProfile()
    {
        // servico-curso
        CreateMap<CursoRequest, CursoArgument>().ReverseMap();
        CreateMap<DisciplinaRequest, DisciplinaArgument>().ReverseMap();
        CreateMap<AreaConhecimentoRequest, AreaConhecimentoArgument>().ReverseMap();

        CreateMap<NovoCursoRequest, CursoArgument>().ReverseMap();
        CreateMap<NovaDisciplinaRequest, DisciplinaArgument>().ReverseMap();
        CreateMap<NovaAreaConhecimentoRequest, AreaConhecimentoArgument>().ReverseMap();

        //servico-usuario
        CreateMap<NovoUsuarioRequest, NovoUsuarioArgument>().ReverseMap();        
    }
}