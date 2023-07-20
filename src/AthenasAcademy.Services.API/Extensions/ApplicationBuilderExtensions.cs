using AthenasAcademy.Services.Core.Middlewares;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace AthenasAcademy.Services.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerUIDoc(this IApplicationBuilder builder, IServiceProvider services)
    {
        builder.UseSwaggerUI(options =>
        {
            options.DefaultModelsExpandDepth(-1);
            foreach (var description in services.GetRequiredService<IApiVersionDescriptionProvider>().ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });

        return builder;
    }

    public static IApplicationBuilder UseHandleException(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ExceptionHandlerMiddleware>();

        return builder;
    }
}