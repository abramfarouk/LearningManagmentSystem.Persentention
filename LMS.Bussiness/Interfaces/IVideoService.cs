using Microsoft.AspNetCore.Http;

namespace LMS.Bussiness.Interfaces
{
    public interface IVideoService
    {
        public Task<string?> UploadVideoAsync(IFormFile? videoFile);
        public bool DeleteVideo(string videoUrl);
    }
}
