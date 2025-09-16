using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SistemaGestionInventario.Pages.Transactions
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            ViewData["ActivePage"] = "Transactions";
        }
    }
}
