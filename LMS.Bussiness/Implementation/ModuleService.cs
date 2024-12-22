using LMS.Bussiness.DTOS.ModuleDtos;
using LMS.Data.Abstract;
using LMS.Data.Bases;
using LMS.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Bussiness.Implementation
{
    public class ModuleService : ResponseHandler, IModuleService
    {

        private readonly IGenericRepository<Module> _moduleRepo;
        private readonly ICourseService _courseService;
        public ModuleService(IGenericRepository<Module> moduleRepo, ICourseService courseService)
        {
            _moduleRepo = moduleRepo;
            _courseService = courseService;
        }
        public async Task<GResponse<string>> AddModuleAsync(AddModuleRequest request)
        {
            try
            {
                var Course = await _courseService.GetCourseByIdAsync(request.CourseId);
                if (Course.DataCount == 0)
                {
                    return NotFound<string>($"the course Id {request.CourseId} not found ");
                }

                var Module = new Module
                {
                    Title = request.Title,
                    Description = request.Description,
                    CourseId = request.CourseId,
                };
                var result = await _moduleRepo.AddAsync(Module);
                if (result)
                {
                    return Success<string>("the Module Is Successfull !");
                }
                else
                {
                    return BadRequest<string>("the Module Is Failure ");
                }
            }
            catch (Exception ex)
            {
                return BadRequest<string>($"Invilad an errors {ex.Message}");
            }
        }

        public async Task<GResponse<string>> DeleteModuleAsync(int Module_Id)
        {
            try
            {

                var Module = await _moduleRepo.GetByIdAsync(Module_Id);
                if (Module == null)
                {
                    return NotFound<string>($"the Module Id {Module_Id} not found ");
                }
                var result = await _moduleRepo.DeleteAsync(Module);
                if (result)
                {
                    return Success<string>("the Module Is Successfull !");
                }
                else
                {
                    return BadRequest<string>("the Module Is Failure ");
                }
            }
            catch (Exception ex)
            {
                return BadRequest<string>($"Invilad an errors {ex.Message}");
            }


        }

        public async Task<GResponse<IEnumerable<GetModuleResponseDto>>> GetAllModuleListAsync()
        {
            var Modules = await _moduleRepo.GetTableNoTracking().Include(x => x.Course).Select(c => new GetModuleResponseDto
            {
                Title = c.Title,
                CourseName = c.Course.Title ?? "No Course",
                Description = c.Description,
                ModuleId = c.Id

            }).ToListAsync();
            if (Modules.Count == 0)
            {
                return NotFound<IEnumerable<GetModuleResponseDto>>("The Modules Is Empty");
            }
            else
            {
                return OK<IEnumerable<GetModuleResponseDto>>(Modules, count: Modules.Count());
            }

        }

        public async Task<GResponse<GetModuleResponseDto>> GetModuleByIdAsync(int moduleId)
        {
            var module = await _moduleRepo.GetTableNoTracking().Include(c => c.Course).FirstOrDefaultAsync(x => x.Id == moduleId);
            if (module == null)
            {
                return NotFound<GetModuleResponseDto>($"the module Id {moduleId} not found");
            }
            else
            {
                var ModuleDto = new GetModuleResponseDto()
                {
                    Title = module.Title,
                    Description = module.Description,
                    CourseName = module.Course.Title,
                    ModuleId = module.Id,

                };
                return OK(ModuleDto, count: 1);
            }
        }

        public async Task<PigatedResult<GetModuleResponseDto>> PaginatedModuleListAsync(ModulePaginatedListRequest request)
        {
            var ModuleQuery = _moduleRepo.GetTableNoTracking().Include(x => x.Course).Select(x => new GetModuleResponseDto()
            {
                CourseName = x.Course.Title,
                Description = x.Description,
                Title = x.Title,
                ModuleId = x.Id
            }).AsQueryable();
            if (!ModuleQuery.Any())
            {
                return new PigatedResult<GetModuleResponseDto>(new List<GetModuleResponseDto>());
            }
            var Paginated = await ModuleQuery.ToPaginatedListAsync(request.NumberPage, request.PageSize);
            return Paginated;

        }

        public async Task<GResponse<string>> UpdatedModuleAsync(UpdatedModuleRequest request)
        {
            try
            {
                var Module = await _moduleRepo.GetByIdAsync(request.ModuleId);
                if (Module == null)
                {
                    return NotFound<string>($"the Module Id {request.ModuleId} not found ");
                }
                var course = await _courseService.GetCourseByIdAsync(request.CourseId);
                if (!course.IsSuccess)
                {
                    return NotFound<string>($"the course Id {request.CourseId} not found ");
                }

                Module.Title = request.Title;
                Module.Description = request.Description;
                Module.CourseId = request.CourseId;
                var result = await _moduleRepo.UpdateAnsyc(Module);
                if (result)
                {
                    return Success<string>("the Module Is Successfull !");
                }
                else
                {
                    return BadRequest<string>("the Module Is Failure ");
                }
            }
            catch (Exception ex)
            {
                return BadRequest<string>($"Invilad an errors {ex.Message}");
            }

        }
    }
}
