using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace SistemaGestionInventario.Pages.Inventory.Types
{
    [Authorize(Policy = "Permission.INVTYPE_DELETE")]
    public class DeleteModel : PageModel
    {
        private readonly SistemaGestionInventario.Data.SistemaGestionInventarioContext _context;

        public DeleteModel(SistemaGestionInventario.Data.SistemaGestionInventarioContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("/Inventory/Types/Index");
            }

            var OrganizationId = int.Parse(User.FindFirstValue("SelectedOrganizationId")!);

            var CategoryToDelete = await _context.OrganizationCategories
                .Include(r => r.Category)
                .Where(r => r.IdCategory == id && r.IdOrganization == OrganizationId)
                .FirstOrDefaultAsync();

            if (CategoryToDelete == null)
            {
                return RedirectToPage("/Inventory/Types/Index");
            }

            _context.OrganizationCategories.Remove(CategoryToDelete);
            _context.Categories.Remove(CategoryToDelete.Category);

            await _context.SaveChangesAsync();

            return RedirectToPage("/Inventory/Types/Index");
        }
    }
}
