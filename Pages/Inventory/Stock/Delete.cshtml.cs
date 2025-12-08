using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace SistemaGestionInventario.Pages.Inventory.Stock
{
    [Authorize(Policy = "Permission.INVWHEX_DELETE")]
    public class DeleteModel : PageModel
    {
        private readonly SistemaGestionInventario.Data.SistemaGestionInventarioContext _context;

        public DeleteModel(SistemaGestionInventario.Data.SistemaGestionInventarioContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync(int? IdWarehouse, int? IdArticle)
        {
            if (IdWarehouse == null || IdArticle == null)
            {
                return RedirectToPage("/Inventory/Stock/Index");
            }

            var WarehouseStockToDelete = await _context.WarehouseArticles
                .Where(r => r.IdWarehouse == IdWarehouse && r.IdArticle == IdArticle)
                .FirstOrDefaultAsync();

            if (WarehouseStockToDelete == null)
            {
                return RedirectToPage("/Inventory/Stock/Index");
            }

            _context.WarehouseArticles.Remove(WarehouseStockToDelete);

            await _context.SaveChangesAsync();

            return RedirectToPage("/Inventory/Stock/Index");
        }
    }
}
