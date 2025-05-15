using Microsoft.IdentityModel.Tokens;
using TKILSAPRFC.Core.Constants;
using TKILSAPRFC.Core.Enums;
using TKILSAPRFC.Core.Helpers;
using TKILSAPRFC.Infrastructure.Repository.Interface;
using TKILSAPRFC.Model.DataBaseModels;
using TKILSAPRFC.Model.ViewModels;
using TKILSAPRFC.Service.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace TKILSAPRFC.Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository loginRepository;
        public LoginService(ILoginRepository loginRepository)
        {
            this.loginRepository = loginRepository;
        }
        public async Task<ApiResponse<AuthResponseVm>> UserLogin(UserLoginVM entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Password) || string.IsNullOrWhiteSpace(entity.Username))
            {
                return ApiResponseHelper.CreateApiResponse<AuthResponseVm>(HttpStatusCode.BadRequest);
            }
            var user = await this.loginRepository.ValidateUserMultiDB(entity);

            if (user == null)
            {
                return ApiResponseHelper.CreateApiResponse<AuthResponseVm>(HttpStatusCode.Unauthorized, new List<string> { "Invalid UserCode Or Email or Password." });
            }

            return ApiResponseHelper.CreateApiResponse<AuthResponseVm>(GenerateToken(user), HttpStatusCode.OK);
        }
        private static AuthResponseVm GenerateToken(UserLoginDetailWithDatabase UserLoginDetailWithDatabase)
        {
            var claims = new[] {
                                new Claim(JwtRegisteredClaimNames.Sub, UserLoginDetailWithDatabase.UserLoginDetail.EMAIL_ADDRESS ?? string.Empty),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim(JwtUserClaimNames.GrantType, "password"),
                                new Claim(JwtUserClaimNames.UserId, Convert.ToString(UserLoginDetailWithDatabase.UserLoginDetail.PERSON_ID)),
                                new Claim(JwtUserClaimNames.UserCode, Convert.ToString(UserLoginDetailWithDatabase.UserLoginDetail.USER_ID) ?? string.Empty)
            };
            var extendclaims = new[] {
                                new Claim(JwtUserClaimNames.UserName, Convert.ToString(UserLoginDetailWithDatabase.UserLoginDetail.FULL_NAME) ?? string.Empty),
                                new Claim(JwtUserClaimNames.UserDatabase, Convert.ToString(UserLoginDetailWithDatabase.DatabaseName) ?? string.Empty)
            };
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.Current.JwtConfiguration.SigningKey));

            var identity = new ClaimsIdentity();
            identity.AddClaims(claims);
            identity.AddClaims(extendclaims);

            var refresh_token = new JwtSecurityToken(issuer: AppSettings.Current.JwtConfiguration.Issuer,
                                            audience: AppSettings.Current.JwtConfiguration.Audience,
                                            expires: DateTime.UtcNow.AddYears(100),
                                            claims: identity.Claims.Union(new[] { new Claim(JwtUserClaimNames.GrantType, Convert.ToString(TokenType.Refresh_Token)) }),
                                            signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

            var access_token = new JwtSecurityToken(issuer: AppSettings.Current.JwtConfiguration.Issuer,
                                             audience: AppSettings.Current.JwtConfiguration.Audience,
                                             expires: DateTime.UtcNow.AddYears(100),
                                             claims: identity.Claims.Union(new[] { new Claim(JwtUserClaimNames.GrantType, Convert.ToString(TokenType.Access_Token)) }),
                                             signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

            return new AuthResponseVm
            {
                Access_token = new JwtSecurityTokenHandler().WriteToken(access_token),
                Refresh_token = new JwtSecurityTokenHandler().WriteToken(refresh_token),
                UserLoginDetail = UserLoginDetailWithDatabase.UserLoginDetail
            };
        }
    }
}
