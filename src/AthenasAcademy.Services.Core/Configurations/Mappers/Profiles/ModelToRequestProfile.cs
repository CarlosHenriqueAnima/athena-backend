using AthenasAcademy.Services.Core.Arguments;
using AthenasAcademy.Services.Core.Models;
using AthenasAcademy.Services.Domain.Requests;
using AthenasAcademy.Services.Domain.Responses;
using AutoMapper;

namespace AthenasAcademy.Services.Core.Configurations.Mappers.Profiles;

public class ModelToRequestProfile : Profile
{
    public ModelToRequestProfile()
    {
        CreateMap<CursoModel, CursoRequest>().ReverseMap();
        CreateMap<DisciplinaModel, DisciplinaRequest>().ReverseMap();
        CreateMap<AreaConhecimentoModel, AreaConhecimentoRequest>().ReverseMap();
    }
}