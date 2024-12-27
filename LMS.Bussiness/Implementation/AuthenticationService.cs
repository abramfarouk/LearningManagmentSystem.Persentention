using LMS.Bussiness.Dtos.AuthenticationDtos;
using LMS.Bussiness.DTOS.AuthenticationDtos;
using LMS.Data.Abstract;
using LMS.Data.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        public AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager, IGenericRepository<UserRefreshToken> userRefreshTokenRepo, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRefreshTokenRepo = userRefreshTokenRepo;
            _jwtSettings = jwtSettings;

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
        public async Task<GResponse<string>> IsValidToken(string accessToken)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var parameters = new TokenValidationParameters
                {
                    ValidateIssuer = _jwtSettings.ValidateIssure,
                    ValidIssuers = new[] { _jwtSettings.Issure },
                    ValidateIssuerSigningKey = _jwtSettings.ValidateIssureSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                    ValidateAudience = _jwtSettings.ValidateAudience,
                    ValidAudience = _jwtSettings.Audience,
                    ValidateLifetime = _jwtSettings.ValidateLifetime
                };

                var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);

                if (validator == null)
                {
                    return BadRequest<string>("Invalid Token.");

                }
                return OK("Token is Valid.");
            }
            catch (SecurityTokenExpiredException)
            {
                return BadRequest<string>("Token is Expired.");
            }
            catch (Exception ex)
            {
                return BadRequest<string>($"An error occurred: {ex.Message}");
            }
        }
        public async Task<GResponse<JwtAuthResponse>> RefreshToken(RefreshTokenRequest request)
        {
            try
            {
                var jwtToken = ReadJwtToken(request.AccessToken);
                var validationResult = await ValidationDetails(jwtToken, request.AccessToken, request.RefreshToken);

                switch (validationResult)
                {
                    case (ValidationResult.AlgorithmIsWrong, null, null):
                        return BadRequest<JwtAuthResponse>("Algorithm Is Wrong");

                    case (ValidationResult.TokenIsNotExpired, null, null):
                        return BadRequest<JwtAuthResponse>("Token Is Not Expired");

                    case (ValidationResult.InvalidUserIdClaim, null, null):
                        return BadRequest<JwtAuthResponse>("Invalid UserId Claim");

                    case (ValidationResult.RefreshTokenNotFound, null, null):
                        return BadRequest<JwtAuthResponse>("Refresh Token Is Not Found");

                    case (ValidationResult.RefreshTokenExpired, null, null):
                        return BadRequest<JwtAuthResponse>("Refresh Token Is Expired");
                }

                var (validResult, userId, ExpiryDate) = validationResult;
                var user = await _userManager.FindByIdAsync(userId!);
                if (user == null)
                    return BadRequest<JwtAuthResponse>("User is not Found");

                var response = await GetRefreshToken(user, jwtToken, ExpiryDate, request.RefreshToken);
                return OK(response, "Refresh Token Operation Successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest<JwtAuthResponse>($"An error occurred: {ex.Message}");
            }
        }

        public async Task<GResponse<JwtAuthResponse>> SignIn(SignInRequest request)
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

            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issure,
                _jwtSettings.Audience,
                claims: Claims,
                expires: DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenExpireDate),
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



        private JwtSecurityToken ReadJwtToken(string accessToken)
        {

            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);

            return response;
        }
        private async Task<(ValidationResult result, string? userId, DateTime? expiryDate)> ValidationDetails(JwtSecurityToken jwtToken,
            string accessToken, string refreshToken)
        {
            if (!IsTokenValidAlgorithm(jwtToken))
                return (ValidationResult.AlgorithmIsWrong, null, null);


            if (!IsTokenExpired(jwtToken))
                return (ValidationResult.TokenIsNotExpired, null, null);


            var userId = GetUserIdFromToken(jwtToken);
            if (userId == null)
                return (ValidationResult.InvalidUserIdClaim, null, null);

            var userRefreshToken = await FindRefreshTokenAsync(accessToken, refreshToken, userId);
            if (userRefreshToken == null)
                return (ValidationResult.RefreshTokenNotFound, null, null);


            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                await RevokeExpiredTokenAsync(userRefreshToken);
                return (ValidationResult.RefreshTokenExpired, null, null);
            }

            return (ValidationResult.Success, userId.ToString(), userRefreshToken.ExpiryDate);
        }

        private bool IsTokenValidAlgorithm(JwtSecurityToken jwtToken) => jwtToken?.Header?.Alg == SecurityAlgorithms.HmacSha256Signature;
        private bool IsTokenExpired(JwtSecurityToken jwtToken) => jwtToken.ValidTo <= DateTime.UtcNow;
        private int? GetUserIdFromToken(JwtSecurityToken jwtToken)
        {
            var userIdClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimsModel.Id))?.Value;
            return int.TryParse(userIdClaim, out var userId) ? userId : (int?)null;
        }
        private async Task<UserRefreshToken?> FindRefreshTokenAsync(string accessToken, string refreshToken, int? userId)
        {
            return await _userRefreshTokenRepo.GetTableNoTracking()
                 .FirstOrDefaultAsync(x => x.Token == accessToken && x.RefreshToken == refreshToken && x.UserId == userId);
        }
        private async Task RevokeExpiredTokenAsync(UserRefreshToken userRefreshToken)
        {
            userRefreshToken.IsRevoked = true;
            userRefreshToken.IsUsed = false;
            await _userRefreshTokenRepo.UpdateAnsyc(userRefreshToken);
        }


















        private string GetErrors(IEnumerable<IdentityError> errors)
        {
            return "An Error " + string.Join(", ", errors.Select(x => x.Description));
        }
        #endregion

    }
}


