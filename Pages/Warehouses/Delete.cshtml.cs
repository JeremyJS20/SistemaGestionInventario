using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace SistemaGestionInventario.Pages.Warehouses
{
    [Authorize(Policy = "Permission.WH_DELETE")]
    public class DeleteModel : PageModel
    {
        private readonly SistemaGestionInventario.Data.SistemaGestionInventarioContext _context;

        public DeleteModel(SistemaGestionInventario.Data.SistemaGestionInventarioContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return RedirectToPage("/Warehouses/Index");
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("/Warehouses/Index");
            }

            var OrganizationId = int.Parse(User.FindFirstValue("SelectedOrganizationId")!);

            var WarehouseToDelete = await _context.OrganizationWarehouses
                .Include(r => r.Warehouse)
                .Where(r => r.IdWarehouse == id && r.IdOrganization== OrganizationId)
                .FirstOrDefaultAsync();

            if (WarehouseToDelete == null)
            {
                return RedirectToPage("/Warehouses/Index");
            }

            _context.OrganizationWarehouses.Remove(WarehouseToDelete);
            _context.Warehouses.Remove(WarehouseToDelete.Warehouse);

            await _context.SaveChangesAsync();

            return RedirectToPage("/Warehouses/Index");
        }
    }
}
