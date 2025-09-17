using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaGestionInventario.Pages.Shared.Types;

namespace SistemaGestionInventario.Pages.AccessControl
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            ViewData["ActivePage"] = "AccessControl";
            ViewData["PageRoutes"] = new List<RouteItem> {
                new RouteItem{Label="Control de Acceso"}
            };
        }
    }
}
