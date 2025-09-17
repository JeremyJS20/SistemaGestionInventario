using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaGestionInventario.Pages.Shared.Types;

namespace SistemaGestionInventario.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            ViewData["ActivePage"] = "Inventory";
            ViewData["PageRoutes"] = new List<RouteItem> {
                new RouteItem{Path="/inventory/articles", Label="Inventario"}
            };
        }
    }
}
