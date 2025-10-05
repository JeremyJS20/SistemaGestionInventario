using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SistemaGestionInventario.Pages.AccessControl.Roles
{
    [Authorize(Policy = "Permission.ROLE_EDIT")]
    public class EditModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            var x = "sss";

            return RedirectToPage("/AccessControl/Index");
        }
    }
}
