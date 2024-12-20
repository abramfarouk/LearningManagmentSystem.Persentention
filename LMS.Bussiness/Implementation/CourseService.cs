using LMS.Bussiness.DTOS.CourseDtos;
using LMS.Data.Abstract;
using LMS.Data.Bases;
using LMS.Data.Data.Entities;

namespace LMS.Bussiness.Implementation
{
    public class CourseService : ResponseHandler, ICourseService
    {

        private readonly IGenericRepository<Course> _courseRepo;
        public CourseService(IGenericRepository<Course> courseRepo)
        {
            _courseRepo = courseRepo;

        }


        public async Task<GResponse<string>> AddCourseAsync(AddCourseRequest request)
        {
            try
            {

                var Course = _courseRepo.GetAllAsync().Result.FirstOrDefault(x => x.Title == request.Title);
                if (Course != null)
                {
                    return BadRequest<string>("Course already exists");
                }
                var newCourse = new Course
                {
                    Title = request.Title,
                    Level = request.Level,
                    Description = request.Description,
                    UserId = request.TeacherId
                };

                var result = await _courseRepo.AddAsync(newCourse);
                if (result != null)

                    return OK("Course added successfully");

                return BadRequest<string>("Failed to add course");


            }
            catch (Exception ex)
            {
                return BadRequest<string>($"Invaild an errors :{ex.Message}");
            }
        }

        public Task<GResponse<string>> DeleteCourseAsync(int courseId)
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<List<CourseResponseDto>>> GetAllCoursesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<CourseResponseDto>> GetCourseByIdAsync(int courseId)
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<PigatedResult<CourseResponseDto>>> GetCoursePaginatedListAsync(CoursePaginatedListRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<string>> UpdateCourseAsync(UpdateCourseRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
