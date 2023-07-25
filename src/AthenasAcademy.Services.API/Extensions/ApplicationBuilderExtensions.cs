using AthenasAcademy.Services.Core.Middlewares;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace AthenasAcademy.Services.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerUIDoc(this IApplicationBuilder builder, string apiTitulo)
    {
        builder.UseSwaggerUI(options =>
        {
            options.DefaultModelsExpandDepth(-1);
            options.SwaggerEndpoint("/swagger/v1/swagger.json", apiTitulo);
        });

        return builder;
    }

    public static IApplicationBuilder UseHandleException(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ExceptionHandlerMiddleware>();

        return builder;
    }
}