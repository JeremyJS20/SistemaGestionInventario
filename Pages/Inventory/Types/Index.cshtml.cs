using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SistemaGestionInventario.Enums;
using SistemaGestionInventario.Pages.Shared.Types;
using System.Security.Claims;

namespace SistemaGestionInventario.Pages.Inventory.Types
{
    [Authorize(Policy = "Permission.INVTYPE")]
    public class IndexModel : PageModel
    {
        private readonly SistemaGestionInventario.Data.SistemaGestionInventarioContext _context;

        public IndexModel(SistemaGestionInventario.Data.SistemaGestionInventarioContext context)
        {
            _context = context;
        }

        public InventoryTypesPageDto InventoryTypesPageDto { get; set; } = default!;

        public IList<CommonStatusesEnum> Statuses { get; set; } = CommonStatusesEnum.GetAll();

        public IList<InventoryTypesLevelEnum> TypeLevels { get; set; } = InventoryTypesLevelEnum.GetAll();

        public async Task OnGetAsync()
        {
            this.InventoryTypesPageDto = await _context.Organizations
                .Where(or => or.Id == int.Parse(User.FindFirstValue("SelectedOrganizationId")!))
                .Select(or => new InventoryTypesPageDto
                {
                    Categories = or.OrganizationCategories
                        .Select(orgWh => new InventoryTypesDto
                        {
                            Id = orgWh.Category.Id,
                            Code = orgWh.Category.Code!,
                            Name = orgWh.Category.Name,
                            Description = orgWh.Category.Description!,
                            TotalArticles = orgWh.Category.Articles.Count(),
                            Level = orgWh.Category.Level,
                            LevelText = InventoryTypesLevelEnum.FromCode(orgWh.Category.Level).Description,
                            ParentCategory = _context.Categories.Where(cat => cat.Id == orgWh.Category.ParentId).FirstOrDefault().Name,
                            Status = orgWh.Category.Status,
                            StatusText = CommonStatusesEnum.FromCode(orgWh.Category.Status).Description

                        }).ToList(),
                    Resume = new InventoryTypesPageDtoResumeDto
                    {
                        TotalCategories = or.OrganizationCategories.Where(orgWh => orgWh.Category.Level == InventoryTypesLevelEnum.PARENT.Code).Count(),
                        TotalSubcategories = or.OrganizationCategories.Where(orgWh => orgWh.Category.Level == InventoryTypesLevelEnum.CHILD.Code).Count(),
                        TotalTypes = or.OrganizationCategories.Count(),
                        TotaRelatedArticles = or.OrganizationCategories.Sum(orgWh => orgWh.Category.Articles.Count())
                    }
                })
                .FirstOrDefaultAsync();

            ViewData["ActivePage"] = "Types";
            ViewData["PageRoutes"] = new List<RouteItem> {
                new RouteItem{Label="Inventario"},
                new RouteItem{Path="/Inventory/Types", Label="Tipos de Inventarios"}
            };
        }
    }
}
