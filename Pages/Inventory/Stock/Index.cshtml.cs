using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SistemaGestionInventario.Enums;
using SistemaGestionInventario.Models;
using SistemaGestionInventario.Pages.Shared.Types;
using System.Security.Claims;

namespace SistemaGestionInventario.Pages.Inventory.Stock
{
    [Authorize(Policy = "Permission.INVWHEX")]
    public class IndexModel : PageModel
    {
        private readonly SistemaGestionInventario.Data.SistemaGestionInventarioContext _context;

        public IndexModel(SistemaGestionInventario.Data.SistemaGestionInventarioContext context)
        {
            _context = context;
        }

        public WarehouseStockPageDto WarehouseStockPageDto { get; set; } = new WarehouseStockPageDto();

        public IEnumerable<Article> organizationArticles { get; set; } = default!;

        public IEnumerable<Warehouse> organizationWarehouses { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var OrganizationId = int.Parse(User.FindFirstValue("SelectedOrganizationId")!);

            var WarehouseStock = await _context.OrganizationWarehouses
                .Where(ow => ow.IdOrganization == int.Parse(User.FindFirstValue("SelectedOrganizationId")!))
                .SelectMany(ow => ow.Warehouse.WarehouseArticles)
                .Select(wa => new WarehouseStockDto
                {
                    IdArticle = wa.IdArticle,
                    IdWarehouse = wa.IdWarehouse,
                    ArticleName = wa.Article.Name,
                    ArticleDescription = wa.Article.Description!,
                    WarehouseName = wa.Warehouse.Name,
                    Location = wa.Location,
                    CurrentStock = wa.CurrentStock,
                    MinStock = wa.MinimunStock,
                    MaxStock = wa.MaximunStock,
                    StatusText = GetStockStatus(wa.CurrentStock, wa.MinimunStock, wa.MaximunStock)
                })
                .ToListAsync();

            this.WarehouseStockPageDto.WarehouseStock = WarehouseStock;

            this.WarehouseStockPageDto.Resume = new WarehouseStockPageResumeDto
            {
                TotalExistence = WarehouseStockPageDto.WarehouseStock.Sum(x => x.CurrentStock),
                CriticalStock = WarehouseStockPageDto.WarehouseStock.Count(x => x.StatusText == "Critical"),
                LowStock = WarehouseStockPageDto.WarehouseStock.Count(x => x.StatusText == "Low"),
                OverStock = WarehouseStockPageDto.WarehouseStock.Count(x => x.StatusText == "Overstock")
            };

            this.organizationWarehouses = await _context.OrganizationWarehouses
                .Include(r => r.Warehouse)
                .Where(r => r.IdOrganization == OrganizationId && r.Warehouse.Status == CommonStatusesEnum.AC.Code)
                .Select(r => r.Warehouse)
                .ToListAsync();

            this.organizationArticles = await _context.OrganizationArticles
                .Include(r => r.Article)
                .Where(r => r.IdOrganization == OrganizationId && r.Article.State)
                .Select(r => r.Article)
                .ToListAsync();

            ViewData["ActivePage"] = "Stock";
            ViewData["PageRoutes"] = new List<RouteItem> {
                new RouteItem{Label="Inventario"},
                new RouteItem{Path="/Inventory/Stock", Label="Existencias por Almacén"}
            };
        }

        private static string GetStockStatus(int current, int min, int max)
        {
            if (current < min)
                return "Critical";

            if (current > max)
                return "Overstock";

            if (current <= min + ((max - min) * 0.25))
                return "Low";

            return "Normal";
        }
    }
}

