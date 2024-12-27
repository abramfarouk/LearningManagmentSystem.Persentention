using LMS.Bussiness.DTOS.EnrollmentDtos;
using LMS.Data.Abstract;
using LMS.Data.Bases;
using LMS.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Bussiness.Implementation
{
    public class EnrollmentService : ResponseHandler, IEnrollmentService
    {
        private readonly IGenericRepository<Enrollment> _enrollmentRepo;
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;
        public EnrollmentService(IGenericRepository<Enrollment> enrollmentRepo, ICourseService courseService, IUserService userService)
        {
            _enrollmentRepo = enrollmentRepo;
            _courseService = courseService;
            _userService = userService;
        }


        public async Task<GResponse<string>> CreateEnrollmentAsync(AddEnrollmentRequest request)
        {
            try
            {
                var Course = await _courseService.GetCourseByIdAsync(request.CourseId);
                if (!Course.IsSuccess)
                {
                    return NotFound<string>($"the course Id {request.CourseId} not found ");
                }
                var User = await _userService.GetUserByIdAsync(request.UserId);
                if (!User.IsSuccess)
                {
                    return NotFound<string>($"the user Id {request.UserId} not found ");
                }

                var Enrollment = new Enrollment
                {
                    CourseId = request.CourseId,
                    UserId = request.UserId,
                    EnrollmentDate = DateTime.UtcNow,
                };
                var result = await _enrollmentRepo.AddAsync(Enrollment);
                if (result)
                {
                    return Success<string>("the Enrollment Is Successfull !");
                }
                else
                {
                    return BadRequest<string>("the Enrollment Is Failure ");
                }

            }
            catch (Exception e)
            {
                return BadRequest<string>($"Invaild an error {e.Message}");
            }
        }

        public async Task<GResponse<string>> DeleteEnrollmentAsync(int id)
        {
            try
            {
                var Enrollment = await _enrollmentRepo.GetByIdAsync(id);
                if (Enrollment == null)
                {
                    return NotFound<string>($"the Enrollment Id {id} not found ");
                }
                var result = await _enrollmentRepo.DeleteAsync(Enrollment);
                if (result)
                {
                    return Deleted<string>("the Enrollment Is Deleted !");
                }
                else
                {
                    return BadRequest<string>("the Enrollment Is Failure ");
                }

            }
            catch (Exception ex)
            {
                return BadRequest<string>($"An error occurred: {ex.Message}");
            }
        }
        public async Task<PigatedResult<EnrollmentResponse>> EnrollmentListPaginatedAsync(EnrollmentPaginatedListRequest request)
        {
            var query = _enrollmentRepo.GetTableNoTracking().Include(x => x.Course).Include(x => x.User).Select(x => new EnrollmentResponse()
            {
                Id = x.Id,
                CourseName = x.Course.Title,
                UserName = x.User.UserName,
                EnrollmentDate = new DateOnly(x.EnrollmentDate.Year, x.EnrollmentDate.Month, x.EnrollmentDate.Day)
            }).AsQueryable();
            if (!query.Any())
            {
                return new PigatedResult<EnrollmentResponse>(new List<EnrollmentResponse>());
            }
            return await query.ToPaginatedListAsync(request.NumberPage, request.PageSize);
        }

        public async Task<GResponse<EnrollmentResponse>> GetEnrollmentByIdAsync(int id)
        {
            try
            {
                var Enrollment = await _enrollmentRepo.GetTableNoTracking().Include(x => x.Course)
                    .Include(x => x.User).Select(x => new EnrollmentResponse()
                    {
                        Id = id,
                        CourseName = x.Course.Title,
                        EnrollmentDate = new DateOnly(x.EnrollmentDate.Year, x.EnrollmentDate.Month, x.EnrollmentDate.Day),
                        UserName = x.User.UserName ?? "No User Name"
                    }).FirstOrDefaultAsync(x => x.Id == id);
                if (Enrollment == null)
                {
                    return BadRequest<EnrollmentResponse>($"The Enrollment By Id ={id}  Not Found ");

                }
                else
                {
                    return OK(Enrollment, count: 1);
                }

            }

            catch (Exception e)
            {
                return BadRequest<EnrollmentResponse>($"Invaild an error {e.Message}");
            }
        }

        public async Task<GResponse<List<EnrollmentResponse>>> GetEnrollmentsAsync()
        {
            try
            {
                var enrollments = await _enrollmentRepo.GetTableNoTracking().Include(x => x.Course).Include(x => x.User).Select(x => new EnrollmentResponse()
                {
                    Id = x.Id,
                    CourseName = x.Course.Title,
                    UserName = x.User.UserName,
                    EnrollmentDate = new DateOnly(x.EnrollmentDate.Year, x.EnrollmentDate.Month, x.EnrollmentDate.Day)
                }).ToListAsync();
                if (enrollments.Count > 0)
                {
                    return OK(enrollments, count: enrollments.Count());
                }
                else
                {
                    return NotFound<List<EnrollmentResponse>>("The Enrollment List Is Empty");
                }

            }
            catch (Exception e)
            {
                return BadRequest<List<EnrollmentResponse>>($"Invaild an error {e.Message}");
            }
        }

        public async Task<GResponse<string>> UpdateEnrollmentAsync(UpdateEnrollmentRequest request)
        {
            try
            {
                var OldEnrollment = await _enrollmentRepo.GetByIdAsync(request.Id);
                if (OldEnrollment == null && OldEnrollment.Id != request.Id)
                {
                    return NotFound<string>($"the Enrollment Id {request.Id} not found ");
                }
                var Course = await _courseService.GetCourseByIdAsync(request.CourseId);
                if (!Course.IsSuccess)
                {
                    return NotFound<string>($"the course Id {request.CourseId} not found ");
                }
                var User = await _userService.GetUserByIdAsync(request.UserId);
                if (!User.IsSuccess)
                {
                    return NotFound<string>($"the user Id {request.UserId} not found ");
                }
                OldEnrollment.CourseId = request.CourseId;
                OldEnrollment.UserId = request.UserId;
                OldEnrollment.EnrollmentDate = DateTime.UtcNow;
                var result = await _enrollmentRepo.UpdateAnsyc(OldEnrollment);
                if (result)
                {
                    return Success<string>("the Enrollment Is Updated !");
                }
                else
                {
                    return BadRequest<string>("the Enrollment Is Failure ");
                }
            }
            catch (Exception ex)
            {
                return BadRequest<string>($"Invalid an errors {ex.Message}");
            }
        }
    }
}
