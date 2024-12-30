using LMS.Bussiness.DTOS.LessonDtos;
using LMS.Data.Bases;

namespace LMS.Bussiness.Interfaces
{
    public interface ILessonService
    {
        public Task<GResponse<string>> AddLessonAsync(AddLessonRequest request);
        public Task<GResponse<string>> UpdateLessonAsync(UpdateLessonRequest request);
        public Task<GResponse<string>> DeleteLessonAsync(int lessonId);
        public Task<GResponse<List<LessonResponseDto>>> GetLessonsAsync();
        public Task<GResponse<LessonResponseDto>> GetLessonByIdAsync(int lessonId);
        public Task<PigatedResult<LessonResponseDto>> GetLessonsPaginatedListAsync(LessonPaginatedListRequest request);

    }
}
