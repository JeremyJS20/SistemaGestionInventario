using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaGestionInventario.Pages.Shared.Types;

namespace SistemaGestionInventario.Pages.Transactions
{
    [Authorize(Roles = "Test")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            ViewData["ActivePage"] = "Transactions";
            ViewData["PageRoutes"] = new List<RouteItem> {
                new RouteItem{Label="Transacciones"}
            };
        }
    }
}
