namespace WebUIAgenda.Services.Responses
{
    public class BaseResponse
    {
        public bool Success { get; set; } = true;
        public string Code { get; set; }
        public string Message { get; set; }



        public void FailWithMessage(string message)
        {
            Success = false;
            Message = message;
        }
    }
}
