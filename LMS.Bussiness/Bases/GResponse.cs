using System.Net;

namespace LMS.Bussiness.Bases
{
    public class GResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public int DataCount { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
