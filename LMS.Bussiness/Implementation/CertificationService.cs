using LMS.Bussiness.DTOS.CertificateDtos;
using LMS.Data.Abstract;
using LMS.Data.Bases;
using LMS.Data.Data.Entities;
using LMS.Data.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LMS.Bussiness.Implementation
{
    public class CertificationService : ResponseHandler, ICertificationService
    {
        #region Fields  
        private readonly IGenericRepository<Certificate> _certificateRepo;
        private readonly UserManager<User> _userManager;
        private readonly ICourseService _courseService;

        #endregion

        #region Ctor 
        public CertificationService(IGenericRepository<Certificate> certificateRepo, ICourseService courseService, UserManager<User> userManager)
        {
            _certificateRepo = certificateRepo;
            _courseService = courseService;
            _userManager = userManager;
        }
        #endregion

        #region Methods  

        public async Task<GResponse<string>> AddCeritifcationAsync(AddCeritificationRequest request)
        {
            try
            {
                if (request != null)
                {
                    var Stduent = await _userManager.FindByIdAsync(request.Std_Id.ToString());
                    if (Stduent == null)
                    {
                        return NotFound<string>($"Student by Id {request.Std_Id} not Found");

                    }
                    var ISstudent = await _userManager.IsInRoleAsync(Stduent, "student");
                    if (!ISstudent)
                    {
                        return BadRequest<string>("the User Is Not Student");
                    }

                    var Course = await _courseService.GetCourseByIdAsync(request.Crs_Id);
                    if (!Course.IsSuccess)
                    {
                        return NotFound<string>($"Course by Id {request.Crs_Id} not Found");

                    }




                    var certificated = new Certificate()
                    {
                        CourseId = request.Crs_Id,
                        UserId = request.Std_Id,
                        IssueDate = DateTime.UtcNow
                    };
                    var res = await _certificateRepo.AddAsync(certificated);
                    if (res)
                        return Success<string>("Certification Created Successfull");
                    return BadRequest<string>("Failed Certification  Create");
                }
                else
                {
                    return BadRequest<string>();
                }

            }
            catch (Exception ex)
            {
                return BadRequest<string>($"Invaild an errors {ex.Message}");
            }
        }

        public async Task<GResponse<string>> DeleteCeritifcationAsync(int ceritificateId)
        {
            try
            {
                var certification = await _certificateRepo.GetByIdAsync(ceritificateId);
                if (certification != null)
                {
                    var res = await _certificateRepo.DeleteAsync(certification);
                    if (res)
                        return Deleted<string>("Certification Deteled Is Successfull");
                    return BadRequest<string>("Certification Deleteled Is Failure ");
                }
                return NotFound<string>($"Certification By Id {certification.Id} not Found");
            }
            catch (Exception ex)
            {
                return BadRequest<string>($"Invaild an errors{ex.Message} ");
            }
        }
        public async Task<GResponse<CerificationResponseDto>> GetCerificationByIdAsync(int CertificationId)
        {
            try
            {
                var Certification = await _certificateRepo.GetTableNoTracking().Include(x => x.Course).Include(X => X.User).FirstOrDefaultAsync(x => x.Id == CertificationId);
                if (Certification != null)
                {
                    var CertificatedDto = new CerificationResponseDto()
                    {
                        CeritifedId = Certification.Id,
                        CourseName = Certification.Course.Title,
                        StudentName = Certification.User.UserName,
                        IssueDate = new DateOnly(Certification.IssueDate.Year, Certification.IssueDate.Month, Certification.IssueDate.Day)

                    };
                    return OK(CertificatedDto, count: 1);
                }
                else
                {
                    return NotFound<CerificationResponseDto>($"Certificated By ID {CertificationId} not Found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest<CerificationResponseDto>($"there Is Invaild {ex.Message}");
            }
        }
        public async Task<GResponse<List<CerificationResponseDto>>> GetCerificationListAsync()
        {
            var Certificates = await _certificateRepo.GetTableNoTracking().Include(x => x.Course).Include(x => x.User).Select(x => new CerificationResponseDto
            {
                CeritifedId = x.Id,
                IssueDate = new DateOnly(x.IssueDate.Year, x.IssueDate.Month, x.IssueDate.Day),
                CourseName = x.Course.Title,
                StudentName = x.User.UserName

            }).ToListAsync();
            if (!Certificates.Any())
            {
                return NotFound<List<CerificationResponseDto>>("No Certificates Found");
            }
            else
            {
                return OK(Certificates, count: Certificates.Count);
            }
        }
        public Task<GResponse<PigatedResult<CerificationResponseDto>>> GetCerificationPigationListAsync(CertificationPaginatedListRequest request)
        {
            throw new NotImplementedException();
        }
        public async Task<GResponse<string>> UpdateCeritifcationAsync(UpdateCeritificationRequest request)
        {
            try
            {
                var OldCertification = await _certificateRepo.GetByIdAsync(request.Certificated_Id);
                if (OldCertification != null && OldCertification.Id == request.Certificated_Id)
                {

                    var Stduent = await _userManager.FindByIdAsync(request.Std_Id.ToString());
                    if (Stduent == null)
                    {
                        return NotFound<string>($"Student by Id {request.Std_Id} not Found");

                    }
                    var ISstudent = await _userManager.IsInRoleAsync(Stduent, "student");
                    if (!ISstudent)
                    {
                        return BadRequest<string>("the User Is Not Student");
                    }

                    var Course = await _courseService.GetCourseByIdAsync(request.Crs_Id);
                    if (!Course.IsSuccess)
                    {
                        return NotFound<string>($"Course by Id {request.Crs_Id} not Found");

                    }
                    OldCertification.CourseId = request.Crs_Id;
                    OldCertification.UserId = request.Std_Id;
                    OldCertification.IssueDate = DateTime.UtcNow;
                    var result = await _certificateRepo.UpdateAnsyc(OldCertification);
                    if (result)
                        return Success<string>("Certification Updated Successfull");
                    return BadRequest<string>("Certification Updated Failure");


                }
                else
                {
                    return NotFound<string>($"Certification By Id{request.Certificated_Id} not Found ");
                }
            }
            catch (Exception ex)
            {
                return BadRequest<string>($"Invalid an errors {ex.Message}");
            }

        }
        #endregion
    }
}
