using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SistemaGestionInventario.Enums;
using SistemaGestionInventario.Models;
using SistemaGestionInventario.Pages.Shared.Types;
using System.Security.Claims;

namespace SistemaGestionInventario.Pages.Warehouses
{
    [Authorize(Policy = "Permission.WH")]
    public class IndexModel : PageModel
    {
        private readonly SistemaGestionInventario.Data.SistemaGestionInventarioContext _context;

        public IndexModel(SistemaGestionInventario.Data.SistemaGestionInventarioContext context)
        {
            _context = context;
        }

        public OrganizationWarehousesPageDto OrganizationWarehousesPageDto { get; set; } = default!;

        public IList<WarehouseStatusEnum> Statuses { get; set; } = WarehouseStatusEnum.GetAll();

        public async Task OnGetAsync()
        {
            this.OrganizationWarehousesPageDto = await _context.Organizations
                .Where(or => or.Id == int.Parse(User.FindFirstValue("SelectedOrganizationId")!))
                .Select(or => new OrganizationWarehousesPageDto
                {
                    Warehouses = or.OrganizationWarehouses
                        .Select(orgWh => new WarehouseDto
                        {
                            Id = orgWh.Warehouse.Id,
                            Code = orgWh.Warehouse.Code,
                            Name = orgWh.Warehouse.Name,
                            Description = orgWh.Warehouse.Description,
                            Address = orgWh.Warehouse.Address,
                            City = orgWh.Warehouse.City,
                            Capacity = orgWh.Warehouse.Capacity,
                            Stock = orgWh.Warehouse.Stock,
                            ResponsibleName = orgWh.Warehouse.ResponsibleName,
                            Status = orgWh.Warehouse.Status,
                            StatusText = WarehouseStatusEnum.FromCode(orgWh.Warehouse.Status).Description

                        }).ToList(),
                    Resume = new OrganizationWarehousesPageResumeDto
                    {
                        TotalWarehouses = or.OrganizationWarehouses.Count(),
                        TotalActiveWarehouses = or.OrganizationWarehouses.Where(orgWh => orgWh.Warehouse.Status == WarehouseStatusEnum.AC.Code).Count(),
                        TotalWarehousesCapacity = or.OrganizationWarehouses.Sum(orgWh => orgWh.Warehouse.Capacity),
                        TotalWarehousesStock = or.OrganizationWarehouses.Sum(orgWh => orgWh.Warehouse.Stock)
                    }
                })
                .FirstOrDefaultAsync();

            ViewData["ActivePage"] = "Warehouses";
            ViewData["PageRoutes"] = new List<RouteItem> {
                new RouteItem{Label="Almacenes"}
            };
        }
    }
}
