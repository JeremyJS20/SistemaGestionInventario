using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaGestionInventario.Pages.Shared.Types;

namespace SistemaGestionInventario.Pages.Inventory
{
    public class ArticlesModel : PageModel
    {
        public void OnGet()
        {
            ViewData["ActivePage"] = "Articles";
            ViewData["PageRoutes"] = new List<RouteItem> {
                new RouteItem { Label = "Inventario > <strong>Getión de Artículos</strong>" }
            };
        }
    }
}
