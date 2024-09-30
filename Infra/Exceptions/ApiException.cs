namespace TrabalhoFinal.Infra.Exceptions;

public class ApiException : Exception
{
    public int StatusCode { get; }
    public string Type { get; }

    public ApiException(string type, int statusCode, string message) : base(message)
    {
        Type = type;
        StatusCode = statusCode;
    }
}