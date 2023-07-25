using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using AthenasAcademy.Services.Core.Configurations;
using AthenasAcademy.Services.Core.Repositories;
using AthenasAcademy.Services.Core.Repositories.Clients;
using AthenasAcademy.Services.Core.Repositories.Interfaces;
using AthenasAcademy.Services.Core.Repositories.S3;
using AthenasAcademy.Services.Core.Services;
using AthenasAcademy.Services.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;
using System.Reflection;
using System.Text;

namespace AthenasAcademy.Services.API.Extensions;

/// <summary>
/// Classe com métodos de extensão para configurações relacionadas a serviços da aplicação.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registra os serviços relacionados ao domínio da aplicação no contêiner de injeção de dependências.
    /// </summary>
    /// <param name="services">A coleção de serviços.</param>
    /// <returns>A coleção de serviços atualizada.</returns>
    public static IServiceCollection AddAthenasServicesDI(this IServiceCollection services)
    {
        services.AddScoped<IAlunoService, AlunoService>();
        services.AddScoped<ICertificadoService, CertificadoService>();
        services.AddScoped<ICursoService, CursoService>();
        services.AddScoped<IInscricaoService, InscricaoService>();
        services.AddScoped<IMatriculaService, MatriculaService>();
        services.AddScoped<IPagamentoService, PagamentoService>();
        services.AddScoped<IAutorizaUsuarioService, AutorizaUsuarioService>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }

    /// <summary>
    /// Registra os repositórios da aplicação no contêiner de injeção de dependências.
    /// </summary>
    /// <param name="services">A coleção de serviços.</param>
    /// <returns>A coleção de serviços atualizada.</returns>
    public static IServiceCollection AddAthenasRepositoriesDI(this IServiceCollection services)
    {
        services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
        services.AddSingleton<IAlunoRepository, AlunoRepository>();
        services.AddSingleton<ICertificadoRepository, CertificadoRepository>();
        services.AddSingleton<ICursoRepository, CursoRepository>();
        services.AddSingleton<IInscricaoRepository, InscricaoRepository>();
        services.AddSingleton<IMatriculaRepository, MatriculaRepository>();
        services.AddSingleton<IPagamentoRepository, PagamentoRepository>();

        return services;
    }

    /// <summary>
    /// Configura o versionamento da API, permitindo que diferentes versões de uma API coexistam.
    /// </summary>
    /// <param name="services">A coleção de serviços.</param>
    /// <returns>A coleção de serviços atualizada.</returns>
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

    /// <summary>
    /// Configura e adiciona o Swagger (OpenAPI) para documentar a API.
    /// </summary>
    /// <param name="services">A coleção de serviços.</param>
    /// <param name="apiTitulo">O título da API.</param>
    /// <param name="apiVersao">A versão da API.</param>
    /// <returns>A coleção de serviços atualizada.</returns>
    public static IServiceCollection AddSwaggerGenDoc(this IServiceCollection services, string apiTitulo, string apiVersao)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = apiTitulo,
                Version = apiVersao,
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

    /// <summary>
    /// Configura a exploração de API versionada, que é útil para visualizar as diferentes versões da API no Swagger.
    /// </summary>
    /// <param name="services">A coleção de serviços.</param>
    /// <returns>A coleção de serviços atualizada.</returns>
    public static IServiceCollection AddApiVersionamentoExplorer(this IServiceCollection services)
    {
        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    /// <summary>
    /// Configura a autenticação JWT (JSON Web Token) para proteger os endpoints da API.
    /// </summary>
    /// <param name="services">A coleção de serviços.</param>
    /// <param name="configuration">A configuração da aplicação.</param>
    /// <returns>A coleção de serviços atualizada.</returns>
    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var key = Encoding.UTF8.GetBytes(configuration["Jwt:key"]);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        });

        return services;
    }

    /// <summary>
    /// Configura o Swagger para suportar autenticação JWT Bearer.
    /// </summary>
    /// <param name="services">A coleção de serviços.</param>
    /// <returns>A coleção de serviços atualizada.</returns>
    public static IServiceCollection AddSwaggerAutenticacaoJwtBearer(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            //options.SwaggerDoc(apiVersion, new OpenApiInfo { Title = apiTitle, Version = apiVersion });

            // Define o esquema de segurança Bearer
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                //Name = "Authorization",
                //Type = SecuritySchemeType.ApiKey,
                //Scheme = "Bearer",
                //BearerFormat = "JWT",
                //In = ParameterLocation.Header,

                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "Header de autenticação JWT - Schema Bearer.\r\n\r\nInforme 'Bearer <token>'.\r\n\r\n"
            });

            // Define a exigência de segurança com o esquema Bearer
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }

    /// <summary>
    /// Configura as rotas da aplicação para serem lowercase.
    /// </summary>
    /// <param name="services">A coleção de serviços.</param>
    /// <returns>A coleção de serviços atualizada.</returns>
    public static IServiceCollection AddRotasLowerCase(this IServiceCollection services)
    {
        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
        });

        return services;
    }

    /// <summary>
    /// Configura as políticas CORS para permitir acesso de qualquer origem, método e cabeçalho.
    /// </summary>
    /// <param name="services">A coleção de serviços.</param>
    /// <returns>A coleção de serviços atualizada.</returns>
    public static IServiceCollection AddPoliciesCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
        });

        return services;
    }

    /// <summary>
    /// Configura as configurações do Refit para serviços REST.
    /// </summary>
    /// <param name="services">A coleção de serviços.</param>
    /// <param name="configuration">A configuração da aplicação.</param>
    /// <returns>A coleção de serviços atualizada.</returns>
    public static IServiceCollection AddConfiguracaoRestClient(this IServiceCollection services, IConfiguration configuration)
    {
        RefitSettings config = new RefitSettings()
        {
            ContentSerializer = new NewtonsoftJsonContentSerializer(
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                })
        };

        services.AddRefitClient<IGeradorCertificadoRepository>(config)
            .ConfigureHttpClient(client => client.BaseAddress = new Uri(configuration.GetValue<string>("Clients:Certificado")));

        services.AddRefitClient<IProcessosPagamentoRepository>(config)
            .ConfigureHttpClient(client => client.BaseAddress = new Uri(configuration.GetValue<string>("Clients:Boleto")));

        return services;
    }

    /// <summary>
    /// Configura as configurações do AWSS3.
    /// </summary>
    /// <param name="services">A coleção de serviços.</param>
    /// <param name="configuration">A configuração da aplicação.</param>
    /// <returns>A coleção de serviços atualizada.</returns>
    public static IServiceCollection AddAWSBucketS3(this IServiceCollection services, IConfiguration configuration)
    {
        //services.Configure<ParametrosS3>(configuration.GetSection("AWS:S3"));
        //AWSOptions optionsAws = configuration.GetAWSOptions();
        //optionsAws.Credentials = new BasicAWSCredentials(configuration["AWS:S3:AccessKey"], configuration["AWS:S3:SecretKey"]);
        //services.AddDefaultAWSOptions(configuration.GetAWSOptions());
        //services.AddAWSService<IAmazonS3>();

        services.AddSingleton<IAwsS3Repository, AwsS3Repository>();
        return services;
    }

    
}