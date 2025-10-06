using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace SistemaGestionInventario.Pages.AccessControl.Roles
{
    [Authorize(Policy = "Permission.ROLE_DELETE")]
    public class DeleteModel : PageModel
    {
        private readonly SistemaGestionInventario.Data.SistemaGestionInventarioContext _context;

        public DeleteModel(SistemaGestionInventario.Data.SistemaGestionInventarioContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("/AccessControl/Index");
            }

            var OrganizationId = int.Parse(User.FindFirstValue("SelectedOrganizationId")!);

            var RoleToDelete = await _context.Roles
                .Include(r => r.OrganizationUserRole)
                    .ThenInclude(our => our.User)
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                .Where(r => r.Id == id && r.IdOrganization== OrganizationId)
                .FirstOrDefaultAsync();

            if (RoleToDelete == null)
            {
                return RedirectToPage("/AccessControl/Index");
            }

            if (RoleToDelete.RolePermissions.Count() > 0)
            {
                foreach (var rolePermission in RoleToDelete.RolePermissions)
                {
                    _context.RolePermissions.Remove(rolePermission);
                }
            }

            if (RoleToDelete.OrganizationUserRole.Count() > 0)
            {
                foreach (var userRole in RoleToDelete.OrganizationUserRole)
                {
                    _context.OrganizationUserRoles.Remove(userRole);
                }
            }

            _context.Roles.Remove(RoleToDelete);

            await _context.SaveChangesAsync();

            return RedirectToPage("/AccessControl/Index");
        }
    }
}
