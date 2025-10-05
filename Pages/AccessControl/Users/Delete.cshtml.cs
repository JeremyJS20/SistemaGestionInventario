using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SistemaGestionInventario.Pages.AccessControl.Users
{
    [Authorize(Policy = "Permission.USR_DELETE")]
    public class DeleteModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var x = "sss";

            return RedirectToPage("/AccessControl/Index");
        }
    }
}
