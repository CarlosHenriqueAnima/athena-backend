using AthenasAcademy.Services.Core.Configurations.Mappers.Profiles;
using AutoMapper;

namespace AthenasAcademy.Services.Core.Configurations.Mappers;

public static class AutoMapperConfig
{
    public static MapperConfiguration RegisterMappings()
    {
        return new MapperConfiguration(
            cfg => { 
                cfg.AddProfile(new RequestoToArrgumentProfile());
                cfg.AddProfile(new ModelToResponseProfile());
            });
    }
}