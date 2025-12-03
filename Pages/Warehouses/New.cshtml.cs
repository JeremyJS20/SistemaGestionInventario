using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaGestionInventario.Models;
using System.Security.Claims;

namespace SistemaGestionInventario.Pages.Warehouses
{
    //[Authorize(Policy = "Permission.WH_CREATE")]
    public class NewModel : PageModel
    {
        private readonly SistemaGestionInventario.Data.SistemaGestionInventarioContext _context;

        public NewModel(SistemaGestionInventario.Data.SistemaGestionInventarioContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CreateWarehouseDto NewWarehouse { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/Warehouses/Index");
            }

            var OrganizationId = int.Parse(User.FindFirstValue("SelectedOrganizationId")!);

            var NewWarehouseDb = await _context.Warehouses.AddAsync(new Warehouse
            {
                Code=NewWarehouse.Code,
                Name=NewWarehouse.Name,
                Description=NewWarehouse.Description,
                Address=NewWarehouse.Address,
                City=NewWarehouse.City,
                Capacity=NewWarehouse.Capacity,
                Stock=NewWarehouse.Stock,
                ResponsibleName=NewWarehouse.ResponsibleName,
                Status=NewWarehouse.Status
            });

            await _context.SaveChangesAsync();

            await _context.OrganizationWarehouses.AddAsync(new OrganizationWarehouse
            {
                IdOrganization=OrganizationId,
                IdWarehouse=NewWarehouseDb.Entity.Id
            });

            await _context.SaveChangesAsync();

            return RedirectToPage("/Warehouses/Index");
        }
    }
}
