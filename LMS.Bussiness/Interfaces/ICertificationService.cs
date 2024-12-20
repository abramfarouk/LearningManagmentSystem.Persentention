using LMS.Bussiness.DTOS.CertificateDtos;
using LMS.Data.Bases;

namespace LMS.Bussiness.Interfaces
{
    public interface ICertificationService
    {
        #region Command  
        public Task<GResponse<string>> AddCeritifcationAsync(AddCeritificationRequest request);
        public Task<GResponse<string>> UpdateCeritifcationAsync(UpdateCeritificationRequest request);
        public Task<GResponse<string>> DeleteCeritifcationAsync(int ceritificateId);
        #endregion

        #region Query

        public Task<GResponse<List<CerificationResponseDto>>> GetCerificationListAsync();
        public Task<GResponse<PigatedResult<CerificationResponseDto>>> GetCerificationPigationListAsync(CertificationPaginatedListRequest request);
        public Task<GResponse<CerificationResponseDto>> GetCerificationByIdAsync(int Crs_Id);

        #endregion
    }
}
