using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SistemaGestionInventario.Models;
using System.Security.Claims;
using System.Text;

namespace SistemaGestionInventario.Pages.Reports.Articles.BelowReorderPoint
{
    //[Authorize(Policy = "Permission.ROLE_DELETE")]
    public class ExportModel : PageModel
    {
        private readonly SistemaGestionInventario.Data.SistemaGestionInventarioContext _context;

        public ExportModel(SistemaGestionInventario.Data.SistemaGestionInventarioContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            IList<Article> data = await _context.OrganizationArticles.
                Where(oa => oa.IdOrganization == int.Parse(User.FindFirstValue("SelectedOrganizationId")!))
                .AsNoTracking()
                .Select(oa => oa.Article)
                .Where(a => a.State && a.Stock < a.MinimumStock)
                .ToListAsync();

            var csv = new StringBuilder();

            csv.AppendLine("Id,Name,Price,Stock,Minimun Stock,Difference");

            foreach (Article s in data)
            {
                csv.AppendLine($"{s.Id},{Escape(s.Name)},{s.Price},{s.Stock},{s.MinimumStock},{s.MinimumStock - s.Stock}");
            }

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());

            return File(bytes, "text/csv", "below_reorder_point_report.csv");
        }

        string Escape(string value)
        {
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
            {
                value = value.Replace("\"", "\"\"");
                return $"\"{value}\"";
            }
            return value;
        }
    }
}
