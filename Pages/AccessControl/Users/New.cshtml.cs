using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaGestionInventario.Models;
using System.Security.Claims;

namespace SistemaGestionInventario.Pages.AccessControl.Users
{
    [Authorize(Policy = "Permission.USR_CREATE")]
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
        public CreateUserDto NewUser { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/AccessControl/Index");
            }

            var NewUserDb = await _context.Users.AddAsync(new User
            {
                Name=NewUser.Name,
                LastName=NewUser.LastName,
                Username=NewUser.Username,
                Email=NewUser.Email,
                Status=NewUser.Status,
                Password=BCrypt.Net.BCrypt.HashPassword("Test")
            });

            await _context.SaveChangesAsync();

            var OrganizationId = int.Parse(User.FindFirstValue("SelectedOrganizationId")!);
            var UserId = NewUserDb.Entity.Id;

            await _context.OrganizationUsers.AddAsync(new OrganizationUser
            {
                IdUser= UserId,
                IdOrganization= OrganizationId
            });

            await _context.OrganizationUserRoles.AddAsync(new OrganizationUserRole
            {
                IdOrganization=OrganizationId,
                IdUser= UserId,
                IdRole = NewUser.RoleId
            });

            await _context.SaveChangesAsync();

            return RedirectToPage("/AccessControl/Index");
        }
    }
}
