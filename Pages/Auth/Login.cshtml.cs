using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SistemaGestionInventario.Models;
using SistemaGestionInventario.Pages.Shared.Types;
using System;
using System.Security.Claims;

namespace SistemaGestionInventario.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly SistemaGestionInventario.Data.SistemaGestionInventarioContext _context;

        public LoginModel(SistemaGestionInventario.Data.SistemaGestionInventarioContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            ViewData["ActivePage"] = "Login";
            ViewData["IsSidebarLayoutHidden"] = true;
        }

        [BindProperty]
        public UserLogin user { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userDb = await _context.Users.FirstOrDefaultAsync(m => m.Username == user.email || m.Email == user.email);

            if (userDb is null)
            {
                return Page();
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(user.password, userDb.Password);

            if (!isValidPassword)
            {
                return Page();
            }

            var userDbInfo = _context.Users
                .Where(u => u.Email == user.email)
                .Select(u => new
                {
                    UserId = u.Id,
                    u.Username,
                    u.Email,
                    Organizations = u.OrganizationUsers
                        .Select(ou => new
                        {
                            OrganizationId = ou.Organization.Id,
                            OrganizationName = ou.Organization.Name,
                            Roles = ou.Organization.Roles
                                .Where(r => r.IdOrganization == ou.IdOrganization)
                                .Select(r => new
                                {
                                    RoleId = r.Id,
                                    RoleName = r.Name,
                                    Permissions = r.RolePermissions
                                        .Select(rp => new
                                        {
                                            rp.Permission.Id,
                                            rp.Permission.Name,
                                            rp.Permission.Key
                                        }).ToList()
                                }).ToList()
                        }).ToList()
                })
                .FirstOrDefault();

            var claims = new List<Claim>
            {
                new("UserId", userDbInfo!.UserId.ToString()),
                new(ClaimTypes.Email, userDbInfo!.Email),
                new("Username", userDbInfo!.Username),
                new(ClaimTypes.Role, userDbInfo!.Organizations[0].Roles[0].RoleName),
                new("SelectedOrganizationId", userDbInfo!.Organizations[0].OrganizationId.ToString()),
            };

            foreach (var org in userDbInfo.Organizations)
            {
                claims.Add(new("OrganizationId", org.OrganizationId.ToString()));

                foreach (var role in org.Roles)
                {
                    //claims.Add(new Claim(ClaimTypes.Role, role.RoleName));

                    foreach (var perm in role.Permissions)
                    {
                        claims.Add(new Claim("Permission", perm.Key));
                    }
                }
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal).Wait();

            return RedirectToPage("/Index");
        }
    }
}
