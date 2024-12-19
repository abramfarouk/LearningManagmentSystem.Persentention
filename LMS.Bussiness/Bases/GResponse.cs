namespace LMS.Bussiness.Bases
{
    public class GResponse<T>
    {

        public GResponse()
        {

        }
        public GResponse(T data, string message = null)
        {
            IsSuccess = true;
            Data = data;
            Message = message;
        }

        public GResponse(string message)
        {
            IsSuccess = false;
            Message = message;
        }
        public GResponse(string message, bool succeeded)
        {
            IsSuccess = succeeded;
            Message = message;
        }




        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public int DataCount { get; set; }
        public List<string> Errors { get; set; }
        public object Meta { get; set; }

        public HttpStatusCode StatusCode { get; set; }


    }




}
