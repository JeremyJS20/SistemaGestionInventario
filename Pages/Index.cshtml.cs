using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaGestionInventario.Pages.Shared.Types;
using System.Security.Claims;

namespace SistemaGestionInventario.Pages;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public IActionResult OnGet()
    {

        ViewData["ActivePage"] = "Index";
        ViewData["PageRoutes"] = new List<RouteItem> {
                new RouteItem{Label="Dashboard"}
        };

        return Page();
    }
}
