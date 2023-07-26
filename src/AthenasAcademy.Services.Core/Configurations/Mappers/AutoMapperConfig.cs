using AthenasAcademy.Services.Core.Configurations.Mappers.Profiles;
using AutoMapper;

namespace AthenasAcademy.Services.Core.Configurations.Mappers;

public static class AutoMapperConfig
{
    public static MapperConfiguration RegisterMappings()
    {
        return new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new RequestToArgumentProfile());
                cfg.AddProfile(new RequestToResponseProfile());

                cfg.AddProfile(new ModelToResponseProfile());
                cfg.AddProfile(new ModelToRequestProfile());

                cfg.AddProfile(new ResponseToArgumentProfile());
            });
    }
}