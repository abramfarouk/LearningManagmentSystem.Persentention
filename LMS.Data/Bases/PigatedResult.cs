namespace LMS.Data.Bases
{
    public class PigatedResult<T>
    {

        #region Properties 
        public IEnumerable<T>? Data { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage { get { return CurrentPage > 1; } }
        public bool HasNextPage { get { return CurrentPage < TotalPages; } }
        public IEnumerable<string>? Messages { get; set; }
        public bool Successed { get; set; }

        #endregion

        public PigatedResult(IEnumerable<T> data)
        {
            Data = data;
        }

        public PigatedResult(bool successed, int totalcount = 0, int page = 1
            , List<T> data = default, List<string> mess = null, int pageSize = 20)
        {
            Successed = successed;
            PageSize = pageSize;
            TotalCount = totalcount;
            CurrentPage = page;
            Data = data;
            Messages = mess;
            this.TotalPages = (int)Math.Ceiling(totalcount / (double)pageSize);




        }

        public static PigatedResult<T> Success(List<T> data, int page,
            int count, int pagesize)
        => new PigatedResult<T>(true, count, page, data, null, pagesize);


    }
}
