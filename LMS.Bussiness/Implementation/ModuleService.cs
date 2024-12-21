using LMS.Bussiness.DTOS.ModuleDtos;

namespace LMS.Bussiness.Implementation
{
    public class ModuleService : IModuleService
    {
        public Task<GResponse<string>> AddModuleServiceAsync(AddModuleRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<string>> DeleteModuleServiceAsync(int Module_Id)
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<IEnumerable<GetModuleResponseDto>>> GetAllModuleServiceAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<GetModuleResponseDto>> GetModuleServiceByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<GetModuleResponseDto>> GetPaginatedModuleServiceListAsync(ModulePaginatedListRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<string>> UpdatedModuleServiceAsync(UpdatedModuleRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
