using AthenasAcademy.Services.Domain.Configurations.Enums;

namespace AthenasAcademy.Services.Core.Exceptions;

public class DatabaseCustomException : Exception
{
    public ExceptionResponseType ResponseType { get; set; }


    public DatabaseCustomException(string message, ExceptionResponseType responseType) : base(message)
    {
        ResponseType = responseType;
    }

    public DatabaseCustomException(string message, ExceptionResponseType responseType, Exception innerException) : base(message, innerException)
    {
        ResponseType = responseType;
    }
}