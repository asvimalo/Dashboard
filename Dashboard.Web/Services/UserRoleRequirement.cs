using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Dashboard.Web.Services
{
    public class UserRoleRequirement : IAuthorizationRequirement
    {
    }
    public class UserRoleHandler : AuthorizationHandler<UserRoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRoleRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.Role))
            {
                context.Succeed(requirement);
            };
            return Task.CompletedTask;
        }
    }
}
