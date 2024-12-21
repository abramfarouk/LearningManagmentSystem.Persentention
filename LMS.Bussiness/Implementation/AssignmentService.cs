using LMS.Bussiness.DTOS.AssignmentDtos;
using LMS.Data.Abstract;
using LMS.Data.Bases;
using LMS.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Bussiness.Implementation
{
    public class AssignmentService : ResponseHandler, IAssignmentService
    {
        private readonly ICourseService _courseService;
        private readonly IGenericRepository<Assignment> _assignmentRepo;
        public AssignmentService(ICourseService courseService, IGenericRepository<Assignment> assignmentRepo)
        {
            _courseService = courseService;
            _assignmentRepo = assignmentRepo;
        }
        public async Task<GResponse<string>> AddAssignmentAsync(AddAssignmentRequest request)
        {
            try
            {
                var Course = await _courseService.GetCourseByIdAsync(request.CourseId);
                if (Course == null)
                {

                    return NotFound<string>($"The Course By Id {request.CourseId} Not Found");
                }
                var Assignment = new Assignment()
                {
                    CourseId = request.CourseId,
                    Description = request.Description,
                    Title = request.Title,
                    CreatedDate = DateTime.UtcNow,

                };
                var result = await _assignmentRepo.AddAsync(Assignment);
                if (result)
                {
                    return Success<string>("The Assignment Is Successfull");
                }
                else
                {
                    return BadRequest<string>("The Assignment Is Failure ");
                }

            }
            catch (Exception ex)
            {
                return BadRequest<string>($"Invalid an errors {ex.Message}");
            }
        }

        public async Task<GResponse<string>> DeleteAssignmentAsync(int Assignment_Id)
        {
            try
            {
                var Assignment = await _assignmentRepo.GetByIdAsync(Assignment_Id);
                if (Assignment == null)

                    return NotFound<string>($"the assignment {Assignment_Id} not found");
                var Result = await _assignmentRepo.DeleteAsync(Assignment);
                if (Result)
                {
                    return Deleted<string>("The Assignment Deleted Is Successful");
                }
                else
                {
                    return BadRequest<string>("The Assignment Deleted Is Failure");
                }

            }


            catch (Exception ex)
            {
                return BadRequest<string>($"Invaild an errors {ex.Message}");
            }
        }

        public async Task<GResponse<IEnumerable<GetAssignmentResponseDto>>> GetAllAssignmentListAsync()
        {
            var assignments = await _assignmentRepo.GetTableNoTracking()
                                                   .Include(x => x.Course)
                                                   .ToListAsync();

            if (!assignments.Any())
            {
                return NotFound<IEnumerable<GetAssignmentResponseDto>>("The Assignment List is Empty");
            }
            else
            {
                var assignmentDto = assignments.Select(x => new GetAssignmentResponseDto
                {
                    Id = x.Id,
                    CourseName = x.Course?.Title ?? "No Course",
                    Description = x?.Description ?? "No Description",
                    CreatedDate = new DateOnly(x.CreatedDate.Year, x.CreatedDate.Month, x.CreatedDate.Day),
                    Title = x.Title
                }).ToList();

                return OK<IEnumerable<GetAssignmentResponseDto>>(assignmentDto, count: assignmentDto.Count());
            }
        }
        public async Task<GResponse<GetAssignmentResponseDto>> GetAssignmentByIdAsync(int AssignmentId)
        {
            var assignment = await _assignmentRepo.GetTableNoTracking()
                                           .Include(x => x.Course).FirstOrDefaultAsync(x => x.Id == AssignmentId);
            if (assignment == null)
            {
                return NotFound<GetAssignmentResponseDto>($"The Assignment By Id {AssignmentId} not Found ");
            }
            else
            {
                var assignmentDto = new GetAssignmentResponseDto
                {
                    Id = assignment.Id,
                    CourseName = assignment.Course?.Title ?? "No Course",
                    Description = assignment?.Description ?? "No Description",
                    CreatedDate = new DateOnly(assignment.CreatedDate.Year, assignment.CreatedDate.Month, assignment.CreatedDate.Day),
                    Title = assignment.Title
                };
                return OK(assignmentDto, count: 1);


            }


        }

        public async Task<PigatedResult<GetAssignmentResponseDto>> GetPaginatedAssignmentListAsync(GetAssignmentPaginatedListRequest request)
        {
            var AssignmentQuery = _assignmentRepo.GetTableNoTracking().Include(x => x.Course).Select(x => new GetAssignmentResponseDto()
            {
                CourseName = x.Course.Title,
                CreatedDate = new DateOnly(x.CreatedDate.Year, x.CreatedDate.Month, x.CreatedDate.Day),
                Title = x.Title,
                Description = x.Description,
                Id = x.Id
            }).AsQueryable();
            if (!AssignmentQuery.Any())
            {
                return new PigatedResult<GetAssignmentResponseDto>(new List<GetAssignmentResponseDto>());
            }

            var AssignmentPaginated = await AssignmentQuery.ToPaginatedListAsync(request.NumberPage, request.PageSize);



            return AssignmentPaginated;
        }




        public async Task<GResponse<string>> UpdatedAssignmentAsync(UpdatedAssignmentRequest request)
        {
            try
            {
                var Course = await _courseService.GetCourseByIdAsync(request.CourseId);
                if (Course == null)
                    return NotFound<string>($"the Course By Id {request.CourseId} not found ");
                var OldAssignment = await _assignmentRepo.GetTableNoTracking().FirstOrDefaultAsync(x => x.Id == request.AssignmentId);
                if (OldAssignment != null && OldAssignment.Id == request.AssignmentId)
                {
                    OldAssignment.Title = request.Title;
                    OldAssignment.Description = request.Description;
                    OldAssignment.CreatedDate = DateTime.UtcNow;
                    OldAssignment.CourseId = request.CourseId;
                    var res = await _assignmentRepo.UpdateAnsyc(OldAssignment);
                    if (res)
                        return Success("Update Is Succcessful");
                    return BadRequest<string>("Update Is Failure");

                }
                return BadRequest<string>();


            }
            catch (Exception ex)
            {
                return BadRequest<string>($"Invalid an errors {ex.Message}");
            }
        }
    }
}
