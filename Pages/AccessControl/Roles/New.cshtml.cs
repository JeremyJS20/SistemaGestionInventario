using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaGestionInventario.Models;
using System.Security.Claims;

namespace SistemaGestionInventario.Pages.AccessControl.Roles
{
    [Authorize(Policy = "Permission.ROLE_CREATE")]
    public class NewModel : PageModel
    {
        private readonly SistemaGestionInventario.Data.SistemaGestionInventarioContext _context;

        public NewModel(SistemaGestionInventario.Data.SistemaGestionInventarioContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return RedirectToPage("/AccessControl/Index");
        }

        [BindProperty]
        public CreateRoleDto NewRole { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/AccessControl/Index");
            }

            var OrganizationId = int.Parse(User.FindFirstValue("SelectedOrganizationId")!);

            //if (NewRole.IsAdmin) 
            //{
            //    var ExistsAdminRole = await _context.Roles
            //    .Where(r => r.IsAdmin)
            //    .AnyAsync();

            //    if (ExistsAdminRole)
            //    {
            //        return RedirectToPage("/AccessControl/Index");
            //    }
            //}

            var NewRoleDb = await _context.Roles.AddAsync(new Role
            {
                Name = NewRole.Name,
                Description= NewRole.Description,
                IdOrganization= OrganizationId,
                //IsAdmin= NewRole.IsAdmin,
            });

            await _context.SaveChangesAsync();

            List<RolePermission> rolePermissions = new List<RolePermission>();

            foreach (var p in NewRole.SelectedPermissionIds)
            {
                rolePermissions.Add(new RolePermission { IdPermission = p, IdRole = NewRoleDb.Entity.Id });
            }

            await _context.RolePermissions.AddRangeAsync(rolePermissions);

            await _context.SaveChangesAsync();

            return RedirectToPage("/AccessControl/Index");
        }
    }
}
