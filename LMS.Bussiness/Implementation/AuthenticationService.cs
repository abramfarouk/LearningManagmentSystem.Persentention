using LMS.Bussiness.Dtos.AuthenticationDtos;
using LMS.Bussiness.DTOS.AuthenticationDtos;
using LMS.Data.Abstract;
using LMS.Data.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LMS.Bussiness.Implementation
{
    public class AuthenticationService : ResponseHandler, IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenRepo;
        private readonly JwtSettings _jwtSettings;
        public AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager, IGenericRepository<UserRefreshToken> userRefreshTokenRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRefreshTokenRepo = userRefreshTokenRepo;
        }
        public async Task<GResponse<string>> ConfirmEmailAsync(ConfirmEmailRequest request)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.userId.ToString());
                if (user == null)
                {

                    return NotFound<string>($"No found user with Id : {request.userId}.");

                }


                var decodeCode = WebUtility.UrlDecode(request.code);
                var confirmEmail = await _userManager.ConfirmEmailAsync(user, decodeCode);
                if (!confirmEmail.Succeeded)
                {
                    return BadRequest<string>(GetErrors(confirmEmail.Errors));

                }

                return Success<string>("Confirm Email Operation Successfully.");

            }
            catch (Exception ex)
            {
                return BadRequest<string>($"An error occurred: {ex.Message}");
            }

        }
        public Task<GResponse<string>> IsValidToken(string accessToken)
        {
            throw new NotImplementedException();
        }
        public Task<GResponse<JwtAuthResponse>> RefreshToken(RefreshTokenRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GResponse<JwtAuthResponse>> SignInAsync(SignInRequest request)
        {
            try
            {
                var users = await _userManager.FindByEmailAsync(request.Email);
                if (users == null)
                {
                    return BadRequest<JwtAuthResponse>("Email Or Password not Correct");
                }
                var Result = await _userManager.CheckPasswordAsync(users, request.Password);
                if (!Result)
                {
                    return BadRequest<JwtAuthResponse>("Email Or Password not Correct");
                }
                if (users.EmailConfirmed == false)
                    return BadRequest<JwtAuthResponse>("Email not Confirm");
                //------Generate Token-------//

                var accessToken = await GetJwtToken(users);

                return Created<JwtAuthResponse>(accessToken, "Sign In Operation Successfully.");

            }
            catch (Exception ex)
            {
                return BadRequest<JwtAuthResponse>($"Invalid an errors {ex.Message}");
            }

        }

        #region Private Methods 

        private async Task<JwtAuthResponse> GetJwtToken(User user)
        {

            var (jwtToken, accessToken) = await GenerateJwtToken(user);

            var refreshToken = GetRefreshToken(user.UserName!);
            var userRefreshTokenTable = new UserRefreshToken
            {
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpireDate),
                IsUsed = true,
                IsRevoked = false,
                JwtId = jwtToken.Id,
                RefreshToken = refreshToken.refreshTokenString,
                Token = accessToken,
                UserId = user.Id
            };

            await _userRefreshTokenRepo.AddAsync(userRefreshTokenTable);

            var response = new JwtAuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return response;
        }
        private async Task<(JwtSecurityToken, string)> GenerateJwtToken(User user)
        {
            var Claims = await GetClaims(user);

            var jwtToken = new JwtSecurityToken(_jwtSettings.Issure, _jwtSettings.Audience,
                claims: Claims, expires: DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return (jwtToken, accessToken);
        }
        private async Task<List<Claim>> GetClaims(User user)
        {
            var rolesForUser = await _userManager.GetRolesAsync(user);
            var claimsForUser = await _userManager.GetClaimsAsync(user);

            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName!),
                new Claim(ClaimTypes.NameIdentifier,user.UserName!),
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(nameof(UserClaimsModel.PhoneNumber),user.PhoneNumber),
                new Claim(nameof(UserClaimsModel.Id),user.Id.ToString())
            };

            foreach (var role in rolesForUser)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }
            Claims.AddRange(claimsForUser);

            return Claims;
        }

        private async Task<JwtAuthResponse> GetRefreshToken(User user, JwtSecurityToken jwtToken,
            DateTime? expiryDate, string refreshToken)
        {
            var (jwtSecurityToken, newToken) = await GenerateJwtToken(user);

            var userNameClaim = jwtToken.Claims.FirstOrDefault(x => x.Type.Contains("name", StringComparison.OrdinalIgnoreCase))?.Value;
            if (userNameClaim == null)
            {
                // معالجة الخطأ إذا كان UserName claim غير موجود
                throw new InvalidOperationException("UserName claim is missing in the JWT token.");
            }

            var refreshtokenResult = new RefreshToken();
            refreshtokenResult.UserName = userNameClaim;
            refreshtokenResult.refreshTokenString = refreshToken;
            refreshtokenResult.ExpireAt = (DateTime)expiryDate;

            var response = new JwtAuthResponse();
            response.AccessToken = newToken;
            response.RefreshToken = refreshtokenResult;
            return response;
        }

        private RefreshToken GetRefreshToken(string username)
        {
            var refreshToken = new RefreshToken
            {
                ExpireAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpireDate),
                UserName = username,
                refreshTokenString = GenerateRefreshToken()
            };

            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }





















        private string GetErrors(IEnumerable<IdentityError> errors)
        {
            return "An Error " + string.Join(", ", errors.Select(x => x.Description));
        }
        #endregion

    }
}


