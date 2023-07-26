using AthenasAcademy.Services.Domain.Configurations.Enums;
using System.Net;

namespace AthenasAcademy.Services.Core.Exceptions;

public class TokenCustomException : Exception
{
    public ExceptionResponseType ResponseType { get; set; }

    public HttpStatusCode StatusCode { get; set; }


    public TokenCustomException(string message, ExceptionResponseType responseType, HttpStatusCode statusCode) : base(message)
    {
        ResponseType = responseType;
        StatusCode = statusCode;
    }

    public TokenCustomException(string message, ExceptionResponseType responseType, Exception innerException, HttpStatusCode statusCode) : base(message, innerException)
    {
        ResponseType = responseType;
        StatusCode = statusCode;
    }

    public TokenCustomException(string message, Exception innerException) : base(message, innerException)
    {

    }
}