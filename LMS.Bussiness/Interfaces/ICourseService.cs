using LMS.Bussiness.DTOS.CourseDtos;
using LMS.Data.Bases;

namespace LMS.Bussiness.Interfaces
{
    public interface ICourseService
    {
        public Task<GResponse<string>> AddCourseAsync(AddCourseRequest request);
        public Task<GResponse<string>> UpdateCourseAsync(UpdateCourseRequest request);
        public Task<GResponse<string>> DeleteCourseAsync(int courseId);
        public Task<GResponse<CourseResponseDto>> GetCourseByIdAsync(int courseId);
        public Task<GResponse<List<CourseResponseDto>>> GetAllCoursesAsync();
        public Task<PigatedResult<CourseResponseDto>> GetCoursePaginatedListAsync(CoursePaginatedListRequest request);



    }
}
