using LMS.Bussiness.DTOS.ModuleDtos;
using LMS.Data.Bases;

namespace LMS.Bussiness.Interfaces
{
    public interface IModuleService
    {
        #region Command  
        public Task<GResponse<string>> AddModuleAsync(AddModuleRequest request);
        public Task<GResponse<string>> UpdatedModuleAsync(UpdatedModuleRequest request);
        public Task<GResponse<string>> DeleteModuleAsync(int Module_Id);

        #endregion

        #region Query  
        public Task<GResponse<IEnumerable<GetModuleResponseDto>>> GetAllModuleListAsync();
        public Task<GResponse<GetModuleResponseDto>> GetModuleByIdAsync(int moduleId);
        public Task<PigatedResult<GetModuleResponseDto>> PaginatedModuleListAsync(ModulePaginatedListRequest request);

        #endregion

    }

}