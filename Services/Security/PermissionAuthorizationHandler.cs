using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SistemaGestionInventario.Services.Security
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            var userPermissions = context.User
                .Claims
                .Where(c => c.Type == "Permission")
                .Select(c => c.Value)
                .ToHashSet();

            if (requirement.RequireAll)
            {
                if (requirement.Permissions.All(p => userPermissions.Contains(p)))
                    context.Succeed(requirement);
            }
            else
            {
                if (requirement.Permissions.Any(p => userPermissions.Contains(p)))
                    context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
