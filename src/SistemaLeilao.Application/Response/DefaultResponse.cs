using System.Net;

namespace SistemaLeilao.Application.Response;

public class DefaultResponse<T>
{
    public DefaultResponse(T data, int statusCode)
    {
        Data = data;
        StatusCode = statusCode.ToString();
    }

    public DefaultResponse(int statusCode, IEnumerable<string> errors)
    {
        StatusCode = statusCode.ToString();
        Errors = errors;
    }

    public DefaultResponse(int statusCode, string error)
    {
        StatusCode = statusCode.ToString();
        Errors = [error];
    }
    
  
    public string StatusCode { get; private set; }
    public T Data { get; private set; }
    public IEnumerable<string> Errors { get; private set; }
    
}