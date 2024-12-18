using LMS.Bussiness.DTOS.CertificateDtos;
using LMS.Data.Abstract;
using LMS.Data.Bases;
using LMS.Data.Data.Entities;
using LMS.Data.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace LMS.Bussiness.Implementation
{
    public class CertificationService //: ICertificationService
    {
        private readonly IGenericRepository<Certificate> _certificateRepo;
        private readonly UserManager<User> _userManager;


        public CertificationService(IGenericRepository<Certificate> certificateRepo)
        {
            _certificateRepo = certificateRepo;
        }

        //public async Task<GResponse<string>> AddCeritifcationAsync(AddCeritificationRequest request)
        //{
        //    var student = await _userManager.FindByIdAsync(request.StudentID.ToString());
        //    if (student == null)
        //    {
        //        return ErrorRespone($"student with Id : {request.StudentID} not found!");
        //    }



        //}

        public Task<GResponse<string>> DeleteCeritifcationAsync(int ceritificateId)
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<CerificationResponseDto>> GetCerificationByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<List<CerificationResponseDto>>> GetCerificationListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<PigatedResult<CerificationResponseDto>>> GetCerificationPigationListAsync(CertificationPaginatedListRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<string>> UpdateCeritifcationAsync(UpdateCeritificationRequest request)
        {
            throw new NotImplementedException();
        }

    }
}
