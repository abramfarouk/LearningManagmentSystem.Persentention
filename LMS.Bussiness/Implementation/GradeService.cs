using LMS.Bussiness.DTOS.GradeDtos;
using LMS.Data.Abstract;
using LMS.Data.Bases;
using LMS.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Bussiness.Implementation
{
    public class GradeService : ResponseHandler, IGradeService
    {
        private readonly IGenericRepository<Grade> _gradeRepo;
        private readonly ISubmissionService _submissionService;
        public GradeService(IGenericRepository<Grade> gradeRepo, ISubmissionService submissionService)
        {
            _gradeRepo = gradeRepo;
            _submissionService = submissionService;
        }

        public async Task<GResponse<string>> CreateGradeAsync(AddGradeRequest request)
        {
            try
            {
                var Submission = await _submissionService.GetSubmissionByIdAsync(request.SubmissionId);
                if (!Submission.IsSuccess)
                {
                    return NotFound<string>($"the Submission Id {request.SubmissionId} not found ");
                }
                var grade = new Grade
                {
                    grade = request.grade,
                    SubmissionId = request.SubmissionId,
                };
                var result = await _gradeRepo.AddAsync(grade);
                if (result)
                {
                    return Success<string>("the Grade Is Successfull !");
                }
                else
                {
                    return BadRequest<string>("the Grade Is Failure ");
                }


            }
            catch (Exception e)
            {
                return BadRequest<string>($"Invalid an errors{e.Message}");
            }
        }

        public async Task<GResponse<string>> DeleteGradeAsync(int id)
        {
            var grade = await _gradeRepo.GetByIdAsync(id);
            if (grade == null)
            {
                return NotFound<string>("Grade not found");
            }
            var result = await _gradeRepo.DeleteAsync(grade);
            if (result == null)
            {
                return BadRequest<string>("Failed to delete grade");
            }
            return Deleted<string>("Grade deleted successfully");
        }

        public async Task<GResponse<List<GradeResponse>>> GetAllGradesAsync()
        {
            var grades = await _gradeRepo.GetTableNoTracking().Select(x => new GradeResponse()
            {
                Id = x.Id,
                grade = x.grade,
                Submission = x.Submission.Content,
            }).ToListAsync();
            if (grades == null)
            {
                return NotFound<List<GradeResponse>>("Grades not found");
            }
            return OK(grades, count: grades.Count());

        }

        public async Task<GResponse<GradeResponse>> GetGradesByIdAsync(int id)
        {
            var grade = await _gradeRepo.GetTableNoTracking().Select(x => new GradeResponse()
            {
                Id = x.Id,
                grade = x.grade,
                Submission = x.Submission.Content,
            }).FirstOrDefaultAsync(x => x.Id == id);
            if (grade == null)
            {
                return NotFound<GradeResponse>("Grade not found");
            }
            return OK(grade, count: 1);
        }

        public Task<GResponse<PigatedResult<GradeResponse>>> PaginatedListRequestAsync(PaginatedListRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GResponse<string>> UpdateGradeAsync(UpdateGradeRequest request)
        {
            var OldGrade = await _gradeRepo.GetTableNoTracking().Include(x => x.Submission).FirstOrDefaultAsync(x => x.Id == request.Id);
            if (OldGrade == null)
            {
                return NotFound<string>("Grade not found");
            }
            OldGrade.grade = request.grade;
            OldGrade.SubmissionId = request.SubmissionId;
            var result = await _gradeRepo.UpdateAnsyc(OldGrade);
            if (result)
            {
                return Success<string>("Grade updated successfully");
            }
            else
            {
                return BadRequest<string>("Failed to update grade");


            }
        }
    }
}
