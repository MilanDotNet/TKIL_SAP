using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using TKILSAPRFC.Core.Constants;
using TKILSAPRFC.Core.Enums;

namespace TKILSAPRFC.Core.Helpers
{
    public class AuthorizationGrantTypeRequirement : IAuthorizationRequirement
    {
    }
    public class RefreshTokenGrantTypeHandler : AuthorizationHandler<AuthorizationGrantTypeRequirement>
    {
        /// <summary>
        /// Refresh token should only be used for the purpose of refreshing the expired token. For all other purpose, user must use the access token
        /// </summary>
        /// <param name="context">Auth context</param>
        /// <param name="requirement">Auth Policy requirement</param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationGrantTypeRequirement requirement)
        {
            if (context.Resource is AuthorizationFilterContext ctx)
            {
                if (context.User.HasClaim(c => c.Type == JwtUserClaimNames.GrantType && c.Value == Convert.ToString(TokenType.Refresh_Token)))
                {
                    if (Convert.ToString(ctx.RouteData.Values["controller"]).ToLower().Equals("auth") && Convert.ToString(ctx.RouteData.Values["action"]).ToLower().Equals("refreshtoken"))
                        context.Succeed(requirement);
                }
                else
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
