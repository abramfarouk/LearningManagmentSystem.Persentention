namespace LMS.Bussiness.DTOS.ForumPostDtos
{
    public class ForumPostResponse
    {
        public int ForumPostId { get; set; }
        public string Content { get; set; } = null!;
        public DateOnly PostDate { get; set; }
        public string? ForumName { get; set; }
        public string UserName { get; set; }
    }
}
