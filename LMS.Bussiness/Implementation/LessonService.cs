using LMS.Bussiness.DTOS.LessonDtos;
using LMS.Data.Abstract;
using LMS.Data.Bases;
using LMS.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Bussiness.Implementation
{
    public class LessonService : ResponseHandler, ILessonService
    {
        private readonly IGenericRepository<Lesson> _lessonRepo;
        private readonly IModuleService _moduleService;
        private readonly IVideoService _videoService;
        public LessonService(IModuleService moduleService, IVideoService videoService, IGenericRepository<Lesson> lessonRepo)
        {
            _moduleService = moduleService;
            _videoService = videoService;
            _lessonRepo = lessonRepo;
        }
        public async Task<GResponse<string>> AddLessonAsync(AddLessonRequest request)
        {
            try
            {
                var module = await _moduleService.GetModuleByIdAsync(request.ModuleId);
                if (module == null)

                    return NotFound<string>("Module Not Found");
                string UploadVideoResponse = string.Empty;
                if (request.VedioFile.Length > 0)
                {
                    UploadVideoResponse = await _videoService.UploadVideoAsync(request.VedioFile);
                }
                var lesson = new Lesson
                {
                    ModuleId = request.ModuleId,
                    Title = request.Title,
                    UrlVedio = UploadVideoResponse,
                    Content = request.Content,
                };
                bool result = await _lessonRepo.AddAsync(lesson);
                if (!result)
                    return BadRequest<string>("Failed To Add Lesson");
                return OK<string>("Lesson Added Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest<string>($"Invalid An Errors{ex.Message}");
            }
        }

        public async Task<GResponse<string>> DeleteLessonAsync(int lessonId)
        {
            try
            {
                var lessson = await _lessonRepo.GetTableNoTracking().Include(x => x.Module).FirstOrDefaultAsync(x => x.Id == lessonId);
                if (lessson == null)
                    return NotFound<string>("Lesson Not Found");
                bool result = await _lessonRepo.DeleteAsync(lessson);
                if (!result)
                    return BadRequest<string>("Failed To Delete Lesson");
                return OK<string>("Lesson Deleted Successfully");

            }
            catch (Exception ex)
            {
                return BadRequest<string>($"Invalid An Errors{ex.Message}");


            }
        }

        public async Task<GResponse<LessonResponseDto>> GetLessonByIdAsync(int lessonId)
        {
            try
            {
                var lesson = await _lessonRepo.GetTableNoTracking().Include(x => x.Module).FirstOrDefaultAsync(x => x.Id == lessonId);
                if (lesson == null)
                    return NotFound<LessonResponseDto>("Lesson Not Found");
                var lessonResponse = new LessonResponseDto
                {
                    Id = lesson.Id,
                    Content = lesson.Content,
                    Title = lesson.Title,
                    UrlVedio = lesson.UrlVedio,
                    ModuleName = lesson.Module.Title
                };
                return OK(lessonResponse, count: 1);
            }
            catch (Exception ex)
            {
                return BadRequest<LessonResponseDto>($"Invalid An Errors{ex.Message}");
            }
        }
        public async Task<GResponse<List<LessonResponseDto>>> GetLessonsAsync()
        {
            try
            {
                var lessons = _lessonRepo.GetTableNoTracking().Include(x => x.Module).Select(x => new LessonResponseDto
                {
                    Id = x.Id,
                    Content = x.Content,
                    Title = x.Title,
                    UrlVedio = x.UrlVedio,
                    ModuleName = x.Module.Title
                }).ToList();
                if (lessons.Count == 0)
                    return NotFound<List<LessonResponseDto>>("No Lessons Found");
                return OK(lessons, count: lessons.Count);
            }
            catch (Exception ex)
            {
                return BadRequest<List<LessonResponseDto>>($"Invalid An Errors{ex.Message}");
            }
        }
        public async Task<PigatedResult<LessonResponseDto>> GetLessonsPaginatedListAsync(LessonPaginatedListRequest request)
        {
            var Query = _lessonRepo.GetTableNoTracking().Include(x => x.Module).Select(x => new LessonResponseDto
            {
                Id = x.Id,
                Content = x.Content,
                Title = x.Title,
                UrlVedio = x.UrlVedio,
                ModuleName = x.Module.Title
            }).AsQueryable();
            if (Query.Count() == 0)
                return new PigatedResult<LessonResponseDto>(new List<LessonResponseDto>());
            var result = await Query.ToPaginatedListAsync(request.NumberPage, request.PageSize);
            return result;
        }

        public async Task<GResponse<string>> UpdateLessonAsync(UpdateLessonRequest request)
        {
            try
            {
                var Oldlesson = await _lessonRepo.GetByIdAsync(request.LessonId);
                if (Oldlesson == null)
                    return NotFound<string>("Lesson Not Found");
                var module = await _moduleService.GetModuleByIdAsync(request.ModuleId);
                if (module == null)
                    return NotFound<string>("Module Not Found");
                string UploadVideoResponse = string.Empty;
                if (request.VedioFile.Length > 0)
                {
                    if (!string.IsNullOrEmpty(Oldlesson.UrlVedio))
                    {
                        _videoService.DeleteVideo(Oldlesson.UrlVedio);

                    }
                    UploadVideoResponse = await _videoService.UploadVideoAsync(request.VedioFile);
                }
                Oldlesson.ModuleId = request.ModuleId;
                Oldlesson.Title = request.Title;
                Oldlesson.UrlVedio = UploadVideoResponse;
                Oldlesson.Content = request.Content;
                bool result = await _lessonRepo.UpdateAnsyc(Oldlesson);
                if (!result)
                    return BadRequest<string>("Failed To Update Lesson");

                return OK<string>("Lesson Updated Successfully");


            }
            catch (Exception ex)
            {
                return BadRequest<string>($"Invalid An Errors{ex.Message}");
            }
        }
    }
}
