using LMS.Bussiness.DTOS.GradeDtos;
using LMS.Data.Bases;

namespace LMS.Bussiness.Interfaces
{
    public interface IGradeService
    {
        public Task<GResponse<string>> CreateGradeAsync(AddGradeRequest request);
        public Task<GResponse<string>> DeleteGradeAsync(int id);
        public Task<GResponse<string>> UpdateGradeAsync(UpdateGradeRequest request);
        public Task<GResponse<List<GradeResponse>>> GetAllGradesAsync();

        public Task<GResponse<GradeResponse>> GetGradesByIdAsync(int id);
        public Task<GResponse<PigatedResult<GradeResponse>>> PaginatedListRequestAsync(PaginatedListRequest request);

    }
}
