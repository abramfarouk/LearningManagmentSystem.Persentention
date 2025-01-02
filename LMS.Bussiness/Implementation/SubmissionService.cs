using LMS.Bussiness.DTOS.SubmissionDtos;
using LMS.Data.Abstract;
using LMS.Data.Bases;
using LMS.Data.Data.Entities;
using LMS.Data.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LMS.Bussiness.Implementation
{
    public class SubmissionService : ResponseHandler, ISubmissionService
    {

        #region Fileds
        private readonly UserManager<User> _userManager;
        private readonly IGenericRepository<Submission> _submissionRepository;
        private readonly IAssignmentService _assignmentService;
        #endregion

        #region Constructors
        public SubmissionService(UserManager<User> userManager, IGenericRepository<Submission> submissionRepository,
            IAssignmentService assignmentService)
        {
            _userManager = userManager;
            _submissionRepository = submissionRepository;
            _assignmentService = assignmentService;
        }

        public async Task<GResponse<string>> CreateSubmissionAsync(AddSubmissionRequest request)
        {
            try
            {
                var student = await _userManager.FindByIdAsync(request.StudentId.ToString());
                if (student == null)
                {
                    return NotFound<string>($"student with Id : {request.StudentId} not found!");
                }

                var IsStudent = await _userManager.IsInRoleAsync(student, "student");
                if (!IsStudent)
                {
                    return BadRequest<string>($"This user not Student Plz Try Again");
                }

                var assignment = await _assignmentService.GetAssignmentByIdAsync(request.AssignmentId);
                if (!assignment.IsSuccess)
                {
                    return NotFound<string>($"assignment with Id : {request.AssignmentId} not found!");
                }

                var submission = new Submission();
                submission.Content = request.Content;
                submission.AssignmentId = request.AssignmentId;
                submission.UserId = request.StudentId;
                submission.SubmissionDate = DateTime.UtcNow;

                var result = await _submissionRepository.AddAsync(submission);

                return Success<string>("Submission Created Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest<string>($"An error occurred: {ex.Message}");
            }
        }

        public async Task<GResponse<string>> DeleteSubmissionAsync(int id)
        {
            try
            {
                var Submission = await _submissionRepository.GetByIdAsync(id);
                if (Submission == null)
                {
                    return NotFound<string>($"Submission with Id : {id} not found!");
                }
                await _submissionRepository.DeleteAsync(Submission);
                return Deleted<string>($"Submission with Id : {id} Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest<string>($"An error occurred: {ex.Message}");
            }
        }
        public async Task<GResponse<List<SubmissionResponseDto>>> GetAllSubmissionsAsync()
        {

            var submissions = await _submissionRepository.GetTableNoTracking().Include(x => x.User).Include(x => x.Assignment).Select(x => new SubmissionResponseDto()
            {
                Id = x.Id,
                Content = x.Content,
                SubmissionDate = new DateOnly(x.SubmissionDate.Year, x.SubmissionDate.Month, x.SubmissionDate.Day),
                AssignmentName = x.Assignment.Title,
                StudentName = x.User.UserName

            }).ToListAsync();
            if (submissions == null)
            {
                return NotFound<List<SubmissionResponseDto>>("No Submissions Found");
            }
            return Success<List<SubmissionResponseDto>>(submissions);


        }

        public async Task<GResponse<SubmissionResponseDto>> GetSubmissionByIdAsync(int id)
        {
            var submission = await _submissionRepository.GetTableNoTracking().Include(x => x.User).Include(x => x.Assignment).FirstOrDefaultAsync(x => x.Id == id);
            if (submission == null)
            {
                return NotFound<SubmissionResponseDto>($"Submission with Id : {id} not found!");
            }
            var submissionDto = new SubmissionResponseDto()
            {
                Id = submission.Id,
                Content = submission.Content,
                SubmissionDate = new DateOnly(submission.SubmissionDate.Year, submission.SubmissionDate.Month, submission.SubmissionDate.Day),
                AssignmentName = submission.Assignment.Title,
                StudentName = submission.User.UserName
            };
            return Success<SubmissionResponseDto>(submissionDto);
        }
        public Task<GResponse<PigatedResult<SubmissionResponseDto>>> PaginatedSubmissionListAsync(PaginatedListSubmissionRequest pagination)
        {
            throw new NotImplementedException();
        }
        public async Task<GResponse<string>> UpdateSubmissionAsync(UpdateSubmissionRequest request)
        {
            var OldSubmission = _submissionRepository.GetTableNoTracking().Include(x => x.User).Include(x => x.Assignment).FirstOrDefault(x => x.Id == request.Id);
            if (OldSubmission == null)
            {
                return NotFound<string>($"Submission with Id : {request.Id} not found!");
            }
            var student = await _userManager.FindByIdAsync(request.StudentId.ToString());
            if (student == null)
            {
                return NotFound<string>($"student with Id : {request.StudentId} not found!");
            }
            var IsStudent = await _userManager.IsInRoleAsync(student, "student");
            if (!IsStudent)
            {
                return BadRequest<string>($"This user not Student Plz Try Again");
            }
            var assignment = await _assignmentService.GetAssignmentByIdAsync(request.AssignmentId);
            if (!assignment.IsSuccess)
            {
                return NotFound<string>($"assignment with Id : {request.AssignmentId} not found!");
            }
            OldSubmission.Content = request.Content;
            OldSubmission.AssignmentId = request.AssignmentId;
            OldSubmission.UserId = request.StudentId;
            OldSubmission.SubmissionDate = DateTime.UtcNow;
            var result = await _submissionRepository.UpdateAnsyc(OldSubmission);
            if (result == null)
            {
                return BadRequest<string>("An error occurred while updating the Submission");
            }
            return Success<string>("Submission Updated Successfully");
        }
    }
}
#endregion