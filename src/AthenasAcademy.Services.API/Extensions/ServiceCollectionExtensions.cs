using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;
using AthenasAcademy.Services.Core.Services.Interfaces;
using AthenasAcademy.Services.Core.Services;

namespace AthenasAcademy.Services.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServicosScopo(this IServiceCollection services)
    {
        services.AddScoped<IAlunoService, AlunoService>();
        services.AddScoped<ICertificadoService, CertificadoService>();
        services.AddScoped<ICursoService, CursoService>();
        services.AddScoped<IInscricaoService, InscricaoService>();
        services.AddScoped<IMatriculaService, MatriculaService>();
        services.AddScoped<IPagamentoService, PagamentoService>();

        return services;
    }

    public static IServiceCollection AddApiVersionamento(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                            new HeaderApiVersionReader("x-api-version"),
                                                            new MediaTypeApiVersionReader("x-api-version"));

        });

        return services;
    }

    public static IServiceCollection AddSwaggerGenDoc(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API Athenas Academy",
                Version = "1.0",
                Description = "API para gerenciamento dos microservicos da Athenas Academy",
                Contact = new OpenApiContact
                {
                    Name = "Time de Desenvolvimento",
                    Email = "athenasacademy.flow@gmail.com",
                    Url = new Uri("https://www.athena-academy.tech")
                }
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }

    public static IServiceCollection AddVersionamentoApiExplorer(this IServiceCollection services)
    {
        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}