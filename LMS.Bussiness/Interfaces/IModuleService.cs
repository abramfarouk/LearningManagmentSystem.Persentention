using LMS.Bussiness.DTOS.ModuleDtos;

namespace LMS.Bussiness.Interfaces
{
    public interface IModuleService
    {
        #region Command  
        public Task<GResponse<string>> AddModuleServiceAsync(AddModuleRequest request);
        public Task<GResponse<string>> UpdatedModuleServiceAsync(UpdatedModuleRequest request);
        public Task<GResponse<string>> DeleteModuleServiceAsync(int Module_Id);

        #endregion

        #region Query  
        public Task<GResponse<IEnumerable<GetModuleResponseDto>>> GetAllModuleServiceAsync();
        public Task<GResponse<GetModuleResponseDto>> GetModuleServiceByIdAsync();
        public Task<GResponse<GetModuleResponseDto>> GetPaginatedModuleServiceListAsync(ModulePaginatedListRequest request);

        #endregion

    }

}