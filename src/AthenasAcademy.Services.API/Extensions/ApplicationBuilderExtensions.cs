using AthenasAcademy.Services.Core.Middlewares;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace AthenasAcademy.Services.API.Extensions;

/// <summary>
/// Extensões para a configuração do pipeline de requisição do aplicativo.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adiciona o suporte ao Swagger UI para documentação interativa da API.
    /// </summary>
    /// <param name="builder">O construtor de aplicativo a ser estendido.</param>
    /// <param name="apiTitulo">O título da API a ser exibido na interface do Swagger.</param>
    /// <returns>O construtor de aplicativo após a configuração do Swagger UI.</returns>
    public static IApplicationBuilder UseSwaggerUIDoc(this IApplicationBuilder builder, string apiTitulo)
    {
        builder.UseSwaggerUI(options =>
        {
            options.DefaultModelsExpandDepth(-1);
            options.SwaggerEndpoint("/swagger/v1/swagger.json", apiTitulo);
        });

        return builder;
    }

    /// <summary>
    /// Adiciona um middleware para tratamento centralizado de exceções.
    /// </summary>
    /// <param name="builder">O construtor de aplicativo a ser estendido.</param>
    /// <returns>O construtor de aplicativo após a configuração do middleware de tratamento de exceções.</returns>

    public static IApplicationBuilder UseHandleException(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ExceptionHandlerMiddleware>();

        return builder;
    }

    /// <summary>
    /// Adiciona configuração do CORS no build.
    /// </summary>
    /// <param name="builder">O construtor de aplicativo a ser estendido.</param>
    /// <returns>O construtor de aplicativo após a configuração do CORS.</returns>

    public static IApplicationBuilder UseCorsConfig(this IApplicationBuilder builder)
    {
        builder.UseCors(builder =>
        {
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
            builder.AllowAnyOrigin();
        });

        return builder;
    }
}