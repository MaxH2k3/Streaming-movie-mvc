using System.Net;

namespace SMovie.Domain.Models
{   
    public class ResponseDTO
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; } = null!;
        public object Data { get; set; } = null!;

        public ResponseDTO()
        {
        }

        public ResponseDTO(HttpStatusCode status, string message, object data)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        public ResponseDTO(HttpStatusCode status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}
