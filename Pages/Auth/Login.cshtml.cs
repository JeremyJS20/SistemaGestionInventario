using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SistemaGestionInventario.Models;
using SistemaGestionInventario.Pages.Shared.Types;
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
            //var userDb = await _context.Users
            //    .FromSql($"SELECT * FROM Users u WHERE (u.Username = {user.email} or u.Email = {user.email}) and u.Status = 'AC'")
            //    .FirstOrDefaultAsync();

            if (userDb is null)
            {
                return Page();
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(user.password, userDb.Password);

            if (!isValidPassword)
            {
                return Page();
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.email),
                new(ClaimTypes.Role, "Admin")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal).Wait();

            return RedirectToPage("/Index");
        }
    }
}
