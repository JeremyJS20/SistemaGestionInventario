using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaGestionInventario.Models;
using System.Security.Claims;

namespace SistemaGestionInventario.Pages.Inventory.Types
{
    [Authorize(Policy = "Permission.INVTYPE_CREATE")]
    public class NewModel : PageModel
    {
        private readonly SistemaGestionInventario.Data.SistemaGestionInventarioContext _context;

        public NewModel(SistemaGestionInventario.Data.SistemaGestionInventarioContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return RedirectToPage("/Inventory/Types/Index");
        }

        [BindProperty]
        public CreateInventoryTypeDto NewInventoryType { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/Inventory/Types/Index");
            }

            var NewInventoryTypeDb = await _context.Categories.AddAsync(new Category
            {
                Code = NewInventoryType.Code,
                Name = NewInventoryType.Name,
                Description = NewInventoryType.Description,
                Level = NewInventoryType.Level,
                ParentId = NewInventoryType.ParentId != 0? NewInventoryType.ParentId: null,
                Status = NewInventoryType.Status,
            });

            await _context.SaveChangesAsync();

            var OrganizationId = int.Parse(User.FindFirstValue("SelectedOrganizationId")!);

            await _context.OrganizationCategories.AddAsync(new OrganizationCategory
            {
                IdCategory = NewInventoryTypeDb.Entity.Id,
                IdOrganization = OrganizationId
            });

            await _context.SaveChangesAsync();

            return RedirectToPage("/Inventory/Types/Index");
        }
    }
}
