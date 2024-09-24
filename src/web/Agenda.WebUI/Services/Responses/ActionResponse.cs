namespace Agenda.WebUI.Services.Responses;

public class ActionResponse<T>
{
    public bool Success { get; set; } = true;
    public AcaoResponseEnum Action { get; set; } = AcaoResponseEnum.Adicionar;
    public string Message { get; set; } = string.Empty;
    public T Data { get; set; }

    public ActionResponse()
    {
        
    }

    public ActionResponse(AcaoResponseEnum action, T data)
    {
        Data = data;
        Action = action;
    }

    public void FailWithMessage(string message)
    {
        Success = false;
        Message = message;
    }
}
