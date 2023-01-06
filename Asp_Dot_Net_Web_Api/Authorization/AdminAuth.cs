using System;
using Microsoft.AspNetCore.Authorization;

namespace Asp_Dot_Net_Web_Api.Authorization
{
    public class IsAdminRequirement : IAuthorizationRequirement
    {
    }

    public class IsAdminHandler : AuthorizationHandler<IsAdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAdminRequirement requirement)
        {
            if (context.User.HasClaim(claim => claim.Type == "isAdmin" && claim.Value == "true"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

