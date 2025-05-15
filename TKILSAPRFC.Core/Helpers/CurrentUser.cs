using Microsoft.AspNetCore.Http;
using TKILSAPRFC.Core.Constants;
using TKILSAPRFC.Core.Enums;
using TKILSAPRFC.Core.Helpers.Interface;
using System.Security.Claims;

namespace TKILSAPRFC.Core.Helpers
{
    public sealed class CurrentUser : ICurrentUser
    {
        private readonly HttpContext? httpContext;
        private readonly ClaimsIdentity? claimsIdentity;
        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            httpContext = httpContextAccessor.HttpContext;
            claimsIdentity = httpContext?.User?.Identity as ClaimsIdentity;
        }

        public int UserId => Convert.ToInt32((claimsIdentity?.Claims.Any()) != true ? "-1" : claimsIdentity?.FindFirst(JwtUserClaimNames.UserId)?.Value);

        public string? UserCode => Convert.ToString((claimsIdentity?.Claims.Any()) != true ? string.Empty : claimsIdentity?.FindFirst(JwtUserClaimNames.UserCode)?.Value);

        public string? UserName => Convert.ToString((claimsIdentity?.Claims.Any()) != true ? string.Empty : claimsIdentity?.FindFirst(JwtUserClaimNames.UserName)?.Value);
        public string? UserDatabase => Convert.ToString((claimsIdentity?.Claims.Any()) != true ? string.Empty : claimsIdentity?.FindFirst(JwtUserClaimNames.UserDatabase)?.Value);

        public TokenType GrantType
        {
            get
            {
                _ = Enum.TryParse(Convert.ToString((claimsIdentity?.Claims.Any()) != true ? string.Empty : claimsIdentity?.FindFirst(JwtUserClaimNames.GrantType)?.Value), out TokenType tokenType);
                return tokenType;
            }
        }
    }
}
