using LMS.Bussiness.DTOS.CourseDtos;
using LMS.Data.Abstract;
using LMS.Data.Bases;
using LMS.Data.Data.Entities;
using LMS.Data.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LMS.Bussiness.Implementation
{
    public class CourseService : ResponseHandler, ICourseService
    {

        private readonly IGenericRepository<Course> _courseRepo;
        private readonly UserManager<User> _userManager;
        public CourseService(IGenericRepository<Course> courseRepo, UserManager<User> userManager)
        {
            _courseRepo = courseRepo;
            _userManager = userManager;

        }
        public async Task<GResponse<string>> AddCourseAsync(AddCourseRequest request)
        {
            try
            {
                var teacher = await _userManager.FindByIdAsync(request.TeacherId.ToString());
                if (teacher == null)
                {
                    return BadRequest<string>("Teacher not found");
                }
                var IsTeacher = await _userManager.IsInRoleAsync(teacher, "Teacher");
                if (!IsTeacher)
                {
                    return BadRequest<string>("User is not a teacher");
                }

                var newCourse = new Course
                {
                    Title = request.Title,
                    Level = request.Level,
                    Description = request.Description,
                    CreateDate = DateTime.UtcNow,
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

        public async Task<GResponse<string>> DeleteCourseAsync(int courseId)

        {
            try
            {
                var Course = await _courseRepo.GetByIdAsync(courseId);
                if (Course == null)
                {
                    return BadRequest<string>($"Course not found with Id {courseId}");

                }
                var res = await _courseRepo.DeleteAsync(Course);
                if (res)

                    return OK("Course deleted successfully");

                return BadRequest<string>("Failed to delete course");

            }
            catch (Exception ex)
            {
                return BadRequest<string>($"Invaild an errors :{ex.Message}");
            }
        }

        public async Task<GResponse<List<CourseResponseDto>>> GetAllCoursesAsync()
        {
            var courses = await _courseRepo.GetTableNoTracking().Include(u => u.User).ToListAsync();
            if (!courses.Any())
            {
                return NotFound<List<CourseResponseDto>>("No courses found");
            }
            var coursesDto = courses.Select(c => new CourseResponseDto
            {
                Cr_Id = c.Id,
                Title = c.Title,
                Level = c.Level,
                Description = c.Description,
                CreatedTime = DateOnly.FromDateTime(DateTime.Now),
                TeacherName = c.User.UserName
            }).ToList();

            return OK(coursesDto, count: coursesDto.Count());
        }

        public async Task<GResponse<CourseResponseDto>> GetCourseByIdAsync(int courseId)
        {
            try
            {
                var course = await _courseRepo.GetTableNoTracking().Include(c => c.User).FirstOrDefaultAsync(c => c.Id == courseId);
                if (course == null)
                {
                    return NotFound<CourseResponseDto>($"Course not found with Id {courseId}");
                }
                var cousreDto = new CourseResponseDto
                {
                    Cr_Id = course.Id,
                    Title = course.Title,
                    Level = course.Level,
                    Description = course.Description,
                    CreatedTime = new DateOnly(course.CreateDate.Year, course.CreateDate.Month, course.CreateDate.Day),
                    TeacherName = course.User.UserName

                };
                return OK(cousreDto, count: 1);

            }
            catch (Exception ex)
            {
                return BadRequest<CourseResponseDto>($"Invaild an errors :{ex.Message}");
            }
        }

        public async Task<GResponse<string>> UpdateCourseAsync(UpdateCourseRequest request)
        {
            try
            {
                var OldCourse = await _courseRepo.GetByIdAsync(request.Crs_Id);
                if (OldCourse != null && OldCourse.Id == request.Crs_Id)
                {
                    OldCourse.Title = request.Title;
                    OldCourse.Level = request.Level;
                    OldCourse.Description = request.Description;
                    OldCourse.CreateDate = DateTime.UtcNow;
                    var result = await _courseRepo.UpdateAnsyc(OldCourse);
                    if (result)
                        return OK("Course updated successfully");
                    return BadRequest<string>("Failed to update course");
                }
                else
                {
                    return NotFound<string>($"Course not found with Id {request.Crs_Id}");
                }
            }
            catch (Exception ex)
            {
                return BadRequest<string>($"Invaild an errors :{ex.Message}");
            }
        }
        public Task<GResponse<PigatedResult<CourseResponseDto>>> GetCoursePaginatedListAsync(CoursePaginatedListRequest request)
        {
            throw new NotImplementedException();
        }


    }

}
