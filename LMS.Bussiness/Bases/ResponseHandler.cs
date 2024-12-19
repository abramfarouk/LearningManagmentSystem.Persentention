namespace LMS.Bussiness.Bases
{
    public class ResponseHandler
    {
        public static GResponse<T> Deleted<T>(string mess = null)
        {
            return new GResponse<T>()
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Message = mess == null ? "Deleted Successfully" : mess
            };
        }
        public GResponse<T> Success<T>(T entity, object Meta = null)
        {
            return new GResponse<T>()
            {
                Data = entity,
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Message = "Added Successfully",
                Meta = Meta
            };
        }
        public GResponse<T> Unauthorized<T>()
        {
            return new GResponse<T>()
            {
                StatusCode = HttpStatusCode.Unauthorized,
                IsSuccess = true,
                Message = "UnAuthorized"
            };
        }
        public GResponse<T> BadRequest<T>(string Message = null, List<string> errors = null)
        {
            return new GResponse<T>()
            {
                StatusCode = HttpStatusCode.BadRequest,
                IsSuccess = false,
                Message = Message == null ? "Bad Request" : Message,
                Errors = errors
            };
        }

        public GResponse<T> NotFound<T>(string message = null)
        {
            return new GResponse<T>()
            {
                StatusCode = HttpStatusCode.NotFound,
                IsSuccess = false,
                Message = message == null ? "Not Found" : message
            };
        }
        public GResponse<T> UnprocessableEntity<T>(string message = null)
        {
            return new GResponse<T>()
            {
                StatusCode = HttpStatusCode.UnprocessableEntity,
                IsSuccess = false,
                Message = message == null ? "There Is Problem" : message
            };
        }

        public GResponse<T> Created<T>(T entity, string mess = null)
        {
            return new GResponse<T>()
            {
                Data = entity,
                StatusCode = HttpStatusCode.Created,
                IsSuccess = true,
                Message = mess == null ? "Created Successful" : mess,
                DataCount = 1

            };
        }
        public GResponse<T> OK<T>(T entity, string mess = null, int count = 1)
        {
            return new GResponse<T>()
            {
                Data = entity,
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Message = mess == null ? "Successful" : mess,
                DataCount = count
            };
        }
    }
}
