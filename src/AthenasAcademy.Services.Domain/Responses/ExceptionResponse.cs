using AthenasAcademy.Services.Domain.Configurations.Enums;

namespace AthenasAcademy.Services.Domain.Responses;

public class ExceptionResponse
{
    public string Message;

    public ExceptionResponseType ResponseType { get; set; }

    public string Titulo { get; set; }
    public string[] Mensagens { get; set; }

}