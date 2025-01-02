namespace LMS.Bussiness.DTOS.ForumPostDtos
{
    public class AddForumPostRequest
    {
        public string Content { get; set; } = null!;
        public int ForumId { get; set; }
        public int UserId { get; set; }
    }
}
