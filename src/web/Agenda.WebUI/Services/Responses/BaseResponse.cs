namespace Agenda.WebUI.Services.Responses;

public class BaseResponse
{
    public bool Success { get; set; } = true;
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    public void FailWithMessage(string message)
    {
        Success = false;
        Message = message;
    }
}
