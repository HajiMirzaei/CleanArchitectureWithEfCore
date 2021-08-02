namespace SampleProject.Core.Contracts
{
    public class ResponseMessage
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }

        public ResponseMessage(bool success, string message = null)
        {
            Success = success;
            Message = message;
        }
    }
}