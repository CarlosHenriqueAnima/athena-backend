using AthenasAcademy.Services.Core.Exceptions;
using AthenasAcademy.Services.Domain.Configurations.Enums;
using AthenasAcademy.Services.Domain.Responses;
using System.Net;
using System.Text.Json;

namespace AthenasAcademy.Services.Core.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (CustomAPIException ex)
        {
            ExceptionResponse errorResponse = new()
            {
                ResponseType = ex.ResponseType,
                Titulo = "Erro",
                Mensagens = new[] { ex.Message }
            };

            string jsonResponse = JsonSerializer.Serialize(errorResponse);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)ex.StatusCode;

            await context.Response.WriteAsync(jsonResponse);
        }
        catch (Exception)
        {
            ExceptionResponse errorResponse = new()
            {
                ResponseType = ExceptionResponseType.Error,
                Titulo = "Erro",
                Mensagens = new[] { "Ocorreu um erro durante o processamento da solicitação." }
            };

            string jsonResponse = JsonSerializer.Serialize(errorResponse);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}

