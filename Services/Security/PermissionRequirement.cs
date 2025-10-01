using Microsoft.AspNetCore.Authorization;

namespace SistemaGestionInventario.Services.Security
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public IReadOnlyList<string> Permissions { get; }
        public bool RequireAll { get; }

        public PermissionRequirement(IEnumerable<string> permissions, bool requireAll = false)
        {
            Permissions = permissions.ToList();
            RequireAll = requireAll;
        }
    }
}
