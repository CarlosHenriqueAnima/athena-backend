using AutoMapper;

namespace AthenasAcademy.Services.Core.Configurations.Mappers;

public interface IObjectConverter
{
    T Map<T>(object source);

    D Map<T, D>(T source, D destination);
}

public class ObjectConverter : IObjectConverter
{
    private readonly IMapper _mapper;

    public ObjectConverter()
    {
        _mapper = AutoMapperConfig.RegisterMappings().CreateMapper();
    }

    public T Map<T>(object source)
    {
        return _mapper.Map<T>(source);
    }

    public D Map<T, D>(T source, D destination)
    {
        return source is null ? destination : _mapper.Map(source, destination);
    }
}