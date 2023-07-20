using AthenasAcademy.Services.Domain.Configurations.Enums;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace AthenasAcademy.Services.Core.Exceptions;

public class CustomAPIException : Exception
{
    public ExceptionResponseType ResponseType { get; set; }

    public HttpStatusCode StatusCode { get; set; }


    public CustomAPIException(string message, ExceptionResponseType responseType, HttpStatusCode statusCode) : base(message)
    {
        ResponseType = responseType;
        StatusCode = statusCode;
    }

    public CustomAPIException(string message, ExceptionResponseType responseType, Exception innerException, HttpStatusCode statusCode) : base(message, innerException)
    {
        ResponseType = responseType;
        StatusCode = statusCode;
    }
}
