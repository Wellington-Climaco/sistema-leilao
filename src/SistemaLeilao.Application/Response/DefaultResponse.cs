namespace SistemaLeilao.Application.Response;

public class DefaultResponse<T>
{
    public DefaultResponse(T data, string statusCode)
    {
        Data = data;
        StatusCode = statusCode;
    }

    public DefaultResponse(string statusCode, List<string> errors)
    {
        StatusCode = statusCode;
        Errors = errors;
    }

    public DefaultResponse(string statusCode, string error)
    {
        StatusCode = statusCode;
        Errors.Add(error);
    }
    
  
    public string StatusCode { get; private set; }
    public T Data { get; private set; }
    public List<string> Errors { get; private set; } = new();
    
}