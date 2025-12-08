using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaGestionInventario.Models;
using System.Security.Claims;

namespace SistemaGestionInventario.Pages.Inventory.Stock
{
    //[Authorize(Policy = "Permission.INVWHEX_CREATE")]
    public class NewModel : PageModel
    {
        private readonly SistemaGestionInventario.Data.SistemaGestionInventarioContext _context;

        public NewModel(SistemaGestionInventario.Data.SistemaGestionInventarioContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return RedirectToPage("/Inventory/Stock/Index");
        }

        [BindProperty]
        public CreateWarehouseStockDto NewWarehouseStock { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/Inventory/Stock/Index");
            }

            var NewInventoryStockDb = await _context.WarehouseArticles.AddAsync(new WarehouseArticle
            {
                IdWarehouse = NewWarehouseStock.IdWarehouse,
                IdArticle = NewWarehouseStock.IdArticle,
                CurrentStock = NewWarehouseStock.CurrentStock,
                MinimunStock = NewWarehouseStock.MinimumStock,
                MaximunStock = NewWarehouseStock.MaximunStock,
                Location = NewWarehouseStock.Location
            });

            await _context.SaveChangesAsync();

            return RedirectToPage("/Inventory/Stock/Index");
        }
    }
}
