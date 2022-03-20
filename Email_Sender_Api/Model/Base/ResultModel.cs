using System.Net;

namespace Email_Sender_Api.Model
{
    public class ResultModel
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public object Data { get; set; }
        public bool IsSuccess { get; set; } = true;
    }
}
