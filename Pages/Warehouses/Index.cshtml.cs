using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaGestionInventario.Pages.Shared.Types;

namespace SistemaGestionInventario.Pages.Warehouse
{
    [Authorize(Policy = "Permission.WH")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            ViewData["ActivePage"] = "Warehouses";
            ViewData["PageRoutes"] = new List<RouteItem> { 
                new RouteItem { Label = "Almacenes" } 
            };
        }
    }
}
