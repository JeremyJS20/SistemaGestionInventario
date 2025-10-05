using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace SistemaGestionInventario.Pages.AccessControl.Users
{
    [Authorize(Policy = "Permission.USR_DELETE")]
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

            var UserToDelete = await _context.Users
                .Include(u => u.OrganizationUsers)
                    .ThenInclude(ou => ou.OrganizationUserRole)
                .Include(u => u.OrganizationUserRole)
                .Where(u => u.Id == id && u.OrganizationUsers.Any(ou => ou.IdOrganization == OrganizationId))
                .FirstOrDefaultAsync();

            if (UserToDelete == null)
            {
                return RedirectToPage("/AccessControl/Index");
            }

            var organizationUser = UserToDelete.OrganizationUsers
                .FirstOrDefault(ou => ou.IdOrganization == OrganizationId);

            if (organizationUser != null)
            {
                foreach (var userRole in organizationUser.OrganizationUserRole.ToList())
                {
                    _context.OrganizationUserRoles.Remove(userRole);
                }

                _context.OrganizationUsers.Remove(organizationUser);
            }

            var hasOtherOrganizations = await _context.OrganizationUsers
                .AnyAsync(ou => ou.IdUser == id && ou.IdOrganization != OrganizationId);

            if (!hasOtherOrganizations)
            {
                foreach (var userRole in UserToDelete.OrganizationUserRole.ToList())
                {
                    _context.OrganizationUserRoles.Remove(userRole);
                }

                _context.Users.Remove(UserToDelete);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("/AccessControl/Index");
        }
    }
}
