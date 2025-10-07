using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaGestionInventario.Data;
using SistemaGestionInventario.DTOs;
using SistemaGestionInventario.Pages.Shared.Types;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SistemaGestionInventario.Pages.Inventory
{
    public class ArticlesModel : PageModel
    {
        [BindProperty]
        public ArticleDto ArticleDto { get; set; }
        private readonly SistemaGestionInventarioContext _context;

        public ArticlesModel(SistemaGestionInventarioContext context)
        {
            _context = context;
        }

        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> States { get; set; }
        public int Total { get; set; }
        public int Activos { get; set; }
        public int StockBajo { get; set; }

        public async Task OnGetAsync()
        {
            ViewData["ActivePage"] = "Articles";
            ViewData["PageRoutes"] = new List<RouteItem> {
                new RouteItem { Label = "Inventario > <strong>Getión de Artículos</strong>" }
            };

            Categories = await _context.Categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            }).ToListAsync();

            States = new List<SelectListItem>
            {
                new SelectListItem{ Value = "1", Text = "Activo" },
                new SelectListItem{ Value = "0", Text = "Inactivo" }
            };

            await RefreshData();
        }

        public async Task<IActionResult> OnPostEndpoint()
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();
            if (!ModelState.IsValid)
            {
                errors = ModelState
                    .Where(kvp => kvp.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.First().ErrorMessage
                    );
            }

            bool existsCode = await _context.Articles.Where(a => a.Code.ToUpper() == (ArticleDto.Code ?? "").ToUpper()).AnyAsync();

            if (existsCode) errors.Add("ArticleDto.Code", "Este codigo ya existe");
            if (ArticleDto.Category == 0) errors.Add("ArticleDto.Category", "Esta categoria no es valida");

            if (errors.Any()) return new JsonResult(new { success = false, errors });

            await _context.Articles.AddAsync(new Models.Article
            {
                Code = ArticleDto.Code.ToUpper(),
                Category = ArticleDto.Category,
                Name = ArticleDto.Name,
                Description = ArticleDto.Description,
                Price = ArticleDto.Price,
                Stock = ArticleDto.Stock,
                MinimumStock = ArticleDto.MinimumStock,
                State = ArticleDto.State == "1",
            });

            await _context.SaveChangesAsync();

            await RefreshData();

            return new JsonResult(new { success = true });
        }

        public async Task RefreshData()
        {
            Total = await _context.Articles.CountAsync();
            Activos = await _context.Articles.Where(a => a.State).CountAsync();
            StockBajo = await _context.Articles.Where(a => a.Stock <= a.MinimumStock).CountAsync();
        }
    }
}
