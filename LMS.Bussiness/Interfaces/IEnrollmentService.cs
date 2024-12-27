using LMS.Bussiness.DTOS.EnrollmentDtos;
using LMS.Data.Bases;

namespace LMS.Bussiness.Interfaces
{
    public interface IEnrollmentService
    {
        public Task<GResponse<string>> CreateEnrollmentAsync(AddEnrollmentRequest request);
        public Task<GResponse<string>> DeleteEnrollmentAsync(int id);
        public Task<GResponse<string>> UpdateEnrollmentAsync(UpdateEnrollmentRequest request);
        public Task<GResponse<EnrollmentResponse>> GetEnrollmentByIdAsync(int id);
        public Task<GResponse<List<EnrollmentResponse>>> GetEnrollmentsAsync();
        public Task<PigatedResult<EnrollmentResponse>> EnrollmentListPaginatedAsync(EnrollmentPaginatedListRequest request);

    }
}
