using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SistemaGestionInventario.Pages.Inventory.Types
{
    [Authorize(Policy = "Permission.INVTYPE_EDIT")]
    public class EditModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            var x = "sss";

            return RedirectToPage("/Inventory/Types/Index");
        }
    }
}
