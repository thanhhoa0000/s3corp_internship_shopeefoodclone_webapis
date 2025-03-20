namespace ShopeeFoodClone.WebApi.Users.Application.Responses;

public sealed class Response
{
    public object? Body { get; set; }
    public bool IsSuccessful { get; set; } = true;
    public string Message { get; set; } = String.Empty;
}
