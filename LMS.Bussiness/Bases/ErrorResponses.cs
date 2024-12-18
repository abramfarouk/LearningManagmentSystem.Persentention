namespace LMS.Bussiness.Bases
{
    public static class ErrorResponses
    {
        public static GResponse<string> ErrorRespone(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {

            return new GResponse<string>
            {
                Message = message,
                StatusCode = statusCode,
                IsSuccess = false,
                DataCount = 0,
                Data = null,
            };

        }
    }
}
