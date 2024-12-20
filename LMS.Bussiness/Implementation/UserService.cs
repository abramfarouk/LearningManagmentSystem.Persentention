using LMS.Bussiness.DTOS.UserDto;
using LMS.Data.Abstract;
using LMS.Data.Bases;
using LMS.Data.Data.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Bussiness.Implementation
{
    public class UserService : ResponseHandler, IUserService
    {
        #region Fields  
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IGenericRepository<User> _UserRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUrlHelper _urlHelper;
        #endregion

        #region Ctor 
        public UserService(UserManager<User> userManager, IEmailService emailService
            , IGenericRepository<User> userRepo
            , RoleManager<Role> roleManager,
            IHttpContextAccessor httpContextAccessor,
            IUrlHelper urlHelper)
        {
            _userManager = userManager;
            _emailService = emailService;
            _UserRepo = userRepo;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
            _urlHelper = urlHelper;

        }
        #endregion

        #region Methods 
        public async Task<GResponse<string>> AddUserAsync(AddUserRequest request, string roleName)
        {
            var trans = await _UserRepo.BeginTransactionAsync();
            try
            {
                var SameUserName = await _userManager.FindByNameAsync(request.UserName);
                if (SameUserName != null)
                {
                    return NotFound<string>($"the UserName {request.UserName} Is Already Exist");
                }
                var SameEmail = await _userManager.FindByEmailAsync(request.Email);
                if (SameEmail != null)
                {
                    return NotFound<string>($"the Email {request.Email} Is Already Exist");
                }

                var PhoneNumber = await _UserRepo.GetAllAsync();
                var SamePhoneNumber = PhoneNumber.Select(x => x.PhoneNumber == request.PhoneNumber).FirstOrDefault();
                if (SamePhoneNumber)
                {
                    return NotFound<string>($"the PhoneNumber {request.PhoneNumber} Is Already Exist");
                }
                var user = new User()
                {
                    UserName = request.UserName,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Country = request.Country,
                    City = request.City,
                    Address = request.Address,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber
                };
                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    return BadRequest<string>(null, GetErrors(result.Errors));
                }
                var roleExist = await _roleManager.RoleExistsAsync(roleName.ToLower());
                if (!roleExist)
                {
                    return NotFound<string>($"Invalid role specified.");
                }

                await _userManager.AddToRoleAsync(user, roleName.ToLower());

                var Code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                Code = WebUtility.UrlEncode(Code);
                var requestAccessor = _httpContextAccessor.HttpContext.Request;
                var returnURL = requestAccessor.Scheme + "://" + requestAccessor.Host +
                    _urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = Code });
                await _emailService.SendEmailAsync(user.Email, returnURL);
                await trans.CommitAsync();

                return Created<string>(user.FirstName, "Added Operation Successfully.");
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return BadRequest<string>($"An error occurred: {ex.Message}");
            }

        }

        public async Task<GResponse<string>> ChangeUserPasswordAsync(ChangeUserPasswordDto request)
        {
            try
            {

                var OldUser = await _userManager.FindByIdAsync(request.Id.ToString());
                if (OldUser == null)
                {
                    return NotFound<string>($"user not Found by ID {request.Id}");
                }
                var result = await _userManager.ChangePasswordAsync(OldUser, request.CurrentPassword, request.NewPassword);
                if (result.Succeeded)
                {
                    return Success<string>("Password Changed Successfully");
                }
                else
                {
                    return BadRequest<string>(null, GetErrors(result.Errors));

                }


            }
            catch (Exception ex)
            {
                return BadRequest<string>($"An error occurred: {ex.Message}");
            }
        }

        public async Task<GResponse<string>> DeleteUserAsync(int userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return NotFound<string>($"No found user with Id : {userId}.");
                }
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    return BadRequest<string>(null, GetErrors(result.Errors));
                }

                return Deleted<string>("Delete Operation Successfully.");


            }
            catch (Exception ex)
            {
                return BadRequest<string>($"An error occurred: {ex.Message}");

            }
        }

        public async Task<GResponse<UserResponseDto>> GetUserByIdAsync(int Id)
        {
            try
            {
                var user = await _UserRepo.GetByIdAsync(Id);
                if (user == null)
                {
                    return NotFound<UserResponseDto>($"No found user with Id : {Id}.");

                }

                var userDto = new UserResponseDto
                {
                    UserId = user.Id,
                    Roles = _userManager.GetRolesAsync(user).Result.ToList(),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Country = user.Country,
                    City = user.City,
                    Address = user.Address,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };

                return OK(userDto, "Get operation successfully completed.");
            }
            catch (Exception ex)
            {
                return BadRequest<UserResponseDto>($"An error occurred: {ex.Message}");

            }
        }

        public async Task<GResponse<IEnumerable<UserResponseDto>>> GetUserListAsync()
        {
            var users = await _UserRepo.GetAllAsync();
            if (users == null)
            {
                return NotFound<IEnumerable<UserResponseDto>>($"Data Is Empty");
            }
            var userDto = users.Select(x => new UserResponseDto
            {
                UserId = x.Id,
                Roles = _userManager.GetRolesAsync(x).Result.ToList(),
                FirstName = x.FirstName,
                LastName = x.LastName,
                Country = x.Country,
                City = x.City,
                Address = x.Address,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber
            }).ToList();
            return OK<IEnumerable<UserResponseDto>>(userDto, "Get operation successfully completed.", users.Count());


        }

        public async Task<PigatedResult<UserPaginatedListResponseDto>> GetUserPaginatedListAsync(UserPaginatedListRequest request)
        {

            var userQuery = _userManager.Users.Select(x => new UserPaginatedListResponseDto
            {
                UserId = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Country = x.Country,
                City = x.City,
                Address = x.Address,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber


            }).AsQueryable();
            if (!userQuery.Any())
            {
                return new PigatedResult<UserPaginatedListResponseDto>(new List<UserPaginatedListResponseDto>());
            }
            var usersPaginated = await userQuery.ToPaginatedListAsync(request.NumberPage, request.PageSize);

            foreach (var userDto in usersPaginated.Data)
            {
                var user = await _userManager.FindByIdAsync(userDto.UserId.ToString());
                if (user != null)
                {
                    var Roles = await _userManager.GetRolesAsync(user);
                    userDto.Roles = Roles.ToList();
                }
            }

            return usersPaginated;
        }

        public async Task<GResponse<string>> UpdateUserAsync(UpdateUserRequest request)
        {
            try
            {

                var OldUser = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (OldUser != null && OldUser.Id == request.UserId)
                {

                    var SameUserName = await _userManager.FindByNameAsync(request.UserName);
                    if (SameUserName != null)
                    {
                        return NotFound<string>($"the UserName {request.FirstName} Is Already Exist");
                    }
                    var SameEmail = await _userManager.FindByEmailAsync(request.Email);
                    if (SameEmail != null)
                    {
                        return NotFound<string>($"the Email {request.Email} Is Already Exist");
                    }
                    var SamePhoneNumber = await _UserRepo.GetAllAsync();
                    if (SamePhoneNumber.Select(x => x.PhoneNumber == request.PhoneNumber).FirstOrDefault())
                    {
                        return NotFound<string>($"the PhoneNumber {request.PhoneNumber} Is Already Exist");
                    }


                    OldUser.FirstName = request.FirstName;
                    OldUser.LastName = request.LastName;
                    OldUser.UserName = request.UserName;
                    OldUser.Country = request.Country;
                    OldUser.City = request.City;
                    OldUser.Address = request.Address;
                    OldUser.Email = request.Email;
                    OldUser.PhoneNumber = request.PhoneNumber;


                    var result = await _userManager.UpdateAsync(OldUser);
                    if (!result.Succeeded)
                    {
                        return BadRequest<string>(null, GetErrors(result.Errors));
                    }
                    return Success<string>("Update Operation Successfully.");
                }
                else
                {
                    return NotFound<string>($"No found user with Id : {request.UserId}.");
                }

            }
            catch (Exception ex)
            {
                return BadRequest<string>($"An error occurred: {ex.Message}");
            }
        }


        #endregion
        private List<string> GetErrors(IEnumerable<IdentityError> errors)
        {
            return errors.Select(x => x.Description).ToList();

        }

    }
}




