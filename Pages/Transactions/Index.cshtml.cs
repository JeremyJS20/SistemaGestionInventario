using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaGestionInventario.Data;
using SistemaGestionInventario.Models;
using SistemaGestionInventario.Pages.Shared.Types;

namespace SistemaGestionInventario.Pages.Transactions
{
    //[Authorize(Policy = "Permission.TRSCTN")]
    public class IndexModel : PageModel
    {
        private readonly SistemaGestionInventarioContext _context;

        public IndexModel(SistemaGestionInventarioContext context)
        {
            _context = context;
        }

        public List<Article> Articles { get; set; }
        public List<Warehouse> Warehouses { get; set; }

        public async Task OnGet()
        {
            ViewData["ActivePage"] = "Transactions";
            ViewData["PageRoutes"] = new List<RouteItem> {
                new RouteItem { Label = "Inventario > <strong>Transacciones</strong>" }
            };

            Articles = await _context.Articles.ToListAsync();
            Warehouses = await _context.Warehouses.ToListAsync();
        }
    }
}
